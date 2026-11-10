using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text.Json;
using Cun.Poo.Modulo07Apis.Clientes;
using Cun.Poo.Modulo07Apis.CunExpress;
using Cun.Poo.Modulo07Apis.Modelos;
using Cun.Poo.Modulo07Apis.Servidor;
using Cun.Poo.Modulo07Apis.Taller;

namespace Cun.Poo.Modulo07Apis;

/// <summary>
/// Tematica 7 (G7): conexion y consumo de APIs REST.
///
/// La demo levanta una API local (ServidorApiLocal, "la cocina") y la consume
/// con HttpClient, el equivalente en .NET del java.net.http.HttpClient del
/// recurso. Asi la clase funciona sin internet y la salida es repetible.
/// La seccion 8 llama a APIs reales (JSONPlaceholder y la TRM de datos.gov.co)
/// solo si se define la variable de entorno POO2_EN_VIVO=1.
/// </summary>
public class Demo07Apis : ModuloBase
{
    public override string Titulo =>
        "APIs REST: HttpClient, JSON, CRUD, errores, OAuth2 y datos abiertos";

    public override string Descripcion =>
        "Solicitud y respuesta HTTP, deserializar JSON a objetos, CRUD con verbos y codigos de estado, reintentos, token Bearer, el caso CunExpress y API -> SQLite.";

    private static readonly string CarpetaDatos = Path.Combine(AppContext.BaseDirectory, "datos");

    /// <summary>ModuloBase pide un metodo sincrono; todo el trabajo real es asincrono.</summary>
    public override void Ejecutar() => EjecutarAsync().GetAwaiter().GetResult();

    private static async Task EjecutarAsync()
    {
        using var servidor = new ServidorApiLocal();
        Console.WriteLine("   API local escuchando en un puerto libre de localhost (la \"cocina\" de la demo).");

        // Se crea UNA vez y se reutiliza (consejo del recurso). Cada manejador
        // envuelve al siguiente: Bitacora -> red. Es el patron Decorator de G4.
        using HttpClient http = CrearCliente(servidor.Direccion);

        await SeccionAnatomia(servidor.Direccion);
        await SeccionDeserializar(http);
        await SeccionCrud(http);
        await SeccionErrores(servidor.Direccion, http);
        await SeccionOAuth(http);
        await SeccionCunExpress(http);
        await SeccionTaller(http);
        await SeccionEnVivo();
    }

    // ========================================================================
    private static async Task SeccionAnatomia(Uri direccion)
    {
        Seccion("1. ANATOMIA DE UNA SOLICITUD: el mesero lleva el pedido");
        // Los tres actores del recurso: HttpClient, HttpRequest y HttpResponse
        using var cliente = new HttpClient { BaseAddress = direccion };
        using var solicitud = new HttpRequestMessage(HttpMethod.Get, "api/usuarios/1");
        solicitud.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        using HttpResponseMessage respuesta = await cliente.SendAsync(solicitud);
        string cuerpo = await respuesta.Content.ReadAsStringAsync();

        Console.WriteLine($"   Solicitud : {solicitud.Method} {solicitud.RequestUri!.PathAndQuery}   Accept: application/json");
        Console.WriteLine($"   Estado    : {(int)respuesta.StatusCode} {respuesta.ReasonPhrase}");
        Console.WriteLine($"   Tipo      : {respuesta.Content.Headers.ContentType}");
        Console.WriteLine($"   Cuerpo    : {cuerpo}");
        Nota("El endpoint es la direccion (/api/usuarios/1); el recurso es el usuario; GET es el verbo.");
        Nota("await = el sendAsync de Java: el programa no se congela mientras llega la respuesta.");
    }

    // ========================================================================
    private static async Task SeccionDeserializar(HttpClient http)
    {
        Seccion("2. DESERIALIZAR: de texto JSON a un objeto Usuario");
        string json = await http.GetStringAsync("api/usuarios/1");

        // Gson:  Usuario u = gson.fromJson(json, Usuario.class);
        Usuario usuario = JsonSerializer.Deserialize<Usuario>(json, JsonApi.Opciones)!;
        Console.WriteLine($"   Objeto    : {usuario}");
        Console.WriteLine($"   Saludo    : Hola, {usuario.Nombre.Split(' ')[0]} ({usuario.Ciudad})");
        Nota("\"telefono\" venia en el JSON pero Usuario no lo tiene: se ignora sin error.");

        // Atajo: pedir y deserializar en un solo paso, incluso una lista
        List<Usuario> todos = await http.GetFromJsonAsync<List<Usuario>>("api/usuarios", JsonApi.Opciones) ?? [];
        Console.WriteLine($"   Lista     : {todos.Count} usuarios -> {string.Join(", ", todos.Select(u => u.Nombre))}");
        Nota("Desde aqui ya no trabajamos con texto sino con objetos del dominio (encapsulamiento).");
    }

