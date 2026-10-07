# Módulo 03 · Herencia

## Idea central
Una subclase **extiende** a una superclase: hereda sus atributos y métodos, puede **agregar** nuevos y **sobreescribir** los existentes. Relación **"es-un"** (is-a).

```
Object
 └── Persona
      ├── Estudiante
      └── Docente
           └── DocenteInvestigador
```

## Palabras clave
| Palabra | Para qué |
|---|---|
| `extends` | Declarar la herencia |
| `super(...)` | Llamar al constructor del padre (siempre primera línea) |
| `super.metodo()` | Invocar la versión del padre dentro de una sobreescritura |
| `protected` | Visible para las subclases |
| `@Override` | Pedirle al compilador que verifique que realmente se sobreescribe |
| `final` (en clase o método) | Prohibir heredar o sobreescribir |

## Reglas que conviene recordar
1. Java solo permite herencia **simple** de clases (una sola superclase). Para "heredar" varios contratos se usan interfaces (módulo 5).
2. Los constructores **no** se heredan; se invocan en cadena con `super(...)`.
3. Si redefines `equals()`, redefine `hashCode()` con los mismos campos.
4. Herencia es una relación fuerte. Antes de usarla pregunta: ¿realmente "X es-un Y"? Si la respuesta es "X tiene-un Y", usa composición (módulo 6).

## Preguntas para la clase
- ¿Qué imprime `di.getCorreoInstitucional()` y de qué clase viene ese método?
- ¿Qué pasa si quitas `@Override` y escribes `presentarse()` con una letra cambiada?
- ¿Por qué `Persona.equals` compara `getClass()` y no usa `instanceof`?

## Ejercicio propuesto
Agregar `Administrativo extends Persona` con `cargo` y `dependencia`, y `Monitor extends Estudiante` (estudiante que apoya una asignatura) con `asignaturaApoyo` y `horasSemana`.
