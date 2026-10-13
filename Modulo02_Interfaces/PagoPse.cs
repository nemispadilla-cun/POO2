namespace Cun.Poo.Modulo02Interfaces;

/// <summary>Implementacion concreta: "firma" el contrato IMetodoDePago.</summary>
public class PagoPse(string banco) : IMetodoDePago
{
    private string _estado = "PSE: sin transacciones.";

    public string Nombre => $"PSE ({banco})";

    public bool ProcesarPago(decimal monto)
    {
        // La regla comun llega del metodo default de la interfaz.
        if (!((IMetodoDePago)this).MontoValido(monto))
        {
            _estado = "PSE: monto por debajo del minimo.";
            return false;
        }

        // Aqui iria la comunicacion real con ACH Colombia.
        Console.WriteLine($"     Redirigiendo al portal de {banco} para pago con PSE "
                          + $"por {IMetodoDePago.FormatearCop(monto)}");
        _estado = $"PSE: transaccion aprobada en {banco}.";
        return true;
    }

    public string ObtenerEstado() => _estado;
}
