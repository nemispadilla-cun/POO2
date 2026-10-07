package co.edu.cun.poo.modulo10_principios;

import java.time.LocalTime;
import java.time.format.DateTimeFormatter;

/** Implementación que escribe en consola. Serviría igual en una app de escritorio. */
public class LoggerConsola implements Logger {

    private static final DateTimeFormatter HORA = DateTimeFormatter.ofPattern("HH:mm:ss");
    private final String origen;

    public LoggerConsola(String origen) {
        this.origen = origen;
    }

    @Override
    public void info(String mensaje) {
        System.out.println("   [" + LocalTime.now().format(HORA) + "] [" + origen + "] " + mensaje);
    }

    @Override
    public void error(String mensaje, Throwable causa) {
        info("ERROR: " + mensaje + (causa == null ? "" : " (" + causa.getClass().getSimpleName() + ")"));
    }
}
