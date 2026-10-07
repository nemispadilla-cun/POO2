package co.edu.cun.poo.modulo09_integrador;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import co.edu.cun.poo.modulo03_herencia.Estudiante;
import co.edu.cun.poo.modulo05_abstraccion.Evaluacion;
import co.edu.cun.poo.modulo06_composicion.Curso;

/**
 * Une un Estudiante con un Curso y acumula sus evaluaciones.
 * Encapsulamiento (módulo 2) + composición (módulo 6) + abstracción (módulo 5).
 */
public class Matricula {

    private final Estudiante estudiante;
    private final Curso curso;
    private final double valorPagado;
    private final List<Evaluacion> evaluaciones = new ArrayList<>();

    public Matricula(Estudiante estudiante, Curso curso, double valorPagado) {
        this.estudiante = estudiante;
        this.curso = curso;
        this.valorPagado = valorPagado;
    }

    public void registrar(Evaluacion evaluacion) {
        double acumulado = porcentajeAcumulado() + evaluacion.getPorcentaje();
        if (acumulado > 1.0001) {
            throw new IllegalArgumentException(
                    "Las evaluaciones superarían el 100 % (" + Math.round(acumulado * 100) + " %).");
        }
        evaluaciones.add(evaluacion);
    }

    private double porcentajeAcumulado() {
        double total = 0;
        for (Evaluacion e : evaluaciones) total += e.getPorcentaje();
        return total;
    }

    /** Polimorfismo: cada Evaluacion sabe calcular su propio aporte. */
    public double notaDefinitiva() {
        double nota = 0;
        for (Evaluacion e : evaluaciones) nota += e.aporteANotaFinal();
        return Math.round(nota * 100) / 100.0;
    }

    public boolean aprobo() {
        return porcentajeAcumulado() >= 0.9999 && notaDefinitiva() >= 3.0;
    }

    public Estudiante getEstudiante() {
        return estudiante;
    }

    public Curso getCurso() {
        return curso;
    }

    public double getValorPagado() {
        return valorPagado;
    }

    public List<Evaluacion> getEvaluaciones() {
        return Collections.unmodifiableList(evaluaciones);
    }
}
