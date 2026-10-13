namespace Cun.Poo.Modulo01Principios;

/// <summary>
/// La persistencia se DELEGA. Factura calcula; el repositorio guarda.
/// </summary>
public interface IRepositorioFacturas
{
    void Guardar(Factura factura);

    Factura? BuscarPorNumero(string numero);

    IReadOnlyList<Factura> Listar();
}
