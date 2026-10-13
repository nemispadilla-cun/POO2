namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// GENERICOS con RESTRICCION de tipo.
///
/// "where T : IIdentificable" le garantiza al compilador que cualquier T
/// tendra una propiedad Codigo. Eso es SEGURIDAD DE TIPO: los errores se
/// detectan al compilar, no en produccion.
///
/// Ademas, la estructura interna (el Dictionary) queda ENCAPSULADA: quien use
/// la clase no puede corromperla, y manana se puede cambiar por otra
/// estructura sin romper a nadie.
/// </summary>
public class Inventario<T> where T : IIdentificable
{
    private readonly Dictionary<string, T> _stock = [];

    public int Cantidad => _stock.Count;

    public void Agregar(T elemento) => _stock[elemento.Codigo] = elemento;

    public T? Buscar(string codigo) =>
        _stock.TryGetValue(codigo, out T? valor) ? valor : default;

    public bool Eliminar(string codigo) => _stock.Remove(codigo);

    public IReadOnlyCollection<T> Todos => _stock.Values;
}
