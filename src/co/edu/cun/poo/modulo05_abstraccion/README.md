# Módulo 05 · Abstracción: clases abstractas e interfaces

## Idea central
**Abstraer** es quedarse con lo esencial y ocultar el detalle. En Java se materializa con dos herramientas:

| | Clase abstracta | Interfaz |
|---|---|---|
| Se instancia | No | No |
| Puede tener estado (atributos) | Sí | Solo constantes |
| Métodos con código | Sí | Solo `default` y `static` |
| Cuántas puede tener una clase | Una (`extends`) | Muchas (`implements`) |
| Relación que modela | **es-un** (jerarquía) | **puede-hacer** (capacidad, rol) |
| En este módulo | `Evaluacion` | `Entregable`, `Retroalimentable`, `EstadoMatricula` |

## Archivos
- `Evaluacion.java`: clase abstracta con método abstracto `calcularNota()` y método concreto `aporteANotaFinal()` que lo usa (**template method**).
- `Parcial`, `Quiz`, `Proyecto`: tres formas distintas de calcular la nota.
- `Entregable.java`: interfaz con método `default`.
- `Retroalimentable.java`: interfaz simple. `Proyecto` implementa ambas.
- `EstadoMatricula.java`: interfaz **sealed** con tres `record` permitidos.

## Preguntas para la clase
- ¿Por qué `Evaluacion` tiene constructor si no se puede instanciar?
- ¿Qué gana el diseño con que `aporteANotaFinal()` esté en la clase abstracta y no repetido en cada hija?
- ¿Cuándo preferirías un método `default` en la interfaz en vez de una clase abstracta?
- ¿Qué problema evita `sealed`?

## Ejercicio propuesto
Agregar `Taller extends Evaluacion` (nota = promedio de ejercicios entregados a tiempo, penalizando 0.5 por cada uno tardío) y hacer que implemente `Entregable`.
