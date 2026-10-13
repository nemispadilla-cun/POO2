namespace Cun.Poo.Modulo02Interfaces;

/// <summary>Segundo medio, mismo contrato: el servicio no nota la diferencia.</summary>
public class NotificadorSms : INotificador
{
    public void Enviar(string destino, string mensaje) =>
        Console.WriteLine($"   [SMS -> {destino}] {mensaje}");     // aqui iria la pasarela
}
