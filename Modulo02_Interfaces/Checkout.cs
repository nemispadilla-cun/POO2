namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// El nucleo del carrito de compras. No conoce PSE, ni Nequi, ni Efecty: solo
/// conoce el contrato IMetodoDePago. Agregar DaviPlata manana es crear una
/// clase nueva, sin tocar una sola linea de este archivo.
/// </summary>
public class Checkout
{
    private readonly List<string> _recibos = [];

    public IReadOnlyList<string> Recibos => _recibos;

    public bool Cobrar(IMetodoDePago metodo, decimal monto)
    {
        Console.WriteLine($"    > Cobrando con {metodo.Nombre} - {IMetodoDePago.FormatearCop(monto)}");

        bool aprobado = metodo.ProcesarPago(monto);     // enlace dinamico
        Console.WriteLine($"      Estado -> {metodo.ObtenerEstado()}");

        if (aprobado)
        {
            _recibos.Add($"{metodo.Nombre} - {IMetodoDePago.FormatearCop(monto)}");
        }
        return aprobado;
    }
}
