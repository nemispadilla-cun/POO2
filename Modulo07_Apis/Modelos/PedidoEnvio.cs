namespace Cun.Poo.Modulo07Apis.Modelos;

/// <summary>
/// Pedido de CunExpress que entrega la API protegida /api/pedidos.
/// Un record tambien se deserializa: el serializador usa su constructor.
/// </summary>
public record PedidoEnvio(int Id, string Cliente, string Destino, string Estado);
