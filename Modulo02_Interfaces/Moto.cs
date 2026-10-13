namespace Cun.Poo.Modulo02Interfaces;

/// <summary>La flota original de EntregaYa.</summary>
public class Moto(string placa) : Vehiculo(placa, 15)
{
    public override decimal CalcularSoat() => 320_000m;

    protected override double VelocidadPromedioKmh => 40.0;

    protected override decimal CostoPorKm => 300m;
}
