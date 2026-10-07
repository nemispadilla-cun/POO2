package co.edu.cun.poo.modulo11_interfaces;

/**
 * DISEÑO FLEXIBLE: el servicio depende de la abstracción, no de los vehículos.
 *
 * Integrar un medio de transporte nuevo no toca este archivo: se crea una clase
 * que cumpla EstrategiaDeTransporte y listo (principio Abierto/Cerrado).
 */
public class ServicioDeDespacho {

    public String asignarPedido(Pedido pedido, EstrategiaDeTransporte transporte) {
        double minutos = transporte.calcularTiempoEstimado(pedido.ruta());
        double costo = transporte.calcularCostoEnvio(pedido.ruta());

        return String.format("%s · %-16s %5.1f min   %s",
                pedido.codigo(), transporte.medio(), minutos,
                MetodoDePago.formatearCOP(costo));
    }
}
