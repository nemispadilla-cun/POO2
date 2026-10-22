namespace Cun.Poo.Modulo01Principios;

/// <summary>
/// REUTILIZACION: una interfaz estandar y pequena. Se escribe una vez y la
/// usan todos los servicios de la aplicacion.
/// </summary>
public interface ILogger
{
    void Info(string mensaje);

    void Error(string mensaje, Exception? causa = null);

    /// <summary>
    /// Metodo DEFAULT de interfaz (C# 8). Todas las implementaciones lo
    /// heredan sin escribirlo. Ojo: solo es visible a traves de una
    /// referencia de tipo ILogger.
    /// </summary>
    //void Advertencia(string mensaje) => Info("ADVERTENCIA: " + mensaje);
    void Advertencia(string mensaje) {
        Info("ADVERTENCIA: " + mensaje);
    }
}
