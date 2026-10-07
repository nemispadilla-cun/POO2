package co.edu.cun.poo.modulo11_interfaces;

/**
 * INTERFAZ FUNCIONAL: un solo método abstracto.
 *
 * Esa única restricción es la que permite escribirla como una expresión lambda:
 *     Notificador push = (destino, mensaje) -> System.out.println(...);
 *
 * La anotación no es obligatoria, pero hace que el compilador falle si alguien
 * agrega un segundo método abstracto y rompe la condición.
 */
@FunctionalInterface
public interface Notificador {

    void enviar(String destino, String mensaje);

    /**
     * Método default: combina dos notificadores en uno solo.
     * Agregar comportamiento a la interfaz sin romper a quien ya la implementa.
     */
    default Notificador y(Notificador otro) {
        return (destino, mensaje) -> {
            this.enviar(destino, mensaje);
            otro.enviar(destino, mensaje);
        };
    }

    /** Método static: implementación de "no hacer nada", útil en pruebas. */
    static Notificador silencioso() {
        return (destino, mensaje) -> { };
    }
}
