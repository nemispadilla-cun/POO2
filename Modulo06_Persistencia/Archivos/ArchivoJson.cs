using System.Text.Encodings.Web;
using System.Text.Json;

namespace Cun.Poo.Modulo06Persistencia.Archivos;

/// <summary>
/// Serializa cualquier objeto a un archivo JSON y lo reconstruye.
/// Es el equivalente de Gson en Java, pero viene incluido en .NET
/// (System.Text.Json): no hay que instalar nada.
/// </summary>
public static class ArchivoJson
{
    private static readonly JsonSerializerOptions Opciones = new()
    {
        WriteIndented = true,                                    // legible para personas
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,       // Idioma -> "idioma"
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,   // tildes legibles, sin é
    };

    /// <summary>SERIALIZAR: objeto en RAM -> texto -> disco.</summary>
    public static void Guardar<T>(T objeto, string ruta)
    {
        string json = JsonSerializer.Serialize(objeto, Opciones);
        File.WriteAllText(ruta, json);
    }

    /// <summary>
    /// DESERIALIZAR: disco -> texto -> objeto nuevo en RAM.
    /// El tipo generico T le dice al serializador que construir; por eso en
    /// C# no hace falta el TypeToken de Gson, ni siquiera para List&lt;Producto&gt;.
    /// </summary>
    public static T Cargar<T>(string ruta)
    {
        string json = File.ReadAllText(ruta);
        return JsonSerializer.Deserialize<T>(json, Opciones)
               ?? throw new InvalidDataException($"El archivo {Path.GetFileName(ruta)} esta vacio.");
    }

    /// <summary>Convierte a JSON sin guardar, para mostrarlo en pantalla.</summary>
    public static string Texto<T>(T objeto) => JsonSerializer.Serialize(objeto, Opciones);
}
