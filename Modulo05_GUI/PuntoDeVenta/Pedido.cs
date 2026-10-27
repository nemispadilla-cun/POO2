namespace Cun.Poo.Modulo05Gui.PuntoDeVenta;

/// <summary>
/// MODELO del punto de venta: el pedido en curso y su total.
/// Cuando cambia, avisa con un event (Observer de G4). No sabe cuantas
/// pantallas lo estan mostrando.
/// </summary>
public class Pedido
{
    private readonly List<Producto> _productos = [];

    public IReadOnlyList<Producto> Productos => _productos;

    public decimal Total => _productos.Sum(p => p.Precio);

    public string? MedioDePago { get; private set; }

    /// <summary>Se dispara cada vez que el pedido cambia.</summary>
    public event EventHandler? Cambio;

    public void Agregar(Producto producto)
    {
        if (MedioDePago is not null)
        {
            throw new InvalidOperationException("El pedido ya fue pagado.");
        }
        _productos.Add(producto);
        Cambio?.Invoke(this, EventArgs.Empty);
    }

    public void Pagar(string medio)
    {
        if (_productos.Count == 0)
        {
            throw new InvalidOperationException("No hay productos en el pedido.");
        }
        MedioDePago = medio;
        Cambio?.Invoke(this, EventArgs.Empty);
    }
}
