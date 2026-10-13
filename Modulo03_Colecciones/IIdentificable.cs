namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// Contrato minimo para que algo pueda guardarse en un Inventario&lt;T&gt;.
/// Es la restriccion que usa el generico: "where T : IIdentificable".
/// </summary>
public interface IIdentificable
{
    string Codigo { get; }
}
