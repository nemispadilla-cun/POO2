namespace Cun.Poo.Modulo01Principios;

/// <summary>Para aplicaciones de escritorio o servidor.</summary>
public class LoggerConsola(string aplicacion) : ILogger
{
    public void Info(string mensaje) =>
        Console.WriteLine($"   [{aplicacion}] {mensaje}");

    public void Error(string mensaje, Exception? causa = null) =>
        Console.WriteLine($"   [{aplicacion}] ERROR: {mensaje}");
}
