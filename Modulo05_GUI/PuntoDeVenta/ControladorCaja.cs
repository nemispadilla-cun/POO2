using Cun.Poo.Modulo05Gui.Componentes;

namespace Cun.Poo.Modulo05Gui.PuntoDeVenta;

/// <summary>
/// CONTROLADOR del punto de venta. Un solo oyente atiende todos los botones
/// de productos (el "ActionListener central" del recurso): mira cual boton
/// fue la fuente y agrega el producto que le corresponde.
/// </summary>
public class ControladorCaja
{
    private readonly Pedido _pedido;
    private readonly Dictionary<Boton, Producto> _productoPorBoton = [];

    public ControladorCaja(Pedido pedido)
    {
        _pedido = pedido;
    }

    public void ConectarProducto(Boton boton, Producto producto)
    {
        _productoPorBoton[boton] = producto;
        boton.Click += AlElegirProducto;        // el mismo metodo para todos los productos
    }

    public void ConectarPago(Boton boton, string medio)
    {
        boton.Click += (sender, e) =>
        {
            // En una ventana real seria un MessageBox / JDialog de confirmacion.
            Console.WriteLine($"   [Dialogo]  Confirmar pago {medio} por {Dinero.Cop(_pedido.Total)}? -> Si");
            _pedido.Pagar(medio);
        };
    }

    private void AlElegirProducto(object? sender, ClicEventArgs e)
    {
        Producto producto = _productoPorBoton[(Boton)sender!];  // sender = la FUENTE
        _pedido.Agregar(producto);
    }
}
