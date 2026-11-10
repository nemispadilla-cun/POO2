using System.Text.Encodings.Web;
using System.Text.Json;

namespace Cun.Poo.Modulo07Apis.Clientes;

/// <summary>
/// Opciones de JSON que comparten los clientes de la demo.
///
/// JsonSerializerDefaults.Web es la convencion de las APIs REST (y la que usa
/// ASP.NET Core en CunExpressApi):
/// propiedades en camelCase (Nombre -> "nombre") y lectura sin distinguir
/// mayusculas. Es el "new Gson()" del recurso, pero incluido en .NET.
/// </summary>
public static class JsonApi
{
    public static readonly JsonSerializerOptions Opciones = new(JsonSerializerDefaults.Web)
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,   // tildes legibles en pantalla
    };

    /// <summary>SERIALIZAR: objeto -> texto JSON (lo que viaja en el cuerpo de un POST o PUT).</summary>
    public static string Texto<T>(T objeto) => JsonSerializer.Serialize(objeto, Opciones);
}
