using Cun.Poo.CunExpressApi.Modelos;

namespace Cun.Poo.CunExpressApi.Servicios;

/// <summary>
/// Almacen de usuarios EN MEMORIA (un Dictionary de G3).
///
/// La API atiende varias solicitudes a la vez, en hilos distintos; por eso
/// cada operacion va dentro de lock. En un proyecto real este repositorio
/// usaria EF Core y una base de datos (G6), y el controlador no cambiaria.
/// </summary>
public class RepositorioUsuarios
{
    private readonly Dictionary<int, UsuarioRegistro> _usuarios = [];
    private readonly object _candado = new();
    private int _siguienteId = 4;

    public RepositorioUsuarios()
    {
        _usuarios[1] = new UsuarioRegistro(1, "Ana Rojas", "ana.rojas@cun.edu.co", "Bogota", "300 555 0101");
        _usuarios[2] = new UsuarioRegistro(2, "Carlos Moncada", "carlos.moncada@cun.edu.co", "Medellin", "301 555 0202");
        _usuarios[3] = new UsuarioRegistro(3, "Laura Gomez", "laura.gomez@cun.edu.co", "Cali", "302 555 0303");
    }

    public List<UsuarioRegistro> Listar()
    {
        lock (_candado)
        {
            return _usuarios.Values.OrderBy(u => u.Id).ToList();
        }
    }

    public UsuarioRegistro? Buscar(int id)
    {
        lock (_candado)
        {
            return _usuarios.GetValueOrDefault(id);
        }
    }

    public UsuarioRegistro Agregar(UsuarioEntrada datos)
    {
        lock (_candado)
        {
            var nuevo = new UsuarioRegistro(_siguienteId++, datos.Nombre.Trim(), datos.Email.Trim(), datos.Ciudad?.Trim() ?? "", "");
            _usuarios[nuevo.Id] = nuevo;
            return nuevo;
        }
    }

    /// <summary>Reemplaza los datos (PUT). null si el usuario no existe.</summary>
    public UsuarioRegistro? Reemplazar(int id, UsuarioEntrada datos)
    {
        lock (_candado)
        {
            if (!_usuarios.TryGetValue(id, out UsuarioRegistro? actual))
            {
                return null;
            }
            UsuarioRegistro actualizado = actual with
            {
                Nombre = datos.Nombre.Trim(),
                Email = datos.Email.Trim(),
                Ciudad = datos.Ciudad?.Trim() ?? "",
            };
            _usuarios[id] = actualizado;
            return actualizado;
        }
    }

    public bool Eliminar(int id)
    {
        lock (_candado)
        {
            return _usuarios.Remove(id);
        }
    }
}
