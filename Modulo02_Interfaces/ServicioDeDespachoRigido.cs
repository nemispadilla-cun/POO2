namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// CONTRAEJEMPLO: el enfoque que tenia EntregaYa antes de refactorizar.
///
/// Cada vehiculo nuevo obliga a abrir, modificar y volver a probar esta clase,
/// una de las mas criticas del sistema. Viola el principio Abierto/Cerrado.
/// </summary>
public class ServicioDeDespachoRigido
{
    public string AsignarPedido(Pedido pedido, string tipoVehiculo)
    {
        double km = pedido.Ruta.DistanciaEnKm;
        double minutos;
        decimal costo;

        if (tipoVehiculo == "Moto")
        {
            minutos = km / 40.0 * 60;
            costo = (decimal)km * 300m;
        }
        else if (tipoVehiculo == "Bicicleta")
        {
            minutos = km / 15.0 * 60;
            costo = (decimal)km * 150m;
        }
        else
        {
            // ... y aqui entra cada vehiculo futuro: scooter, dron, camioneta ...
            return $"{pedido.Codigo} - tipo de vehiculo no soportado: {tipoVehiculo}";
        }

        return $"{pedido.Codigo} - {tipoVehiculo,-16} {minutos,5:F1} min   {Dinero.Cop(costo)}";
    }
}
