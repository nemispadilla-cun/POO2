namespace Cun.Poo.Modulo01Principios;

/// <summary>
/// Excepcion ESPECIFICA DEL DOMINIO: no lanza un Exception generico, lleva
/// datos utiles para quien la capture (motivo y referencia).
/// </summary>
public class PagoRechazadoException(
    PagoRechazadoException.Motivos motivo,
    string referencia,
    string mensaje) : Exception(mensaje)
{
    public enum Motivos
    {
        FondosInsuficientes,
        TarjetaBloqueada,
        MontoInvalido,
    }

    public Motivos Motivo { get; } = motivo;

    public string Referencia { get; } = referencia;
}
