package co.edu.cun.poo.modulo05_abstraccion;

/**
 * INTERFAZ SELLADA (sealed, Java 17): el conjunto de implementaciones está
 * CERRADO y se declara aquí. Nadie más puede agregar un estado nuevo.
 * Sirve para modelar "uno de estos casos y ninguno más".
 */
public sealed interface EstadoMatricula
        permits EstadoMatricula.Activa, EstadoMatricula.Cancelada, EstadoMatricula.Finalizada {

    String describir();

    // Los tres casos viven DENTRO de la interfaz: son records anidados
    // (implícitamente public static). Se usan como EstadoMatricula.Activa, etc.

    record Activa(int creditos) implements EstadoMatricula {
        @Override
        public String describir() {
            return "Activa con " + creditos + " créditos";
        }
    }

    record Cancelada(String motivo) implements EstadoMatricula {
        @Override
        public String describir() {
            return "Cancelada: " + motivo;
        }
    }

    record Finalizada(double promedio) implements EstadoMatricula {
        @Override
        public String describir() {
            return "Finalizada con promedio " + promedio;
        }
    }
}
