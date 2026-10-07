package co.edu.cun.poo.modulo05_abstraccion;

import java.util.List;

import co.edu.cun.poo.Modulo;

public class Demo05Abstraccion implements Modulo {

    @Override
    public String titulo() {
        return "Abstracción: clases abstractas e interfaces";
    }

    @Override
    public String descripcion() {
        return "abstract, métodos abstractos, template method, interfaces, default, sealed.";
    }

    @Override
    public void ejecutar() {
        seccion("1. Clase abstracta: el esqueleto común, el detalle en cada subclase");
        // Evaluacion e = new Evaluacion("x", 0.5);   // NO compila: es abstracta
        List<Evaluacion> evaluaciones = List.of(
                new Parcial("Parcial 1", 0.30, 3.8),
                new Quiz("Quiz herencia", 0.20, 8, 10),
                new Proyecto("Proyecto final", 0.50, List.of(4.5, 4.0, 5.0))
        );
        double notaFinal = 0;
        for (Evaluacion e : evaluaciones) {
            System.out.println(e);              // toString usa calcularNota() polimórfico
            notaFinal += e.aporteANotaFinal();  // método concreto que llama al abstracto
        }
        System.out.printf("NOTA FINAL: %.2f%n", notaFinal);
        nota("aporteANotaFinal() está escrito UNA vez en Evaluacion, pero calcularNota() varía.");

        seccion("2. Interfaces: contratos independientes de la jerarquía");
        Proyecto proyecto = new Proyecto("Proyecto final", 0.50, List.of(4.5, 4.0, 5.0));
        System.out.println("Estado inicial: " + proyecto.estadoEntrega());   // método default
        proyecto.marcarEntregado();
        System.out.println("Después:        " + proyecto.estadoEntrega());
        System.out.println("Retro:          " + proyecto.retroalimentacion());

        // Podemos referirnos al proyecto SOLO por lo que nos interesa de él:
        Entregable soloEntrega = proyecto;
        Retroalimentable soloRetro = proyecto;
        System.out.println("Como Entregable:       " + soloEntrega.estaEntregado());
        System.out.println("Como Retroalimentable: " + soloRetro.retroalimentacion());
        nota("Una clase hereda de UNA clase pero puede implementar MUCHAS interfaces.");

        seccion("3. ¿Clase abstracta o interfaz?");
        nota("Clase abstracta: hay código y estado que compartir (es-un). Ej.: Evaluacion.");
        nota("Interfaz: solo un rol o capacidad, sin estado (puede-hacer). Ej.: Entregable.");

        seccion("4. Interfaz sellada: un conjunto cerrado de casos");
        List<EstadoMatricula> estados = List.of(
                new EstadoMatricula.Activa(16),
                new EstadoMatricula.Cancelada("Retiro voluntario"),
                new EstadoMatricula.Finalizada(4.1));
        for (EstadoMatricula estado : estados) {
            System.out.println("- " + estado.describir());
        }
        nota("sealed + permits: el compilador sabe que NO existen más estados que estos tres.");
    }
}
