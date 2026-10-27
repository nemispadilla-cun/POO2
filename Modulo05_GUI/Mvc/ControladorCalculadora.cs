namespace Cun.Poo.Modulo05Gui.Mvc;

/// <summary>
/// CONTROLADOR: recibe los eventos de la vista, los traduce en acciones sobre
/// el modelo y le pide a la vista que se actualice. Es el unico que conoce a los dos.
///
/// En el recurso (Java) el controlador implementa ActionListener y su
/// actionPerformed tiene un switch; aqui es igual, con el event de la vista.
/// </summary>
public class ControladorCalculadora
{
    private readonly ICalculadoraVista _vista;
    private readonly CalculadoraLogica _modelo;

    public ControladorCalculadora(ICalculadoraVista vista, CalculadoraLogica modelo)
    {
        _vista = vista;
        _modelo = modelo;
        _vista.TeclaPresionada += Procesar;     // el controlador es el OYENTE de la vista
        _vista.MostrarPantalla(_modelo.Pantalla);
    }

    private void Procesar(string tecla)
    {
        try
        {
            switch (tecla)
            {
                case "C":
                    _modelo.Limpiar();
                    break;
                case "=":
                    _modelo.Calcular();
                    break;
                case "+" or "-" or "*" or "/":
                    _modelo.AgregarOperador(tecla);
                    break;
                default:
                    _modelo.AgregarDigito(tecla);
                    break;
            }
            _vista.MostrarPantalla(_modelo.Pantalla);
        }
        catch (DivideByZeroException ex)
        {
            _modelo.Limpiar();
            _vista.MostrarError(ex.Message);    // el modelo detecta, la vista muestra
        }
    }
}
