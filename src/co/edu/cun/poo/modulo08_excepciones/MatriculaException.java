package co.edu.cun.poo.modulo08_excepciones;

/**
 * EXCEPCIÓN VERIFICADA (checked): extiende Exception.
 * El compilador OBLIGA a quien llame a un método que la lance a hacer
 * try/catch o a declararla con throws. Se usa para situaciones esperables
 * del negocio que el llamador puede y debe manejar.
 *
 * Es la RAÍZ de una pequeña jerarquía: permite capturar todas las fallas
 * de matrícula con un solo catch, o cada una por separado.
 */
public class MatriculaException extends Exception {

    private final String codigoCurso;

    public MatriculaException(String mensaje, String codigoCurso) {
        super(mensaje);
        this.codigoCurso = codigoCurso;
    }

    /** Las excepciones son objetos: pueden llevar datos útiles para quien las atiende. */
    public String getCodigoCurso() {
        return codigoCurso;
    }
}
