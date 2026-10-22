namespace Cun.Poo.Modulo04Patrones.Comportamiento;

/// <summary>
/// STRATEGY: la familia de algoritmos intercambiables.
///
/// Caso del recurso: las promociones de una cadena de supermercados cambian
/// cada dia. Cada promocion es una clase; la caja solo conoce esta interfaz.
/// Devuelve el VALOR del descuento (no el total), para poder combinarlos.
/// </summary>
public interface IEstrategiaDescuento
{
    string Nombre { get; }

    decimal CalcularDescuento(IReadOnlyList<LineaCompra> compra);
}