    // ========================================================================
    private static async Task SeccionCrud(HttpClient http)
    {
        Seccion("3. CRUD: los cuatro verbos y sus codigos de estado");
        var api = new ClienteUsuarios(http);

        var nueva = new Usuario { Nombre = "Sofia Medina", Email = "sofia.medina@cun.edu.co", Ciudad = "Tunja" };
        Console.WriteLine($"   POST serializa el objeto: {JsonApi.Texto(nueva)}");
        Usuario creada = await api.CrearAsync(nueva);
        Console.WriteLine($"   CREATE -> el servidor le asigno el Id {creada.Id}");

        Usuario leida = (await api.ObtenerAsync(creada.Id))!;
        Console.WriteLine($"   READ   -> {leida}");

        leida.Ciudad = "Bogota";
        Usuario actualizada = (await api.ActualizarAsync(leida))!;
        Console.WriteLine($"   UPDATE -> ahora vive en {actualizada.Ciudad}");

        bool borrada = await api.EliminarAsync(creada.Id);
        Console.WriteLine($"   DELETE -> borrada: {(borrada ? "si" : "no")}");

        Usuario? fantasma = await api.ObtenerAsync(creada.Id);
        Console.WriteLine($"   READ   -> despues de borrar: {(fantasma is null ? "no existe (404)" : fantasma.ToString())}");
        Nota("Id = 0 no viaja en el POST ([JsonIgnore WhenWritingDefault]): el Id lo decide el servidor.");
        Nota("201 Created trae la cabecera Location; 204 No Content responde sin cuerpo.");
    }

    // ========================================================================
    private static async Task SeccionErrores(Uri direccion, HttpClient http)
    {
        Seccion("4. CUANDO ALGO SALE MAL: 4xx, 5xx, timeouts y red caida");
        var api = new ClienteUsuarios(http);

        // 4xx: el error es NUESTRO (datos malos)
        try
        {
            await api.CrearAsync(new Usuario { Nombre = "Pedro Paramo", Email = "pedro.cun.edu.co" });
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"   ApiException       : {ex.Message}");
        }

