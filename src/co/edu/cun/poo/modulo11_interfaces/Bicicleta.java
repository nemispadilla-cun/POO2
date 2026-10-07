package co.edu.cun.poo.modulo11_interfaces;

/** Distancias cortas y cero emisiones: clase nueva, despacho intacto. */
public class Bicicleta extends Vehiculo {

    public Bicicleta(String placa) {
        super(placa, 8);
    }

    @Override
    public double calcularSOAT() {
        return 0;   // las bicicletas no pagan SOAT en Colombia
    }

    @Override
    protected double velocidadPromedioKmh() {
        return 15.0;
    }

    @Override
    protected double costoPorKm() {
        return 150.0;
    }
}
