package co.edu.cun.poo.modulo11_interfaces;

/** Menos vida, más energía. Solo implementa lo que cambia. */
public class Mago extends Personaje {

    public Mago(String nombre) {
        super(nombre, 70, 150);
    }

    @Override
    public void usarHabilidadEspecial() {
        System.out.println("   ¡" + nombre + " lanza una bola de fuego!");
    }

    @Override
    public int costoEnergia() {
        return 50;
    }
}
