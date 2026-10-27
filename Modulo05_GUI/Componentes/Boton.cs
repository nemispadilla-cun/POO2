namespace Cun.Poo.Modulo05Gui.Componentes;

/// <summary>
/// FUENTE DEL EVENTO: el componente sobre el que el usuario actua.
/// Quien quiera enterarse del clic se registra con += (el addActionListener de Java).
/// </summary>
public class Boton : Componente
{
    public Boton(string texto) : base("btn" + texto)
    {
        Texto = texto;
    }

    public string Texto { get; }

    /// <summary>Los OYENTES se suscriben aqui. El boton no sabe quienes son ni cuantos.</summary>
    public event EventHandler<ClicEventArgs>? Click;

    public override (int Ancho, int Alto) TamanoPreferido => (Texto.Length * 8 + 24, 30);

    public override string Dibujar()
    {
        int interior = Math.Max(Texto.Length + 2, Ancho / 10 - 2);
        int izquierda = (interior - Texto.Length) / 2;
        return "[" + new string(' ', izquierda) + Texto + new string(' ', interior - Texto.Length - izquierda) + "]";
    }

    /// <summary>
    /// Simula lo que hace el sistema operativo cuando el usuario hace clic:
    /// crea el objeto evento y se lo entrega a cada oyente registrado.
    /// </summary>
    public void SimularClic()
    {
        var evento = new ClicEventArgs(Texto, DateTime.Now);
        Click?.Invoke(this, evento);        // delegacion: la fuente no maneja el evento
    }
}
