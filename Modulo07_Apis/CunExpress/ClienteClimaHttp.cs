using System.Net;
using System.Net.Http.Json;
using Cun.Poo.Modulo07Apis.Clientes;

namespace Cun.Poo.Modulo07Apis.CunExpress;

/// <summary>
/// Implementacion de IClienteClima que llama a una API REST de clima
/// (en la demo, el servidor local; en CunExpress, OpenWeatherMap).
/// </summary>
public class ClienteClimaHttp : IClienteClima
{
    private readonly HttpClient _http;

    public ClienteClimaHttp(HttpClient http)
    {
        _http = http;
    }

    public async Task<ReporteClima?> ConsultarAsync(string zona)
    {
        // Uri.EscapeDataString: "San Cristobal" viaja como "San%20Cristobal"
        using HttpResponseMessage respuesta =
            await _http.GetAsync($"api/clima?zona={Uri.EscapeDataString(zona)}");
        if (respuesta.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        respuesta.EnsureSuccessStatusCode();
        return await respuesta.Content.ReadFromJsonAsync<ReporteClima>(JsonApi.Opciones);
    }
}
