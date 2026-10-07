package co.edu.cun.poo.modulo07_colecciones;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.TreeMap;
import java.util.stream.Collectors;

import co.edu.cun.poo.Modulo;
import co.edu.cun.poo.modulo03_herencia.Docente;
import co.edu.cun.poo.modulo03_herencia.Estudiante;

public class Demo07ColeccionesYGenericos implements Modulo {

    @Override
    public String titulo() {
        return "Colecciones y genéricos";
    }

    @Override
    public String descripcion() {
        return "List, Set, Map, clase y método genérico, tipos acotados, Comparator, Optional, streams.";
    }

    @Override
    public void ejecutar() {
        Estudiante ana = new Estudiante("Ana Torres", "1023456789", "Ing. de Sistemas", 3);
        Estudiante luis = new Estudiante("Luis Pérez", "1034567890", "Ing. de Sistemas", 5);
        Estudiante carla = new Estudiante("Carla Ruiz", "1045678901", "Ing. Industrial", 3);
        Estudiante anaOtraVez = new Estudiante("Ana T.", "1023456789", "Ing. de Sistemas", 4);

        seccion("1. List: ordenada, permite duplicados");
        List<Estudiante> lista = new ArrayList<>(List.of(ana, luis, carla, anaOtraVez));
        System.out.println("Tamaño de la lista: " + lista.size() + "  (Ana está dos veces)");

        seccion("2. Set: sin duplicados (usa equals/hashCode del módulo 3)");
        Set<Estudiante> conjunto = new HashSet<>(lista);
        System.out.println("Tamaño del set:     " + conjunto.size() + "  (el mismo documento cuenta una vez)");
        nota("HashSet llama a hashCode() y equals(): si no los redefines, todo objeto es distinto.");

        seccion("3. Map: clave -> valor");
        Map<String, List<Estudiante>> porPrograma = new TreeMap<>();   // TreeMap ordena las claves
        for (Estudiante e : conjunto) {
            porPrograma.computeIfAbsent(e.getPrograma(), k -> new ArrayList<>()).add(e);
        }
        porPrograma.forEach((programa, ests) ->
                System.out.println("  " + programa + " -> " + ests.size() + " estudiante(s)"));

        seccion("4. Clase genérica propia: Repositorio<T extends Persona>");
        Repositorio<Estudiante> repoEstudiantes = new Repositorio<>();
        repoEstudiantes.guardar(ana);
        repoEstudiantes.guardar(luis);
        repoEstudiantes.guardar(carla);
        // repoEstudiantes.guardar(new Docente(...));  // NO compila: el tipo está fijado
        Repositorio<Docente> repoDocentes = new Repositorio<>();
        repoDocentes.guardar(new Docente("Carlos Mora", "79111222", "BD", 4_500_000));
        System.out.println("Estudiantes guardados: " + repoEstudiantes.contar()
                + " | Docentes guardados: " + repoDocentes.contar());
        nota("El mismo código de Repositorio sirve para Estudiante y para Docente sin casts.");

        seccion("5. Optional: el resultado puede no existir");
        System.out.println("Buscar 1034567890 -> "
                + repoEstudiantes.buscarPorDocumento("1034567890").map(Estudiante::getNombre).orElse("no existe"));
        System.out.println("Buscar 0000000000 -> "
                + repoEstudiantes.buscarPorDocumento("0000000000").map(Estudiante::getNombre).orElse("no existe"));

        seccion("6. Comportamiento como parámetro (Predicate + lambda)");
        List<Estudiante> tercerSemestre = repoEstudiantes.filtrar(e -> e.getSemestre() == 3);
        System.out.println("Semestre 3: " + Repositorio.unir(
                tercerSemestre.stream().map(Estudiante::getNombre).collect(Collectors.toList()), ", "));

        seccion("7. Ordenar con Comparator");
        List<Estudiante> ordenados = repoEstudiantes.listar();
        ordenados.sort(Comparator.comparing(Estudiante::getSemestre).reversed()
                .thenComparing(Estudiante::getNombre));
        for (Estudiante e : ordenados) {
            System.out.println("  sem " + e.getSemestre() + " · " + e.getNombre());
        }

        seccion("8. Record genérico Par<A,B> y streams");
        Par<String, Double> promedio = Par.de("Promedio de semestre",
                repoEstudiantes.listar().stream().mapToInt(Estudiante::getSemestre).average().orElse(0));
        System.out.printf("%s = %.2f%n", promedio.primero(), promedio.segundo());
        nota("Los genéricos dan seguridad de tipos en compilación y evitan casts en ejecución.");
    }
}
