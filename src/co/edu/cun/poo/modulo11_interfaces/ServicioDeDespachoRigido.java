package co.edu.cun.poo.modulo11_interfaces;

/**
 * CONTRAEJEMPLO: el enfoque que tenía EntregaYa antes de refactorizar.
 *
 * Cada vehículo nuevo obliga a abrir, modificar y volver a probar esta clase,
 * que es una de las más críticas del sistema. Viola el principio Abierto/Cerrado
 * y concentra las reglas de todos los medios en un solo lugar.
 */
public class ServicioDeDespachoRigido {

    public String asignarPedido(Pedido pedido, String tipoVehiculo) {
        double km = pedido.ruta().distanciaEnKm();
        double minutos;
        double costo;

        if (tipoVehiculo.equals("Moto")) {
            minutos = (km / 40.0) * 60;
            costo = km * 300.0;
        } else if (tipoVehiculo.equals("Bicicleta")) {
            minutos = (km / 15.0) * 60;
            costo = km * 150.0;
        } else {
            // ... y aquí entra cada vehículo futuro: scooter, dron, camioneta ...
            return pedido.codigo() + " · tipo de vehículo no soportado: " + tipoVehiculo;
        }

        return String.format("%s · %-16s %5.1f min   %s",
                pedido.codigo(), tipoVehiculo, minutos, MetodoDePago.formatearCOP(costo));
    }
}
