using Cun.Poo.CunExpressApi.Modelos;
using Cun.Poo.CunExpressApi.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Cun.Poo.CunExpressApi.Controllers;

/// <summary>
/// El recurso /api/usuarios con sus cuatro operaciones CRUD.
///
/// Cada metodo publico es un ENDPOINT: el atributo dice el verbo HTTP y la
/// ruta, y el tipo de retorno decide el codigo de estado (Ok = 200,
/// CreatedAtAction = 201, NoContent = 204, NotFound = 404).
/// [ApiController] valida el cuerpo con las anotaciones de UsuarioEntrada y,
/// si algo falla, responde 400 antes de llegar al metodo.
/// </summary>
[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly RepositorioUsuarios _repositorio;

    /// <summary>ASP.NET Core entrega el repositorio por el constructor (inyeccion de dependencias).</summary>
    public UsuariosController(RepositorioUsuarios repositorio)
    {
        _repositorio = repositorio;
    }

    /// <summary>GET /api/usuarios -> 200 con la lista.</summary>
    [HttpGet]
    public List<UsuarioRegistro> Listar() => _repositorio.Listar();

    /// <summary>GET /api/usuarios/{id} -> 200 con el usuario, o 404.</summary>
    [HttpGet("{id:int}")]
    public ActionResult<UsuarioRegistro> Obtener(int id) =>
        _repositorio.Buscar(id) is { } usuario
            ? Ok(usuario)
            : NotFound(new Error($"No existe el usuario {id}"));

    /// <summary>POST /api/usuarios -> 201 Created + cabecera Location con la URL del nuevo recurso.</summary>
    [HttpPost]
    public ActionResult<UsuarioRegistro> Crear(UsuarioEntrada datos)
    {
        UsuarioRegistro nuevo = _repositorio.Agregar(datos);
        return CreatedAtAction(nameof(Obtener), new { id = nuevo.Id }, nuevo);
    }

    /// <summary>PUT /api/usuarios/{id} -> 200 con el usuario actualizado, o 404.</summary>
    [HttpPut("{id:int}")]
    public ActionResult<UsuarioRegistro> Actualizar(int id, UsuarioEntrada datos) =>
        _repositorio.Reemplazar(id, datos) is { } actualizado
            ? Ok(actualizado)
            : NotFound(new Error($"No existe el usuario {id}"));

    /// <summary>DELETE /api/usuarios/{id} -> 204 No Content (sin cuerpo), o 404.</summary>
    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id) =>
        _repositorio.Eliminar(id)
            ? NoContent()
            : NotFound(new Error($"No existe el usuario {id}"));
}
