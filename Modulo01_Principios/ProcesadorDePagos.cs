namespace Cun.Poo.Modulo01Principios;

/// <summary>
/// MANEJO DE EXCEPCIONES: quien detecta lanza, quien sabe que hacer captura.
/// El bucle de pagos nunca se interrumpe.
/// </summary>
public class ProcesadorDePagos(ILogger logger)
{
    private const decimal CupoMaximo = 500_000m;

    public string Procesar(Pago pago)
    {
        try
        {
            Autorizar(pago);                         // puede lanzar
            logger.Info($"Pago {pago.Referencia} aprobado por {Dinero.Cop(pago.Monto)}");
            return "Pago aprobado. Gracias por su compra.";
        }
        catch (PagoRechazadoException e)
        {
            logger.Error($"Incidente en pago {e.Referencia}: {e.Message}", e);
            return MensajeParaUsuario(e);            // el flujo continua
        }
    }

    private static void Autorizar(Pago pago)
    {
        if (pago.Monto <= 0)
        {
            throw new PagoRechazadoException(PagoRechazadoException.Motivos.MontoInvalido,
                pago.Referencia, "el monto debe ser mayor que cero");
        }
        if (pago.Tarjeta.StartsWith("0000", StringComparison.Ordinal))
        {
            throw new PagoRechazadoException(PagoRechazadoException.Motivos.TarjetaBloqueada,
                pago.Referencia, "la tarjeta esta bloqueada");
        }
        if (pago.Monto > CupoMaximo)
        {
            throw new PagoRechazadoException(PagoRechazadoException.Motivos.FondosInsuficientes,
                pago.Referencia, "fondos insuficientes");
        }
    }

    private static string MensajeParaUsuario(PagoRechazadoException e) => e.Motivo switch
    {
        PagoRechazadoException.Motivos.TarjetaBloqueada =>
            "Su tarjeta esta bloqueada. Comuniquese con su banco.",
        PagoRechazadoException.Motivos.FondosInsuficientes =>
            "Su tarjeta no tiene fondos suficientes.",
        PagoRechazadoException.Motivos.MontoInvalido =>
            "El valor del pago no es valido.",
        _ => "No fue posible procesar el pago.",
    };
}
