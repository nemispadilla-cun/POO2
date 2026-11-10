using System.Text.Json.Serialization;

namespace Cun.Poo.Modulo07Apis.Modelos;

/// <summary>
/// Un registro de la TRM tal como lo publica datos.gov.co:
///   {"valor":"3297.07","unidad":"COP","vigenciadesde":"2026-11-11T00:00:00.000", ...}
///
/// Ojo: "valor" llega como TEXTO, no como numero. AllowReadingFromString le
/// permite al serializador convertirlo a decimal. Leer la respuesta real de
/// la API antes de escribir la clase evita esta sorpresa.
/// </summary>
public class TasaRepresentativa
{
    [JsonPropertyName("valor")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal Valor { get; set; }

    [JsonPropertyName("unidad")]
    public string Unidad { get; set; } = "";

    [JsonPropertyName("vigenciadesde")]
    public DateTime VigenciaDesde { get; set; }

    [JsonPropertyName("vigenciahasta")]
    public DateTime VigenciaHasta { get; set; }

    /// <summary>Convierte un precio en dolares a pesos con esta tasa.</summary>
    public decimal APesos(decimal dolares) => Math.Round(dolares * Valor, 0);
}
