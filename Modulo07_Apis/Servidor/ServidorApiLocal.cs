using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Cun.Poo.Modulo07Apis.Clientes;

namespace Cun.Poo.Modulo07Apis.Servidor;

/// <summary>
/// Una API REST de mentiras que corre DENTRO del programa, en localhost.
/// Es "la cocina" del restaurante: la demo la levanta para que la clase
/// funcione sin internet y la salida sea siempre la misma.
///
/// Usa HttpListener, un servidor HTTP minimo de .NET. Fijense en que NO usa
/// las clases de Modelos/: el servidor y el cliente solo comparten el JSON.
/// Este servidor podria estar escrito en Java, Python o PHP y el cliente no
/// cambiaria. En un proyecto real la API seria un proyecto ASP.NET Core aparte.
///
/// Recursos que expone:
///   GET|POST          /api/usuarios          GET|PUT|DELETE /api/usuarios/{id}
///   POST              /oauth/token           GET            /api/pedidos  (pide token)
///   GET               /api/clima?zona=...    GET            /api/posts
///   GET               /api/inestable  (falla 2 veces con 503)
///   GET               /api/lento      (tarda 2 segundos)
/// </summary>
public sealed class ServidorApiLocal : IDisposable
{
    public const string ClientIdDemo = "cunexpress-app";
    public const string ClientSecretDemo = "secreto-de-demo";
    private const string TokenEmitido = "cunx-7f3a9c41e2";   // en la vida real: aleatorio o un JWT firmado

    private readonly HttpListener _listener = new();
    private readonly Dictionary<int, UsuarioGuardado> _usuarios = [];
    private readonly object _candado = new();
    private int _siguienteId = 4;
    private int _llamadasInestable;

    public ServidorApiLocal()
    {
        Direccion = new Uri($"http://localhost:{PuertoLibre()}/");
        _listener.Prefixes.Add(Direccion.ToString());
        _usuarios[1] = new UsuarioGuardado(1, "Ana Rojas", "ana.rojas@cun.edu.co", "Bogota", "300 555 0101");
        _usuarios[2] = new UsuarioGuardado(2, "Carlos Moncada", "carlos.moncada@cun.edu.co", "Medellin", "301 555 0202");
        _usuarios[3] = new UsuarioGuardado(3, "Laura Gomez", "laura.gomez@cun.edu.co", "Cali", "302 555 0303");
        _listener.Start();
        _ = Task.Run(AtenderAsync);
    }

    /// <summary>URL base, por ejemplo http://localhost:52731/ (el puerto cambia en cada corrida).</summary>
    public Uri Direccion { get; }

    public void Dispose() => _listener.Close();

    // ------------------------------------------------------------------------
    private async Task AtenderAsync()
    {
        while (_listener.IsListening)
        {
            HttpListenerContext contexto;
            try
            {
                contexto = await _listener.GetContextAsync();
            }
            catch (Exception ex) when (ex is HttpListenerException or ObjectDisposedException or InvalidOperationException)
            {
                return;                                    // el servidor se apago
            }
            _ = Task.Run(() => ResponderSeguroAsync(contexto));   // cada solicitud en su propio hilo
        }
    }

    private async Task ResponderSeguroAsync(HttpListenerContext contexto)
    {
        try
        {
            await EnrutarAsync(contexto.Request, contexto.Response);
        }
        catch (Exception ex) when (ex is HttpListenerException or ObjectDisposedException or IOException)
        {
            // el cliente se canso de esperar y cerro la conexion (timeout)
        }
        catch (Exception)
        {
            Escribir(contexto.Response, 500, new { error = "Error interno del servidor" });
        }
    }

