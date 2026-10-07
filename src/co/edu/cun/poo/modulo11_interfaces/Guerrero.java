package co.edu.cun.poo.modulo11_interfaces;

/** Mucha vida, poca energía. Hereda moverse(), recibirDanio() y jugarTurno(). */
public class Guerrero extends Personaje {

    public Guerrero(String nombre) {
        super(nombre, 150, 60);
    }

    @Override
    public void usarHabilidadEspecial() {
        System.out.println("   ¡" + nombre + " ejecuta un golpe devastador con el hacha!");
    }

    @Override
    public int costoEnergia() {
        return 30;
    }
}
