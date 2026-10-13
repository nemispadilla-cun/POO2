namespace Cun.Poo.Modulo01Principios;

public class RepositorioUsuariosSql : IRepositorioUsuarios
{
    private readonly Dictionary<string, string> _tabla = new()
    {
        ["ana"] = Hash("cun2026"),
        ["luis"] = Hash("poo-rocks"),
    };

    public string Tecnologia => "SQL";

    public string? BuscarHashDeContrasena(string usuario) =>
        _tabla.TryGetValue(usuario, out string? hash) ? hash : null;

    internal static string Hash(string clave) => "#" + clave.GetHashCode().ToString("x8");
}
