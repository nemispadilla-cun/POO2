package co.edu.cun.poo.modulo02_encapsulamiento;

import co.edu.cun.poo.Modulo;

public class Demo02Encapsulamiento implements Modulo {

    @Override
    public String titulo() {
        return "Encapsulamiento";
    }

    @Override
    public String descripcion() {
        return "private/public, getters y setters con validación, invariantes, inmutabilidad y records.";
    }

    @Override
    public void ejecutar() {
        seccion("1. Sin encapsular: el objeto puede quedar en un estado inválido");
        CalificacionSinEncapsular mala = new CalificacionSinEncapsular();
        mala.asignatura = "POO II";
        mala.valor = 12.0;   // nadie lo impide
        System.out.println("Nota guardada: " + mala.valor + "  <- ¡fuera de la escala 0-5!");
        nota("El error se descubrirá tarde, en otra parte del programa, y será difícil de rastrear.");

        seccion("2. Encapsulado: el objeto protege sus reglas");
        Calificacion buena = new Calificacion("POO II", 4.2);
        System.out.println("Creada: " + buena);
        System.out.println("¿Aprobó? " + buena.esAprobatoria());

        try {
            buena.setValor(12.0);
        } catch (IllegalArgumentException e) {
            System.out.println("Rechazado por el propio objeto -> " + e.getMessage());
        }
        System.out.println("Sigue intacta: " + buena);
        nota("El error se detecta EN EL MOMENTO en que alguien intenta violar la regla.");

        seccion("3. Solo exponer lo necesario");
        System.out.println("Asignatura: " + buena.getAsignatura());
        // buena.asignatura = "Otra";  // NO compila: el atributo es private
        // buena.setAsignatura(...);   // NO existe: decidimos que no se cambia
        nota("Hay getter de asignatura pero no setter: la interfaz pública es una decisión de diseño.");

        seccion("4. Inmutabilidad con record");
        Documento doc = new Documento("CC", "1023456789");
        System.out.println("Documento: " + doc.formateado());
        System.out.println("toString automático: " + doc);
        System.out.println("equals automático: "
                + doc.equals(new Documento("CC", "1023456789")));
        try {
            new Documento("PASAPORTE", "AB123");
        } catch (IllegalArgumentException e) {
            System.out.println("Record también valida -> " + e.getMessage());
        }
        nota("Un record no tiene setters: su estado no puede corromperse después de creado.");
    }
}
