package co.edu.cun.poo.modulo10_principios;

import java.util.Map;
import java.util.Optional;

/** Simula una tabla SQL. En la vida real aquí habría JDBC o JPA. */
public class RepositorioUsuariosSql implements RepositorioUsuarios {

    // Simulación de: SELECT hash FROM usuarios WHERE nombre = ?
    private final Map<String, String> tabla = Map.of(
            "ana", hash("cun2026"),
            "luis", hash("poo-rocks")
    );

    @Override
    public Optional<String> buscarHashDeContrasena(String usuario) {
        return Optional.ofNullable(tabla.get(usuario));
    }

    @Override
    public String tecnologia() {
        return "PostgreSQL (SQL)";
    }

    static String hash(String texto) {
        return Integer.toHexString(texto.hashCode());   // solo para la demo, NO usar en producción
    }
}
