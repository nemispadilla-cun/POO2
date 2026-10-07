package co.edu.cun.poo.modulo11_interfaces;

/**
 * Pago en efectivo. Demuestra que una implementación PUEDE sobreescribir
 * un método default cuando su regla de negocio es distinta: en Efecty el
 * mínimo de recaudo es más alto que el del resto del ecosistema.
 */
public class PagoEfecty implements MetodoDePago {

    private static final double MINIMO_EFECTY = 20_000;

    private String estado = "Efecty: sin transacciones.";

    @Override
    public boolean procesarPago(double monto) {
        if (!montoValido(monto)) {
            estado = "Efecty: el recaudo en efectivo exige al menos "
                    + MetodoDePago.formatearCOP(MINIMO_EFECTY) + ".";
            return false;
        }
        System.out.println("   Generando recibo de pago en Efecty por "
                + MetodoDePago.formatearCOP(monto) + " (vence en 48 horas)");
        estado = "Efecty: recibo generado, pendiente de pago en punto físico.";
        return true;
    }

    /** Sobreescribe el default: aquí el mínimo NO es el del contrato. */
    @Override
    public boolean montoValido(double monto) {
        return monto >= MINIMO_EFECTY;
    }

    @Override
    public String obtenerEstado() {
        return estado;
    }

    @Override
    public String nombre() {
        return "Efecty";
    }
}
