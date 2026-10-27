namespace Cun.Poo.Modulo05Gui.Mvc;

/// <summary>
/// Una VISTA de consola para la calculadora. Cumple el mismo contrato que la
/// ventana de Windows Forms (FormCalculadora), por eso el controlador no nota
/// la diferencia.
/// </summary>
public class VistaConsolaCalculadora : ICalculadoraVista
{
    private readonly List<string> _pantallas = [];

    public event Action<string>? TeclaPresionada;

    /// <summary>Simula al usuario presionando una secuencia de teclas, por ejemplo "12+30=".</summary>
    public void Presionar(string teclas)
    {
        _pantallas.Clear();
        foreach (char tecla in teclas)
        {
            TeclaPresionada?.Invoke(tecla.ToString());
        }
        Console.WriteLine($"   Teclas  : {string.Join(" ", teclas.ToCharArray())}");
        Console.WriteLine($"   Pantalla: {string.Join(" > ", _pantallas)}");
    }

    public void MostrarPantalla(string texto) => _pantallas.Add(texto);

    public void MostrarError(string mensaje) => _pantallas.Add("ERROR: " + mensaje);
}
