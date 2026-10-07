package co.edu.cun.poo.modulo10_principios;

/** Un renglón de la factura. Inmutable. */
public record LineaFactura(String concepto, int cantidad, double precioUnitario) {

    public LineaFactura {
        if (cantidad <= 0) throw new IllegalArgumentException("La cantidad debe ser positiva.");
        if (precioUnitario < 0) throw new IllegalArgumentException("El precio no puede ser negativo.");
    }

    public double subtotal() {
        return cantidad * precioUnitario;
    }
}