    private async Task EnrutarAsync(HttpListenerRequest req, HttpListenerResponse resp)
    {
        string[] partes = req.Url!.AbsolutePath.Trim('/').Split('/');
        string metodo = req.HttpMethod;

        switch (partes)
        {
            case ["api", "usuarios"] when metodo == "GET":
                lock (_candado)
                {
                    Escribir(resp, 200, _usuarios.Values.OrderBy(u => u.Id).ToList());
                }
                break;
            case ["api", "usuarios"] when metodo == "POST":
                CrearUsuario(await LeerCuerpoAsync(req), resp);
                break;
            case ["api", "usuarios", var texto] when int.TryParse(texto, out int id):
                UsuarioPorId(metodo, id, await LeerCuerpoAsync(req), resp);
                break;
            case ["oauth", "token"] when metodo == "POST":
                EmitirToken(await LeerCuerpoAsync(req), resp);
                break;
            case ["api", "pedidos"] when metodo == "GET":
                ListarPedidos(req, resp);
                break;
            case ["api", "clima"] when metodo == "GET":
                Clima(req.QueryString["zona"] ?? "", resp);
                break;
            case ["api", "posts"] when metodo == "GET":
                Escribir(resp, 200, Publicaciones());
                break;
            case ["api", "inestable"] when metodo == "GET":
                if (Interlocked.Increment(ref _llamadasInestable) <= 2)
                {
                    Escribir(resp, 503, new { error = "Servicio en mantenimiento, intente de nuevo" });
                }
                else
                {
                    Escribir(resp, 200, new { mensaje = "Por fin respondi" });
                }
                break;
            case ["api", "lento"]:
                await Task.Delay(2000);
                Escribir(resp, 200, new { mensaje = "Perdon la demora" });
                break;
            default:
                Escribir(resp, 404, new { error = $"No existe el recurso {req.Url.AbsolutePath}" });
                break;
        }
    }

    // ------------------------------------------------------------------------
    private void CrearUsuario(string cuerpo, HttpListenerResponse resp)
    {
        if (!Validar(cuerpo, resp, out string nombre, out string email, out string ciudad))
        {
            return;
        }
        UsuarioGuardado nuevo;
        lock (_candado)
        {
            nuevo = new UsuarioGuardado(_siguienteId++, nombre, email, ciudad, "");
            _usuarios[nuevo.Id] = nuevo;
        }
        resp.AddHeader("Location", $"/api/usuarios/{nuevo.Id}");   // donde quedo el recurso nuevo
        Escribir(resp, 201, nuevo);
    }

    private void UsuarioPorId(string metodo, int id, string cuerpo, HttpListenerResponse resp)
    {
        lock (_candado)
        {
            if (!_usuarios.TryGetValue(id, out UsuarioGuardado? actual))
            {
                Escribir(resp, 404, new { error = $"No existe el usuario {id}" });
                return;
            }
            switch (metodo)
            {
                case "GET":
                    Escribir(resp, 200, actual);
                    break;
                case "PUT":
                    if (Validar(cuerpo, resp, out string nombre, out string email, out string ciudad))
                    {
                        _usuarios[id] = actual with { Nombre = nombre, Email = email, Ciudad = ciudad };
                        Escribir(resp, 200, _usuarios[id]);
                    }
                    break;
                case "DELETE":
                    _usuarios.Remove(id);
                    resp.StatusCode = 204;                 // No Content: borrado, sin cuerpo
                    resp.Close();
                    break;
                default:
                    Escribir(resp, 405, new { error = $"Metodo {metodo} no permitido" });
                    break;
            }
        }
    }

    /// <summary>El servidor NUNCA confia en el cliente: valida todo lo que llega.</summary>
    private static bool Validar(string cuerpo, HttpListenerResponse resp,
                                out string nombre, out string email, out string ciudad)
    {
        nombre = email = ciudad = "";
        try
        {
            using JsonDocument doc = JsonDocument.Parse(cuerpo);
            nombre = Texto(doc.RootElement, "nombre");
            email = Texto(doc.RootElement, "email");
            ciudad = Texto(doc.RootElement, "ciudad");
        }
        catch (JsonException)
        {
            Escribir(resp, 400, new { error = "El cuerpo no es un JSON valido" });
            return false;
        }
        if (nombre.Length == 0)
        {
            Escribir(resp, 400, new { error = "El campo 'nombre' es obligatorio" });
            return false;
        }
        if (!email.Contains('@'))
        {
            Escribir(resp, 400, new { error = $"El email '{email}' no es valido" });
            return false;
        }
        return true;
    }

