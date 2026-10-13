namespace Cun.Poo.Modulo01Principios;

/// <summary>
/// Otra tecnologia, el mismo contrato. ServicioAutenticacion no se entera
/// del cambio: eso es bajo acoplamiento.
/// </summary>
public class RepositorioUsuariosMongo : IRepositorioUsuarios
{
    private readonly Dictionary<string, string> _documentos = new()
    {
        ["ana"] = RepositorioUsuariosSql.Hash("cun2026"),
        ["carla"] = RepositorioUsuariosSql.Hash("mongo123"),
    };

    public string Tecnologia => "MongoDB";

    public string? BuscarHashDeContrasena(string usuario) =>
        _documentos.TryGetValue(usuario, out string? hash) ? hash : null;
}
