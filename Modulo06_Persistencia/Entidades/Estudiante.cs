using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cun.Poo.Modulo06Persistencia.Entidades;

/// <summary>
/// ENTIDAD mapeada con atributos, igual que el ejemplo JPA/Hibernate del recurso:
///
///   Java (JPA)                          C# (EF Core)
///   @Entity @Table(name="estudiantes")  [Table("estudiantes")]
///   @Id                                 [Key]
///   @Column(name="codigo_estudiante")   [Column("codigo_estudiante")]
///
/// La clase no hereda de nada ni sabe de SQL: el ORM lee los atributos y
/// traduce el objeto a una fila.
/// </summary>
[Table("estudiantes")]
public class Estudiante
{
    [Key, Column("codigo_estudiante"), MaxLength(10)]
    public string Codigo { get; set; } = "";

    [Column("nombre_completo"), MaxLength(80)]
    public string Nombre { get; set; } = "";

    [MaxLength(60)]
    public string Carrera { get; set; } = "";       // sin atributo: columna "Carrera"

    /// <summary>EF Core necesita un constructor sin parametros para reconstruir el objeto.</summary>
    public Estudiante() { }

    public Estudiante(string codigo, string nombre, string carrera)
    {
        Codigo = codigo;
        Nombre = nombre;
        Carrera = carrera;
    }

    public override string ToString() => $"{Codigo} {Nombre} ({Carrera})";
}
