namespace Cun.Poo.Modulo03Colecciones;

public record TicketSoporte(string Codigo, string Asunto)
{
    public override string ToString() => $"{Codigo}: {Asunto}";
}
