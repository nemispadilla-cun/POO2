using System.ComponentModel.DataAnnotations;

namespace Cun.Poo.CunExpressApi.Modelos;

/// <summary>
/// Lo que el cliente puede ENVIAR en un POST o PUT (un DTO, objeto de
/// transferencia). No trae Id ni Telefono: esos los controla el servidor.
///
/// Las anotaciones son las reglas de validacion. Con [ApiController], si
/// alguna falla, ASP.NET Core responde 400 Bad Request solo, sin que el
/// controlador escriba un if. El servidor nunca confia en el cliente.
/// </summary>
public class UsuarioEntrada
{
    [Required(ErrorMessage = "El campo 'nombre' es obligatorio")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 80 letras")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El campo 'email' es obligatorio")]
    [EmailAddress(ErrorMessage = "El email no es valido")]
    public string Email { get; set; } = "";

    public string? Ciudad { get; set; }
}
