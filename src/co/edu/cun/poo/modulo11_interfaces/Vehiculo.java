package co.edu.cun.poo.modulo11_interfaces;

/**
 * Las dos herramientas trabajando juntas:
 *   - clase ABSTRACTA  -> comparte estado (placa, capacidad) y las FÓRMULAS;
 *   - interfaz         -> publica el contrato que el despacho consume.
 *
 * Cada subclase solo aporta sus números; la fórmula se escribe una sola vez.
 */
public abstract class Vehiculo implements EstrategiaDeTransporte {

    protected final String placa;
    protected final double capacidadDeCargaKg;

    protected Vehiculo(String placa, double capacidadDeCargaKg) {
        this.placa = placa;
        this.capacidadDeCargaKg = capacidadDeCargaKg;
    }

    // ---------- Comportamiento concreto compartido ----------

    public void arrancar() {
        System.out.println("   El vehículo con placa " + placa + " ha arrancado.");
    }

    public void detenerse() {
        System.out.println("   El vehículo con placa " + placa + " se ha detenido.");
    }

    // ---------- Lo que CADA subclase debe aportar ----------

    public abstract double calcularSOAT();

    protected abstract double velocidadPromedioKmh();

    protected abstract double costoPorKm();

    // ---------- El contrato se cumple UNA vez, aquí ----------

    @Override
    public double calcularTiempoEstimado(Ruta ruta) {
        return (ruta.distanciaEnKm() / velocidadPromedioKmh()) * 60;   // minutos
    }

    @Override
    public double calcularCostoEnvio(Ruta ruta) {
        return ruta.distanciaEnKm() * costoPorKm();
    }

    @Override
    public String medio() {
        return getClass().getSimpleName() + " " + placa;
    }
}
