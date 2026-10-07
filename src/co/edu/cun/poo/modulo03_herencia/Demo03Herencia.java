package co.edu.cun.poo.modulo03_herencia;

import co.edu.cun.poo.Modulo;

public class Demo03Herencia implements Modulo {

    @Override
    public String titulo() {
        return "Herencia";
    }

    @Override
    public String descripcion() {
        return "extends, super, protected, sobreescritura (@Override), herencia multinivel, equals/hashCode.";
    }

    @Override
    public void ejecutar() {
        seccion("1. La subclase hereda lo del padre y agrega lo suyo");
        Estudiante est = new Estudiante("Ana Torres", "1023456789", "Ingeniería de Sistemas", 3);
        System.out.println(est.presentarse());
        System.out.println("Correo (heredado sin cambios): " + est.getCorreoInstitucional());
        System.out.println("Programa (propio de Estudiante): " + est.getPrograma());

        seccion("2. Sobreescritura: mismo mensaje, respuesta distinta según la clase");
        Persona p1 = new Persona("Rosa Díaz", "52123456");
        Docente d1 = new Docente("Carlos Mora", "79111222", "Bases de datos", 4_500_000);
        System.out.println(p1.presentarse());
        System.out.println(d1.presentarse());
        nota("Docente.presentarse() llama a super.presentarse() y luego agrega su parte.");

        seccion("3. Herencia multinivel: Object -> Persona -> Docente -> DocenteInvestigador");
        DocenteInvestigador di = new DocenteInvestigador(
                "Marta Gil", "41999888", "Inteligencia Artificial", 5_000_000, "GIDATI");
        System.out.println(di.presentarse());
        System.out.printf("Salario Docente normal:   $%,.0f%n", d1.calcularSalario());
        System.out.printf("Salario Investigador:     $%,.0f  (con bonificación)%n", di.calcularSalario());

        seccion("4. equals() y hashCode(): igualdad por contenido");
        Estudiante mismoDoc = new Estudiante("A. Torres", "1023456789", "Ing. de Sistemas", 4);
        System.out.println("est == mismoDoc      -> " + (est == mismoDoc) + "  (referencias distintas)");
        System.out.println("est.equals(mismoDoc) -> " + est.equals(mismoDoc) + "   (mismo documento)");
        System.out.println("hashCode iguales     -> " + (est.hashCode() == mismoDoc.hashCode()));
        nota("Las colecciones (HashSet, HashMap) dependen de que equals y hashCode sean coherentes.");

        seccion("5. Orden de construcción");
        nota("Al hacer new DocenteInvestigador(...) se ejecuta primero el constructor de Persona,"
                + " luego el de Docente y por último el de DocenteInvestigador.");
        System.out.println("toString usa getClass().getSimpleName(): " + di);
    }
}
