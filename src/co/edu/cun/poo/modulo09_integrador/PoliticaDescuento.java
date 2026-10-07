package co.edu.cun.poo.modulo09_integrador;

import co.edu.cun.poo.modulo03_herencia.Estudiante;

/**
 * PATRÓN STRATEGY con una interfaz funcional: la regla de descuento es un
 * objeto intercambiable. Se puede implementar con una clase o con una lambda.
 */
@FunctionalInterface
public interface PoliticaDescuento {

    /** Devuelve el porcentaje de descuento (0.0 a 1.0) para el estudiante. */
    double porcentaje(Estudiante estudiante);

    // ---------- Implementaciones listas para usar ----------
    PoliticaDescuento SIN_DESCUENTO = e -> 0.0;

    PoliticaDescuento POR_ANTIGUEDAD = e -> e.getSemestre() >= 6 ? 0.10 : 0.0;

    PoliticaDescuento PRIMER_SEMESTRE = e -> e.getSemestre() == 1 ? 0.15 : 0.0;

    /** Combina dos políticas sumando descuentos (máximo 50 %). */
    default PoliticaDescuento y(PoliticaDescuento otra) {
        return e -> Math.min(0.50, this.porcentaje(e) + otra.porcentaje(e));
    }
}
