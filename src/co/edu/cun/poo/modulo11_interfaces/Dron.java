package co.edu.cun.poo.modulo11_interfaces;

/**
 * Herencia simple + implementación múltiple:
 *   extends Vehiculo         -> reutiliza placa, fórmulas, arrancar / detenerse
 *   implements RastreableGPS -> adquiere un segundo contrato de comportamiento
 */
public class Dron extends Vehiculo implements RastreableGPS {

    private final double latitud = 4.6097;      // Bogotá
    private final double longitud = -74.0817;

    public Dron(String placa) {
        super(placa, 3);
    }

    @Override
    public double calcularSOAT() {
        return 0;   // no aplica: requiere registro ante la Aerocivil
    }

    @Override
    protected double velocidadPromedioKmh() {
        return 60.0;
    }

    @Override
    protected double costoPorKm() {
        return 900.0;
    }

    // ---------- Contrato RastreableGPS ----------

    @Override
    public String obtenerCoordenadas() {
        return String.format("%.4f, %.4f", latitud, longitud);
    }

    @Override
    public void enviarAlertaPosicion() {
        System.out.println("   Alerta de posición del dron " + placa
                + " en " + obtenerCoordenadas());
    }
}
