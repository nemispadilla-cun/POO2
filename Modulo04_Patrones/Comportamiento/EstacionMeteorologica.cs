namespace Cun.Poo.Modulo04Patrones.Comportamiento;

/// <summary>
/// OBSERVER: el SUJETO concreto. Mide temperatura y humedad y, cuando cambian,
/// avisa a todos sus observadores. No sabe si del otro lado hay una pantalla,
/// un celular o un sitio web: solo conoce IObservador.
///
/// Relacion uno-a-muchos, como El Tiempo con sus suscriptores: nadie tiene
/// que estar preguntando "hay algo nuevo?".
///
/// Nota de C#: el lenguaje trae Observer incorporado con "event". La clase
/// expone las dos formas para compararlas en clase.
/// </summary>
public class EstacionMeteorologica : ISujeto
{
    private readonly List<IObservador> _observadores = [];

    public double Temperatura { get; private set; }

    public double Humedad { get; private set; }

    /// <summary>La misma idea con la sintaxis propia de C#: delegado + event.</summary>
    public event Action<double, double>? MedicionCambio;

    public void RegistrarObservador(IObservador observador) => _observadores.Add(observador);

    public void RemoverObservador(IObservador observador) => _observadores.Remove(observador);

    public void NotificarObservadores()
    {
        foreach (IObservador observador in _observadores)
        {
            observador.Actualizar(Temperatura, Humedad);
        }
        MedicionCambio?.Invoke(Temperatura, Humedad);
    }

    public void SetMediciones(double temperatura, double humedad)
    {
        Temperatura = temperatura;
        Humedad = humedad;
        NotificarObservadores();            // el cambio de estado dispara el aviso
    }
}
