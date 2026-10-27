namespace Cun.Poo.Modulo05Gui.Mvc;

/// <summary>
/// VISTA vista desde el controlador: lo minimo que debe saber hacer cualquier
/// pantalla de calculadora. El controlador depende de esta interfaz y no de
/// una ventana concreta (inversion de dependencias, G1).
/// </summary>
public interface ICalculadoraVista
{
    /// <summary>La vista avisa que el usuario presiono una tecla; no decide que hacer con ella.</summary>
    event Action<string>? TeclaPresionada;

    void MostrarPantalla(string texto);

    void MostrarError(string mensaje);
}
