using System.Net;
using System.Net.Http.Json;
using Cun.Poo.Modulo07Apis.Modelos;

namespace Cun.Poo.Modulo07Apis.Clientes;

/// <summary>
/// Cliente "tipado" del recurso /api/usuarios: las cuatro operaciones CRUD.
///
/// Encapsula todo lo de HTTP (rutas, verbos, codigos, JSON). Quien lo usa
/// solo ve metodos y objetos Usuario, igual que con un DAO de G6. Si la API
/// cambia de ruta o de version, solo se modifica esta clase.
/// </summary>
public class ClienteUsuarios
{
    private const string Recurso = "api/usuarios";
    private readonly HttpClient _http;

    /// <summary>El HttpClient llega por el constructor (inyeccion de dependencias, G1).</summary>
    public ClienteUsuarios(HttpClient http)
    {
        _http = http;
    }

    /// <summary>GET /api/usuarios -> la lista completa.</summary>
    public async Task<List<Usuario>> ListarAsync() =>
        await _http.GetFromJsonAsync<List<Usuario>>(Recurso, JsonApi.Opciones) ?? [];

    /// <summary>GET /api/usuarios/{id}. Un 404 no es una excepcion: significa "no existe" (null).</summary>
    public async Task<Usuario?> ObtenerAsync(int id)
    {
        using HttpResponseMessage respuesta = await _http.GetAsync($"{Recurso}/{id}");
        if (respuesta.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        await AsegurarExitoAsync(respuesta);
        return await respuesta.Content.ReadFromJsonAsync<Usuario>(JsonApi.Opciones);
    }

    /// <summary>POST /api/usuarios. Serializa el objeto y devuelve el que creo el servidor (con su Id).</summary>
    public async Task<Usuario> CrearAsync(Usuario nuevo)
    {
        using HttpResponseMessage respuesta = await _http.PostAsJsonAsync(Recurso, nuevo, JsonApi.Opciones);
        await AsegurarExitoAsync(respuesta);
        return (await respuesta.Content.ReadFromJsonAsync<Usuario>(JsonApi.Opciones))!;
    }

    /// <summary>PUT /api/usuarios/{id}: reemplaza el recurso completo.</summary>
    public async Task<Usuario?> ActualizarAsync(Usuario usuario)
    {
        using HttpResponseMessage respuesta =
            await _http.PutAsJsonAsync($"{Recurso}/{usuario.Id}", usuario, JsonApi.Opciones);
        if (respuesta.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        await AsegurarExitoAsync(respuesta);
        return await respuesta.Content.ReadFromJsonAsync<Usuario>(JsonApi.Opciones);
    }

    /// <summary>DELETE /api/usuarios/{id}. true si se borro (204), false si no existia (404).</summary>
    public async Task<bool> EliminarAsync(int id)
    {
        using HttpResponseMessage respuesta = await _http.DeleteAsync($"{Recurso}/{id}");
        if (respuesta.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }
        await AsegurarExitoAsync(respuesta);
        return true;
    }

    /// <summary>Convierte un 4xx/5xx en una ApiException con el mensaje que mando el servidor.</summary>
    private static async Task AsegurarExitoAsync(HttpResponseMessage respuesta)
    {
        if (respuesta.IsSuccessStatusCode)
        {
            return;
        }
        string cuerpo = await respuesta.Content.ReadAsStringAsync();
        throw ApiException.Desde(respuesta.StatusCode, cuerpo);
    }
}
