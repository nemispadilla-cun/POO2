namespace Cun.Poo.Modulo04Patrones.Estructurales;

/// <summary>
/// COMPOSITE: el COMPONENTE comun a hojas (archivos) y compuestos (carpetas).
/// El cliente trata a los dos igual: les pide el tamano y ya.
/// </summary>
public interface IComponenteFs
{
    string Nombre { get; }

    /// <summary>Tamano en KB.</summary>
    long Tamano { get; }

    void Imprimir(string sangria = "");
}
