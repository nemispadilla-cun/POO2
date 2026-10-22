namespace Cun.Poo.Modulo04Patrones.Comportamiento;

/// <summary>OBSERVER: lo unico que el sujeto sabe de quien lo observa.</summary>
public interface IObservador
{
    void Actualizar(double temperatura, double humedad);
}

/// <summary>OBSERVER: el SUJETO observado. Mantiene la lista de suscriptores.</summary>
public interface ISujeto
{
    void RegistrarObservador(IObservador observador);

    void RemoverObservador(IObservador observador);

    void NotificarObservadores();
}
