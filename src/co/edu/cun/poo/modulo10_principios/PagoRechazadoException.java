package co.edu.cun.poo.modulo10_principios;

/**
 * Excepción ESPECÍFICA del dominio de pagos. Lleva el motivo y un código
 * para que quien la capture pueda dar retroalimentación útil al usuario.
 */
public class PagoRechazadoException extends Exception {

    public enum Motivo { FONDOS_INSUFICIENTES, TARJETA_BLOQUEADA, MONTO_INVALIDO }

    private final Motivo motivo;
    private final String referencia;

    public PagoRechazadoException(Motivo motivo, String referencia, String detalle) {
        super("Pago " + referencia + " rechazado: " + detalle);
        this.motivo = motivo;
        this.referencia = referencia;
    }

    public Motivo getMotivo() {
        return motivo;
    }

    public String getReferencia() {
        return referencia;
    }
}
