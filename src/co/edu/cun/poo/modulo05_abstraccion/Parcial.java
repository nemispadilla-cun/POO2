package co.edu.cun.poo.modulo05_abstraccion;

/** Un parcial tiene una nota directa. */
public class Parcial extends Evaluacion {

    private final double nota;

    public Parcial(String nombre, double porcentaje, double nota) {
        super(nombre, porcentaje);
        this.nota = nota;
    }

    @Override
    public double calcularNota() {
        return nota;
    }
}
