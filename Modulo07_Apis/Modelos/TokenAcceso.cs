using System.Text.Json.Serialization;

namespace Cun.Poo.Modulo07Apis.Modelos;

/// <summary>
/// Respuesta del servidor de autorizacion OAuth2.
///
/// El estandar usa snake_case ("access_token"), que no se parece a nuestros
/// nombres. [JsonPropertyName] conecta la clave del JSON con la propiedad:
/// asi el dominio conserva nombres en espanol sin depender de la API.
/// (En Gson es @SerializedName("access_token").)
/// </summary>
public class TokenAcceso
{
    [JsonPropertyName("access_token")]
    public string Valor { get; set; } = "";

    [JsonPropertyName("token_type")]
    public string Tipo { get; set; } = "";

    [JsonPropertyName("expires_in")]
    public int ExpiraEnSegundos { get; set; }
}
