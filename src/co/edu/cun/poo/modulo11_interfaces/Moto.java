package co.edu.cun.poo.modulo11_interfaces;

/** La flota original de EntregaYa. */
public class Moto extends Vehiculo {

    public Moto(String placa) {
        super(placa, 15);
    }

    @Override
    public double calcularSOAT() {
        return 320_000;
    }

    @Override
    protected double velocidadPromedioKmh() {
        return 40.0;
    }

    @Override
    protected double costoPorKm() {
        return 300.0;
    }
}
