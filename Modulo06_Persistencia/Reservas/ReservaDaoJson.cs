using Cun.Poo.Modulo06Persistencia.Archivos;
using Cun.Poo.Modulo06Persistencia.Entidades;

namespace Cun.Poo.Modulo06Persistencia.Reservas;

/// <summary>
/// Implementacion del DAO con un archivo JSON: sirve para un prototipo o una
/// taquilla sin red. Cada operacion lee el archivo completo y lo reescribe,
/// por eso no escala a miles de reservas ni a varias taquillas a la vez.
/// </summary>
public class ReservaDaoJson : IReservaDao
{
    private readonly string _ruta;

    public ReservaDaoJson(string ruta)
    {
        _ruta = ruta;
    }

    public string Tecnologia => "archivo JSON";

    public void Agregar(Reserva reserva)
    {
        List<Reserva> todas = LeerTodas();
        reserva.Id = todas.Count == 0 ? 1 : todas.Max(r => r.Id) + 1;   // el "autoincremento" lo hacemos a mano
        todas.Add(reserva);
        ArchivoJson.Guardar(todas, _ruta);
    }

    public List<Reserva> ListarPorViaje(int viajeId) =>
        LeerTodas().Where(r => r.ViajeId == viajeId).OrderBy(r => r.Silla).ToList();

    public int ContarPorViaje(int viajeId) => LeerTodas().Count(r => r.ViajeId == viajeId);

    private List<Reserva> LeerTodas() =>
        File.Exists(_ruta) ? ArchivoJson.Cargar<List<Reserva>>(_ruta) : [];
}