    private static void EmitirToken(string cuerpo, HttpListenerResponse resp)
    {
        var formulario = System.Web.HttpUtility.ParseQueryString(cuerpo);
        if (formulario["grant_type"] != "client_credentials")
        {
            Escribir(resp, 400, new { error = "unsupported_grant_type" });
            return;
        }
        if (formulario["client_id"] != ClientIdDemo || formulario["client_secret"] != ClientSecretDemo)
        {
            Escribir(resp, 401, new { error = "invalid_client: credenciales de la aplicacion incorrectas" });
            return;
        }
        Escribir(resp, 200, new { access_token = TokenEmitido, token_type = "Bearer", expires_in = 3600 });
    }

    private static void ListarPedidos(HttpListenerRequest req, HttpListenerResponse resp)
    {
        string? autorizacion = req.Headers["Authorization"];
        if (autorizacion != $"Bearer {TokenEmitido}")
        {
            resp.AddHeader("WWW-Authenticate", "Bearer");
            Escribir(resp, 401, new { error = "Falta un token de acceso valido" });
            return;
        }
        Escribir(resp, 200, new[]
        {
            new { id = 501, cliente = "Drogueria La Rebaja", destino = "Chapinero", estado = "en bodega" },
            new { id = 502, cliente = "Panaderia El Trigal", destino = "Suba", estado = "en ruta" },
            new { id = 503, cliente = "Papeleria Kennedy", destino = "Kennedy", estado = "en bodega" },
        });
    }

    private static void Clima(string zona, HttpListenerResponse resp)
    {
        // Pronosticos fijos para que la salida de la clase sea repetible
        (string Condicion, double Mm, int Temp)? dato = zona.ToLowerInvariant() switch
        {
            "chapinero" => ("lluvia intensa", 18.5, 12),
            "suba" => ("nublado", 0.4, 15),
            "kennedy" => ("tormenta electrica", 12.0, 13),
            "usaquen" => ("soleado", 0.0, 19),
            "bosa" => ("llovizna", 2.1, 14),
            "teusaquillo" => ("parcialmente nublado", 0.0, 17),
            _ => null,
        };
        if (dato is null)
        {
            Escribir(resp, 404, new { error = $"Sin estacion meteorologica en '{zona}'" });
            return;
        }
        Escribir(resp, 200, new { zona, condicion = dato.Value.Condicion, precipitacionMm = dato.Value.Mm, temperaturaC = dato.Value.Temp });
    }

    private static object Publicaciones()
    {
        string[] titulos =
        [
            "Como llegar a la CUN en TransMilenio", "Receta de ajiaco santafereno", "Tips para el parcial de POO II",
            "Las mejores ciclovias de Bogota", "Que es una API REST", "Ferias de emprendimiento en Chapinero",
            "Guia de cafes especiales de Colombia", "Como preparar una entrevista tecnica", "Datos abiertos del Distrito",
            "Proyectos finales de Ingenieria de Sistemas", "Rutas del SITP hacia Suba", "Becas y convenios 2026",
        ];
        return titulos.Select((t, i) => new
        {
            userId = i % 4 + 1,
            id = i + 1,
            title = t,
            body = $"Contenido de la publicacion {i + 1}.",
        }).ToList();
    }

    // ------------------------------------------------------------------------
    private static void Escribir(HttpListenerResponse resp, int codigo, object cuerpo)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(cuerpo, JsonApi.Opciones));
        resp.StatusCode = codigo;
        resp.ContentType = "application/json; charset=utf-8";
        resp.ContentLength64 = bytes.Length;
        resp.OutputStream.Write(bytes);
        resp.Close();
    }

    private static async Task<string> LeerCuerpoAsync(HttpListenerRequest req)
    {
        using var lector = new StreamReader(req.InputStream, Encoding.UTF8);
        return await lector.ReadToEndAsync();
    }

    private static string Texto(JsonElement raiz, string clave) =>
        raiz.TryGetProperty(clave, out JsonElement valor) && valor.ValueKind == JsonValueKind.String
            ? valor.GetString()!.Trim()
            : "";

    private static int PuertoLibre()
    {
        var sonda = new TcpListener(IPAddress.Loopback, 0);
        sonda.Start();
        int puerto = ((IPEndPoint)sonda.LocalEndpoint).Port;
        sonda.Stop();
        return puerto;
    }

    /// <summary>Como guarda el SERVIDOR un usuario: tiene un telefono que el cliente no conoce.</summary>
    private sealed record UsuarioGuardado(int Id, string Nombre, string Email, string Ciudad, string Telefono);
}
