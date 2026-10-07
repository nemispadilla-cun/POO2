# POO-Fundamentos · Programación Orientada a Objetos II (Java)

Proyecto de apoyo para la clase. Cada **módulo** es una demostración ejecutable, con código comentado en español y un `README.md` con la explicación, preguntas para discutir y un ejercicio propuesto.

Todo el proyecto usa **un solo dominio**: una universidad con personas, cursos, matrículas, evaluaciones y pagos. Así los conceptos se conectan entre sí en vez de ser ejemplos aislados.

## Requisitos
- **JDK 17 o superior** (el código se compila con `--release 17`, así funciona en 17, 21, 26...).
- Sin Maven, sin Gradle, sin dependencias: solo `javac` y `java`.

## Cómo ejecutar

### Windows
```bat
compilar.bat
ejecutar.bat            :: menú interactivo
ejecutar.bat 3          :: solo el módulo 3
ejecutar.bat 1 4 7      :: varios módulos
ejecutar.bat todos      :: todos en orden
```

### Linux / macOS / Git Bash
```bash
chmod +x *.sh
./compilar.sh
./ejecutar.sh           # menú interactivo
./ejecutar.sh 3
./ejecutar.sh todos
```

### Desde un IDE (IntelliJ, Eclipse, VS Code, NetBeans)
Abre la carpeta como proyecto Java, marca `src` como carpeta de fuentes y ejecuta `co.edu.cun.poo.Main`.

## Módulos

| # | Módulo | Conceptos | Clases clave |
|---|---|---|---|
| 01 | [Clases y objetos](src/co/edu/cun/poo/modulo01_clases/README.md) | Clase vs objeto, atributos, constructores, `this`, `static`, referencias | `Estudiante` |
| 02 | [Encapsulamiento](src/co/edu/cun/poo/modulo02_encapsulamiento/README.md) | `private`, getters/setters con validación, invariantes, inmutabilidad, `record` | `Calificacion`, `Documento` |
| 03 | [Herencia](src/co/edu/cun/poo/modulo03_herencia/README.md) | `extends`, `super`, `protected`, `@Override`, multinivel, `equals`/`hashCode` | `Persona` → `Estudiante`, `Docente` → `DocenteInvestigador` |
| 04 | [Polimorfismo](src/co/edu/cun/poo/modulo04_polimorfismo/README.md) | Sobreescritura vs sobrecarga, enlace dinámico, casting, `instanceof` | `LiquidadorNomina` |
| 05 | [Abstracción](src/co/edu/cun/poo/modulo05_abstraccion/README.md) | `abstract`, template method, interfaces, `default`, `sealed` | `Evaluacion`, `Entregable`, `EstadoMatricula` |
| 06 | [Composición](src/co/edu/cun/poo/modulo06_composicion/README.md) | Tiene-un, composición vs agregación, delegación, vistas de solo lectura | `Universidad`, `Curso`, `Horario` |
| 07 | [Colecciones y genéricos](src/co/edu/cun/poo/modulo07_colecciones/README.md) | `List`/`Set`/`Map`, `<T extends ...>`, `Optional`, `Comparator`, streams | `Repositorio<T>`, `Par<A,B>` |
| 08 | [Excepciones](src/co/edu/cun/poo/modulo08_excepciones/README.md) | checked vs unchecked, jerarquía propia, `finally`, try-with-resources, causa | `MatriculaException`, `Grupo` |
| 09 | [Caso integrador](src/co/edu/cun/poo/modulo09_integrador/README.md) | Todo lo anterior junto + Strategy con lambdas | `SistemaMatriculas`, `Matricula`, `PoliticaDescuento` |
| 10 | [Principios avanzados de POO](src/co/edu/cun/poo/modulo10_principios/README.md) · **Temática 1 (G1)** | Cohesión alta, bajo acoplamiento, reutilización, SOLID, manejo de excepciones | `Factura`, `ServicioAutenticacion`, `Logger`, `ProcesadorDePagos` |
| 11 | [Interfaces y clases abstractas](src/co/edu/cun/poo/modulo11_interfaces/README.md) · **Temática 2 (G2)** | Contrato vs molde, `default`/`static`, template method, herencia + implementación múltiple, Strategy, interfaces funcionales | `MetodoDePago`, `Personaje`, `Vehiculo`, `EstrategiaDeTransporte`, `Notificador` |

> **Para la clase de la Temática 1 (recurso G1):** ejecutar `ejecutar.bat 10`. Los módulos 1 a 9 son el repaso de fundamentos que ese material da por sabidos (encapsulamiento, herencia, polimorfismo, interfaces, excepciones) y sirven de apoyo cuando haga falta volver atrás.
>
> **Para la clase de la Temática 2 (recurso G2):** ejecutar `ejecutar.bat 11`. El módulo 5 (abstracción) es el repaso previo; el módulo 11 retoma esos mecanismos con los ejemplos del documento maestro (pagos en Colombia, personajes de videojuego, flota de EntregaYa y notificaciones con lambdas).

## Estructura
```
POO-Fundamentos/
├── compilar.bat / compilar.sh
├── ejecutar.bat / ejecutar.sh
├── README.md
└── src/co/edu/cun/poo/
    ├── Main.java                 (menú: List<Modulo>, polimorfismo en acción)
    ├── Modulo.java               (interfaz que cumple cada demo)
    ├── modulo01_clases/
    ├── modulo02_encapsulamiento/
    ├── ...
    ├── modulo10_principios/
    └── modulo11_interfaces/
```

## Sugerencia de uso en clase
1. Ejecutar el módulo del día con `ejecutar.bat N` y leer la salida junto con el código.
2. Cambiar algo en vivo (quitar un `@Override`, hacer público un atributo, invertir dos `catch`) y volver a compilar para ver qué pasa.
3. Dejar el **ejercicio propuesto** del README del módulo como taller.

El propio `Main` es un ejemplo: no conoce ninguna clase `DemoXX` por dentro, solo la interfaz `Modulo`. Agregar un módulo nuevo es crear una clase que implemente `Modulo` y añadirla a la lista.
