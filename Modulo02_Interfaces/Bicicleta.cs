namespace Cun.Poo.Modulo02Interfaces;

/// <summary>Distancias cortas y cero emisiones: clase nueva, despacho intacto.</summary>
public class Bicicleta(string placa) : Vehiculo(placa, 8)
{
    public override decimal CalcularSoat() => 0m;   // no pagan SOAT en Colombia

    protected override double VelocidadPromedioKmh => 15.0;

    protected override decimal CostoPorKm => 150m;
}
