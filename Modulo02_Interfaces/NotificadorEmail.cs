namespace Cun.Poo.Modulo02Interfaces;

/// <summary>Implementacion clasica del contrato, como clase con nombre.</summary>
public class NotificadorEmail : INotificador
{
    public void Enviar(string destino, string mensaje) =>
        Console.WriteLine($"   [Email -> {destino}] {mensaje}");   // aqui iria SMTP
}
