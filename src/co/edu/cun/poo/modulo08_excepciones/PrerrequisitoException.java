package co.edu.cun.poo.modulo08_excepciones;

public class PrerrequisitoException extends MatriculaException {

    public PrerrequisitoException(String codigoCurso, String prerrequisito) {
        super("Para matricular " + codigoCurso + " debe haber aprobado " + prerrequisito + ".", codigoCurso);
    }
}
