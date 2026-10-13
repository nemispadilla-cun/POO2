namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// Segundo contrato, independiente del anterior.
///
/// Una clase en C# hereda de UNA sola clase base, pero puede implementar
/// TANTAS interfaces como necesite.
/// </summary>
public interface IRastreableGps
{
    string ObtenerCoordenadas();

    void EnviarAlertaPosicion();
}
