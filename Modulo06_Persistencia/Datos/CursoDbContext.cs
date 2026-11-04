using Cun.Poo.Modulo06Persistencia.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cun.Poo.Modulo06Persistencia.Datos;

/// <summary>
/// La "conversacion" con la base de datos: el EntityManager / Session de
/// Hibernate. Es liviano, se crea para una unidad de trabajo y se desecha
/// con using.
///
/// Cada DbSet es una tabla vista como una coleccion de objetos.
/// La base es SQLite: un solo archivo, sin servidor que instalar. Pasar a
/// SQL Server o PostgreSQL es cambiar UseSqlite por UseSqlServer o UseNpgsql
/// (y su paquete NuGet); el resto del codigo no cambia.
/// </summary>
public class CursoDbContext : DbContext
{
    private readonly string _rutaBaseDeDatos;

    public CursoDbContext(string rutaBaseDeDatos)
    {
        _rutaBaseDeDatos = rutaBaseDeDatos;
    }

    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Cafe> Cafes => Set<Cafe>();
    public DbSet<ItemCarrito> Carrito => Set<ItemCarrito>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<Viaje> Viajes => Set<Viaje>();
    public DbSet<Reserva> Reservas => Set<Reserva>();

    /// <summary>Equivale a hibernate.cfg.xml: a que base nos conectamos.</summary>
    protected override void OnConfiguring(DbContextOptionsBuilder opciones) =>
        opciones.UseSqlite($"Data Source={_rutaBaseDeDatos};Pooling=False");

    /// <summary>
    /// Fluent API: el mapeo escrito en codigo en lugar de atributos.
    /// Estudiante usa atributos; el resto se mapea aqui, con los nombres de
    /// tabla del recurso.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelo)
    {
        modelo.Entity<Cafe>().ToTable("productos");
        modelo.Entity<ItemCarrito>().ToTable("carrito_items");
        modelo.Entity<Pedido>().ToTable("pedidos");
        modelo.Entity<DetallePedido>().ToTable("detalles_pedido");
        modelo.Entity<Viaje>().ToTable("viajes");
        modelo.Entity<Reserva>().ToTable("reservas")
              .HasIndex(r => new { r.ViajeId, r.Silla }).IsUnique();   // una silla, un pasajero
    }
}
