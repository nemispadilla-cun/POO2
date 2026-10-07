package co.edu.cun.poo.modulo11_interfaces;

/** Equilibrado. Tercera subclase sin duplicar ni una línea del molde. */
public class Arquero extends Personaje {

    public Arquero(String nombre) {
        super(nombre, 100, 90);
    }

    @Override
    public void usarHabilidadEspecial() {
        System.out.println("   ¡" + nombre + " dispara una lluvia de flechas!");
    }

    @Override
    public int costoEnergia() {
        return 40;
    }
}
