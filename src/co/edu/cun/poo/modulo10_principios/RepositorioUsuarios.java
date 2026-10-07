package co.edu.cun.poo.modulo10_principios;

import java.util.Optional;

/**
 * BAJO ACOPLAMIENTO: el módulo de autenticación depende de ESTA interfaz,
 * no de una base de datos concreta. Define lo mínimo que autenticación
 * necesita saber del almacenamiento.
 */
public interface RepositorioUsuarios {

    /** Devuelve la contraseña almacenada (ya cifrada) del usuario, si existe. */
    Optional<String> buscarHashDeContrasena(String usuario);

    /** Nombre descriptivo de la tecnología, solo para la demo. */
    String tecnologia();
}
