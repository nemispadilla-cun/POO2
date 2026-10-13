namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// Las dos herramientas trabajando juntas:
///   - clase ABSTRACTA -> comparte estado (placa, capacidad) y las FORMULAS;
///   - interfaz        -> publica el contrato que el despacho consume.
///
/// Cada subclase solo aporta sus numeros; la formula se escribe una sola vez.
/// </summary>
public abstract class Vehiculo(string placa, double capacidadDeCargaKg) : IEstrategiaDeTransporte
{
    protected string Placa { get; } = placa;

    protected double CapacidadDeCargaKg { get; } = capacidadDeCargaKg;

    // ---------- Comportamiento concreto compartido ----------

    public void Arrancar() =>
        Console.WriteLine($"   El vehiculo con placa {Placa} ha arrancado.");

    public void Detenerse() =>
        Console.WriteLine($"   El vehiculo con placa {Placa} se ha detenido.");

    // ---------- Lo que CADA subclase debe aportar ----------

    public abstract decimal CalcularSoat();

    protected abstract double VelocidadPromedioKmh { get; }

    protected abstract decimal CostoPorKm { get; }

    // ---------- El contrato se cumple UNA vez, aqui ----------

    public double CalcularTiempoEstimado(Ruta ruta) =>
        ruta.DistanciaEnKm / VelocidadPromedioKmh * 60;      // minutos

    public decimal CalcularCostoEnvio(Ruta ruta) =>
        (decimal)ruta.DistanciaEnKm * CostoPorKm;

    public string Medio => $"{GetType().Name} {Placa}";
}
