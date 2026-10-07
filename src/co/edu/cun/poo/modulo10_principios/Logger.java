package co.edu.cun.poo.modulo10_principios;

/**
 * REUTILIZACIÓN: una interfaz estándar para registrar mensajes.
 *
 * Cualquier aplicación (escritorio, web, móvil) que programe contra esta
 * interfaz puede usar cualquier Logger sin cambiar una línea. Y cualquier
 * Logger que la implemente sirve en todas ellas sin modificaciones.
 */
public interface Logger {

    void info(String mensaje);

    void error(String mensaje, Throwable causa);

    /** Método default: las implementaciones lo heredan sin escribirlo. */
    default void advertencia(String mensaje) {
        info("ADVERTENCIA: " + mensaje);
    }
}
