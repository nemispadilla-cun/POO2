package co.edu.cun.poo.modulo11_interfaces;

/**
 * CONTRATO del caso de estudio "EntregaYa S.A.S.".
 *
 * Cualquier medio de transporte que quiera entrar a la flota debe saber
 * responder dos preguntas. Nada más. Esto es el patrón STRATEGY: una familia
 * de algoritmos intercambiables detrás de una sola abstracción.
 */
public interface EstrategiaDeTransporte {

    /** Tiempo estimado de la entrega, en minutos. */
    double calcularTiempoEstimado(Ruta ruta);

    /** Costo del envío, en pesos colombianos. */
    double calcularCostoEnvio(Ruta ruta);

    /** Identificación legible del medio (para el tablero de despacho). */
    String medio();
}
