namespace Cun.Poo.Modulo04Patrones.Comportamiento;

/// <summary>Un renglon del tiquete de caja.</summary>
public record LineaCompra(string Producto, string Categoria, decimal PrecioUnitario, int Cantidad)
{
    public decimal Subtotal => PrecioUnitario * Cantidad;
}
