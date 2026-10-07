package co.edu.cun.poo.modulo05_abstraccion;

import java.util.List;

/**
 * Un proyecto se califica con una rúbrica: promedio de varios criterios.
 * Además implementa DOS interfaces: Java no permite heredar de dos clases,
 * pero sí implementar cuantas interfaces se necesiten.
 */
public class Proyecto extends Evaluacion implements Entregable, Retroalimentable {

    private final List<Double> criterios;
    private boolean entregado = false;

    public Proyecto(String nombre, double porcentaje, List<Double> criterios) {
        super(nombre, porcentaje);
        this.criterios = List.copyOf(criterios);
    }

    @Override
    public double calcularNota() {
        return criterios.stream().mapToDouble(Double::doubleValue).average().orElse(0);
    }

    // ----- Entregable -----
    @Override
    public void marcarEntregado() {
        entregado = true;
    }

    @Override
    public boolean estaEntregado() {
        return entregado;
    }

    // ----- Retroalimentable -----
    @Override
    public String retroalimentacion() {
        double nota = calcularNota();
        return nota >= 4.0 ? "Excelente trabajo, dominio del tema."
                : nota >= 3.0 ? "Cumple, pero revisa los criterios más bajos."
                : "Debe reforzar conceptos antes de la siguiente entrega.";
    }
}
