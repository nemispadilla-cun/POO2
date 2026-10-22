namespace Cun.Poo.Modulo04Patrones.Estructurales;

/// <summary>
/// El CLIENTE del Adapter. Solo conoce IPasarelaPago: no hay ni un
/// "if (esPayU) ... else if (esWompi) ..." en toda la clase.
/// </summary>
public class CarritoDeCompras
{
    private readonly List<(string Producto, decimal Precio)> _items = [];
    private readonly IPasarelaPago _pasarela;

    public CarritoDeCompras(IPasarelaPago pasarela) => _pasarela = pasarela;

    public decimal Total => _items.Sum(i => i.Precio);

    public void Agregar(string producto, decimal precio) => _items.Add((producto, precio));

    public string Pagar()
    {
        string id = _pasarela.IniciarPago(Total, $"Pedido de {_items.Count} prendas");
        EstadoPago estado = _pasarela.VerificarEstado(id);
        string corto = id.Length > 13 ? id[..13] + "..." : id;
        return $"{_pasarela.Nombre,-6} cobra {Dinero.Cop(Total)} -> {estado} (id {corto})";
    }
}
