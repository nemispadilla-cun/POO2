package co.edu.cun.poo.modulo05_abstraccion;

/** Un quiz se califica por preguntas correctas sobre el total. */
public class Quiz extends Evaluacion {

    private final int correctas;
    private final int total;

    public Quiz(String nombre, double porcentaje, int correctas, int total) {
        super(nombre, porcentaje);
        this.correctas = correctas;
        this.total = total;
    }

    @Override
    public double calcularNota() {
        return 5.0 * correctas / total;
    }
}
