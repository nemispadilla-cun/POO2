package co.edu.cun.poo.modulo08_excepciones;

/**
 * Recurso que debe cerrarse siempre (como un archivo o una conexión a BD).
 * Al implementar AutoCloseable se puede usar con try-with-resources y Java
 * garantiza que close() se ejecute, haya o no excepción.
 */
public class ConexionSimulada implements AutoCloseable {

    private final String destino;

    public ConexionSimulada(String destino) {
        this.destino = destino;
        System.out.println("   [conexión] abierta hacia " + destino);
    }

    public void enviar(String dato) {
        if (dato.isBlank()) {
            throw new IllegalArgumentException("No se puede enviar un dato vacío.");
        }
        System.out.println("   [conexión] enviado: " + dato);
    }

    @Override
    public void close() {
        System.out.println("   [conexión] cerrada hacia " + destino);
    }
}
