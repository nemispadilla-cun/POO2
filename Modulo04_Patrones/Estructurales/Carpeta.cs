namespace Cun.Poo.Modulo04Patrones.Estructurales;

/// <summary>
/// COMPOSITE: el COMPUESTO. Guarda una lista de IComponenteFs, que pueden ser
/// archivos u otras carpetas. Su tamano se calcula de forma RECURSIVA: le
/// pregunta a cada hijo, sin saber si es hoja o compuesto.
/// </summary>
public class Carpeta : IComponenteFs
{
    private readonly List<IComponenteFs> _hijos = [];

    public Carpeta(string nombre) => Nombre = nombre;

    public string Nombre { get; }

    public long Tamano => _hijos.Sum(hijo => hijo.Tamano);

    public Carpeta Agregar(IComponenteFs componente)
    {
        _hijos.Add(componente);
        return this;
    }

    public void Remover(IComponenteFs componente) => _hijos.Remove(componente);

    public void Imprimir(string sangria = "")
    {
        Console.WriteLine($"   {sangria}{Nombre}/ ({Tamano} KB)");
        foreach (IComponenteFs hijo in _hijos)
        {
            hijo.Imprimir(sangria + "   ");
        }
    }
}
