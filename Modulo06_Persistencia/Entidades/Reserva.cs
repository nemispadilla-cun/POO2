using System.Text.Json.Serialization;

namespace Cun.Poo.Modulo06Persistencia.Entidades;

/// <summary>Tiquete reservado por un pasajero en un viaje.</summary>
public class Reserva
{
    public int Id { get; set; }
    public int ViajeId { get; set; }
    public string Pasajero { get; set; } = "";
    public int Silla { get; set; }

    /// <summary>
    /// Navegacion para el ORM (permite el JOIN). En JSON no se guarda: el
    /// archivo solo necesita el ViajeId, y guardar el viaje completo dentro
    /// de cada reserva duplicaria los datos.
    /// </summary>
    [JsonIgnore]
    public Viaje? Viaje { get; set; }

    public override string ToString() => $"silla {Silla}: {Pasajero}";
}
