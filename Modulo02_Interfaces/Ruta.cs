namespace Cun.Poo.Modulo02Interfaces;

/// <summary>Dato inmutable: de donde a donde y cuantos kilometros.</summary>
public record Ruta
{
    public Ruta(string origen, string destino, double distanciaEnKm)
    {
        if (distanciaEnKm <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(distanciaEnKm),
                "La distancia debe ser mayor que cero.");
        }
        Origen = origen;
        Destino = destino;
        DistanciaEnKm = distanciaEnKm;
    }

    public string Origen { get; }

    public string Destino { get; }

    public double DistanciaEnKm { get; }

    public override string ToString() => $"{Origen} -> {Destino} ({DistanciaEnKm} km)";
}
