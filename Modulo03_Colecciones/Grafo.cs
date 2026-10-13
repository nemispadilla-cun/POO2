namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// GRAFO: estructura NO lineal, ideal para representar redes.
///
/// Se implementa como lista de adyacencia: un Dictionary cuya clave es el
/// vertice y cuyo valor es la List de aristas que salen de el. Es decir, dos
/// de las estructuras ya vistas combinadas para formar una tercera.
///
///     Dictionary&lt;string, List&lt;Arista&gt;&gt;
/// </summary>
public class Grafo
{
    private readonly Dictionary<string, List<Arista>> _adyacencia = [];

    public int Vertices => _adyacencia.Count;

    public int Calles { get; private set; }

    /// <summary>Calle de doble sentido: se registra en ambas direcciones.</summary>
    public void Conectar(string origen, string destino, double minutos)
    {
        Agregar(origen, destino, minutos);
        Agregar(destino, origen, minutos);
        Calles++;
    }

    private void Agregar(string desde, string hacia, double minutos)
    {
        if (!_adyacencia.TryGetValue(desde, out List<Arista>? salidas))
        {
            salidas = [];
            _adyacencia[desde] = salidas;
        }
        salidas.Add(new Arista(hacia, minutos));
    }

    public IReadOnlyList<Arista> Vecinos(string vertice) =>
        _adyacencia.TryGetValue(vertice, out List<Arista>? salidas) ? salidas : [];

    public IEnumerable<string> Nodos => _adyacencia.Keys;
}
