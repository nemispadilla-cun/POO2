# Módulo 04 · Polimorfismo

## Idea central
**Un mismo mensaje, muchas respuestas.** Una variable de tipo `Persona` puede apuntar a un `Estudiante`, un `Docente` o un `DocenteInvestigador`, y al llamar `presentarse()` Java ejecuta la versión del **objeto real**, no la del tipo declarado.

Este módulo **reutiliza** las clases del módulo 03.

## Dos tipos de polimorfismo
| | Sobrecarga (overloading) | Sobreescritura (overriding) |
|---|---|---|
| Qué es | Mismo nombre, **distintos parámetros**, en la misma clase | **Misma firma** en la subclase |
| Cuándo se decide | Al **compilar** (estático) | Al **ejecutar** (dinámico, *late binding*) |
| Ejemplo | `LiquidadorNomina.calcularPago(...)` ×3 | `presentarse()` en cada subclase |

## Conceptos en la demo
1. **Upcasting** implícito: `Persona p = new Estudiante(...)`.
2. **Enlace dinámico**: el bucle sobre `List<Persona>` llama al método correcto sin `if`.
3. El **tipo declarado** limita qué métodos se pueden invocar.
4. **Downcasting** seguro con `instanceof` + *pattern matching* (`if (p instanceof Docente doc)`).
5. Cast incorrecto produce `ClassCastException` en ejecución.

## Preguntas para la clase
- ¿Por qué el bucle de la sección 1 no necesita cambiar si agregamos una clase nueva?
- ¿Por qué `DocenteInvestigador` entra en `instanceof Docente`?
- ¿La sobrecarga es "verdadero" polimorfismo? (Debate: algunos autores solo consideran la sobreescritura.)

## Ejercicio propuesto
Crear `interface Saludable { String saludar(); }`, implementarla en `Estudiante` y `Docente` de formas distintas, y recorrer una `List<Saludable>`.
