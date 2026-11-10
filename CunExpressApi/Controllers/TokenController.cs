using Cun.Poo.CunExpressApi.Modelos;
using Cun.Poo.CunExpressApi.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Cun.Poo.CunExpressApi.Controllers;

/// <summary>
/// POST /oauth/token: el flujo OAuth2 "client credentials".
/// La aplicacion cliente envia su client_id y su client_secret como
/// formulario y recibe un token de acceso para las demas solicitudes.
/// </summary>
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ServicioTokens _tokens;

    public TokenController(ServicioTokens tokens)
    {
        _tokens = tokens;
    }

    [HttpPost("oauth/token")]
    [Consumes("application/x-www-form-urlencoded")]
    public IActionResult Emitir(
        [FromForm(Name = "grant_type")] string tipoDeConcesion,
        [FromForm(Name = "client_id")] string clientId,
        [FromForm(Name = "client_secret")] string clientSecret)
    {
        if (tipoDeConcesion != "client_credentials")
        {
            return BadRequest(new Error("unsupported_grant_type"));
        }
        if (!_tokens.CredencialesValidas(clientId, clientSecret))
        {
            return Unauthorized(new Error("invalid_client: credenciales de la aplicacion incorrectas"));
        }
        // Los nombres en snake_case los fija el estandar OAuth2 (RFC 6749)
        return Ok(new { access_token = _tokens.Emitir(), token_type = "Bearer", expires_in = _tokens.SegundosDeVigencia });
    }
}
