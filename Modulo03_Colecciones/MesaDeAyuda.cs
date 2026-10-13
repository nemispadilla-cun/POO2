namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// QUEUE&lt;T&gt;: principio FIFO (First-In, First-Out).
///
/// Caso: las solicitudes de soporte de una telco (Tigo, Movistar). El primer
/// ticket que entra es el primero en atenderse: servicio justo y sin perdidas.
///
/// Equivalencia: Queue&lt;T&gt; de C#  ==  Queue implementada con LinkedList en Java.
/// </summary>
public class MesaDeAyuda
{
    private readonly Queue<TicketSoporte> _tickets = new();

    public int EnEspera => _tickets.Count;

    /// <summary>Entra al final de la fila. O(1).</summary>
    public void Recibir(TicketSoporte ticket) => _tickets.Enqueue(ticket);

    /// <summary>Sale el primero de la fila. O(1). Null si no hay nadie.</summary>
    public TicketSoporte? Atender() =>
        _tickets.TryDequeue(out TicketSoporte? ticket) ? ticket : null;

    public IEnumerable<TicketSoporte> Pendientes => _tickets;
}
