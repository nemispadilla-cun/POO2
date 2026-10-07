package co.edu.cun.poo;

import java.util.List;
import java.util.Scanner;

import co.edu.cun.poo.modulo01_clases.Demo01ClasesYObjetos;
import co.edu.cun.poo.modulo02_encapsulamiento.Demo02Encapsulamiento;
import co.edu.cun.poo.modulo03_herencia.Demo03Herencia;
import co.edu.cun.poo.modulo04_polimorfismo.Demo04Polimorfismo;
import co.edu.cun.poo.modulo05_abstraccion.Demo05Abstraccion;
import co.edu.cun.poo.modulo06_composicion.Demo06Composicion;
import co.edu.cun.poo.modulo07_colecciones.Demo07ColeccionesYGenericos;
import co.edu.cun.poo.modulo08_excepciones.Demo08Excepciones;
import co.edu.cun.poo.modulo09_integrador.Demo09Integrador;
import co.edu.cun.poo.modulo10_principios.Demo10PrincipiosDiseno;
import co.edu.cun.poo.modulo11_interfaces.Demo11InterfacesYAbstractas;

/**
 * Punto de entrada del proyecto.
 *
 * Uso:
 *   java co.edu.cun.poo.Main            -> menú interactivo
 *   java co.edu.cun.poo.Main 3          -> ejecuta solo el módulo 3
 *   java co.edu.cun.poo.Main 1 4 7      -> ejecuta los módulos 1, 4 y 7
 *   java co.edu.cun.poo.Main todos      -> ejecuta todos en orden
 */
public class Main {

    // La lista es de tipo Modulo (la interfaz), no de cada clase concreta.
    // Gracias a eso podemos agregar módulos sin tocar el resto del programa.
    private static final List<Modulo> MODULOS = List.of(
            new Demo01ClasesYObjetos(),
            new Demo02Encapsulamiento(),
            new Demo03Herencia(),
            new Demo04Polimorfismo(),
            new Demo05Abstraccion(),
            new Demo06Composicion(),
            new Demo07ColeccionesYGenericos(),
            new Demo08Excepciones(),
            new Demo09Integrador(),
            new Demo10PrincipiosDiseno(),
            new Demo11InterfacesYAbstractas()
    );

    public static void main(String[] args) {
        if (args.length > 0) {
            ejecutarDesdeArgumentos(args);
            return;
        }
        menuInteractivo();
    }

    private static void ejecutarDesdeArgumentos(String[] args) {
        if (args.length == 1 && args[0].equalsIgnoreCase("todos")) {
            for (int i = 0; i < MODULOS.size(); i++) {
                ejecutarModulo(i);
            }
            return;
        }
        for (String arg : args) {
            try {
                int numero = Integer.parseInt(arg);
                ejecutarModulo(numero - 1);
            } catch (NumberFormatException e) {
                System.out.println("Argumento no válido: " + arg);
            }
        }
    }

    private static void menuInteractivo() {
        Scanner teclado = new Scanner(System.in);
        while (true) {
            imprimirMenu();
            System.out.print("Elige una opción: ");
            String entrada = teclado.nextLine().trim();

            if (entrada.equals("0") || entrada.equalsIgnoreCase("salir")) {
                System.out.println("¡Hasta la próxima clase!");
                return;
            }
            if (entrada.equalsIgnoreCase("t") || entrada.equalsIgnoreCase("todos")) {
                for (int i = 0; i < MODULOS.size(); i++) {
                    ejecutarModulo(i);
                }
                continue;
            }
            try {
                ejecutarModulo(Integer.parseInt(entrada) - 1);
            } catch (NumberFormatException e) {
                System.out.println("Opción no reconocida.\n");
            }
        }
    }

    private static void imprimirMenu() {
        System.out.println();
        System.out.println("╔══════════════════════════════════════════════════════════════╗");
        System.out.println("║      PROGRAMACIÓN ORIENTADA A OBJETOS II · Java · CUN        ║");
        System.out.println("╚══════════════════════════════════════════════════════════════╝");
        for (int i = 0; i < MODULOS.size(); i++) {
            System.out.printf("  %d. %s%n", i + 1, MODULOS.get(i).titulo());
        }
        System.out.println("  T. Ejecutar todos");
        System.out.println("  0. Salir");
    }

    private static void ejecutarModulo(int indice) {
        if (indice < 0 || indice >= MODULOS.size()) {
            System.out.println("No existe el módulo " + (indice + 1) + ".\n");
            return;
        }
        Modulo modulo = MODULOS.get(indice);   // referencia polimórfica
        System.out.println();
        System.out.println("══════════════════════════════════════════════════════════════");
        System.out.println(" MÓDULO " + (indice + 1) + ": " + modulo.titulo().toUpperCase());
        System.out.println(" " + modulo.descripcion());
        System.out.println("══════════════════════════════════════════════════════════════");
        modulo.ejecutar();                      // enlace dinámico: cada módulo hace lo suyo
        System.out.println();
    }
}
