# Módulo 06 · Composición y agregación

## Idea central
Además de "es-un" (herencia) existe **"tiene-un"**: un objeto está formado por otros objetos.

| Relación | Fuerza | Ciclo de vida | UML | En este módulo |
|---|---|---|---|---|
| **Composición** | Fuerte | La parte muere con el todo | Rombo negro | `Curso` → `Horario` |
| **Agregación** | Débil | La parte vive por su cuenta | Rombo blanco | `Curso` → `Docente`, `Curso` → `Estudiante` |

```
Universidad ◆── Curso ◆── Horario
                  │
                  ◇── Docente
                  ◇── Estudiante (0..*)
```

## Buenas prácticas que muestra `Curso.java`
- El todo **crea** sus partes compuestas (`agregarHorario` hace el `new Horario`).
- Las colecciones internas se exponen como **vistas de solo lectura** (`Collections.unmodifiableList`).
- El todo **delega**: `Curso.seCruzaCon()` pregunta a cada `Horario`.
- Se detectan duplicados con `equals()` (definido en el módulo 03).

## Composición vs herencia
> "Favor composition over inheritance" (Gamma et al., *Design Patterns*)

- Herencia acopla fuertemente a la superclase y no se puede cambiar en ejecución.
- Composición permite cambiar las partes (`asignarDocente`) y combinar comportamientos.

## Preguntas para la clase
- ¿Qué pasaría si `getHorarios()` devolviera la lista real?
- ¿`Universidad → Curso` es composición o agregación? ¿Por qué?
- ¿Por qué `Curso` no hereda de `ArrayList<Estudiante>` si "tiene" una lista?

## Ejercicio propuesto
Agregar `Salon(String codigo, int capacidad)` y que cada `Horario` tenga un salón. `Universidad` debe detectar si dos cursos usan el mismo salón a la misma hora.
