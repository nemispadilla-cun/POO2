namespace Cun.Poo;

/// <summary>
/// Clase ABSTRACTA con las utilidades que comparten todos los modulos.
///
/// Las dos piezas de la Tematica 2, una al lado de la otra:
///   IModulo     -> el CONTRATO: lo unico que Program conoce.
///   ModuloBase  -> el MOLDE: codigo compartido que nadie reescribe.
///
/// Nota de C#: una interfaz puede tener metodos "default" desde C# 8, pero
/// solo son visibles a traves de una referencia del tipo de la interfaz.
/// Por eso el codigo compartido va aqui, en una clase abstracta.
/// </summary>
public abstract class ModuloBase : IModulo
{
    public abstract string Titulo { get; }

    public abstract string Descripcion { get; }

    public abstract void Ejecutar();

    /// <summary>Imprime un separador con titulo, para que la salida sea legible.</summary>
    protected static void Seccion(string texto)
    {
        Console.WriteLine();
        Console.WriteLine("-- " + texto + " " + new string('-', Math.Max(0, 60 - texto.Length)));
    }

    /// <summary>Imprime una nota pedagogica destacada.</summary>
    protected static void Nota(string texto)
    {
        Console.WriteLine("   >> " + texto);
    }
}
