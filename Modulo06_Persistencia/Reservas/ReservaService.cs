using Cun.Poo.Modulo06Persistencia.Entidades;

namespace Cun.Poo.Modulo06Persistencia.Reservas;

/// <summary>
/// Capa de LOGICA DE NEGOCIO de Transportes Andinos. Aqui viven las reglas
/// (no vender mas tiquetes que sillas), no el SQL ni los archivos.
///
/// Recibe el DAO por el constructor (inyeccion de dependencias, G1): no sabe
/// si las reservas terminan en un JSON o en una base de datos.
/// </summary>
public class ReservaService
{
    private readonly IReservaDao _dao;

    public ReservaService(IReservaDao dao)
    {
        _dao = dao;
    }

    /// <summary>Verifica disponibilidad y confirma la reserva en la siguiente silla libre.</summary>
    /// <exception cref="InvalidOperationException">Si el bus ya esta lleno (evita la sobreventa).</exception>
    public Reserva CrearReserva(Viaje viaje, string pasajero)
    {
        int vendidas = _dao.ContarPorViaje(viaje.Id);
        if (vendidas >= viaje.Capacidad)
        {
            throw new InvalidOperationException(
                $"Sobreventa evitada: el bus {viaje.PlacaBus} ya tiene sus {viaje.Capacidad} sillas vendidas.");
        }
        var reserva = new Reserva { ViajeId = viaje.Id, Pasajero = pasajero, Silla = vendidas + 1 };
        _dao.Agregar(reserva);
        return reserva;
    }

    public List<Reserva> Pasajeros(Viaje viaje) => _dao.ListarPorViaje(viaje.Id);
}
