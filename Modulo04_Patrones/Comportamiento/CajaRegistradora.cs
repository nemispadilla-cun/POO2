namespace Cun.Poo.Modulo04Patrones.Comportamiento;

/// <summary>
/// STRATEGY: el CONTEXTO. La caja (POS) del supermercado.
///
/// No tiene ni un if ni un switch sobre el tipo de promocion: DELEGA el
/// calculo a la estrategia que le configuren en tiempo de ejecucion.
///   - Abierto/Cerrado: nueva promocion = nueva clase; la caja no se toca.
///   - Responsabilidad unica: la caja cobra; la estrategia calcula descuentos.
/// </summary>
public class CajaRegistradora
{
    private readonly List<LineaCompra> _compra = [];
    private IEstrategiaDescuento _estrategia = new SinDescuento();

    public decimal Bruto => _compra.Sum(l => l.Subtotal);

    public decimal Descuento => Math.Min(Bruto, _estrategia.CalcularDescuento(_compra));

    public decimal TotalAPagar => Bruto - Descuento;

    public void Registrar(LineaCompra linea) => _compra.Add(linea);

    /// <summary>Cambiar de promocion en caliente, sin redesplegar la caja.</summary>
    public void UsarPromocion(IEstrategiaDescuento estrategia) => _estrategia = estrategia;

    public string Resumen() =>
        $"{_estrategia.Nombre,-42} descuento {Dinero.Cop(Descuento),-12} paga {Dinero.Cop(TotalAPagar)}";
}
