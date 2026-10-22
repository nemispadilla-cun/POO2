using System.Text.Json;

namespace Cun.Poo.Modulo04Patrones.Creacionales;

/// <summary>
/// SINGLETON: una sola instancia y un punto de acceso global.
///
/// Caso del recurso: una fintech de microcreditos carga UNA VEZ las claves de
/// la central de riesgo y la cadena de conexion, y todo el sistema las lee del
/// mismo objeto. Analogia: solo hay un Gerente del Banco de la Republica.
///
/// Como se garantiza en C#:
///   - constructor PRIVADO: nadie fuera de la clase puede hacer "new AppConfig()".
///   - Lazy&lt;T&gt;: crea la instancia la primera vez que se pide y es seguro con
///     varios hilos (en Java se usa getInstance() con doble verificacion).
///   - sealed: nadie puede heredar y crear una "segunda" configuracion.
/// </summary>
public sealed class AppConfig
{
    private static readonly Lazy<AppConfig> _instancia = new(() => new AppConfig());

    private readonly Dictionary<string, string> _valores;

    /// <summary>El equivalente de getInstance() en Java.</summary>
    public static AppConfig Instancia => _instancia.Value;

    /// <summary>Cuantas veces se leyo el archivo. Debe ser siempre 1.</summary>
    public static int VecesCargada { get; private set; }

    /// <summary>Ruta del archivo, relativa al ejecutable.</summary>
    public string Origen { get; }

    private AppConfig()
    {
        VecesCargada++;
        Origen = Path.Combine("Modulo04_Patrones", "Creacionales", "microcreditos.json");
        string ruta = Path.Combine(AppContext.BaseDirectory, Origen);

        // El archivo se copia junto al ejecutable (ver POO.Fundamentos.csproj).
        _valores = File.Exists(ruta)
            ? JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(ruta)) ?? []
            : [];
    }

    public string Obtener(string clave) =>
        _valores.TryGetValue(clave, out string? valor)
            ? valor
            : throw new KeyNotFoundException($"La configuracion no tiene la clave '{clave}'.");

    /// <summary>Para imprimir secretos sin exponerlos: solo los 6 primeros caracteres.</summary>
    public string ObtenerOculto(string clave)
    {
        string valor = Obtener(clave);
        return valor.Length <= 6 ? "******" : valor[..6] + new string('*', 10);
    }
}
