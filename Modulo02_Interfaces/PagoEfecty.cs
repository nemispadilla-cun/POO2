namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// Pago en efectivo. Demuestra que una implementacion PUEDE sobreescribir un
/// metodo default cuando su regla de negocio es distinta.
/// </summary>
public class PagoEfecty : IMetodoDePago
{
    private const decimal MinimoEfecty = 20_000m;

    private string _estado = "Efecty: sin transacciones.";

    public string Nombre => "Efecty";

    public bool ProcesarPago(decimal monto)
    {
        if (!MontoValido(monto))
        {
            _estado = "Efecty: el recaudo en efectivo exige al menos "
                      + IMetodoDePago.FormatearCop(MinimoEfecty) + ".";
            return false;
        }

        Console.WriteLine($"     Generando recibo de pago en Efecty "
                          + $"por {IMetodoDePago.FormatearCop(monto)} (vence en 48 horas)");
        _estado = "Efecty: recibo generado, pendiente de pago en punto fisico.";
        return true;
    }

    /// <summary>Sobreescribe el default: aqui el minimo NO es el del contrato.</summary>
    public bool MontoValido(decimal monto) => monto >= MinimoEfecty;

    public string ObtenerEstado() => _estado;
}
