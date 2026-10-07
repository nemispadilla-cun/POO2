package co.edu.cun.poo.modulo10_principios;

import java.util.Optional;

/**
 * Módulo de AUTENTICACIÓN con BAJO ACOPLAMIENTO.
 *
 * Fíjate en los tipos de los atributos: RepositorioUsuarios y Logger son
 * INTERFACES. Esta clase no sabe si detrás hay SQL, MongoDB o un mapa en
 * memoria; ni si el log va a consola, archivo o red.
 *
 * Las dependencias se reciben por el constructor (INYECCIÓN DE DEPENDENCIAS):
 * quien arma la aplicación decide qué implementación usar.
 */
public class ServicioAutenticacion {

    private final RepositorioUsuarios repositorio;
    private final Logger logger;

    public ServicioAutenticacion(RepositorioUsuarios repositorio, Logger logger) {
        this.repositorio = repositorio;
        this.logger = logger;
    }

    public boolean autenticar(String usuario, String contrasena) {
        Optional<String> hashGuardado = repositorio.buscarHashDeContrasena(usuario);
        if (hashGuardado.isEmpty()) {
            logger.advertencia("Intento de acceso con usuario inexistente: " + usuario);
            return false;
        }
        boolean ok = hashGuardado.get().equals(RepositorioUsuariosSql.hash(contrasena));
        logger.info("Autenticación de " + usuario + " -> " + (ok ? "EXITOSA" : "FALLIDA")
                + " [almacén: " + repositorio.tecnologia() + "]");
        return ok;
    }
}
