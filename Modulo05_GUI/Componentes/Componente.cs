namespace Cun.Poo.Modulo05Gui.Componentes;

/// <summary>
/// Raiz de la jerarquia de componentes visuales, como Control en Windows Forms
/// o JComponent en Swing. Un boton, una etiqueta o un panel ES UN Componente:
/// la herencia de G1 aplicada a la interfaz grafica.
///
/// Es una version de juguete para ver por dentro lo que hace un framework real.
/// </summary>
public abstract class Componente
{
    protected Componente(string nombre)
    {
        Nombre = nombre;
    }

    public string Nombre { get; }

    // Posicion y tamano: los asigna el LAYOUT del contenedor, no el programador.
    public int X { get; set; }

    public int Y { get; set; }

    public int Ancho { get; set; }

    public int Alto { get; set; }

    /// <summary>Tamano que el componente quisiera tener (PreferredSize en Swing y WinForms).</summary>
    public abstract (int Ancho, int Alto) TamanoPreferido { get; }

    /// <summary>Como se ve el componente en una linea de texto.</summary>
    public abstract string Dibujar();
}
