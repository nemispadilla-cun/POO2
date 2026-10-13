namespace Cun.Poo.Modulo02Interfaces;

/// <summary>Menos vida, mas energia. Solo implementa lo que cambia.</summary>
public class Mago(string nombre) : Personaje(nombre, 70, 150)
{
    public override int CostoEnergia => 50;

    public override void UsarHabilidadEspecial() =>
        Console.WriteLine($"   !{Nombre} lanza una bola de fuego!");
}
