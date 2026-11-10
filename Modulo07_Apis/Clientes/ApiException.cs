using System.Net;
using System.Text.Json;

namespace Cun.Poo.Modulo07Apis.Clientes;

/// <summary>
/// Error que devolvio la API (un 4xx o 5xx), con su codigo de estado.
/// El resto del programa atrapa ApiException y no necesita saber nada de HTTP.
/// </summary>
public class ApiException : Exception
{
    public ApiException(HttpStatusCode codigo, string mensaje) : base(mensaje)
    {
        Codigo = codigo;
    }

    public HttpStatusCode Codigo { get; }

    /// <summary>
    /// Saca el mensaje del cuerpo de la respuesta. Entiende dos formatos:
    ///   {"error": "..."}                         el de nuestros controladores
    ///   {"title": "...", "errors": {"Email": ["..."]}}   ProblemDetails, el 400
    ///                                            automatico de ASP.NET Core (RFC 7807)
    /// </summary>
    public static ApiException Desde(HttpStatusCode codigo, string cuerpo)
    {
        string mensaje = cuerpo;
        try
        {
            using JsonDocument doc = JsonDocument.Parse(cuerpo);
            JsonElement raiz = doc.RootElement;
            if (raiz.TryGetProperty("error", out JsonElement error))
            {
                mensaje = error.GetString() ?? cuerpo;
            }
            else if (raiz.TryGetProperty("errors", out JsonElement errores))
            {
                mensaje = string.Join("; ", errores.EnumerateObject()
                                                  .SelectMany(campo => campo.Value.EnumerateArray())
                                                  .Select(m => m.GetString()));
            }
            else if (raiz.TryGetProperty("title", out JsonElement titulo))
            {
                mensaje = titulo.GetString() ?? cuerpo;
            }
        }
        catch (JsonException)
        {
            // el cuerpo no era JSON: se deja el texto tal cual
        }
        return new ApiException(codigo, $"{(int)codigo} {codigo}: {mensaje}");
    }
}
