namespace Cun.Poo.Modulo07Apis.CunExpress;

/// <summary>Pronostico de una zona, como lo devuelve la API de clima.</summary>
public record ReporteClima(string Zona, string Condicion, double PrecipitacionMm, int TemperaturaC)
{
    /// <summary>Regla del negocio: desde 10 mm/h la lluvia ya retrasa una entrega en moto.</summary>
    public bool EsLluviaIntensa => PrecipitacionMm >= 10;
}
