namespace Cun.Poo.Modulo04Patrones.Estructurales;

/// <summary>
/// ADAPTER sobre Wompi: pesos a centavos, string a Guid y estados en ingles
/// a EstadoPago. El carrito no se entera de nada de esto.
/// </summary>
public class WompiAdapter : IPasarelaPago
{
    private readonly WompiApi _wompi;
    private readonly string _correoComercio;

    public WompiAdapter(WompiApi wompi, string correoComercio)
    {
        _wompi = wompi;
        _correoComercio = correoComercio;
    }

    public string Nombre => "Wompi";

    public string IniciarPago(decimal monto, string descripcion)
    {
        long centavos = (long)(monto * 100);                       // traduccion 1: unidades
        WompiTransaccion t = _wompi.CrearTransaccion(centavos, descripcion, _correoComercio);
        return t.Id.ToString();                                    // traduccion 2: tipo del id
    }

    public EstadoPago VerificarEstado(string idTransaccion)
    {
        if (!Guid.TryParse(idTransaccion, out Guid id))
        {
            return EstadoPago.Rechazado;
        }
        return _wompi.ObtenerTransaccion(id)?.Estado switch        // traduccion 3: estados
        {
            "APPROVED" => EstadoPago.Aprobado,
            "PENDING" => EstadoPago.Pendiente,
            _ => EstadoPago.Rechazado,
        };
    }
}
