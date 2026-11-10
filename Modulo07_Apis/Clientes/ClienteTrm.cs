using System.Net.Http.Json;
using Cun.Poo.Modulo07Apis.Modelos;

namespace Cun.Poo.Modulo07Apis.Clientes;

/// <summary>
/// Consulta la TRM (Tasa Representativa del Mercado) vigente en el portal de
/// datos abiertos de Colombia (datos.gov.co), el ejemplo 2.3 del recurso.
///
/// Es una API publica: no pide token. Se filtra con parametros de la URL
/// ($order y $limit): asi el servidor sabe que queremos solo la ultima.
/// </summary>
public class ClienteTrm
{
    private const string Endpoint = "resource/32sa-8pi3.json?$order=vigenciadesde%20DESC&$limit=1";
    private readonly HttpClient _http;

    /// <summary>Espera un HttpClient con BaseAddress = https://www.datos.gov.co/</summary>
    public ClienteTrm(HttpClient http)
    {
        _http = http;
    }

    public async Task<TasaRepresentativa> ObtenerVigenteAsync()
    {
        // La API responde un ARREGLO aunque pidamos un solo elemento
        List<TasaRepresentativa> tasas =
            await _http.GetFromJsonAsync<List<TasaRepresentativa>>(Endpoint, JsonApi.Opciones) ?? [];
        return tasas.Count > 0
            ? tasas[0]
            : throw new InvalidOperationException("La API no devolvio ninguna TRM.");
    }
}
