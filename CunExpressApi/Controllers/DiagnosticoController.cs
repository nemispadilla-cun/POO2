using Cun.Poo.CunExpressApi.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Cun.Poo.CunExpressApi.Controllers;

/// <summary>
/// Endpoints que fallan A PROPOSITO, para practicar el manejo de errores
/// en el cliente (seccion 4 de la demo).
/// </summary>
[ApiController]
[Route("api")]
public class DiagnosticoController : ControllerBase
{
    private static int _llamadasInestable;

    /// <summary>
    /// GET /api/inestable: de cada tres llamadas, las dos primeras responden
    /// 503 Service Unavailable y la tercera 200. Simula un servicio saturado.
    /// </summary>
    [HttpGet("inestable")]
    public IActionResult Inestable()
    {
        int llamada = Interlocked.Increment(ref _llamadasInestable);
        return llamada % 3 != 0
            ? StatusCode(StatusCodes.Status503ServiceUnavailable, new Error("Servicio saturado, intente de nuevo"))
            : Ok(new { mensaje = "Por fin respondi" });
    }

    /// <summary>GET /api/lento: tarda 2 segundos en responder.</summary>
    [HttpGet("lento")]
    public async Task<IActionResult> Lento(CancellationToken cancelacion)
    {
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancelacion);   // se cancela si el cliente se rinde
        }
        catch (TaskCanceledException)
        {
            return new EmptyResult();
        }
        return Ok(new { mensaje = "Perdon la demora" });
    }
}
