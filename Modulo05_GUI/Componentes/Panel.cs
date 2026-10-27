namespace Cun.Poo.Modulo05Gui.Componentes;

/// <summary>
/// CONTENEDOR: un componente que guarda otros componentes (Panel / JPanel).
/// Como puede contener otros paneles, es el patron Composite de G4.
/// </summary>
public class Panel : Componente
{
    private readonly List<Componente> _hijos = [];

    public Panel(string nombre, ILayout layout) : base(nombre)
    {
        Layout = layout;
    }

    /// <summary>Se puede cambiar en caliente: el mismo panel con otra estrategia.</summary>
    public ILayout Layout { get; set; }

    public IReadOnlyList<Componente> Hijos => _hijos;

    public override (int Ancho, int Alto) TamanoPreferido => (Ancho, Alto);

    public void Agregar(Componente componente) => _hijos.Add(componente);

    /// <summary>Cambia el tamano y le pide al layout que reubique a los hijos.</summary>
    public void Redimensionar(int ancho, int alto)
    {
        Ancho = ancho;
        Alto = alto;
        Layout.Organizar(this);
    }

    public override string Dibujar() => $"Panel '{Nombre}' {Ancho}x{Alto}, layout {Layout.Nombre}";

    /// <summary>Muestra el contenido fila por fila, segun las posiciones que calculo el layout.</summary>
    public IEnumerable<string> Renderizar() =>
        _hijos.GroupBy(h => h.Y)
              .OrderBy(fila => fila.Key)
              .Select(fila => string.Concat(fila.OrderBy(h => h.X).Select(h => h.Dibujar())).TrimEnd());
}
