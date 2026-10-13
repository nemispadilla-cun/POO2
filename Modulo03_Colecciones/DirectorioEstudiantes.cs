namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// DICTIONARY&lt;TKey, TValue&gt;: pares clave-valor con busqueda O(1) promedio.
///
/// Caso: la oficina de admisiones consulta un estudiante por su numero de
/// documento. Con una List habria que recorrerla entera: O(n). Con un
/// diccionario, la funcion hash lleva casi directo a la posicion: O(1).
///
/// Equivalencia: Dictionary&lt;K,V&gt; de C#  ==  HashMap de Java.
/// </summary>
public class DirectorioEstudiantes
{
    private readonly Dictionary<string, Estudiante> _porDocumento = [];

    public int Cantidad => _porDocumento.Count;

    /// <summary>Insercion O(1). Si la clave ya existe, reemplaza.</summary>
    public void Inscribir(Estudiante estudiante) =>
        _porDocumento[estudiante.Documento] = estudiante;

    /// <summary>
    /// Busqueda O(1). TryGetValue evita la doble consulta que haria
    /// ContainsKey seguido del indexador.
    /// </summary>
    public Estudiante? Buscar(string documento) =>
        _porDocumento.TryGetValue(documento, out Estudiante? e) ? e : null;

    public bool Retirar(string documento) => _porDocumento.Remove(documento);

    /// <summary>Un Dictionary NO garantiza orden. Para ordenar hay que pedirlo.</summary>
    public IEnumerable<Estudiante> OrdenadosPorNombre() =>
        _porDocumento.Values.OrderBy(e => e.Nombre);
}
