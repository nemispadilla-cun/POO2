namespace Cun.Poo.Modulo01Principios;

/// <summary>
/// INVERSION DE DEPENDENCIAS: depende de abstracciones (IRepositorioUsuarios,
/// ILogger) y las recibe por el constructor. Quien arma la aplicacion decide
/// que implementacion entra.
/// </summary>
public class ServicioAutenticacion(IRepositorioUsuarios repositorio, ILogger logger)
{
    public bool Autenticar(string usuario, string clave)
    {
        string? hashGuardado = repositorio.BuscarHashDeContrasena(usuario);

        if (hashGuardado is null)
        {
            logger.Error($"Usuario inexistente: {usuario} ({repositorio.Tecnologia})");
            return false;
        }

        bool ok = hashGuardado == RepositorioUsuariosSql.Hash(clave);
        logger.Info($"Autenticacion de {usuario} en {repositorio.Tecnologia}: {(ok ? "OK" : "FALLIDA")}");
        return ok;
    }
}
