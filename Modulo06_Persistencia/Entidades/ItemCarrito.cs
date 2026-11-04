namespace Cun.Poo.Modulo06Persistencia.Entidades;

/// <summary>
/// Una linea del carrito (tabla CARRITO_ITEMS). Persiste entre sesiones y
/// dispositivos: el cliente puede volver manana y su carrito sigue ahi.
/// </summary>
public class ItemCarrito
{
    public int Id { get; set; }
    public string Cliente { get; set; } = "";
    public int CafeId { get; set; }                 // clave foranea...
    public Cafe Cafe { get; set; } = null!;         // ...y la referencia al objeto
    public int Cantidad { get; set; }
}
