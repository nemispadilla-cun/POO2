package co.edu.cun.poo.modulo10_principios;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/**
 * COHESIÓN ALTA: todos los métodos de esta clase giran alrededor de UNA sola
 * responsabilidad, la factura en sí: calcular totales, aplicar descuentos y
 * generar su resumen.
 *
 * Lo que NO hace (a propósito):
 *   - No se guarda a sí misma en una base de datos -> RepositorioFacturas.
 *   - No se envía por correo, no imprime en consola, no registra logs.
 *
 * Si mañana cambia la forma de guardar (SQL, archivo, nube), esta clase no
 * se toca. Compara con FacturaBajaCohesion.
 */
public class Factura {

    private static final double IVA = 0.19;

    private final String numero;
    private final String cliente;
    private final List<LineaFactura> lineas = new ArrayList<>();
    private double porcentajeDescuento = 0.0;

    public Factura(String numero, String cliente) {
        this.numero = numero;
        this.cliente = cliente;
    }

    public void agregarLinea(String concepto, int cantidad, double precioUnitario) {
        lineas.add(new LineaFactura(concepto, cantidad, precioUnitario));
    }

    // ---------- Cálculo de totales ----------
    public double calcularSubtotal() {
        double total = 0;
        for (LineaFactura l : lineas) total += l.subtotal();
        return total;
    }

    public double calcularDescuento() {
        return calcularSubtotal() * porcentajeDescuento;
    }

    public double calcularImpuestos() {
        return (calcularSubtotal() - calcularDescuento()) * IVA;
    }

    public double calcularTotal() {
        return calcularSubtotal() - calcularDescuento() + calcularImpuestos();
    }

    // ---------- Descuentos ----------
    public void aplicarDescuento(double porcentaje) {
        if (porcentaje < 0 || porcentaje > 0.5) {
            throw new IllegalArgumentException("El descuento debe estar entre 0 % y 50 %.");
        }
        this.porcentajeDescuento = porcentaje;
    }

    // ---------- Resumen (simula la generación del PDF) ----------
    public String generarResumenPdf() {
        StringBuilder sb = new StringBuilder();
        sb.append("FACTURA ").append(numero).append("  Cliente: ").append(cliente).append('\n');
        for (LineaFactura l : lineas) {
            sb.append(String.format("  %-32s x%-3d $%,12.0f%n", l.concepto(), l.cantidad(), l.subtotal()));
        }
        sb.append(String.format("  %-37s $%,12.0f%n", "Subtotal", calcularSubtotal()));
        sb.append(String.format("  %-37s $%,12.0f%n", "Descuento", -calcularDescuento()));
        sb.append(String.format("  %-37s $%,12.0f%n", "IVA 19%", calcularImpuestos()));
        sb.append(String.format("  %-37s $%,12.0f", "TOTAL", calcularTotal()));
        return sb.toString();
    }

    public String getNumero() {
        return numero;
    }

    public String getCliente() {
        return cliente;
    }

    public List<LineaFactura> getLineas() {
        return Collections.unmodifiableList(lineas);
    }
}
