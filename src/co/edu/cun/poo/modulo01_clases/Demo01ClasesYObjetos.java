package co.edu.cun.poo.modulo01_clases;

import co.edu.cun.poo.Modulo;

public class Demo01ClasesYObjetos implements Modulo {

    @Override
    public String titulo() {
        return "Clases y objetos";
    }

    @Override
    public String descripcion() {
        return "Clase vs objeto, atributos, constructores, métodos, this, static, referencias.";
    }

    @Override
    public void ejecutar() {
        seccion("1. Crear objetos a partir de la clase (instanciar)");
        Estudiante ana = new Estudiante("Ana Torres", "2024101", 3);
        Estudiante luis = new Estudiante("Luis Pérez", "2024102");   // constructor corto
        ana.presentarse();
        luis.presentarse();
        nota("La clase Estudiante es el molde; ana y luis son dos objetos independientes.");

        seccion("2. Cada objeto tiene su propio estado");
        luis.avanzarSemestre();
        luis.avanzarSemestre();
        System.out.println("Luis avanzó dos semestres -> " + luis);
        System.out.println("Ana no cambió              -> " + ana);
        System.out.println("¿Están en el mismo semestre? " + ana.esDeMismoSemestre(luis));

        seccion("3. Atributos y métodos static pertenecen a la CLASE, no al objeto");
        System.out.println("Estudiantes creados hasta ahora: " + Estudiante.contarCreados());
        new Estudiante("Carla Ruiz", "2024103", 5);
        System.out.println("Después de crear otro:           " + Estudiante.contarCreados());
        nota("Se invoca con el nombre de la clase, no hace falta un objeto.");

        seccion("4. Las variables guardan REFERENCIAS, no copias del objeto");
        Estudiante alias = ana;      // NO se crea otro estudiante; apuntan al mismo
        alias.avanzarSemestre();
        System.out.println("Modifiqué alias y cambió ana: " + ana);
        System.out.println("¿ana == alias? " + (ana == alias) + "  (misma dirección en memoria)");

        Estudiante copia = new Estudiante("Ana Torres", "2024101", 4);
        System.out.println("¿ana == copia? " + (ana == copia)
                + "  (mismos datos, pero objetos distintos)");
        nota("== compara referencias. Para comparar contenido se usa equals() (módulo 3).");
    }
}
