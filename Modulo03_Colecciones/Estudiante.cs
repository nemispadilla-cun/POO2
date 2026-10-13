namespace Cun.Poo.Modulo03Colecciones;

public record Estudiante(string Documento, string Nombre, string Programa, int Semestre)
{
    public override string ToString() => $"{Nombre} - {Programa}, semestre {Semestre}";
}
