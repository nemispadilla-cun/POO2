namespace Cun.Poo.Modulo05Gui.Componentes;

/// <summary>
/// Pone los componentes uno tras otro y salta de linea cuando no caben
/// (FlowLayout en Swing, FlowLayoutPanel en WinForms).
/// </summary>
public class LayoutFlujo : ILayout
{
    private const int Separacion = 5;

    public string Nombre => "flujo (FlowLayout)";

    public void Organizar(Panel contenedor)
    {
        int x = Separacion, y = Separacion, altoFila = 0;
        foreach (Componente hijo in contenedor.Hijos)
        {
            (hijo.Ancho, hijo.Alto) = hijo.TamanoPreferido;
            if (x + hijo.Ancho > contenedor.Ancho && x > Separacion)
            {
                x = Separacion;                 // no cabe: siguiente linea
                y += altoFila + Separacion;
                altoFila = 0;
            }
            hijo.X = x;
            hijo.Y = y;
            x += hijo.Ancho + Separacion;
            altoFila = Math.Max(altoFila, hijo.Alto);
        }
    }
}
