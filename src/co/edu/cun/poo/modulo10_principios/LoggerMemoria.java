package co.edu.cun.poo.modulo10_principios;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/**
 * Implementación que guarda los mensajes en memoria. Útil para pruebas
 * automatizadas o para mostrar un historial en una app móvil.
 * El mismo código cliente funciona con LoggerConsola o con este.
 */
public class LoggerMemoria implements Logger {

    private final List<String> registros = new ArrayList<>();

    @Override
    public void info(String mensaje) {
        registros.add("INFO  " + mensaje);
    }

    @Override
    public void error(String mensaje, Throwable causa) {
        registros.add("ERROR " + mensaje + (causa == null ? "" : " <- " + causa.getMessage()));
    }

    public List<String> getRegistros() {
        return Collections.unmodifiableList(registros);
    }
}
