package co.edu.cun.poo.modulo03_herencia;

public class Docente extends Persona {

    private final String especialidad;
    private final double salarioBase;

    public Docente(String nombre, String documento, String especialidad, double salarioBase) {
        super(nombre, documento);
        this.especialidad = especialidad;
        this.salarioBase = salarioBase;
    }

    public String getEspecialidad() {
        return especialidad;
    }

    /** Método que las subclases de Docente podrán refinar. */
    public double calcularSalario() {
        return salarioBase;
    }

    @Override
    public String presentarse() {
        return super.presentarse() + " Soy docente de " + especialidad + ".";
    }
}
