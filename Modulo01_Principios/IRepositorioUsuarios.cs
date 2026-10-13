namespace Cun.Poo.Modulo01Principios;

/// <summary>
/// BAJO ACOPLAMIENTO: el servicio de autenticacion depende de este contrato,
/// jamas de una base de datos concreta.
///
/// Segregacion de interfaces: pide solo lo que autenticacion necesita.
/// </summary>
public interface IRepositorioUsuarios
{
    /// <summary>Devuelve el hash almacenado, o null si el usuario no existe.</summary>
    string? BuscarHashDeContrasena(string usuario);

    string Tecnologia { get; }
}
