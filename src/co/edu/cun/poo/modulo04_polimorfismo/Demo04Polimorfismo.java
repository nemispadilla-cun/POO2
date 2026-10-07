package co.edu.cun.poo.modulo04_polimorfismo;

import java.util.List;

import co.edu.cun.poo.Modulo;
import co.edu.cun.poo.modulo03_herencia.Docente;
import co.edu.cun.poo.modulo03_herencia.DocenteInvestigador;
import co.edu.cun.poo.modulo03_herencia.Estudiante;
import co.edu.cun.poo.modulo03_herencia.Persona;

/**
 * POLIMORFISMO = "muchas formas". Un mismo mensaje (presentarse()) produce
 * comportamientos distintos según el objeto real que lo recibe.
 *
 * Reutilizamos las clases del módulo 3 (Persona, Estudiante, Docente...).
 */
public class Demo04Polimorfismo implements Modulo {

    @Override
    public String titulo() {
        return "Polimorfismo";
    }

    @Override
    public String descripcion() {
        return "Sobreescritura vs sobrecarga, enlace dinámico, upcasting/downcasting, instanceof.";
    }

    @Override
    public void ejecutar() {
        seccion("1. Una lista de Persona con objetos de distintas clases");
        // UPCASTING implícito: cada objeto se guarda como su tipo padre.
        List<Persona> comunidad = List.of(
                new Estudiante("Ana Torres", "1023456789", "Ingeniería de Sistemas", 3),
                new Docente("Carlos Mora", "79111222", "Bases de datos", 4_500_000),
                new DocenteInvestigador("Marta Gil", "41999888", "IA", 5_000_000, "GIDATI"),
                new Persona("Rosa Díaz", "52123456")
        );

        for (Persona p : comunidad) {
            // El tipo DECLARADO es Persona, pero Java ejecuta el presentarse()
            // del tipo REAL del objeto. Esto es ENLACE DINÁMICO (late binding).
            System.out.println("[" + p.getClass().getSimpleName() + "] " + p.presentarse());
        }
        nota("El bucle no cambia si mañana agregamos Administrativo extends Persona. Eso es extensibilidad.");

        seccion("2. Sobreescritura (dinámica) vs sobrecarga (estática)");
        LiquidadorNomina liquidador = new LiquidadorNomina();
        Docente d = new Docente("Carlos Mora", "79111222", "Bases de datos", 4_500_000);
        System.out.printf("calcularPago(d)            = $%,.0f%n", liquidador.calcularPago(d));
        System.out.printf("calcularPago(d, 10)        = $%,.0f%n", liquidador.calcularPago(d, 10));
        System.out.printf("calcularPago(d, 10, 0.05)  = $%,.0f%n", liquidador.calcularPago(d, 10, 0.05));
        nota("Sobrecarga: mismo nombre, distintos parámetros, se decide al COMPILAR.");
        nota("Sobreescritura: misma firma en la subclase, se decide al EJECUTAR.");

        seccion("3. El tipo de la variable limita lo que se puede llamar");
        Persona referencia = new DocenteInvestigador("Marta Gil", "41999888", "IA", 5_000_000, "GIDATI");
        System.out.println(referencia.presentarse());        // OK: está en Persona
        // referencia.calcularSalario();                      // NO compila: Persona no lo tiene
        nota("Aunque el objeto real es DocenteInvestigador, la variable es de tipo Persona.");

        seccion("4. Downcasting seguro con instanceof (pattern matching)");
        for (Persona p : comunidad) {
            if (p instanceof Docente doc) {               // declara y castea en un paso
                System.out.printf("%-12s cobra $%,.0f%n", doc.getNombre(), doc.calcularSalario());
            } else if (p instanceof Estudiante est) {
                System.out.printf("%-12s cursa semestre %d%n", est.getNombre(), est.getSemestre());
            } else {
                System.out.printf("%-12s es solo Persona%n", p.getNombre());
            }
        }
        nota("DocenteInvestigador también entra en instanceof Docente porque ES-UN Docente.");

        seccion("5. Cast incorrecto -> ClassCastException");
        try {
            Persona rosa = comunidad.get(3);
            Estudiante falso = (Estudiante) rosa;   // el compilador lo permite...
            System.out.println(falso);
        } catch (ClassCastException e) {
            System.out.println("...pero en ejecución falla: " + e.getMessage());
        }
        nota("Por eso se verifica con instanceof antes de hacer un cast hacia abajo.");
    }
}
