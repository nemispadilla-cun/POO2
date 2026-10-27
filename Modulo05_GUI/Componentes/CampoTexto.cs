namespace Cun.Poo.Modulo05Gui.Componentes;

/// <summary>
/// Campo de entrada (TextBox / JTextField). Con EsClave = true enmascara lo
/// escrito, como el JPasswordField del cajero (en WinForms: UseSystemPasswordChar).
/// </summary>
public class CampoTexto : Componente
{
    public CampoTexto(string nombre, bool esClave = false) : base(nombre)
    {
        EsClave = esClave;
    }

    public bool EsClave { get; }

    /// <summary>Lo que el usuario escribio. Nunca se muestra si es una clave.</summary>
    public string Texto { get; set; } = "";

    public override (int Ancho, int Alto) TamanoPreferido => (110, 24);

    public override string Dibujar()
    {
        string visible = EsClave ? new string('*', Texto.Length) : Texto;
        return "|" + visible.PadRight(8) + "| ";
    }
}
