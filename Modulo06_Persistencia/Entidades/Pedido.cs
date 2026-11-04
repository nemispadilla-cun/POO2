namespace Cun.Poo.Modulo06Persistencia.Entidades;

/// <summary>
/// Pedido confirmado (tabla PEDIDOS) con sus detalles (tabla DETALLES_PEDIDO).
///
/// En el objeto, los detalles son una LISTA dentro del pedido. En la base de
/// datos no existen las listas: son filas de otra tabla con una clave foranea.
/// Esa traduccion es el desajuste de impedancia, y el ORM la hace por nosotros.
/// </summary>
public class Pedido
{
    public int Id { get; set; }
    public string Cliente { get; set; } = "";
    public decimal Total { get; set; }
    public List<DetallePedido> Detalles { get; set; } = [];
}

/// <summary>Un producto dentro de un pedido, con el precio que tenia al comprar.</summary>
public class DetallePedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int CafeId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
