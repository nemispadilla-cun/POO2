package co.edu.cun.poo.modulo08_excepciones;

public class CupoAgotadoException extends MatriculaException {

    private final int cupoMaximo;

    public CupoAgotadoException(String codigoCurso, int cupoMaximo) {
        super("El curso " + codigoCurso + " ya alcanzó su cupo máximo de " + cupoMaximo + ".", codigoCurso);
        this.cupoMaximo = cupoMaximo;
    }

    public int getCupoMaximo() {
        return cupoMaximo;
    }
}
