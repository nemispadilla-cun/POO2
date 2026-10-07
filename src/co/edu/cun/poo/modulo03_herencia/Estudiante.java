package co.edu.cun.poo.modulo03_herencia;

/**
 * SUBCLASE. Estudiante ES-UNA Persona (relación is-a).
 * Hereda nombre, documento, getCorreoInstitucional(), equals(), etc.
 */
public class Estudiante extends Persona {

    private final String programa;
    private int semestre;

    public Estudiante(String nombre, String documento, String programa, int semestre) {
        super(nombre, documento);   // PRIMERO se construye la parte Persona
        this.programa = programa;
        this.semestre = semestre;
    }

    public String getPrograma() {
        return programa;
    }

    public int getSemestre() {
        return semestre;
    }

    public void avanzarSemestre() {
        semestre++;
    }

    /** SOBREESCRITURA: misma firma que en Persona, comportamiento especializado. */
    @Override
    public String presentarse() {
        // super.metodo() invoca la versión del padre y la extiende.
        return super.presentarse() + " Estudio " + programa + ", semestre " + semestre + ".";
    }
}
