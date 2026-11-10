using Cun.Poo.CunExpressApi.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Cun.Poo.CunExpressApi.Controllers;

/// <summary>
/// GET /api/clima?zona=Chapinero: el papel de OpenWeatherMap en el caso
/// CunExpress. Los pronosticos son fijos para que la salida de la clase se
/// pueda repetir. La zona llega como parametro de consulta ([FromQuery]).
/// </summary>
[ApiController]
[Route("api/clima")]
public class ClimaController : ControllerBase
{
    private static readonly Dictionary<string, Pronostico> Pronosticos =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Chapinero"] = new("Chapinero", "lluvia intensa", 18.5, 12),
            ["Suba"] = new("Suba", "nublado", 0.4, 15),
            ["Kennedy"] = new("Kennedy", "tormenta electrica", 12.0, 13),
            ["Usaquen"] = new("Usaquen", "soleado", 0.0, 19),
            ["Bosa"] = new("Bosa", "llovizna", 2.1, 14),
            ["Teusaquillo"] = new("Teusaquillo", "parcialmente nublado", 0.0, 17),
        };

    [HttpGet]
    public ActionResult<Pronostico> Consultar([FromQuery] string zona) =>
        Pronosticos.TryGetValue(zona, out Pronostico? pronostico)
            ? Ok(pronostico)
            : NotFound(new Error($"Sin estacion meteorologica en '{zona}'"));
}
