namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// Datos del caso de estudio "Entregas Andinas S.A.S.".
/// Red simplificada de la zona nororiental de Bogota, con los minutos
/// promedio de recorrido en hora pico.
/// </summary>
public static class MapaBogota
{
    public const string CentroDistribucion = "Centro de distribucion";

    /// <summary>
    /// Lo que tarda la avenida directa en hora pico: es la ruta "obvia", la que
    /// el conductor elegiria a ojo. Dijkstra tiene que superarla.
    /// </summary>
    public const double MinutosAvenidaDirectaASuba = 62;

    public static Grafo Construir()
    {
        var g = new Grafo();
        g.Conectar(CentroDistribucion, "Calle 26", 8);
        g.Conectar(CentroDistribucion, "Chapinero", 18);
        g.Conectar(CentroDistribucion, "Suba", MinutosAvenidaDirectaASuba);
        g.Conectar("Calle 26", "Chapinero", 7);
        g.Conectar("Chapinero", "Zona T", 6);
        g.Conectar("Chapinero", "Calle 85", 11);
        g.Conectar("Zona T", "Calle 85", 4);
        g.Conectar("Calle 85", "Usaquen", 9);
        g.Conectar("Calle 85", "Cedritos", 12);
        g.Conectar("Calle 85", "Suba", 22);
        g.Conectar("Usaquen", "Cedritos", 5);
        g.Conectar("Cedritos", "Suba", 14);
        return g;
    }

    public static IReadOnlyList<string> Entregas =>
        ["Suba", "Usaquen", "Zona T", "Cedritos"];
}
