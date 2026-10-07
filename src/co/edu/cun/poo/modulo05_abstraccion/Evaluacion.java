package co.edu.cun.poo.modulo05_abstraccion;

/**
 * CLASE ABSTRACTA: define QUÉ hace una evaluación (contrato + código común)
 * pero deja a las subclases decidir CÓMO se calcula la nota.
 *
 * No se puede hacer new Evaluacion(...): representa un concepto general.
 */
public abstract class Evaluacion {

    private final String nombre;
    private final double porcentaje;   // peso dentro de la nota final (0..1)

    protected Evaluacion(String nombre, double porcentaje) {
        if (porcentaje <= 0 || porcentaje > 1) {
            throw new IllegalArgumentException("El porcentaje debe estar entre 0 y 1.");
        }
        this.nombre = nombre;
        this.porcentaje = porcentaje;
    }

    public String getNombre() {
        return nombre;
    }

    public double getPorcentaje() {
        return porcentaje;
    }

    /**
     * MÉTODO ABSTRACTO: sin cuerpo. Toda subclase concreta ESTÁ OBLIGADA a
     * implementarlo. Es la parte que varía.
     */
    public abstract double calcularNota();

    /**
     * MÉTODO CONCRETO que usa el abstracto (patrón "template method"):
     * el algoritmo general está aquí; el detalle lo aporta la subclase.
     */
    public double aporteANotaFinal() {
        return calcularNota() * porcentaje;
    }

    @Override
    public String toString() {
        return String.format("%-22s nota %.2f  (%.0f%% -> aporta %.2f)",
                nombre, calcularNota(), porcentaje * 100, aporteANotaFinal());
    }
}
