namespace Cun.Poo.Modulo05Gui.Componentes;

/// <summary>
/// Divide el contenedor en una rejilla de celdas iguales y estira cada
/// componente a su celda (GridLayout en Swing, TableLayoutPanel en WinForms).
/// </summary>
public class LayoutRejilla : ILayout
{
    private readonly int _filas;
    private readonly int _columnas;

    public LayoutRejilla(int filas, int columnas)
    {
        _filas = filas;
        _columnas = columnas;
    }

    public string Nombre => $"rejilla {_filas}x{_columnas} (GridLayout)";

    public void Organizar(Panel contenedor)
    {
        int anchoCelda = contenedor.Ancho / _columnas;
        int altoCelda = contenedor.Alto / _filas;
        for (int i = 0; i < contenedor.Hijos.Count; i++)
        {
            Componente hijo = contenedor.Hijos[i];
            hijo.X = (i % _columnas) * anchoCelda;
            hijo.Y = (i / _columnas) * altoCelda;
            hijo.Ancho = anchoCelda;            // se ignora el tamano preferido
            hijo.Alto = altoCelda;
        }
    }
}
