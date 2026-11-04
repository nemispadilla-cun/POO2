using Cun.Poo.Modulo06Persistencia.Entidades;

namespace Cun.Poo.Modulo06Persistencia.Reservas;

/// <summary>
/// Patron DAO (Data Access Object) del caso Transportes Andinos.
///
/// Es el CONTRATO de la capa de persistencia: dice QUE se puede hacer con las
/// reservas, no COMO se guardan. La capa de negocio (ReservaService) solo
/// conoce esta interfaz. Es la "I" de SOLID (G1) y el contrato de G2.
/// </summary>
public interface IReservaDao
{
    /// <summary>Nombre de la tecnologia, para mostrar en la salida.</summary>
    string Tecnologia { get; }

    void Agregar(Reserva reserva);

    List<Reserva> ListarPorViaje(int viajeId);

    int ContarPorViaje(int viajeId);
}
