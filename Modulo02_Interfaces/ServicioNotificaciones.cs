namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// Ejercicio guiado del recurso G2.
///
/// El servicio solo conoce la abstraccion INotificador: cambia su
/// comportamiento en tiempo de ejecucion sin modificar su propio codigo.
/// </summary>
public class ServicioNotificaciones
{
    private INotificador _notificador = INotificador.Silencioso();

    public void SetNotificador(INotificador notificador) => _notificador = notificador;

    /// <summary>Sobrecarga: acepta directamente una lambda.</summary>
    public void SetNotificador(Action<string, string> accion) =>
        _notificador = INotificador.Desde(accion);

    public void NotificarUsuario(string destino, string mensaje) =>
        _notificador.Enviar(destino, mensaje);
}
