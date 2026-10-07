package co.edu.cun.poo.modulo02_encapsulamiento;

/**
 * Un RECORD (Java 16+) es la forma moderna de declarar una clase INMUTABLE
 * que solo transporta datos. El compilador genera automáticamente:
 *   - los atributos private final,
 *   - el constructor,
 *   - los métodos de acceso tipo() y numero() (sin prefijo get),
 *   - equals(), hashCode() y toString().
 *
 * Inmutable = una vez creado, su estado no cambia. Es la forma más fuerte
 * de encapsulamiento: no hay setters que puedan romper nada.
 */
public record Documento(String tipo, String numero) {

    // Constructor compacto: permite validar antes de asignar.
    public Documento {
        if (!tipo.equals("CC") && !tipo.equals("TI") && !tipo.equals("CE")) {
            throw new IllegalArgumentException("Tipo de documento no válido: " + tipo);
        }
        if (numero == null || !numero.matches("\\d{6,10}")) {
            throw new IllegalArgumentException("Número de documento no válido: " + numero);
        }
    }

    /** Los records también pueden tener métodos propios. */
    public String formateado() {
        return tipo + " " + numero;
    }
}
