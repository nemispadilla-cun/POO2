namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// Una calle del mapa: a donde lleva y cuanto cuesta recorrerla.
/// El "peso" son minutos estimados considerando el trafico.
/// </summary>
public record Arista(string Destino, double Minutos);
