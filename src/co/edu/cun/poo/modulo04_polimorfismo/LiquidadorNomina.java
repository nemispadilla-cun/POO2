package co.edu.cun.poo.modulo04_polimorfismo;

import co.edu.cun.poo.modulo03_herencia.Docente;

/**
 * SOBRECARGA (overloading): varios métodos con el MISMO nombre pero DISTINTA
 * lista de parámetros. El compilador decide cuál usar según los argumentos.
 * Se resuelve en TIEMPO DE COMPILACIÓN (polimorfismo estático).
 */
public class LiquidadorNomina {

    private static final double VALOR_HORA_EXTRA = 45_000;

    /** Pago básico: solo el salario del docente. */
    public double calcularPago(Docente docente) {
        return docente.calcularSalario();
    }

    /** Pago con horas extra. */
    public double calcularPago(Docente docente, int horasExtra) {
        return calcularPago(docente) + horasExtra * VALOR_HORA_EXTRA;
    }

    /** Pago con horas extra y un porcentaje de bonificación. */
    public double calcularPago(Docente docente, int horasExtra, double porcentajeBono) {
        return calcularPago(docente, horasExtra) * (1 + porcentajeBono);
    }
}
