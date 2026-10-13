namespace Cun.Poo.Modulo02Interfaces;

/// <summary>Mucha vida, poca energia. Hereda Moverse, RecibirDanio y JugarTurno.</summary>
public class Guerrero(string nombre) : Personaje(nombre, 150, 60)
{
    public override int CostoEnergia => 30;

    public override void UsarHabilidadEspecial() =>
        Console.WriteLine($"   !{Nombre} ejecuta un golpe devastador con el hacha!");
}
