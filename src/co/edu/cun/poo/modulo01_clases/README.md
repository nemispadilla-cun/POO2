# Módulo 01 · Clases y objetos

## Idea central
Una **clase** es la plantilla; un **objeto** es una instancia concreta creada con `new`.

## Conceptos que muestra `Estudiante.java`
| Concepto | Dónde verlo |
|---|---|
| Atributos de instancia | `nombre`, `codigo`, `semestre` |
| Atributo de clase (`static`) | `totalCreados` |
| Constructor y sobrecarga de constructores | `Estudiante(String, String, int)` y `Estudiante(String, String)` |
| `this` | Dentro del constructor y en `esDeMismoSemestre` |
| Método de instancia | `presentarse()`, `avanzarSemestre()` |
| Método de clase (`static`) | `contarCreados()` |
| Redefinir `toString()` | Al final de la clase |

## Lo que la demo hace visible
1. Dos objetos de la misma clase tienen estado independiente.
2. Lo `static` es compartido y se accede por la clase.
3. Las variables de tipo objeto guardan **referencias**: `alias = ana` no copia nada.
4. `==` compara referencias, no contenido.

## Preguntas para la clase
- ¿Qué pasaría si `totalCreados` no fuera `static`?
- ¿Por qué `this(nombre, codigo, 1)` debe ser la primera línea del constructor?
- ¿Cuántos objetos `Estudiante` existen al terminar la demo?

## Ejercicio propuesto
Crear la clase `Docente` con `nombre`, `especialidad`, `horasSemana`, un contador estático y un método `asignarHoras(int)` que no permita superar 40 horas.
