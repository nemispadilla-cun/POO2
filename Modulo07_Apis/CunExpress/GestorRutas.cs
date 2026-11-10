namespace Cun.Poo.Modulo07Apis.CunExpress;

/// <summary>
/// El nucleo del caso de estudio 3.1: decide el orden de las entregas del dia
/// usando el pronostico del clima.
///
/// Las zonas con lluvia intensa se dejan para el final y al cliente se le
/// avisa de un posible retraso. GestorRutas no sabe nada de HTTP ni de JSON:
/// solo conoce objetos (Entrega, ReporteClima) y la interfaz IClienteClima.
/// </summary>
public class GestorRutas
{
    private readonly IClienteClima _clima;

    public GestorRutas(IClienteClima clima)
    {
        _clima = clima;
    }

    public async Task<PlanDelDia> PlanificarAsync(IEnumerable<Entrega> entregas)
    {
        var plan = new PlanDelDia();
        // Una consulta por ZONA, no por paquete: menos llamadas, menos costo.
        var pronosticos = new Dictionary<string, ReporteClima?>();

        foreach (Entrega entrega in entregas)
        {
            if (!pronosticos.TryGetValue(entrega.Zona, out ReporteClima? clima))
            {
                clima = await _clima.ConsultarAsync(entrega.Zona);
                pronosticos[entrega.Zona] = clima;
            }

            if (clima is null)
            {
                plan.SinPronostico.Add(entrega);          // se despacha, pero sin garantia
            }
            else if (clima.EsLluviaIntensa)
            {
                plan.ConAviso.Add((entrega, clima));      // al final de la ruta + aviso al cliente
            }
            else
            {
                plan.Normales.Add((entrega, clima));
            }
        }
        return plan;
    }
}

/// <summary>Resultado de la planificacion: en que orden salen las entregas.</summary>
public class PlanDelDia
{
    public List<(Entrega Entrega, ReporteClima Clima)> Normales { get; } = [];

    public List<(Entrega Entrega, ReporteClima Clima)> ConAviso { get; } = [];

    public List<Entrega> SinPronostico { get; } = [];
}
