using Cun.Poo.CunExpressApi.Filtros;
using Cun.Poo.CunExpressApi.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Cun.Poo.CunExpressApi.Controllers;

/// <summary>
/// GET /api/pedidos: recurso PROTEGIDO. Sin un token valido en la cabecera
/// Authorization, el filtro [RequiereToken] responde 401 y este codigo no corre.
/// </summary>
[ApiController]
[Route("api/pedidos")]
[RequiereToken]
public class PedidosController : ControllerBase
{
    [HttpGet]
    public List<PedidoResumen> Listar() =>
    [
        new(501, "Drogueria La Rebaja", "Chapinero", "en bodega"),
        new(502, "Panaderia El Trigal", "Suba", "en ruta"),
        new(503, "Papeleria Kennedy", "Kennedy", "en bodega"),
    ];
}
