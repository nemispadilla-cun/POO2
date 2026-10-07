package co.edu.cun.poo.modulo10_principios;

import java.util.Map;
import java.util.Optional;

/**
 * Simula una colección de documentos MongoDB. Misma interfaz, distinta
 * tecnología: la clase ServicioAutenticacion no nota la diferencia.
 */
public class RepositorioUsuariosMongo implements RepositorioUsuarios {

    // Simulación de: db.usuarios.findOne({ nombre: ... })
    private final Map<String, String> coleccion = Map.of(
            "ana", RepositorioUsuariosSql.hash("cun2026"),
            "carla", RepositorioUsuariosSql.hash("mongo123")
    );

    @Override
    public Optional<String> buscarHashDeContrasena(String usuario) {
        return Optional.ofNullable(coleccion.get(usuario));
    }

    @Override
    public String tecnologia() {
        return "MongoDB (documentos)";
    }
}
