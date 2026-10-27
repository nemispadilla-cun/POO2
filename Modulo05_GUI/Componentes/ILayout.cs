namespace Cun.Poo.Modulo05Gui.Componentes;

/// <summary>
/// LAYOUT MANAGER: objeto invisible que decide donde va cada componente.
/// Es el patron Strategy de G4: el panel no cambia, cambia la estrategia de ubicacion.
/// </summary>
public interface ILayout
{
    string Nombre { get; }

    /// <summary>Calcula X, Y, Ancho y Alto de cada hijo segun el tamano del contenedor.</summary>
    void Organizar(Panel contenedor);
}
