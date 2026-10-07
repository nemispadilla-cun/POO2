package co.edu.cun.poo.modulo11_interfaces;

/**
 * MOLDE SEMI-CONSTRUIDO (clase abstracta).
 *
 * Reúne el estado y el comportamiento que comparten todos los personajes de un
 * videojuego, y deja sin construir lo que cambia en cada uno. Al tener partes
 * sin construir NO se puede instanciar: "new Personaje(...)" no compila.
 *
 * Diferencia clave con una interfaz: aquí SÍ hay atributos de estado
 * (puntosDeVida, nivelDeEnergia) y constructor.
 */
public abstract class Personaje {

    protected final String nombre;
    protected int puntosDeVida;
    protected int nivelDeEnergia;

    /** Constructor protegido: solo lo usan las subclases con super(...). */
    protected Personaje(String nombre, int vidaInicial, int energiaInicial) {
        this.nombre = nombre;
        this.puntosDeVida = vidaInicial;
        this.nivelDeEnergia = energiaInicial;
    }

    // ---------- Métodos CONCRETOS: iguales para todos, se escriben una vez ----------

    public void moverse(String direccion) {
        System.out.println("   " + nombre + " se mueve hacia " + direccion + ".");
    }

    public void recibirDanio(int cantidad) {
        puntosDeVida -= cantidad;
        if (puntosDeVida <= 0) {
            puntosDeVida = 0;
            System.out.println("   " + nombre + " ha sido derrotado.");
        } else {
            System.out.println("   " + nombre + " recibe " + cantidad
                    + " de daño (quedan " + puntosDeVida + " PV).");
        }
    }

    public boolean estaVivo() {
        return puntosDeVida > 0;
    }

    // ---------- Métodos ABSTRACTOS: cada subclase DEBE implementarlos ----------

    /** El ataque especial es distinto en cada clase de personaje. */
    public abstract void usarHabilidadEspecial();

    /** Cuánta energía cuesta esa habilidad. */
    public abstract int costoEnergia();

    // ---------- Template method: el esqueleto del turno es común ----------

    /**
     * "final" para que ninguna subclase altere el orden del turno.
     * El esqueleto está aquí; el paso variable se delega hacia abajo.
     */
    public final void jugarTurno(String direccion) {
        moverse(direccion);
        if (nivelDeEnergia >= costoEnergia()) {
            usarHabilidadEspecial();            // enlace dinámico
            nivelDeEnergia -= costoEnergia();
        } else {
            System.out.println("   " + nombre + " no tiene energía suficiente ("
                    + nivelDeEnergia + "/" + costoEnergia() + ") y descansa.");
            nivelDeEnergia += 20;
        }
    }

    @Override
    public String toString() {
        return String.format("%-9s PV:%-4d Energía:%-4d", nombre, puntosDeVida, nivelDeEnergia);
    }
}
