package co.edu.cun.poo.modulo09_integrador;

import java.time.DayOfWeek;
import java.time.LocalTime;
import java.util.List;
import java.util.Set;

import co.edu.cun.poo.Modulo;
import co.edu.cun.poo.modulo03_herencia.Docente;
import co.edu.cun.poo.modulo03_herencia.Estudiante;
import co.edu.cun.poo.modulo05_abstraccion.Parcial;
import co.edu.cun.poo.modulo05_abstraccion.Proyecto;
import co.edu.cun.poo.modulo05_abstraccion.Quiz;
import co.edu.cun.poo.modulo06_composicion.Curso;
import co.edu.cun.poo.modulo06_composicion.Universidad;
import co.edu.cun.poo.modulo08_excepciones.MatriculaException;

/**
 * CASO INTEGRADOR: un mini sistema de matrículas que usa TODO lo anterior.
 *
 *  Módulo 1-2  Clases, objetos y encapsulamiento  -> Matricula, SistemaMatriculas
 *  Módulo 3-4  Herencia y polimorfismo            -> Persona/Estudiante/Docente, Evaluacion
 *  Módulo 5    Abstracción                        -> Evaluacion (abstract), PoliticaDescuento (interfaz)
 *  Módulo 6    Composición                        -> Universidad/Curso/Horario, Matricula
 *  Módulo 7    Colecciones y genéricos            -> Repositorio<Estudiante>, List, Set, streams
 *  Módulo 8    Excepciones                        -> MatriculaException y sus hijas
 */
public class Demo09Integrador implements Modulo {

    @Override
    public String titulo() {
        return "Caso integrador: sistema de matrículas";
    }

    @Override
    public String descripcion() {
        return "Todos los conceptos trabajando juntos en un escenario realista.";
    }

    @Override
    public void ejecutar() {
        seccion("1. Montar la universidad (composición)");
        Universidad cun = new Universidad("CUN");
        Docente carlos = new Docente("Carlos Mora", "79111222", "Programación", 4_500_000);

        Curso poo2 = cun.abrirCurso("POO2", "Programación Orientada a Objetos II");
        poo2.asignarDocente(carlos);
        poo2.agregarHorario(DayOfWeek.MONDAY, LocalTime.of(18, 0), LocalTime.of(20, 0));

        Curso bd1 = cun.abrirCurso("BD1", "Bases de Datos I");
        bd1.asignarDocente(carlos);
        bd1.agregarHorario(DayOfWeek.TUESDAY, LocalTime.of(18, 0), LocalTime.of(20, 0));
        cun.imprimirOferta();

        seccion("2. Registrar estudiantes (repositorio genérico)");
        SistemaMatriculas sistema = new SistemaMatriculas();
        List<Estudiante> nuevos = List.of(
                new Estudiante("Ana Torres", "1023456789", "Ing. de Sistemas", 6),
                new Estudiante("Luis Pérez", "1034567890", "Ing. de Sistemas", 1),
                new Estudiante("Carla Ruiz", "1045678901", "Ing. Industrial", 3),
                new Estudiante("Diego Sanz", "1056789012", "Ing. de Sistemas", 3),
                new Estudiante("Elena Vega", "1067890123", "Ing. de Sistemas", 4)
        );
        nuevos.forEach(sistema::registrarEstudiante);
        System.out.println("Registrados: " + nuevos.size());

        seccion("3. Matricular con política de descuento intercambiable (strategy) y excepciones");
        sistema.usarPolitica(PoliticaDescuento.POR_ANTIGUEDAD.y(PoliticaDescuento.PRIMER_SEMESTRE));

        matricular(sistema, "1023456789", poo2, 4, "POO1", Set.of("POO1"));   // Ana: 10 % antigüedad
        matricular(sistema, "1034567890", poo2, 4, "POO1", Set.of());         // Luis: sin prerrequisito
        matricular(sistema, "1034567890", bd1, 3, null, Set.of());           // Luis: 15 % primer semestre
        matricular(sistema, "1045678901", poo2, 4, "POO1", Set.of("POO1"));   // Carla: sin descuento
        matricular(sistema, "1056789012", poo2, 4, "POO1", Set.of("POO1"));   // Diego: cupo 3/3
        matricular(sistema, "1067890123", poo2, 4, "POO1", Set.of("POO1"));   // Elena: cupo agotado
        matricular(sistema, "9999999999", bd1, 3, null, Set.of());           // documento inexistente
        System.out.printf("Total recaudado: $%,.0f%n", sistema.totalRecaudado());

        seccion("4. Registrar evaluaciones (clase abstracta + polimorfismo)");
        Matricula anaPoo2 = sistema.matriculasDe("1023456789").get(0);
        anaPoo2.registrar(new Parcial("Parcial 1", 0.30, 3.6));
        anaPoo2.registrar(new Quiz("Quiz interfaces", 0.20, 9, 10));
        Proyecto proyecto = new Proyecto("Proyecto final", 0.50, List.of(4.8, 4.5, 4.9));
        proyecto.marcarEntregado();
        anaPoo2.registrar(proyecto);
        try {
            anaPoo2.registrar(new Parcial("Parcial extra", 0.10, 5.0));
        } catch (IllegalArgumentException e) {
            System.out.println("Rechazado -> " + e.getMessage());
        }
        anaPoo2.getEvaluaciones().forEach(ev -> System.out.println("  " + ev));
        System.out.printf("Definitiva de %s en %s: %.2f -> %s%n",
                anaPoo2.getEstudiante().getNombre(), anaPoo2.getCurso().getCodigo(),
                anaPoo2.notaDefinitiva(), anaPoo2.aprobo() ? "APROBÓ" : "REPROBÓ");
        System.out.println("  Retroalimentación del proyecto: " + proyecto.retroalimentacion());

        seccion("5. Reporte final (streams sobre la colección de matrículas)");
        sistema.getMatriculas().forEach(m -> System.out.printf("  %-12s %-5s $%,10.0f%n",
                m.getEstudiante().getNombre(), m.getCurso().getCodigo(), m.getValorPagado()));
        nota("Cada clase tiene UNA responsabilidad clara y colabora con las demás a través de métodos públicos.");
    }

    private void matricular(SistemaMatriculas sistema, String documento, Curso curso, int creditos,
                            String prerrequisito, Set<String> aprobados) {
        try {
            Matricula m = sistema.matricular(documento, curso, creditos, prerrequisito, aprobados);
            System.out.printf("OK    %-12s en %-5s paga $%,10.0f%n",
                    m.getEstudiante().getNombre(), curso.getCodigo(), m.getValorPagado());
        } catch (MatriculaException e) {
            System.out.println("FALLO " + e.getMessage());
        } catch (IllegalArgumentException e) {
            System.out.println("ERROR " + e.getMessage());
        }
    }
}
