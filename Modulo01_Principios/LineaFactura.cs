namespace Cun.Poo.Modulo01Principios;

/// <summary>
/// Dato inmutable. En C# un "record" genera constructor, propiedades de solo
/// lectura, Equals, GetHashCode y ToString. Es el equivalente del record de Java.
/// </summary>
public record LineaFactura(string Descripcion, int Cantidad, decimal ValorUnitario)
{
    public decimal Subtotal => Cantidad * ValorUnitario;
}
