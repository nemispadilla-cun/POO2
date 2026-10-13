namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// Herencia simple + implementacion multiple:
///   : Vehiculo        -> reutiliza placa, formulas, Arrancar / Detenerse
///   , IRastreableGps  -> adquiere un segundo contrato de comportamiento
/// </summary>
public class Dron(string placa) : Vehiculo(placa, 3), IRastreableGps
{
    private const double Latitud = 4.6097;      // Bogota
    private const double Longitud = -74.0817;

    public override decimal CalcularSoat() => 0m;   // requiere registro ante la Aerocivil

    protected override double VelocidadPromedioKmh => 60.0;

    protected override decimal CostoPorKm => 900m;

    // ---------- Contrato IRastreableGps ----------

    public string ObtenerCoordenadas() => $"{Latitud:F4}, {Longitud:F4}";

    public void EnviarAlertaPosicion() =>
        Console.WriteLine($"   Alerta de posicion del dron {Placa} en {ObtenerCoordenadas()}");
}
