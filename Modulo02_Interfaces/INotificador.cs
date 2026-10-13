namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// Contrato de un solo metodo.
///
/// DIFERENCIA CON JAVA: Java necesita una "interfaz funcional"
/// (@FunctionalInterface) para poder escribir una lambda. C# no: tiene
/// DELEGADOS (Action, Func) desde la version 1, y cualquier lambda encaja
/// en un delegado con la misma firma.
///
/// Aqui mantenemos la interfaz para el modelo de dominio y ofrecemos un
/// puente hacia Action&lt;string, string&gt; para poder usar lambdas.
/// </summary>
public interface INotificador
{
    void Enviar(string destino, string mensaje);

    /// <summary>Metodo default: combina dos notificadores en uno solo.</summary>
    INotificador Y(INotificador otro) => new NotificadorCompuesto(this, otro);

    /// <summary>Static: convierte una lambda en un INotificador.</summary>
    static INotificador Desde(Action<string, string> accion) => new NotificadorLambda(accion);

    /// <summary>Static: implementacion de "no hacer nada", util en pruebas.</summary>
    static INotificador Silencioso() => new NotificadorLambda((_, _) => { });
}

/// <summary>Adaptador: envuelve una lambda (delegado) como INotificador.</summary>
internal sealed class NotificadorLambda(Action<string, string> accion) : INotificador
{
    public void Enviar(string destino, string mensaje) => accion(destino, mensaje);
}

/// <summary>Composite: dos notificadores que se comportan como uno.</summary>
internal sealed class NotificadorCompuesto(INotificador primero, INotificador segundo) : INotificador
{
    public void Enviar(string destino, string mensaje)
    {
        primero.Enviar(destino, mensaje);
        segundo.Enviar(destino, mensaje);
    }
}
