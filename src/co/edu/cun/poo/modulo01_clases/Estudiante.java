package co.edu.cun.poo.modulo01_clases;

/**
 * Una CLASE es un molde o plantilla. Describe qué DATOS (atributos) y qué
 * COMPORTAMIENTOS (métodos) tendrán los OBJETOS que se creen a partir de ella.
 *
 * Esta clase todavía NO aplica encapsulamiento a fondo (eso es el módulo 2);
 * aquí el foco es entender la anatomía básica de una clase.
 */
public class Estudiante {

    // ---------- ATRIBUTOS DE INSTANCIA ----------
    // Cada objeto Estudiante tiene SU PROPIA copia de estos valores.
    String nombre;
    String codigo;
    int semestre;

    // ---------- ATRIBUTO DE CLASE (static) ----------
    // Hay UNA sola copia compartida por todos los objetos. Sirve para contar
    // cuántos estudiantes se han creado.
    static int totalCreados = 0;

    // ---------- CONSTRUCTORES ----------
    // Se ejecutan al hacer "new". Su trabajo es dejar el objeto en un estado válido.

    /** Constructor completo. */
    public Estudiante(String nombre, String codigo, int semestre) {
        // "this" se refiere al objeto que se está construyendo.
        // Se usa para distinguir el atributo del parámetro con el mismo nombre.
        this.nombre = nombre;
        this.codigo = codigo;
        this.semestre = semestre;
        totalCreados++;
    }

    /** Constructor sobrecargado: si no se indica semestre, se asume primero. */
    public Estudiante(String nombre, String codigo) {
        this(nombre, codigo, 1);   // reutiliza el otro constructor
    }

    // ---------- MÉTODOS DE INSTANCIA ----------
    // Operan sobre los datos del objeto concreto que los invoca.

    public void presentarse() {
        System.out.println("Hola, soy " + nombre + " (código " + codigo
                + "), estoy en semestre " + semestre + ".");
    }

    public void avanzarSemestre() {
        semestre++;
    }

    public boolean esDeMismoSemestre(Estudiante otro) {
        return this.semestre == otro.semestre;
    }

    // ---------- MÉTODO DE CLASE (static) ----------
    // No necesita un objeto para ser llamado: Estudiante.contarCreados()
    public static int contarCreados() {
        return totalCreados;
    }

    // toString() lo hereda de Object; lo redefinimos para que imprimir un
    // estudiante muestre algo útil en vez de "Estudiante@1b6d3586".
    @Override
    public String toString() {
        return "Estudiante{nombre=" + nombre + ", codigo=" + codigo
                + ", semestre=" + semestre + "}";
    }
}
