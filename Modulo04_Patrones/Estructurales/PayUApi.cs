namespace Cun.Poo.Modulo04Patrones.Estructurales;

public record PayURespuesta(string TransactionId, string State);

/// <summary>
/// SDK externo SIMULADO de PayU. No es nuestro: no podemos cambiarlo.
/// Trabaja en ingles, con valor decimal, moneda explicita y estados en texto.
/// </summary>
public class PayUApi
{
    private readonly Dictionary<string, string> _transacciones = [];
    private int _consecutivo = 1000;

    public PayURespuesta SubmitTransaction(string referenceCode, decimal value, string currency)
    {
        string id = $"PAYU-{++_consecutivo}";
        _transacciones[id] = value > 0 ? "APPROVED" : "DECLINED";
        return new PayURespuesta(id, _transacciones[id]);
    }

    public string GetTransactionState(string transactionId) =>
        _transacciones.TryGetValue(transactionId, out string? estado) ? estado : "NOT_FOUND";
}
