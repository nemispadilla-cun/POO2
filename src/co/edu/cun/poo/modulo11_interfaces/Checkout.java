package co.edu.cun.poo.modulo11_interfaces;

import java.util.ArrayList;
import java.util.List;

/**
 * El núcleo del carrito de compras. No conoce PSE, ni Nequi, ni Efecty:
 * solo conoce el contrato MetodoDePago. Agregar DaviPlata mañana es crear
 * una clase nueva, sin tocar una sola línea de este archivo.
 */
public class Checkout {

    private final List<String> recibos = new ArrayList<>();

    public boolean cobrar(MetodoDePago metodo, double monto) {
        System.out.println(" > Cobrando con " + metodo.nombre()
                + " · " + MetodoDePago.formatearCOP(monto));

        boolean aprobado = metodo.procesarPago(monto);   // enlace dinámico
        System.out.println("   Estado -> " + metodo.obtenerEstado());

        if (aprobado) {
            recibos.add(metodo.nombre() + " · " + MetodoDePago.formatearCOP(monto));
        }
        return aprobado;
    }

    public List<String> recibos() {
        return List.copyOf(recibos);      // vista de solo lectura
    }
}
