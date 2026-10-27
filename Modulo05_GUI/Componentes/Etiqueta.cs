namespace Cun.Poo.Modulo05Gui.Componentes;

/// <summary>Texto que el usuario lee pero no edita (Label / JLabel).</summary>
public class Etiqueta : Componente
{
    public Etiqueta(string texto) : base("lbl")
    {
        Texto = texto;
    }

    public string Texto { get; set; }

    public override (int Ancho, int Alto) TamanoPreferido => (Texto.Length * 7 + 10, 20);

    public override string Dibujar() => Texto + " ";
}
