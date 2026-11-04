using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Cun.Poo.Modulo06Persistencia.Archivos;

/// <summary>
/// Preferencias del usuario de una app de escritorio (caso 2.1 del recurso):
/// idioma, tema y tamano de letra. Pocos datos, de un solo usuario, que deben
/// sobrevivir al cierre de la aplicacion: el caso ideal para un archivo.
///
/// XmlSerializer exige un constructor sin parametros y propiedades publicas
/// con get y set. Los atributos solo cambian el NOMBRE en el archivo.
/// </summary>
[XmlRoot("configuracionUsuario")]
public class ConfiguracionUsuario
{
    [XmlElement("idioma")]
    public string Idioma { get; set; } = "es-CO";

    [XmlElement("tema")]
    public string Tema { get; set; } = "claro";

    [XmlElement("tamanoFuente")]
    public int TamanoFuente { get; set; } = 12;

    /// <summary>Dato calculado: no es estado, no se guarda en ningun formato.</summary>
    [JsonIgnore, XmlIgnore]
    public bool EsModoOscuro => Tema == "oscuro";

    public override string ToString() =>
        $"ConfiguracionUsuario {{ Idioma = {Idioma}, Tema = {Tema}, TamanoFuente = {TamanoFuente} }}";
}
