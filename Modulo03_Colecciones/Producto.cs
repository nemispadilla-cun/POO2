namespace Cun.Poo.Modulo03Colecciones;

public record Producto(string Codigo, string Nombre, decimal Precio) : IIdentificable
{
    public override string ToString() => $"{Codigo} {Nombre} ({Dinero.Cop(Precio)})";
}
