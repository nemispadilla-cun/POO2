namespace Cun.Poo.Modulo04Patrones.Estructurales;

public record WompiTransaccion(Guid Id, long MontoEnCentavos, string Estado);

/// <summary>
/// SDK externo SIMULADO de Wompi. Tampoco es nuestro.
/// Cobra en CENTAVOS (long), exige correo del cliente e identifica con Guid:
/// nada coincide con PayU ni con IPasarelaPago.
/// </summary>
public class WompiApi
{
    private readonly Dictionary<Guid, WompiTransaccion> _transacciones = [];

    public WompiTransaccion CrearTransaccion(long montoEnCentavos, string referencia, string correoCliente)
    {
        // Wompi deja la transaccion en PENDING hasta que el banco confirma.
        var t = new WompiTransaccion(Guid.NewGuid(), montoEnCentavos, "PENDING");
        _transacciones[t.Id] = t with { Estado = "APPROVED" };
        return t;
    }

    public WompiTransaccion? ObtenerTransaccion(Guid id) =>
        _transacciones.TryGetValue(id, out WompiTransaccion? t) ? t : null;
}
