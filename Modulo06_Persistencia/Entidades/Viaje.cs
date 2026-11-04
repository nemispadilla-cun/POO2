namespace Cun.Poo.Modulo06Persistencia.Entidades;

/// <summary>
/// Viaje de "Transportes Andinos S.A.S." (caso de estudio 3.1): una ruta, en
/// un bus, con un numero fijo de sillas.
/// </summary>
public class Viaje
{
    public int Id { get; set; }
    public string Ruta { get; set; } = "";
    public string PlacaBus { get; set; } = "";
    public int Capacidad { get; set; }
    public List<Reserva> Reservas { get; set; } = [];

    public override string ToString() => $"Viaje {Id} {Ruta} (bus {PlacaBus}, {Capacidad} sillas)";
}
