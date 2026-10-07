package co.edu.cun.poo.modulo10_principios;

/** Solicitud de pago. Inmutable. */
public record Pago(String referencia, String tarjeta, double monto) {
}
