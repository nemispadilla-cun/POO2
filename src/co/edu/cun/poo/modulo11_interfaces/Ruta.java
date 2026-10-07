package co.edu.cun.poo.modulo11_interfaces;

/** Dato inmutable: de dónde a dónde y cuántos kilómetros. */
public record Ruta(String origen, String destino, double distanciaEnKm) {

    public Ruta {
        if (distanciaEnKm <= 0) {
            throw new IllegalArgumentException("La distancia debe ser mayor que cero.");
        }
    }

    @Override
    public String toString() {
        return origen + " -> " + destino + " (" + distanciaEnKm + " km)";
    }
}
