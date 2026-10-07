package co.edu.cun.poo.modulo03_herencia;

import java.util.Objects;

/**
 * SUPERCLASE (clase padre o base). Reúne lo que tienen en común todas las
 * personas de la universidad. Las subclases HEREDAN estos atributos y métodos
 * y agregan lo suyo.
 */
public class Persona {

    // protected: las subclases pueden acceder directamente.
    // (Alternativa más estricta: private + getters; ambas se ven en clase.)
    protected final String nombre;
    protected final String documento;

    public Persona(String nombre, String documento) {
        this.nombre = nombre;
        this.documento = documento;
    }

    public String getNombre() {
        return nombre;
    }

    public String getDocumento() {
        return documento;
    }

    /** Comportamiento que las subclases podrán SOBREESCRIBIR. */
    public String presentarse() {
        return "Soy " + nombre + ", documento " + documento + ".";
    }

    /** Comportamiento común que las subclases heredan tal cual. */
    public String getCorreoInstitucional() {
        String usuario = nombre.toLowerCase().replace(" ", ".");
        return usuario + "@cun.edu.co";
    }

    // ---------- equals / hashCode / toString ----------
    // Dos personas son iguales si tienen el mismo documento, sin importar
    // si son la misma referencia en memoria.
    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (obj == null || getClass() != obj.getClass()) return false;
        Persona otra = (Persona) obj;
        return documento.equals(otra.documento);
    }

    // Regla de oro: si redefines equals(), redefine hashCode() con los MISMOS campos.
    @Override
    public int hashCode() {
        return Objects.hash(documento);
    }

    @Override
    public String toString() {
        return getClass().getSimpleName() + "{" + nombre + ", " + documento + "}";
    }
}
