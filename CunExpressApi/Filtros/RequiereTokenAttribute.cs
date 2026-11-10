using Cun.Poo.CunExpressApi.Modelos;
using Cun.Poo.CunExpressApi.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cun.Poo.CunExpressApi.Filtros;

/// <summary>
/// [RequiereToken] protege un controlador: antes de ejecutar la accion revisa
/// la cabecera  Authorization: Bearer &lt;token&gt;. Si falta o no es valido,
/// responde 401 y la accion nunca se ejecuta.
///
/// Es un atributo con comportamiento: la regla de seguridad se escribe una
/// vez y se reutiliza poniendo [RequiereToken] donde haga falta (G1, DRY).
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiereTokenAttribute : Attribute, IAuthorizationFilter
{
    private const string Prefijo = "Bearer ";

    public void OnAuthorization(AuthorizationFilterContext contexto)
    {
        var tokens = contexto.HttpContext.RequestServices.GetRequiredService<ServicioTokens>();
        string cabecera = contexto.HttpContext.Request.Headers.Authorization.ToString();

        bool valido = cabecera.StartsWith(Prefijo, StringComparison.Ordinal)
                      && tokens.EsValido(cabecera[Prefijo.Length..]);
        if (!valido)
        {
            contexto.HttpContext.Response.Headers.WWWAuthenticate = "Bearer";
            contexto.Result = new UnauthorizedObjectResult(new Error("Falta un token de acceso valido"));
        }
    }
}
