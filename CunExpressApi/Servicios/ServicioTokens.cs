using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace Cun.Poo.CunExpressApi.Servicios;

/// <summary>
/// Emite y valida tokens de acceso (el "servidor de autorizacion" de OAuth2).
///
/// Las credenciales de la aplicacion se leen de la configuracion
/// (appsettings.json, seccion OAuth). En produccion el secreto va en una
/// variable de entorno o en un gestor de secretos, nunca en el repositorio.
/// Los tokens reales suelen ser JWT firmados; aqui son texto aleatorio.
/// </summary>
public class ServicioTokens
{
    private readonly ConcurrentDictionary<string, DateTime> _vigentes = new();
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly TimeSpan _vigencia;

    public ServicioTokens(IConfiguration configuracion)
    {
        _clientId = configuracion["OAuth:ClientId"] ?? "";
        _clientSecret = configuracion["OAuth:ClientSecret"] ?? "";
        _vigencia = TimeSpan.FromMinutes(configuracion.GetValue("OAuth:MinutosDeVigencia", 60));
    }

    public int SegundosDeVigencia => (int)_vigencia.TotalSeconds;

    public bool CredencialesValidas(string clientId, string clientSecret) =>
        clientId == _clientId && clientSecret == _clientSecret;

    /// <summary>Crea un token aleatorio, imposible de adivinar, y anota cuando vence.</summary>
    public string Emitir()
    {
        string token = "cunx-" + Convert.ToHexString(RandomNumberGenerator.GetBytes(12)).ToLowerInvariant();
        _vigentes[token] = DateTime.UtcNow + _vigencia;
        return token;
    }

    public bool EsValido(string token) =>
        _vigentes.TryGetValue(token, out DateTime vence) && vence > DateTime.UtcNow;
}
