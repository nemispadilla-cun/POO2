namespace Cun.Poo.Modulo04Patrones.Comportamiento;

/// <summary>Estrategia neutra: evita preguntar "if (estrategia == null)" en la caja.</summary>
public class SinDescuento : IEstrategiaDescuento
{
    public string Nombre => "Precio normal";

    public decimal CalcularDescuento(IReadOnlyList<LineaCompra> compra) => 0m;
}

/// <summary>"20 % de descuento en lacteos".</summary>
public class DescuentoPorcentual(string categoria, decimal porcentaje) : IEstrategiaDescuento
{
    public string Nombre => $"{porcentaje:0}% en {categoria}";

    public decimal CalcularDescuento(IReadOnlyList<LineaCompra> compra) =>
        compra.Where(l => l.Categoria == categoria).Sum(l => l.Subtotal) * porcentaje / 100m;
}

/// <summary>"Pague 2, lleve 3 en galleteria": de cada 3 unidades, una es gratis.</summary>
public class DescuentoPagueLleve(string categoria, int pague, int lleve) : IEstrategiaDescuento
{
    public string Nombre => $"Pague {pague} lleve {lleve} en {categoria}";

    public decimal CalcularDescuento(IReadOnlyList<LineaCompra> compra) =>
        compra.Where(l => l.Categoria == categoria)
              .Sum(l => l.Cantidad / lleve * (lleve - pague) * l.PrecioUnitario);
}

/// <summary>"10 % adicional si paga con la tarjeta del supermercado".</summary>
public class DescuentoTarjetaFidelidad(decimal porcentaje) : IEstrategiaDescuento
{
    public string Nombre => $"{porcentaje:0}% tarjeta de la casa";

    public decimal CalcularDescuento(IReadOnlyList<LineaCompra> compra) =>
        compra.Sum(l => l.Subtotal) * porcentaje / 100m;
}

/// <summary>
/// Varias promociones a la vez. Es una estrategia hecha de estrategias:
/// la misma idea del Composite, aplicada a un Strategy. Los patrones se combinan.
/// </summary>
public class DescuentoCombinado(params IEstrategiaDescuento[] estrategias) : IEstrategiaDescuento
{
    public string Nombre => string.Join(" + ", estrategias.Select(e => e.Nombre));

    public decimal CalcularDescuento(IReadOnlyList<LineaCompra> compra) =>
        estrategias.Sum(e => e.CalcularDescuento(compra));
}
