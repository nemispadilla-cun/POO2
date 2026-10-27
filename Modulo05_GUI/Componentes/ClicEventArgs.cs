namespace Cun.Poo.Modulo05Gui.Componentes;

/// <summary>
/// OBJETO EVENTO: encapsula la informacion de lo que paso (que boton, cuando).
/// Equivale a ActionEvent en Swing o a EventArgs en Windows Forms.
/// </summary>
public class ClicEventArgs : EventArgs
{
    public ClicEventArgs(string texto, DateTime momento)
    {
        Texto = texto;
        Momento = momento;
    }

    /// <summary>Texto del boton que se presiono.</summary>
    public string Texto { get; }

    public DateTime Momento { get; }
}
