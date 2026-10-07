# Módulo 08 · Manejo de excepciones

## Idea central
Una excepción es un **objeto** que representa una situación anormal. Separa el **flujo normal** del **flujo de error** y obliga a decidir quién y cómo atiende cada fallo.

## Jerarquía del JDK (resumen)
```
Throwable
 ├── Error                      (fallos de la JVM: no se capturan)
 └── Exception
      ├── RuntimeException       (UNCHECKED: el compilador no obliga a capturar)
      │    ├── IllegalArgumentException
      │    ├── IllegalStateException
      │    ├── NullPointerException
      │    └── NotaInvalidaException   <- nuestra
      └── (las demás)             (CHECKED: hay que capturar o declarar throws)
           └── MatriculaException      <- nuestra
                ├── CupoAgotadoException
                └── PrerrequisitoException
```

## Checked vs unchecked
| | Checked | Unchecked |
|---|---|---|
| Extiende | `Exception` | `RuntimeException` |
| El compilador exige `try` o `throws` | Sí | No |
| Para qué | Fallas **esperables del negocio** que el llamador puede manejar | **Errores de programación** o de estado |
| Ejemplo aquí | `MatriculaException` | `NotaInvalidaException` |

## Construcciones que se muestran
1. `try / catch / finally`.
2. Varios `catch` ordenados **del más específico al más general**.
3. `throws` en la firma para propagar.
4. `try-with-resources` con `AutoCloseable` (`ConexionSimulada`).
5. Multi-catch `catch (A | B e)`.
6. **Encadenamiento**: `new IllegalStateException(mensaje, causa)` y `getCause()`.
7. Excepciones propias **con datos** (`getCodigoCurso()`, `getCupoMaximo()`).

## Reglas de diseño
- El que **detecta** el problema lanza; el que **sabe qué hacer** captura. `Grupo` lanza, la demo decide.
- Nunca un `catch` vacío.
- No uses excepciones para control de flujo normal.
- Traduce excepciones entre capas conservando la causa.

## Preguntas para la clase
- ¿Qué pasa si intercambias el orden de los `catch` en la sección 2?
- ¿Por qué `registrarNota` lanza unchecked y `matricular` lanza checked?
- ¿En qué momento exacto se ejecuta `close()` en la sección 5?

## Ejercicio propuesto
Crear `SaldoInsuficienteException` (checked, con `faltante`) y `CuentaBloqueadaException` para la `CuentaAhorro` del módulo 02. Escribir un `Cajero` que intente varios retiros y reporte cada fallo sin detenerse.
