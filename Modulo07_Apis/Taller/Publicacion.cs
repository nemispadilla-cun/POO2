using System.Text.Json.Serialization;

namespace Cun.Poo.Modulo07Apis.Taller;

/// <summary>
/// La clase Post del taller 4.4. La API (como JSONPlaceholder) habla en ingles
/// (userId, title, body); nuestro dominio en espanol. [JsonPropertyName] hace
/// la traduccion al deserializar, y EF Core guarda el objeto en SQLite (G6).
/// Una misma clase sirve para las dos cosas: JSON -> objeto -> fila.
/// </summary>
public class Publicacion
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("userId")]
    public int AutorId { get; set; }

    [JsonPropertyName("title")]
    public string Titulo { get; set; } = "";

    [JsonPropertyName("body")]
    public string Cuerpo { get; set; } = "";
}
