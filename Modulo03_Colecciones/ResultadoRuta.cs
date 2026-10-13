namespace Cun.Poo.Modulo03Colecciones;

/// <summary>Resultado de Dijkstra para un destino.</summary>
public record ResultadoRuta(string Destino, double Minutos, IReadOnlyList<string> Camino)
{
    public string CaminoLegible => string.Join(" -> ", Camino);
}
