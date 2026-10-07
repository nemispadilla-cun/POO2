package co.edu.cun.poo.modulo06_composicion;

import java.time.DayOfWeek;
import java.time.LocalTime;

/**
 * PARTE de un Curso. Un horario no tiene sentido fuera del curso al que
 * pertenece: si el curso desaparece, sus horarios desaparecen con él.
 * Eso es COMPOSICIÓN (relación fuerte, rombo negro en UML).
 */
public record Horario(DayOfWeek dia, LocalTime inicio, LocalTime fin) {

    public Horario {
        if (!fin.isAfter(inicio)) {
            throw new IllegalArgumentException("La hora de fin debe ser posterior a la de inicio.");
        }
    }

    public boolean seCruzaCon(Horario otro) {
        return dia == otro.dia && inicio.isBefore(otro.fin) && otro.inicio.isBefore(fin);
    }

    @Override
    public String toString() {
        return dia + " " + inicio + "-" + fin;
    }
}
