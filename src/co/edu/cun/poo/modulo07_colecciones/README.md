# Módulo 07 · Colecciones y genéricos

## Idea central
Las **colecciones** guardan muchos objetos; los **genéricos** (`<T>`) permiten escribir código que funciona para cualquier tipo manteniendo la verificación del compilador.

## Colecciones del JDK que se usan
| Interfaz | Implementación usada | Característica |
|---|---|---|
| `List<E>` | `ArrayList` | Ordenada por posición, permite duplicados |
| `Set<E>` | `HashSet` | Sin duplicados (depende de `equals`/`hashCode`) |
| `Map<K,V>` | `TreeMap`, `LinkedHashMap` | Pares clave-valor; `TreeMap` ordena claves, `LinkedHashMap` conserva inserción |

## Genéricos en el módulo
- **Clase genérica acotada**: `Repositorio<T extends Persona>`. Solo acepta personas; por eso puede llamar `getDocumento()` sobre `T`.
- **Método genérico estático**: `Repositorio.unir(List<E>, String)`.
- **Record genérico con dos tipos**: `Par<A, B>`.

## Otros conceptos
- `Optional<T>`: evita `null` y obliga a tratar el caso "no encontrado".
- `Predicate<T>` + lambda: pasar **comportamiento** como parámetro.
- `Comparator.comparing(...).reversed().thenComparing(...)`: ordenar por varios criterios.
- Streams: `stream().map(...).collect(...)`, `mapToInt(...).average()`.

## Preguntas para la clase
- ¿Por qué el `HashSet` tiene 3 elementos si la lista tiene 4?
- ¿Qué error da el compilador si intentas `Repositorio<String>`?
- ¿Cuál es la diferencia entre `List<Object>` y `List<?>`?

## Ejercicio propuesto
Crear `Pila<T>` (push, pop, peek, estaVacia) usando internamente un `ArrayList<T>`, y usarla para invertir una lista de estudiantes.
