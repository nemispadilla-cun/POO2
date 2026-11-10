using System.Text.Json.Serialization;

namespace Cun.Poo.CunExpressApi.Modelos;

/// <summary>Pronostico de una zona de Bogota (GET /api/clima).</summary>
public record Pronostico(string Zona, string Condicion, double PrecipitacionMm, int TemperaturaC);

/// <summary>Pedido de CunExpress (GET /api/pedidos, protegido con token).</summary>
public record PedidoResumen(int Id, string Cliente, string Destino, string Estado);

/// <summary>
/// Publicacion al estilo de JSONPlaceholder: nombres en ingles a proposito
/// (userId, id, title, body). El cliente los traduce con [JsonPropertyName].
/// </summary>
public record Post(int UserId, int Id, string Title, string Body);

/// <summary>Respuesta de error de la API: {"error": "..."}.</summary>
public record Error([property: JsonPropertyName("error")] string Mensaje);