        // EnsureSuccessStatusCode: la forma corta de convertir un 4xx/5xx en excepcion
        try
        {
            using HttpResponseMessage r = await http.GetAsync("api/productos");
            r.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"   HttpRequestException: codigo {(int)ex.StatusCode!} -> el recurso no existe");
        }

        // 5xx pasajero: el error es del SERVIDOR; tiene sentido reintentar
        Console.WriteLine("   Un servicio inestable, con el ManejadorReintentos en la cadena:");
        using (HttpClient conReintentos = CrearCliente(direccion, reintentos: 3))
        {
            var ok = await conReintentos.GetFromJsonAsync<JsonElement>("api/inestable");
            Console.WriteLine($"      Respuesta final: {ok.GetProperty("mensaje").GetString()}");
        }

        // Timeout: no esperar para siempre a un servidor lento
        using (HttpClient impaciente = CrearCliente(direccion, timeout: TimeSpan.FromMilliseconds(500)))
        {
            try
            {
                await impaciente.GetStringAsync("api/lento");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("   TaskCanceledException: /api/lento tardo mas de 500 ms y se cancelo");
            }
        }

        // Sin servidor: la solicitud ni siquiera llega (no hay codigo de estado)
        using (var sinServidor = new HttpClient { BaseAddress = new Uri("http://localhost:1/") })
        {
            try
            {
                await sinServidor.GetStringAsync("api/usuarios");
            }
            catch (HttpRequestException ex) when (ex.InnerException is SocketException se)
            {
                Console.WriteLine($"   HttpRequestException: sin codigo HTTP, error de red = {se.SocketErrorCode}");
            }
        }
        Nota("4xx = el cliente se equivoco: corregir, no reintentar. 5xx = el servidor fallo: reintentar con espera.");
        Nota("Un usuario nunca debe ver un stack trace: cada caso se traduce a un mensaje claro.");
    }

    // ========================================================================
    private static async Task SeccionOAuth(HttpClient http)
    {
        Seccion("5. APIs PROTEGIDAS: OAuth2 client credentials y token Bearer");
        using (HttpResponseMessage sinToken = await http.GetAsync("api/pedidos"))
        {
            Console.WriteLine($"   Sin token        -> {(int)sinToken.StatusCode} y WWW-Authenticate: {sinToken.Headers.WwwAuthenticate}");
        }

        var oauth = new ClienteOAuth(http);
        try
        {
            await oauth.ObtenerTokenAsync(ServidorApiLocal.ClientIdDemo, "clave-adivinada");
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"   Secreto falso    -> {ex.Message}");
        }

        // El secreto se lee del entorno; en el codigo solo queda un valor de demo.
        string secreto = Environment.GetEnvironmentVariable("CUNEXPRESS_CLIENT_SECRET") ?? ServidorApiLocal.ClientSecretDemo;
        TokenAcceso token = await oauth.ObtenerTokenAsync(ServidorApiLocal.ClientIdDemo, secreto);
        Console.WriteLine($"   Token recibido   -> {token.Tipo} {token.Valor[..6]}... (vence en {token.ExpiraEnSegundos / 60} min)");

        using var solicitud = new HttpRequestMessage(HttpMethod.Get, "api/pedidos");
        solicitud.Headers.Authorization = new AuthenticationHeaderValue(token.Tipo, token.Valor);
        using HttpResponseMessage respuesta = await http.SendAsync(solicitud);
        List<PedidoEnvio> pedidos = (await respuesta.Content.ReadFromJsonAsync<List<PedidoEnvio>>(JsonApi.Opciones))!;
        Console.WriteLine($"   Con token        -> {pedidos.Count} pedidos:");
        foreach (PedidoEnvio p in pedidos)
        {
            Console.WriteLine($"      #{p.Id} {p.Cliente,-22} {p.Destino,-10} {p.Estado}");
        }
        Nota("401 Unauthorized = no se quien eres. 403 Forbidden = se quien eres, pero no tienes permiso.");
        Nota("Con usuarios humanos se usa el flujo Authorization Code: login en Google, codigo, token.");
        Nota("El client_secret nunca va en el codigo ni en GitHub: variable de entorno o gestor de secretos.");
    }

    // ========================================================================
    private static async Task SeccionCunExpress(HttpClient http)
    {
        Seccion("6. CASO CUNEXPRESS: rutas que miran el clima antes de salir");
        List<Entrega> entregas =
        [
            new("CX-1001", "Drogueria La Rebaja", "Chapinero"),
            new("CX-1002", "Panaderia El Trigal", "Suba"),
            new("CX-1003", "Papeleria Kennedy", "Kennedy"),
            new("CX-1004", "Libreria Lerner", "Chapinero"),
            new("CX-1005", "Ferreteria Usaquen", "Usaquen"),
            new("CX-1006", "Tienda Don Jose", "Fontibon"),
        ];
        var gestor = new GestorRutas(new ClienteClimaHttp(http));   // la API de clima entra por el constructor
        Console.WriteLine($"   {entregas.Count} entregas en 5 zonas; el gestor consulta el clima de cada zona una sola vez:");
        PlanDelDia plan = await gestor.PlanificarAsync(entregas);

        Console.WriteLine("   Primero, sin lluvia:");
        foreach (var (entrega, clima) in plan.Normales)
        {
            Console.WriteLine($"      {entrega.Guia} {entrega.Cliente,-20} {entrega.Zona,-10} {clima.Condicion}");
        }
        Console.WriteLine("   Al final, con aviso de posible retraso al cliente:");
        foreach (var (entrega, clima) in plan.ConAviso)
        {
            Console.WriteLine($"      {entrega.Guia} {entrega.Cliente,-20} {entrega.Zona,-10} {clima.Condicion} ({clima.PrecipitacionMm} mm)");
        }
        foreach (Entrega entrega in plan.SinPronostico)
        {
            Console.WriteLine($"   Sin pronostico: {entrega.Guia} en {entrega.Zona} sale sin informacion del clima.");
        }
        Nota("GestorRutas no sabe de HTTP: solo conoce IClienteClima. En G8 se prueba con un clima falso.");
        Nota("Consultar por zona y no por paquete: 6 entregas, 5 llamadas. Las APIs se pagan por consumo.");
    }

    // ========================================================================
    private static async Task SeccionTaller(HttpClient http)
    {
        Seccion("7. TALLER 4.4: de la API a la base de datos (G6)");
        Directory.CreateDirectory(CarpetaDatos);
        string rutaDb = Path.Combine(CarpetaDatos, "api_posts.db");
        File.Delete(rutaDb);                                      // salida repetible

        // Paso 3 y 4: GET /api/posts -> un arreglo JSON -> List<Publicacion>
        List<Publicacion> publicaciones =
            await http.GetFromJsonAsync<List<Publicacion>>("api/posts", JsonApi.Opciones) ?? [];
        Console.WriteLine($"   La API devolvio un arreglo de {publicaciones.Count} publicaciones. La primera:");
        Console.WriteLine($"      Id={publicaciones[0].Id}  AutorId={publicaciones[0].AutorId}  Titulo=\"{publicaciones[0].Titulo}\"");

        // Paso 5: guardar con el ORM (sin SQL a mano, sin riesgo de inyeccion)
        using (var db = new PublicacionesDbContext(rutaDb))
        {
            db.Database.EnsureCreated();
            db.Publicaciones.AddRange(publicaciones);
            int filas = db.SaveChanges();
            Console.WriteLine($"   INSERT de {filas} filas en la tabla posts de datos/api_posts.db");
        }

        // Paso 6: verificar leyendo desde OTRO contexto (vienen del disco)
        using (var db = new PublicacionesDbContext(rutaDb))
        {
            Console.WriteLine($"   SELECT COUNT(*) FROM posts -> {db.Publicaciones.Count()}");
            var porAutor = db.Publicaciones.GroupBy(p => p.AutorId)
                             .Select(g => new { Autor = g.Key, Cantidad = g.Count() })
                             .OrderBy(x => x.Autor).ToList();
            Console.WriteLine($"   Por autor -> {string.Join("  ", porAutor.Select(x => $"autor {x.Autor}: {x.Cantidad}"))}");
        }
        Nota("JSON -> objeto -> fila: la misma clase Publicacion sirve para la API y para la base.");
        Nota("[JsonPropertyName(\"title\")] traduce el ingles de la API al espanol del dominio.");
    }

    // ========================================================================
    private static async Task SeccionEnVivo()
    {
        Seccion("8. EN VIVO: APIs reales en internet (opcional)");
        if (Environment.GetEnvironmentVariable("POO2_EN_VIVO")?.Trim() != "1")
        {
            Console.WriteLine("   Desactivada para que la salida sea repetible y funcione sin internet.");
            Console.WriteLine("   Para activarla (Windows):     set POO2_EN_VIVO=1 && ejecutar.bat 7");
            Console.WriteLine("   Linux / macOS / Git Bash:     POO2_EN_VIVO=1 ./ejecutar.sh 7");
            Nota("Llama a JSONPlaceholder (el ejemplo 4.1 del recurso) y a la TRM de datos.gov.co.");
            return;
        }

        try
        {
            using HttpClient placeholder = CrearCliente(new Uri("https://jsonplaceholder.typicode.com/"),
                                                        timeout: TimeSpan.FromSeconds(10));
            Usuario u = (await placeholder.GetFromJsonAsync<Usuario>("users/1", JsonApi.Opciones))!;
            Console.WriteLine($"   JSONPlaceholder -> {u}");
            Nota("Nombre salio vacio: esa API dice \"name\", no \"nombre\". Se arregla con [JsonPropertyName(\"name\")].");

            using HttpClient datosGov = CrearCliente(new Uri("https://www.datos.gov.co/"),
                                                     timeout: TimeSpan.FromSeconds(10));
            TasaRepresentativa trm = await new ClienteTrm(datosGov).ObtenerVigenteAsync();
            Console.WriteLine($"   TRM vigente     -> {Dinero.Cop(trm.Valor)} por dolar ({trm.VigenciaDesde:yyyy-MM-dd} a {trm.VigenciaHasta:yyyy-MM-dd})");
            Console.WriteLine($"   Un curso de USD 49.99 cuesta hoy {Dinero.Cop(trm.APesos(49.99m))}");
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            Console.WriteLine($"   No hubo respuesta ({ex.GetType().Name}). Revisa la conexion a internet.");
        }
    }

    // ------------------------------------------------------------------------
    /// <summary>
    /// Arma la cadena de manejadores: [Reintentos] -> Bitacora -> red.
    /// Cada uno envuelve al siguiente, como los decoradores de G4.
    /// </summary>
    private static HttpClient CrearCliente(Uri direccion, int reintentos = 0, TimeSpan? timeout = null)
    {
        HttpMessageHandler cadena = new ManejadorBitacora(new HttpClientHandler());
        if (reintentos > 0)
        {
            cadena = new ManejadorReintentos(cadena, reintentos);
        }
        return new HttpClient(cadena)
        {
            BaseAddress = direccion,
            Timeout = timeout ?? TimeSpan.FromSeconds(30),
        };
    }
}
