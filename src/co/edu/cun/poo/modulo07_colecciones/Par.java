package co.edu.cun.poo.modulo07_colecciones;

/**
 * Record genérico con DOS parámetros de tipo. Útil para devolver dos valores
 * relacionados desde un método sin crear una clase específica cada vez.
 */
public record Par<A, B>(A primero, B segundo) {

    public static <A, B> Par<A, B> de(A primero, B segundo) {
        return new Par<>(primero, segundo);
    }
}
