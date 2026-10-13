# Módulo 3 · Estructuras de datos avanzadas (Temática 3 · G3)

El documento maestro explica este tema con el **Java Collections Framework**.
Aquí está resuelto con el equivalente de .NET: `System.Collections.Generic`.

| Ejemplo del recurso | Tipos en este módulo |
|---|---|
| Lista de compras (Éxito / Carulla) | `ListaDeCompras` sobre `List<string>` |
| Deshacer / Ctrl+Z | `EditorDeTexto` sobre `Stack<string>` |
| Cola de soporte (Tigo / Movistar) | `MesaDeAyuda`, `TicketSoporte` sobre `Queue<T>` |
| Cédulas únicas | `HashSet<string>` |
| Directorio de estudiantes por documento | `DirectorioEstudiantes`, `Estudiante` sobre `Dictionary<K,V>` |
| Clase `Inventario` encapsulada con genéricos | `Inventario<T> where T : IIdentificable`, `IIdentificable`, `Producto` |
| Caso "Entregas Andinas S.A.S." | `Grafo`, `Arista`, `MapaBogota`, `CalculadoraDeRutas`, `ResultadoRuta` |
| Tabla de Big O | `ComparadorDeRendimiento` (mide de verdad) |

## Equivalencias Java → C#

| Java (JCF) | C# (`System.Collections.Generic`) |
|---|---|
| `List` / `ArrayList` | `IList<T>` / `List<T>` |
| `LinkedList` | `LinkedList<T>` |
| `Stack` | `Stack<T>` |
| `Queue` (vía `LinkedList`) | `Queue<T>` |
| `Set` / `HashSet` | `ISet<T>` / `HashSet<T>` |
| `TreeSet` | `SortedSet<T>` |
| `Map` / `HashMap` | `IDictionary<K,V>` / `Dictionary<K,V>` |
| `TreeMap` | `SortedDictionary<K,V>` |
| `PriorityQueue<T>` | `PriorityQueue<TElement,TPriority>` |
| Streams (`stream().filter()`) | **LINQ** (`.Where()`, `.GroupBy()`) |

## Big O: lo que mide la demo

| Operación | `List<T>` | `LinkedList<T>` | `HashSet<T>` | `Dictionary<K,V>` |
|---|---|---|---|---|
| Acceso por índice | **O(1)** | O(n) | — | — |
| Buscar un valor | O(n) | O(n) | **O(1)** | **O(1)** por clave |
| Insertar al final | O(1) amortizado | **O(1)** | **O(1)** | **O(1)** |
| Insertar al inicio | O(n) | **O(1)** | — | — |
| Eliminar por clave/valor | O(n) | O(n) + O(1) si tienes el nodo | **O(1)** | **O(1)** |

`ComparadorDeRendimiento` ejecuta estos casos y reporta los milisegundos reales
de tu máquina. En una corrida típica, buscar el peor caso 200 veces sobre
200.000 elementos tarda **~260 ms con `List.Contains`** y **~0,045 ms con
`Dictionary.ContainsKey`**: unas 5.000 veces más rápido.

## El caso de estudio: grafo + Dijkstra
- El mapa es un **grafo** implementado como lista de adyacencia:
  `Dictionary<string, List<Arista>>`. Dos estructuras ya vistas forman una tercera.
- `CalculadoraDeRutas` implementa **Dijkstra** apoyado en una **`PriorityQueue`**,
  que entrega siempre el vértice más cercano aún no visitado en O(log n).
- Resultado: la ruta directa por la avenida tarda **62 min**; la ruta que
  encuentra el algoritmo, con 5 tramos, tarda **47 min**. Un **24 % menos** por entrega.

## Buenas prácticas del recurso
- **Programar hacia la interfaz**: declarar `IList<T>`, `IDictionary<K,V>`,
  `IReadOnlyList<T>`… y no la clase concreta, cuando el tipo se expone.
- **Encapsular la estructura**: `Inventario<T>` esconde su `Dictionary`. Se puede
  cambiar la implementación sin romper a quien use la clase.
- **Elegir antes de escribir**: ¿necesito orden? ¿permito duplicados? ¿la
  operación más frecuente es buscar? Las respuestas señalan la estructura.

## Preguntas para la clase
- ¿Por qué `Dictionary` no garantiza orden? ¿Qué usarías si lo necesitas?
- ¿En qué caso `LinkedList<T>` pierde contra `List<T>` pese a su O(1)?
- ¿Qué pasa si la clave de un `Dictionary` es un objeto mutable y se modifica
  después de insertarlo?
- Dijkstra no admite pesos negativos. ¿Por qué?

## Ejercicio propuesto
Entregas Andinas quiere **recalcular la ruta en caliente**:

1. Agregar a `Grafo` un método `Cerrar(string origen, string destino)` que
   elimine una calle (simula una manifestación en la Av. Caracas).
2. Volver a calcular la ruta a Suba y mostrar cómo cambia el recorrido y el tiempo.
3. Agregar un `SortedDictionary<string, ResultadoRuta>` para listar las entregas
   en orden alfabético sin ordenar manualmente.
4. Medir con `Stopwatch` cuánto tarda Dijkstra sobre el mapa de 8 nodos y
   explicar por qué es despreciable frente al ahorro de 15 min por entrega.
