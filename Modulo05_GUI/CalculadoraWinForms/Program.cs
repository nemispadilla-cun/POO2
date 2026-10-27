using Cun.Poo.Modulo05Gui.Mvc;

namespace Cun.Poo.Modulo05Gui.CalculadoraWinForms;

internal static class Program
{
    /// <summary>
    /// Las tres piezas de MVC se crean y se conectan aqui:
    ///   modelo      -> CalculadoraLogica        (el mismo de la consola)
    ///   vista       -> FormCalculadora          (la unica clase nueva)
    ///   controlador -> ControladorCalculadora   (el mismo de la consola)
    /// </summary>
    [STAThread]                                 // Windows Forms exige un hilo STA
    private static void Main()
    {
        ApplicationConfiguration.Initialize();  // estilos visuales de Windows (el UIManager de Swing)

        var vista = new FormCalculadora();
        _ = new ControladorCalculadora(vista, new CalculadoraLogica());

        Application.Run(vista);                 // bucle de eventos: espera clics hasta cerrar la ventana
    }
}
