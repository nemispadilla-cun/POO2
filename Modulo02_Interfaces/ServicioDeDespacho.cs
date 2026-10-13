namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// DISENO FLEXIBLE: el servicio depende de la abstraccion, no de los vehiculos.
///
/// Integrar un medio de transporte nuevo no toca este archivo: se crea una
/// clase que cumpla IEstrategiaDeTransporte (principio Abierto/Cerrado).
/// </summary>
public class ServicioDeDespacho
{
    public string AsignarPedido(Pedido pedido, IEstrategiaDeTransporte transporte)
    {
        double minutos = transporte.CalcularTiempoEstimado(pedido.Ruta);
        decimal costo = transporte.CalcularCostoEnvio(pedido.Ruta);

        return $"{pedido.Codigo} - {transporte.Medio,-16} {minutos,5:F1} min   {Dinero.Cop(costo)}";
    }
}
