package co.edu.cun.poo.modulo05_abstraccion;

/**
 * INTERFAZ: un contrato puro. Dice QUÉ se puede hacer, nunca CÓMO.
 * Quien la implementa se compromete a escribir todos sus métodos.
 */
public interface Entregable {

    void marcarEntregado();

    boolean estaEntregado();

    /**
     * MÉTODO DEFAULT (Java 8+): una implementación por defecto que las clases
     * heredan sin escribir nada, pero pueden sobreescribir si quieren.
     */
    default String estadoEntrega() {
        return estaEntregado() ? "ENTREGADO" : "PENDIENTE";
    }
}
