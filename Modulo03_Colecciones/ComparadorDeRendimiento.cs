using System.Diagnostics;

namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// Mide de verdad lo que dice la tabla de Big O. Los numeros que imprime
/// salen de esta maquina, en esta ejecucion.
/// </summary>
public static class ComparadorDeRendimiento
{
    public static void BusquedaListaVsDiccionario(int elementos)
    {
        var lista = new List<string>(elementos);
        var diccionario = new Dictionary<string, int>(elementos);

        for (int i = 0; i < elementos; i++)
        {
            string clave = "CC-" + i;
            lista.Add(clave);
            diccionario[clave] = i;
        }

        // El peor caso de la busqueda lineal: el ultimo elemento.
        string buscado = "CC-" + (elementos - 1);
        const int repeticiones = 200;

        var reloj = Stopwatch.StartNew();
        for (int i = 0; i < repeticiones; i++)
        {
            _ = lista.Contains(buscado);              // O(n)
        }
        reloj.Stop();
        double msLista = reloj.Elapsed.TotalMilliseconds;

        reloj.Restart();
        for (int i = 0; i < repeticiones; i++)
        {
            _ = diccionario.ContainsKey(buscado);     // O(1)
        }
        reloj.Stop();
        double msDiccionario = reloj.Elapsed.TotalMilliseconds;

        Console.WriteLine($"   {elementos:N0} elementos, {repeticiones} busquedas del peor caso:");
        Console.WriteLine($"     List.Contains       O(n)  -> {msLista,8:F2} ms");
        Console.WriteLine($"     Dictionary.Contains O(1)  -> {msDiccionario,8:F3} ms");

        if (msDiccionario > 0)
        {
            Console.WriteLine($"     El diccionario fue ~{msLista / msDiccionario:F0}x mas rapido.");
        }
    }

    public static void InsercionAlInicio(int elementos)
    {
        var lista = new List<int>();
        var enlazada = new LinkedList<int>();

        var reloj = Stopwatch.StartNew();
        for (int i = 0; i < elementos; i++)
        {
            lista.Insert(0, i);              // O(n): desplaza todo el arreglo
        }
        reloj.Stop();
        double msLista = reloj.Elapsed.TotalMilliseconds;

        reloj.Restart();
        for (int i = 0; i < elementos; i++)
        {
            enlazada.AddFirst(i);            // O(1): solo reengancha un nodo
        }
        reloj.Stop();
        double msEnlazada = reloj.Elapsed.TotalMilliseconds;

        Console.WriteLine($"   {elementos:N0} inserciones al inicio:");
        Console.WriteLine($"     List.Insert(0, x)       O(n) -> {msLista,8:F2} ms");
        Console.WriteLine($"     LinkedList.AddFirst(x)  O(1) -> {msEnlazada,8:F3} ms");
    }
}
