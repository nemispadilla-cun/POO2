package co.edu.cun.poo;

/**
 * Contrato que cumple cada módulo del curso.
 *
 * Esta interfaz es, en sí misma, un ejemplo de POO: el menú principal (Main)
 * no sabe qué hace cada módulo por dentro; solo sabe que "tiene un título"
 * y que "se puede ejecutar". Eso es ABSTRACCIÓN y POLIMORFISMO en acción.
 */
public interface Modulo {

    /** Nombre corto que se muestra en el menú. */
    String titulo();

    /** Conceptos que el módulo ilustra (se muestra antes de ejecutar). */
    String descripcion();

    /** Ejecuta la demostración imprimiendo por consola. */
    void ejecutar();

    // ---------- Utilidades compartidas (métodos default) ----------

    /** Imprime un separador con título, para que la salida sea legible. */
    default void seccion(String texto) {
        System.out.println();
        System.out.println("── " + texto + " " + "─".repeat(Math.max(0, 60 - texto.length())));
    }

    /** Imprime una nota pedagógica destacada. */
    default void nota(String texto) {
        System.out.println("   💡 " + texto);
    }
}
