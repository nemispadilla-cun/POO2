using System.Net.Http.Json;
using Cun.Poo.Modulo07Apis.Modelos;

namespace Cun.Poo.Modulo07Apis.Clientes;

/// <summary>
/// Pide un token de acceso con el flujo OAuth2 "client credentials":
/// la APLICACION se identifica con su client_id y su client_secret.
///
/// Es el paso 4 del flujo del recurso (cambiar credenciales por un token),
/// sin la pantalla de login, porque aqui no hay un usuario humano.
/// </summary>
public class ClienteOAuth
{
    private readonly HttpClient _http;

    public ClienteOAuth(HttpClient http)
    {
        _http = http;
    }

    public async Task<TokenAcceso> ObtenerTokenAsync(string clientId, string clientSecret)
    {
        // OAuth2 envia las credenciales como formulario, no como JSON
        var formulario = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "client_credentials",
            ["client_id"] = clientId,
            ["client_secret"] = clientSecret,
        });
        using HttpResponseMessage respuesta = await _http.PostAsync("oauth/token", formulario);
        if (!respuesta.IsSuccessStatusCode)
        {
            throw ApiException.Desde(respuesta.StatusCode, await respuesta.Content.ReadAsStringAsync());
        }
        return (await respuesta.Content.ReadFromJsonAsync<TokenAcceso>(JsonApi.Opciones))!;
    }
}
