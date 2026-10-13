namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// ALGORITMO DE DIJKSTRA sobre el grafo de la ciudad.
///
/// La pieza clave es la PRIORITYQUEUE: en cada paso hay que explorar el
/// vertice mas cercano que todavia no se ha visitado. Una cola de prioridad
/// entrega siempre el minimo en O(log n); hacerlo con una List obligaria a
/// recorrerla entera cada vez, O(n).
///
/// PriorityQueue&lt;TElement, TPriority&gt; existe en .NET desde la version 6.
/// </summary>
public class CalculadoraDeRutas(Grafo grafo)
{
    public IReadOnlyDictionary<string, ResultadoRuta> DesdeOrigen(string origen)
    {
        var distancia = new Dictionary<string, double> { [origen] = 0 };
        var anterior = new Dictionary<string, string>();
        var visitados = new HashSet<string>();

        // La cola ordena automaticamente por distancia acumulada.
        var cola = new PriorityQueue<string, double>();
        cola.Enqueue(origen, 0);

        while (cola.TryDequeue(out string? actual, out double acumulado))
        {
            // Un vertice puede estar encolado varias veces con distintas
            // prioridades; nos quedamos con la primera vez que sale (la menor).
            if (!visitados.Add(actual))
            {
                continue;
            }

            foreach (Arista calle in grafo.Vecinos(actual))
            {
                double candidata = acumulado + calle.Minutos;
                double conocida = distancia.GetValueOrDefault(calle.Destino, double.PositiveInfinity);

                if (candidata < conocida)
                {
                    distancia[calle.Destino] = candidata;
                    anterior[calle.Destino] = actual;
                    cola.Enqueue(calle.Destino, candidata);
                }
            }
        }

        return distancia.ToDictionary(
            par => par.Key,
            par => new ResultadoRuta(par.Key, par.Value, Reconstruir(anterior, origen, par.Key)));
    }

    private static List<string> Reconstruir(
        Dictionary<string, string> anterior, string origen, string destino)
    {
        var camino = new List<string>();
        string? paso = destino;

        while (paso is not null)
        {
            camino.Add(paso);
            if (paso == origen)
            {
                break;
            }
            paso = anterior.GetValueOrDefault(paso);
        }

        camino.Reverse();
        return camino;
    }
}
