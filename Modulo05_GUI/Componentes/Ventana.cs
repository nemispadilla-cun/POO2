namespace Cun.Poo.Modulo05Gui.Componentes;

/// <summary>
/// Contenedor principal de la aplicacion (Form en WinForms, JFrame en Swing).
/// Una ventana ES UN panel con titulo: herencia, no copia de codigo.
/// </summary>
public class Ventana : Panel
{
    public Ventana(string titulo, ILayout layout) : base("ventana", layout)
    {
        Titulo = titulo;
    }

    public string Titulo { get; }

    public override string Dibujar() => $"Ventana \"{Titulo}\" {Ancho}x{Alto}, layout {Layout.Nombre}";
}
