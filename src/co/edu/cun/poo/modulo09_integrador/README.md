# Módulo 09 · Caso integrador: sistema de matrículas

## Objetivo
Ver **todos los conceptos trabajando juntos** en un escenario realista, y observar cómo cada clase tiene una única responsabilidad y colabora con las demás.

## Mapa de conceptos → clases
| Concepto | Clase(s) | Módulo de origen |
|---|---|---|
| Clases, objetos, encapsulamiento | `Matricula`, `SistemaMatriculas` | 01, 02 |
| Herencia y polimorfismo | `Persona` → `Estudiante`, `Docente`; `Evaluacion` → `Parcial`, `Quiz`, `Proyecto` | 03, 04, 05 |
| Interfaz funcional + Strategy | `PoliticaDescuento` (constantes con lambdas, método `default y(...)`) | 05 |
| Composición | `Universidad` → `Curso` → `Horario`; `Matricula` → `List<Evaluacion>` | 06 |
| Genéricos y colecciones | `Repositorio<Estudiante>`, `List`, `Set`, streams | 07 |
| Excepciones | `MatriculaException`, `CupoAgotadoException`, `PrerrequisitoException` | 08 |
| Fachada / delegación | `SistemaMatriculas` orquesta sin tener casi lógica propia | — |

## Escenario que ejecuta la demo
1. Se abre la universidad con dos cursos y un docente.
2. Se registran cinco estudiantes en el repositorio genérico.
3. Se activa una política de descuento **combinada** (antigüedad + primer semestre) y se matricula: hay casos de éxito, sin prerrequisito, cupo agotado y documento inexistente. El programa **no se detiene** en ningún fallo.
4. Se registran evaluaciones de distinto tipo y se calcula la definitiva polimórficamente; se rechaza superar el 100 %.
5. Reporte final con streams.

## Preguntas para la clase
- ¿Qué habría que cambiar para agregar una política "descuento por promedio"? (Respuesta esperada: nada en `SistemaMatriculas`.)
- ¿Por qué `SistemaMatriculas.matricular` lanza `MatriculaException` (checked) pero `IllegalArgumentException` (unchecked) cuando el documento no existe?
- Identifica una relación de composición y una de agregación en el escenario.

## Proyecto propuesto para el curso
Extender el sistema con: `Semestre` (agrupa matrículas por periodo), cálculo de **promedio ponderado** por créditos, persistencia en archivo CSV con `try-with-resources`, y una política de descuento leída desde un archivo de configuración.
