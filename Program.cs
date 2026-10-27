using System.Text;

using Cun.Poo;
using Cun.Poo.Modulo01Principios;
using Cun.Poo.Modulo02Interfaces;
using Cun.Poo.Modulo03Colecciones;
using Cun.Poo.Modulo04Patrones;
using Cun.Poo.Modulo05Gui;

// Punto de entrada del proyecto.
//
//   dotnet run                 -> menu interactivo
//   dotnet run -- 2            -> ejecuta solo el modulo 2
//   dotnet run -- 1 3          -> ejecuta los modulos 1 y 3
//   dotnet run -- todos        -> ejecuta todos en orden

Console.OutputEncoding = Encoding.UTF8;

// La lista es del tipo de la INTERFAZ, no de cada clase concreta.
// Agregar un modulo nuevo es crear una clase y anadirla aqui.
List<IModulo> modulos =
[
    new Demo01Principios(),
    new Demo02Interfaces(),
    new Demo03Colecciones(),
    new Demo04Patrones(),
    new Demo05Gui(),
];

if (args.Length > 0)
{
    EjecutarDesdeArgumentos(args);
    return;
}

MenuInteractivo();

void EjecutarDesdeArgumentos(string[] argumentos)
{
    if (argumentos.Length == 1 && argumentos[0].Equals("todos", StringComparison.OrdinalIgnoreCase))
    {
        for (int i = 0; i < modulos.Count; i++)
        {
            EjecutarModulo(i);
        }
        return;
    }

    foreach (string arg in argumentos)
    {
        if (int.TryParse(arg, out int numero))
        {
            EjecutarModulo(numero - 1);
        }
        else
        {
            Console.WriteLine("Argumento no valido: " + arg);
        }
    }
}

void MenuInteractivo()
{
    while (true)
    {
        ImprimirMenu();
        Console.Write("Elige una opcion: ");
        string entrada = (Console.ReadLine() ?? "0").Trim();

        if (entrada == "0" || entrada.Equals("salir", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Hasta la proxima clase.");
            return;
        }

        if (entrada.Equals("t", StringComparison.OrdinalIgnoreCase) ||
            entrada.Equals("todos", StringComparison.OrdinalIgnoreCase))
        {
            for (int i = 0; i < modulos.Count; i++)
            {
                EjecutarModulo(i);
            }
            continue;
        }

        if (int.TryParse(entrada, out int opcion))
        {
            EjecutarModulo(opcion - 1);
        }
        else
        {
            Console.WriteLine("Opcion no reconocida.\n");
        }
    }
}

void ImprimirMenu()
{
    Console.WriteLine();
    Console.WriteLine("==============================================================");
    Console.WriteLine("    PROGRAMACION ORIENTADA A OBJETOS II - C# / .NET - CUN     ");
    Console.WriteLine("==============================================================");
    for (int i = 0; i < modulos.Count; i++)
    {
        Console.WriteLine($"  {i + 1}. {modulos[i].Titulo}");
    }
    Console.WriteLine("  T. Ejecutar todos");
    Console.WriteLine("  0. Salir");
}

void EjecutarModulo(int indice)
{
    if (indice < 0 || indice >= modulos.Count)
    {
        Console.WriteLine($"No existe el modulo {indice + 1}.\n");
        return;
    }

    IModulo modulo = modulos[indice];   // referencia polimorfica
    Console.WriteLine();
    Console.WriteLine("==============================================================");
    Console.WriteLine($" MODULO {indice + 1}: {modulo.Titulo.ToUpperInvariant()}");
    Console.WriteLine($" {modulo.Descripcion}");
    Console.WriteLine("==============================================================");
    modulo.Ejecutar();                  // enlace dinamico
    Console.WriteLine();
}
