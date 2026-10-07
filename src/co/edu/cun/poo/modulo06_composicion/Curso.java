package co.edu.cun.poo.modulo06_composicion;

import java.time.DayOfWeek;
import java.time.LocalTime;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import co.edu.cun.poo.modulo03_herencia.Docente;
import co.edu.cun.poo.modulo03_herencia.Estudiante;

/**
 * Un Curso NO ES-UNA lista de estudiantes ni ES-UN docente: TIENE un docente,
 * TIENE estudiantes y TIENE horarios. Relaciones "tiene-un" (has-a).
 *
 *  - Docente y Estudiantes: AGREGACIÓN. Existen antes y después del curso;
 *    el curso solo los referencia (rombo blanco en UML).
 *  - Horarios: COMPOSICIÓN. El curso los crea y son suyos; nadie de afuera
 *    recibe la lista real para modificarla (rombo negro en UML).
 */
public class Curso {

    private final String codigo;
    private final String nombre;
    private Docente docente;                                   // agregación (puede cambiar)
    private final List<Estudiante> inscritos = new ArrayList<>(); // agregación
    private final List<Horario> horarios = new ArrayList<>();     // composición

    public Curso(String codigo, String nombre) {
        this.codigo = codigo;
        this.nombre = nombre;
    }

    // ---------- Agregación: recibimos objetos que ya existen ----------
    public void asignarDocente(Docente docente) {
        this.docente = docente;
    }

    public void inscribir(Estudiante estudiante) {
        if (inscritos.contains(estudiante)) {     // usa equals() de Persona (documento)
            System.out.println("   (aviso) " + estudiante.getNombre() + " ya estaba inscrito.");
            return;
        }
        inscritos.add(estudiante);
    }

    // ---------- Composición: el curso CREA sus partes ----------
    public void agregarHorario(DayOfWeek dia, LocalTime inicio, LocalTime fin) {
        Horario nuevo = new Horario(dia, inicio, fin);
        for (Horario h : horarios) {
            if (h.seCruzaCon(nuevo)) {
                throw new IllegalArgumentException("El horario " + nuevo + " se cruza con " + h);
            }
        }
        horarios.add(nuevo);
    }

    // Devolvemos vistas de solo lectura: el que llama puede MIRAR, no MODIFICAR.
    public List<Estudiante> getInscritos() {
        return Collections.unmodifiableList(inscritos);
    }

    public List<Horario> getHorarios() {
        return Collections.unmodifiableList(horarios);
    }

    public Docente getDocente() {
        return docente;
    }

    public String getCodigo() {
        return codigo;
    }

    public String getNombre() {
        return nombre;
    }

    /** El curso DELEGA en sus partes para responder preguntas compuestas. */
    public boolean seCruzaCon(Curso otro) {
        for (Horario mio : horarios) {
            for (Horario suyo : otro.horarios) {
                if (mio.seCruzaCon(suyo)) return true;
            }
        }
        return false;
    }

    @Override
    public String toString() {
        String prof = docente == null ? "sin docente" : docente.getNombre();
        return codigo + " " + nombre + " [" + prof + "] " + horarios
                + " · " + inscritos.size() + " inscritos";
    }
}
