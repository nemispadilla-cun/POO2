package co.edu.cun.poo.modulo06_composicion;

import java.time.DayOfWeek;
import java.time.LocalTime;

import co.edu.cun.poo.Modulo;
import co.edu.cun.poo.modulo03_herencia.Docente;
import co.edu.cun.poo.modulo03_herencia.Estudiante;

public class Demo06Composicion implements Modulo {

    @Override
    public String titulo() {
        return "Composición y agregación";
    }

    @Override
    public String descripcion() {
        return "Relaciones tiene-un, composición vs agregación, delegación, colecciones de solo lectura.";
    }

    @Override
    public void ejecutar() {
        seccion("1. Construir el modelo: Universidad -> Curso -> {Docente, Estudiantes, Horarios}");
        Universidad cun = new Universidad("CUN");
        Docente carlos = new Docente("Carlos Mora", "79111222", "Programación", 4_500_000);
        Estudiante ana = new Estudiante("Ana Torres", "1023456789", "Ing. de Sistemas", 3);
        Estudiante luis = new Estudiante("Luis Pérez", "1034567890", "Ing. de Sistemas", 3);

        Curso poo2 = cun.abrirCurso("POO2", "Programación Orientada a Objetos II");
        poo2.asignarDocente(carlos);
        poo2.agregarHorario(DayOfWeek.MONDAY, LocalTime.of(18, 0), LocalTime.of(20, 0));
        poo2.agregarHorario(DayOfWeek.WEDNESDAY, LocalTime.of(18, 0), LocalTime.of(20, 0));
        poo2.inscribir(ana);
        poo2.inscribir(luis);
        poo2.inscribir(ana);   // duplicado: lo detecta con equals()

        Curso bd = cun.abrirCurso("BD1", "Bases de Datos I");
        bd.asignarDocente(carlos);          // el mismo docente en dos cursos: agregación
        bd.agregarHorario(DayOfWeek.MONDAY, LocalTime.of(19, 0), LocalTime.of(21, 0));
        bd.inscribir(ana);

        cun.imprimirOferta();

        seccion("2. Agregación: las partes viven por su cuenta");
        System.out.println("Carlos dicta en dos cursos y sigue siendo el mismo objeto: "
                + (poo2.getDocente() == bd.getDocente()));
        System.out.println("Cursos de Ana: " + cun.cursosDe(ana).size());
        nota("Si eliminamos el curso POO2, Ana y Carlos siguen existiendo.");

        seccion("3. Composición: las partes las crea y controla el todo");
        try {
            poo2.agregarHorario(DayOfWeek.MONDAY, LocalTime.of(19, 0), LocalTime.of(21, 0));
        } catch (IllegalArgumentException e) {
            System.out.println("El curso protege sus horarios -> " + e.getMessage());
        }
        try {
            poo2.getHorarios().clear();   // intento de modificar desde afuera
        } catch (UnsupportedOperationException e) {
            System.out.println("getHorarios() devuelve una vista de solo lectura: no se puede vaciar.");
        }
        nota("Nadie de afuera hace new Horario para el curso: el curso es dueño de sus horarios.");

        seccion("4. Delegación: el todo responde preguntando a sus partes");
        System.out.println("¿POO2 se cruza con BD1? " + poo2.seCruzaCon(bd));
        nota("Curso.seCruzaCon() no calcula horas: le pregunta a cada Horario. Cada clase hace lo suyo.");

        seccion("5. Composición vs herencia");
        nota("MAL: class Curso extends ArrayList<Estudiante>  -> un curso NO ES una lista.");
        nota("BIEN: class Curso { List<Estudiante> inscritos; } -> un curso TIENE estudiantes.");
        nota("Regla práctica: prefiere composición; usa herencia solo cuando el es-un sea evidente.");
    }
}
