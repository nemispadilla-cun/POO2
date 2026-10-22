namespace Cun.Poo.Modulo04Patrones.Estructurales;

/// <summary>COMPOSITE: la HOJA. No contiene a nadie; su tamano es el propio.</summary>
public class Archivo : IComponenteFs
{
    public Archivo(string nombre, long tamano)
    {
        Nombre = nombre;
        Tamano = tamano;
    }

    public string Nombre { get; }

    public long Tamano { get; }

    public void Imprimir(string sangria = "") =>
        Console.WriteLine($"   {sangria}{Nombre} ({Tamano} KB)");
}
