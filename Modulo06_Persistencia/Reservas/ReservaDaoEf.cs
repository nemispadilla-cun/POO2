using Cun.Poo.Modulo06Persistencia.Datos;
using Cun.Poo.Modulo06Persistencia.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cun.Poo.Modulo06Persistencia.Reservas;

/// <summary>
/// Implementacion del DAO con el ORM (EF Core sobre SQLite): el ReservaDAOImpl
/// con Hibernate del recurso. Las consultas se escriben en LINQ, sobre clases
/// y propiedades, y el ORM las traduce a SQL.
/// </summary>
public class ReservaDaoEf : IReservaDao
{
    private readonly CursoDbContext _db;

    public ReservaDaoEf(CursoDbContext db)
    {
        _db = db;
    }

    public string Tecnologia => "EF Core + SQLite";

    public void Agregar(Reserva reserva)
    {
        _db.Reservas.Add(reserva);
        _db.SaveChanges();                          // el Id lo asigna la base de datos
    }

    public List<Reserva> ListarPorViaje(int viajeId) => ConsultaPorViaje(viajeId).ToList();

    public int ContarPorViaje(int viajeId) => _db.Reservas.Count(r => r.ViajeId == viajeId);

    /// <summary>
    /// HQL del recurso:  SELECT r FROM Reserva r JOIN r.viaje v WHERE v.id = :viajeId
    /// LINQ:             _db.Reservas.Where(r => r.Viaje!.Id == viajeId)
    /// Se expone como IQueryable para poder mostrar el SQL que genera.
    /// </summary>
    public IQueryable<Reserva> ConsultaPorViaje(int viajeId) =>
        _db.Reservas.Where(r => r.Viaje!.Id == viajeId).OrderBy(r => r.Silla);
}
