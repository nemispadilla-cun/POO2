namespace Cun.Poo.Modulo02Interfaces;

/// <summary>Equilibrado. Tercera subclase sin duplicar una linea del molde.</summary>
public class Arquero(string nombre) : Personaje(nombre, 100, 90)
{
    public override int CostoEnergia => 40;

    public override void UsarHabilidadEspecial() =>
        Console.WriteLine($"   !{Nombre} dispara una lluvia de flechas!");
}
