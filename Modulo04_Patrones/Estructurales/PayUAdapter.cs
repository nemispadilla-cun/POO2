namespace Cun.Poo.Modulo04Patrones.Estructurales;

/// <summary>
/// ADAPTER sobre PayU: traduce la llamada generica a la forma que pide PayU.
/// </summary>
public class PayUAdapter : IPasarelaPago
{
    private readonly PayUApi _payU;

    public PayUAdapter(PayUApi payU) => _payU = payU;

    public string Nombre => "PayU";

    public string IniciarPago(decimal monto, string descripcion) =>
        _payU.SubmitTransaction(referenceCode: descripcion, value: monto, currency: "COP").TransactionId;

    public EstadoPago VerificarEstado(string idTransaccion) =>
        _payU.GetTransactionState(idTransaccion) switch
        {
            "APPROVED" => EstadoPago.Aprobado,
            "PENDING" => EstadoPago.Pendiente,
            _ => EstadoPago.Rechazado,
        };
}
