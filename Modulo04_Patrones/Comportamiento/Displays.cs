namespace Cun.Poo.Modulo04Patrones.Comportamiento;

/// <summary>Observador concreto: la pantalla fisica de la estacion.</summary>
public class DisplayPantalla : IObservador
{
    public void Actualizar(double temperatura, double humedad) =>
        Console.WriteLine($"   [Pantalla] {temperatura:0.0} C  |  humedad {humedad:0}%");
}

/// <summary>Observador concreto: la app del celular.</summary>
public class DisplayCelular : IObservador
{
    public void Actualizar(double temperatura, double humedad) =>
        Console.WriteLine($"   [Celular]  Push: ahora hace {temperatura:0.0} C en la estacion");
}

/// <summary>Observador concreto: el registro del sitio web, que guarda el historico.</summary>
public class RegistroWeb : IObservador
{
    private readonly List<double> _historico = [];

    public IReadOnlyList<double> Historico => _historico;

    public void Actualizar(double temperatura, double humedad)
    {
        _historico.Add(temperatura);
        Console.WriteLine($"   [Web]      Registro #{_historico.Count} guardado");
    }
}
