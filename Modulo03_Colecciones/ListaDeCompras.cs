namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// LIST&lt;T&gt;: coleccion ORDENADA que admite DUPLICADOS y acceso por indice.
///
/// Caso: la lista de compras de la app de un supermercado (Exito, Carulla).
/// Se elige List&lt;T&gt; porque las operaciones frecuentes son agregar al final
/// y recorrer, y en ambas es muy eficiente.
///
/// Equivalencia: List&lt;T&gt; de C#  ==  ArrayList de Java.
/// </summary>
public class ListaDeCompras
{
    private readonly List<string> _productos = [];

    /// <summary>Vista de solo lectura: nadie modifica la lista por fuera.</summary>
    public IReadOnlyList<string> Productos => _productos;

    public int Cantidad => _productos.Count;

    public void Agregar(string producto) => _productos.Add(producto);

    /// <summary>O(n): hay que recorrer hasta encontrarlo y luego desplazar el resto.</summary>
    public bool Quitar(string producto) => _productos.Remove(producto);

    /// <summary>O(1): el acceso por indice es la gran ventaja de List&lt;T&gt;.</summary>
    public string EnPosicion(int indice) => _productos[indice];

    public override string ToString() => "[" + string.Join(", ", _productos) + "]";
}

