package co.edu.cun.poo.modulo11_interfaces;

/** Implementación clásica del contrato, como clase con nombre. */
public class NotificadorEmail implements Notificador {

    @Override
    public void enviar(String destino, String mensaje) {
        // Aquí iría la conexión con un servidor SMTP.
        System.out.println("   [Email -> " + destino + "] " + mensaje);
    }
}
