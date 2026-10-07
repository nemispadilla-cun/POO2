# Módulo 02 · Encapsulamiento

## Idea central
El objeto **es el dueño de sus datos**. Nadie de afuera los modifica directamente; se pasa por métodos que hacen cumplir las reglas del negocio (**invariantes**).

## Archivos
| Archivo | Rol |
|---|---|
| `CalificacionSinEncapsular.java` | Contraejemplo: atributos públicos, estado inválido posible. |
| `Calificacion.java` | Versión correcta: `private`, getter, setter con validación, comportamiento derivado. |
| `Documento.java` | `record`: inmutabilidad y validación en el constructor compacto. |

## Modificadores de acceso
| Modificador | Visible desde |
|---|---|
| `private` | Solo la propia clase |
| *(sin modificador)* | Clases del mismo paquete |
| `protected` | Mismo paquete + subclases |
| `public` | Todo el mundo |

## Puntos para discutir
- "Getter y setter para todo" **no** es encapsulamiento. Encapsular es decidir **qué** se expone y **con qué reglas**.
- `Calificacion` tiene `getAsignatura()` pero no `setAsignatura()`: decisión consciente.
- La validación está en **un solo lugar** (`setValor`) y el constructor la reutiliza.
- Un `record` es la forma más simple de crear un objeto de solo lectura.

## Ejercicio propuesto
Crear `CuentaAhorro` con `saldo` privado, `consignar(double)` y `retirar(double)`. Reglas: no montos negativos, no retirar más del saldo. Agregar `record Titular(String nombre, Documento documento)`.
