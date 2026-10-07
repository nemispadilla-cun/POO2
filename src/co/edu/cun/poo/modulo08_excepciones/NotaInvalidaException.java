package co.edu.cun.poo.modulo08_excepciones;

/**
 * EXCEPCIÓN NO VERIFICADA (unchecked): extiende RuntimeException.
 * El compilador NO obliga a capturarla. Se usa para errores de programación
 * (argumentos inválidos, estados imposibles) que no deberían ocurrir si el
 * código que llama está bien escrito.
 */
public class NotaInvalidaException extends RuntimeException {

    public NotaInvalidaException(double nota) {
        super("La nota " + nota + " está fuera del rango 0.0 - 5.0.");
    }
}
