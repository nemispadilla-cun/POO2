package co.edu.cun.poo.modulo02_encapsulamiento;

/**
 * CONTRAEJEMPLO: así NO se debe hacer.
 * Los atributos públicos permiten que cualquiera deje el objeto en un estado
 * inválido (por ejemplo una nota de 12.0 en una escala de 0 a 5).
 */
public class CalificacionSinEncapsular {
    public String asignatura;
    public double valor;   // ¡Cualquiera puede escribir aquí lo que quiera!
}
