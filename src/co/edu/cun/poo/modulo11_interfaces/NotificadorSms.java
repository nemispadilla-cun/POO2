package co.edu.cun.poo.modulo11_interfaces;

/** Segundo medio, mismo contrato: el servicio no nota la diferencia. */
public class NotificadorSms implements Notificador {

    @Override
    public void enviar(String destino, String mensaje) {
        // Aquí iría la pasarela de SMS.
        System.out.println("   [SMS -> " + destino + "] " + mensaje);
    }
}
