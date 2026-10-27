using System.Globalization;

namespace Cun.Poo.Modulo05Gui.Mvc;

/// <summary>
/// MODELO de la calculadora: los datos y las reglas, sin una sola linea de
/// interfaz grafica. Es la CalculadoraLogica que propone el documento maestro
/// (seccion 4.4) para sacar la logica de los eventos de la ventana.
///
/// Como no conoce ninguna vista, la usan igual la consola y Windows Forms.
/// </summary>
public class CalculadoraLogica
{
    private decimal _acumulado;
    private string? _operadorPendiente;
    private string _entrada = "";

    /// <summary>Lo que debe mostrar la pantalla de la calculadora.</summary>
    public string Pantalla { get; private set; } = "0";

    public void AgregarDigito(string digito)
    {
        _entrada += digito;
        Pantalla = _entrada;
    }

    public void AgregarOperador(string operador)
    {
        Resolver();
        _operadorPendiente = operador;
        _entrada = "";
    }

    public void Calcular()
    {
        Resolver();
        _operadorPendiente = null;
        _entrada = "";
    }

    public void Limpiar()
    {
        _acumulado = 0;
        _operadorPendiente = null;
        _entrada = "";
        Pantalla = "0";
    }

    /// <summary>Aplica la operacion pendiente al numero que se acaba de escribir.</summary>
    private void Resolver()
    {
        if (_entrada == "")
        {
            return;                             // dos operadores seguidos: se queda el ultimo
        }

        decimal valor = decimal.Parse(_entrada, CultureInfo.InvariantCulture);
        _acumulado = _operadorPendiente switch
        {
            null => valor,
            "+" => _acumulado + valor,
            "-" => _acumulado - valor,
            "*" => _acumulado * valor,
            "/" when valor == 0 => throw new DivideByZeroException("No se puede dividir entre cero."),
            "/" => _acumulado / valor,
            _ => throw new InvalidOperationException("Operador desconocido: " + _operadorPendiente),
        };
        Pantalla = _acumulado.ToString("0.########", CultureInfo.InvariantCulture);
    }
}
