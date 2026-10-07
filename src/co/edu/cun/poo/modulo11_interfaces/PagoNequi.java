package co.edu.cun.poo.modulo11_interfaces;

/** Otra implementación del MISMO contrato, con una lógica completamente distinta. */
public class PagoNequi implements MetodoDePago {

    private final String celular;
    private String estado = "Nequi: sin transacciones.";

    public PagoNequi(String celular) {
        this.celular = celular;
    }

    @Override
    public boolean procesarPago(double monto) {
        if (!montoValido(monto)) {
            estado = "Nequi: monto por debajo del mínimo.";
            return false;
        }
        // Aquí iría la API de Nequi (notificación push / QR).
        System.out.println("   Generando solicitud push a Nequi " + celular
                + " por " + MetodoDePago.formatearCOP(monto));
        estado = "Nequi: esperando confirmación del usuario.";
        return true;
    }

    @Override
    public String obtenerEstado() {
        return estado;
    }

    @Override
    public String nombre() {
        return "Nequi";
    }
}
