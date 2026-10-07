package co.edu.cun.poo.modulo11_interfaces;

/**
 * Ejercicio guiado del recurso G2.
 *
 * El servicio solo conoce la abstracción Notificador: cambia su comportamiento
 * en tiempo de ejecución sin modificar una línea de su propio código.
 */
public class ServicioNotificaciones {

    private Notificador notificador = Notificador.silencioso();

    public void setNotificador(Notificador notificador) {
        this.notificador = notificador;
    }

    public void notificarUsuario(String destino, String mensaje) {
        notificador.enviar(destino, mensaje);
    }
}
