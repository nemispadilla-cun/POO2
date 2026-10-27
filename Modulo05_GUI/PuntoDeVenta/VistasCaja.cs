namespace Cun.Poo.Modulo05Gui.PuntoDeVenta;

/// <summary>
/// VISTA del cajero: la tabla con la cuenta (la JTable del recurso).
/// Se actualiza sola porque escucha el evento Cambio del pedido.
/// </summary>
public class VistaTicket
{
    public VistaTicket(Pedido pedido)
    {
        pedido.Cambio += (sender, e) => Mostrar((Pedido)sender!);
    }

    private static void Mostrar(Pedido pedido)
    {
        string detalle = string.Join(", ", pedido.Productos
            .GroupBy(p => p.Nombre)
            .Select(g => $"{g.Count()} {g.Key}"));
        string estado = pedido.MedioDePago is null ? "" : $"  PAGADO con {pedido.MedioDePago}";
        Console.WriteLine($"   [Ticket]   {detalle} = {Dinero.Cop(pedido.Total)}{estado}");
    }
}

/// <summary>
/// Segunda VISTA del mismo modelo: la pantalla que mira el cliente.
/// Agregarla no obligo a tocar Pedido ni el controlador.
/// </summary>
public class PantallaCliente
{
    public PantallaCliente(Pedido pedido)
    {
        pedido.Cambio += (sender, e) =>
        {
            var p = (Pedido)sender!;
            string mensaje = p.MedioDePago is null ? "Total a pagar" : "Gracias por su compra";
            Console.WriteLine($"   [Cliente]  {mensaje}: {Dinero.Cop(p.Total)}");
        };
    }
}
