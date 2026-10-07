package co.edu.cun.poo.modulo11_interfaces;

/** Implementación concreta: "firma" el contrato MetodoDePago. */
public class PagoPSE implements MetodoDePago {

    private final String banco;
    private String estado = "PSE: sin transacciones.";

    public PagoPSE(String banco) {
        this.banco = banco;
    }

    @Override
    public boolean procesarPago(double monto) {
        if (!montoValido(monto)) {                  // regla heredada del default
            estado = "PSE: monto por debajo del mínimo.";
            return false;
        }
        // Aquí iría la comunicación real con ACH Colombia.
        System.out.println("   Redirigiendo al portal de " + banco
                + " para pago con PSE por " + MetodoDePago.formatearCOP(monto));
        estado = "PSE: transacción aprobada en " + banco + ".";
        return true;
    }

    @Override
    public String obtenerEstado() {
        return estado;
    }

    @Override
    public String nombre() {
        return "PSE (" + banco + ")";
    }
}
