using Microsoft.EntityFrameworkCore;

namespace Cun.Poo.Modulo07Apis.Taller;

/// <summary>
/// Base de datos local del taller: la tabla "posts" del recurso
/// (CREATE TABLE posts (id INT PRIMARY KEY, userId INT, title ..., body ...)).
/// Reutiliza el ORM del modulo 6; el Id lo trae la API, no lo genera SQLite.
/// </summary>
public class PublicacionesDbContext : DbContext
{
    private readonly string _rutaBaseDeDatos;

    public PublicacionesDbContext(string rutaBaseDeDatos)
    {
        _rutaBaseDeDatos = rutaBaseDeDatos;
    }

    public DbSet<Publicacion> Publicaciones => Set<Publicacion>();

    protected override void OnConfiguring(DbContextOptionsBuilder opciones) =>
        opciones.UseSqlite($"Data Source={_rutaBaseDeDatos};Pooling=False");

    protected override void OnModelCreating(ModelBuilder modelo)
    {
        var tabla = modelo.Entity<Publicacion>().ToTable("posts");
        tabla.Property(p => p.Id).HasColumnName("id").ValueGeneratedNever();
        tabla.Property(p => p.AutorId).HasColumnName("userId");
        tabla.Property(p => p.Titulo).HasColumnName("title").HasMaxLength(255);
        tabla.Property(p => p.Cuerpo).HasColumnName("body");
    }
}
