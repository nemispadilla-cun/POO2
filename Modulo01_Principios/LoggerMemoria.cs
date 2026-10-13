namespace Cun.Poo.Modulo01Principios;

/// <summary>Para pruebas automatizadas o la app movil: guarda, no imprime.</summary>
public class LoggerMemoria : ILogger
{
    private readonly List<string> _registros = [];

    public IReadOnlyList<string> Registros => _registros;

    public void Info(string mensaje) => _registros.Add(mensaje);

    public void Error(string mensaje, Exception? causa = null) =>
        _registros.Add("ERROR: " + mensaje);
}
