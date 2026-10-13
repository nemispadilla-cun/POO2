namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// STACK&lt;T&gt;: principio LIFO (Last-In, First-Out).
///
/// Caso: la funcion Deshacer (Ctrl+Z) de un editor. La ultima accion del
/// usuario debe ser la primera en revertirse: una pila lo modela exacto.
/// </summary>
public class EditorDeTexto
{
    private readonly Stack<string> _historial = new();

    public string Contenido { get; private set; } = string.Empty;

    public int AccionesEnHistorial => _historial.Count;

    /// <summary>Antes de cambiar, apila el estado anterior.</summary>
    public void Escribir(string texto)
    {
        _historial.Push(Contenido);     // O(1)
        Contenido += texto;
    }

    /// <summary>Desapila el ultimo estado y lo restaura.</summary>
    public bool Deshacer()
    {
        if (_historial.Count == 0)
        {
            return false;
        }
        Contenido = _historial.Pop();   // O(1)
        return true;
    }

    /// <summary>Mira el tope sin sacarlo.</summary>
    public string? Siguiente => _historial.Count > 0 ? _historial.Peek() : null;
}
