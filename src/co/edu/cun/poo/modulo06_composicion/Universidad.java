package co.edu.cun.poo.modulo06_composicion;

import java.util.ArrayList;
import java.util.List;

import co.edu.cun.poo.modulo03_herencia.Estudiante;

/** Nivel superior de la jerarquía de composición: la universidad TIENE cursos. */
public class Universidad {

    private final String nombre;
    private final List<Curso> cursos = new ArrayList<>();

    public Universidad(String nombre) {
        this.nombre = nombre;
    }

    public Curso abrirCurso(String codigo, String nombreCurso) {
        Curso curso = new Curso(codigo, nombreCurso);   // la universidad crea y posee el curso
        cursos.add(curso);
        return curso;
    }

    /** Navega la composición: Universidad -> Curso -> Estudiante. */
    public List<Curso> cursosDe(Estudiante estudiante) {
        List<Curso> resultado = new ArrayList<>();
        for (Curso c : cursos) {
            if (c.getInscritos().contains(estudiante)) {
                resultado.add(c);
            }
        }
        return resultado;
    }

    public void imprimirOferta() {
        System.out.println("Oferta académica de " + nombre + ":");
        for (Curso c : cursos) {
            System.out.println("  · " + c);
        }
    }
}
