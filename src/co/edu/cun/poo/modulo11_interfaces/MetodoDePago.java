package co.edu.cun.poo.modulo11_interfaces;

/**
 * CONTRATO (interfaz). Dice QUÉ debe poder hacer cualquier método de pago,
 * nunca CÓMO lo hace. Es el "estándar USB" del checkout: al carrito de compras
 * no le importa si por dentro hay PSE, Nequi o Efecty.
 *
 * Una interfaz no tiene estado: no se puede declarar un atributo de instancia.
 */
public interface MetodoDePago {

    /** En una interfaz todo campo es implícitamente public static final. */
    double MONTO_MINIMO = 1_000;

    /** Cada implementación conecta con su propia pasarela. */
    boolean procesarPago(double monto);

    /** Estado legible de la última transacción. */
    String obtenerEstado();

    /** Nombre comercial que el cliente ve en el checkout. */
    String nombre();

    /**
     * Método DEFAULT (Java 8+): regla común a todas las implementaciones.
     * Se hereda sin escribirla y se puede sobreescribir si hace falta.
     */
    default boolean montoValido(double monto) {
        return monto >= MONTO_MINIMO;
    }

    /** Método STATIC: utilidad que pertenece al contrato, no a una implementación. */
    static String formatearCOP(double monto) {
        return String.format("$%,.0f COP", monto);
    }
}
