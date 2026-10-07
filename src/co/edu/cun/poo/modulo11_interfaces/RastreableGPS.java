package co.edu.cun.poo.modulo11_interfaces;

/**
 * Segundo contrato, independiente del anterior.
 *
 * Una clase en Java hereda de UNA sola superclase, pero puede implementar
 * TANTAS interfaces como necesite. El Dron es, a la vez, un Vehiculo
 * (herencia) y un dispositivo rastreable (contrato).
 */
public interface RastreableGPS {

    String obtenerCoordenadas();

    void enviarAlertaPosicion();
}
