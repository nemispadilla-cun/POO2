namespace Cun.Poo.Modulo01Principios;

/// <summary>
/// COHESION ALTA: todos los metodos de esta clase tratan sobre lo mismo
/// (calcular, descontar, resumir). No sabe guardarse, ni enviarse por correo,
/// ni escribir logs: eso vive en otros tipos.
/// </summary>
public class Factura
{
    private const decimal Iva = 0.19m;

    private readonly List<LineaFactura> _lineas = [];

    public Factura(string numero, string cliente)
    {
        Numero = numero;
        Cliente = cliente;
    }

    public string Numero { get; }

    public string Cliente { get; }

    public decimal PorcentajeDescuento { get; private set; }

    /// <summary>Vista de solo lectura: nadie puede modificar la lista por fuera.</summary>
    public IReadOnlyList<LineaFactura> Lineas => _lineas;

    public void AgregarLinea(string descripcion, int cantidad, decimal valorUnitario)
    {
        _lineas.Add(new LineaFactura(descripcion, cantidad, valorUnitario));
    }

    public void AplicarDescuento(decimal porcentaje)
    {
        if (porcentaje is < 0 or > 0.5m)
        {
            throw new ArgumentOutOfRangeException(nameof(porcentaje),
                "El descuento debe estar entre 0 y 0,5.");
        }
        PorcentajeDescuento = porcentaje;
    }

    public decimal CalcularSubtotal() => _lineas.Sum(l => l.Subtotal);

    public decimal CalcularDescuento() => CalcularSubtotal() * PorcentajeDescuento;

    public decimal CalcularImpuestos() => (CalcularSubtotal() - CalcularDescuento()) * Iva;

    public decimal CalcularTotal() =>
        CalcularSubtotal() - CalcularDescuento() + CalcularImpuestos();

    public string GenerarResumen()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"   Factura {Numero} - {Cliente}");
        foreach (LineaFactura l in _lineas)
        {
            sb.AppendLine($"     {l.Cantidad,2} x {l.Descripcion,-38} {Dinero.Cop(l.Subtotal),18}");
        }
        sb.AppendLine($"     {"Subtotal",-43} {Dinero.Cop(CalcularSubtotal()),18}");
        sb.AppendLine($"     {$"Descuento ({PorcentajeDescuento:P0})",-43} {Dinero.Cop(-CalcularDescuento()),18}");
        sb.AppendLine($"     {"IVA (19%)",-43} {Dinero.Cop(CalcularImpuestos()),18}");
        sb.Append($"     {"TOTAL",-43} {Dinero.Cop(CalcularTotal()),18}");
        return sb.ToString();
    }
}
