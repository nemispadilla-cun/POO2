namespace Cun.Poo;

/// <summary>
/// Contrato que cumple cada modulo del curso.
///
/// Esta interfaz es, en si misma, un ejemplo de la Tematica 2: el menu
/// principal (Program) no sabe que hace cada modulo por dentro; solo sabe
/// que "tiene un titulo" y que "se puede ejecutar".
/// </summary>
public interface IModulo
{
    /// <summary>Nombre corto que se muestra en el menu.</summary>
    string Titulo { get; }

    /// <summary>Conceptos que ilustra el modulo.</summary>
    string Descripcion { get; }

    /// <summary>Ejecuta la demostracion imprimiendo por consola.</summary>
    void Ejecutar();
}
