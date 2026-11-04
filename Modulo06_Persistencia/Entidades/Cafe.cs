namespace Cun.Poo.Modulo06Persistencia.Entidades;

/// <summary>
/// Cafe especial que vende la tienda en linea "Cafes de mi Tierra" (caso 2.2).
/// Se mapea a la tabla PRODUCTOS con Fluent API (ver CursoDbContext), no con
/// atributos: la clase queda limpia y el mapeo vive en otra parte.
/// </summary>
public class Cafe
{
    public int Id { get; set; }                     // "Id" -> clave primaria por convencion
    public string Nombre { get; set; } = "";
    public string Region { get; set; } = "";
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public override string ToString() => $"{Nombre} ({Region})";
}
