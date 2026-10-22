# POO-Fundamentos-NET - Programacion Orientada a Objetos II (C# / .NET)

Proyecto de apoyo para la clase, **en C#**. Es el equivalente del proyecto Java
`POO-Fundamentos`, reorganizado por **temáticas del curso**: cada módulo cubre
una guía completa y es una demostración ejecutable con código comentado en
español.

| # | Módulo | Temática | Conceptos | Tipos clave |
|---|---|---|---|---|
| 1 | [Principios avanzados de POO](Modulo01_Principios/README.md) | **G1** | Cohesión alta, bajo acoplamiento, reutilización, SOLID, excepciones | `Factura`, `ServicioAutenticacion`, `ILogger`, `ProcesadorDePagos` |
| 2 | [Interfaces y clases abstractas](Modulo02_Interfaces/README.md) | **G2** | Contrato vs molde, `default`/`static` en interfaz, template method, Strategy, delegados | `IMetodoDePago`, `Personaje`, `Vehiculo`, `INotificador` |
| 3 | [Estructuras de datos avanzadas](Modulo03_Colecciones/README.md) | **G3** | `List`, `Stack`, `Queue`, `HashSet`, `Dictionary`, `PriorityQueue`, genéricos, Big O, grafos | `DirectorioEstudiantes`, `Inventario<T>`, `Grafo`, `CalculadoraDeRutas` |
| 4 | [Patrones de diseño](Modulo04_Patrones/README.md) | **G4** | Singleton, Factory Method, Builder, Adapter, Composite, Decorator, Observer, Strategy | `AppConfig`, `GeneradorReporte`, `WompiAdapter`, `CajaRegistradora` |

## Requisitos
- **SDK de .NET 8 o superior** (`dotnet --version`). Descarga: <https://dotnet.microsoft.com/download>
- Sin paquetes NuGet: todo sale de la biblioteca estándar.

Si solo tienes .NET 9 o 10, cambia `<TargetFramework>` en `POO.Fundamentos.csproj`.

## Cómo ejecutar

### Windows
```bat
compilar.bat
ejecutar.bat            :: menu interactivo
ejecutar.bat 3          :: solo el modulo 3
ejecutar.bat 1 2        :: varios modulos
ejecutar.bat todos      :: todos en orden
```

### Linux / macOS / Git Bash
```bash
chmod +x *.sh
./compilar.sh
./ejecutar.sh 3
```

### Sin scripts
```bash
dotnet run -- 3
```

### Desde un IDE
Abre la carpeta con **Visual Studio 2022**, **Visual Studio Code** (extensión
C# Dev Kit) o **Rider**. El punto de entrada es `Program.cs`.

## Arquitectura del proyecto

```
POO-Fundamentos-NET/
├── POO.Fundamentos.csproj
├── Program.cs                (menu: List<IModulo>, polimorfismo en accion)
├── IModulo.cs                (el CONTRATO que cumple cada modulo)
├── ModuloBase.cs             (el MOLDE: codigo compartido por los modulos)
├── Dinero.cs                 (formato de pesos colombianos)
├── Modulo01_Principios/      -> G1
├── Modulo02_Interfaces/      -> G2
├── Modulo03_Colecciones/     -> G3
└── Modulo04_Patrones/        -> G4 (Creacionales/, Estructurales/, Comportamiento/)
```

El propio menú es un ejemplo de lo que enseña: `Program` no conoce ninguna clase
`DemoXX`, solo la interfaz `IModulo`. Y `ModuloBase` es la clase abstracta que
evita que los cuatro módulos repitan los métodos `Seccion` y `Nota`. Es decir, la
Temática 2 está aplicada en la estructura misma del proyecto.

## De Java a C#: tabla de equivalencias

Este proyecto reemplaza al de Java. Si vienes del código anterior:

| Java | C# | Nota |
|---|---|---|
| `interface Logger` | `interface ILogger` | En .NET las interfaces se nombran con `I` inicial |
| `default void metodo()` | `void Metodo() => ...` en la interfaz | C# 8+; solo visible por referencia de interfaz |
| `static` en interfaz | `static` en interfaz | Igual |
| `@Override` | `override` | En C# el método base debe ser `virtual` o `abstract` |
| `final` (método) | método **no** `virtual` | En C# lo normal es que NO se pueda sobreescribir |
| `record Punto(int x)` | `record Punto(int X)` | Igual |
| `getter/setter` | **propiedades** `{ get; set; }` | Idioma propio de C# |
| `ArrayList` / `List` | `List<T>` | |
| `LinkedList` | `LinkedList<T>` | |
| `HashMap` / `Map` | `Dictionary<K,V>` / `IDictionary<K,V>` | |
| `TreeMap` | `SortedDictionary<K,V>` | |
| `HashSet` / `Set` | `HashSet<T>` / `ISet<T>` | |
| `Queue` (con `LinkedList`) | `Queue<T>` | En C# es una clase propia |
| `Stack` | `Stack<T>` | |
| `PriorityQueue<T>` | `PriorityQueue<TElement,TPriority>` | En C# la prioridad va aparte |
| `@FunctionalInterface` + lambda | **delegado** `Action` / `Func` + lambda | C# no necesita anotación |
| `Optional<T>` | `T?` + `TryGetValue` | Tipos de referencia anulables |
| `getInstance()` (Singleton) | propiedad `Instancia` + `Lazy<T>` | Seguro con varios hilos |
| `Observable` / `Observer` | `event` + delegado | Observer incorporado al lenguaje |
| `System.out.println` | `Console.WriteLine` | |
| `String.format` | interpolación `$"..."` | |
| `List<X> l = new ArrayList<>()` | `List<X> l = [];` | Expresión de colección, C# 12 |
| paquete `co.edu.cun.poo` | namespace `Cun.Poo` | |

## Sugerencia de uso en clase
1. Ejecutar el módulo del día con `ejecutar.bat N` y leer la salida junto al código.
2. Cambiar algo en vivo (quitar un `override`, instanciar una clase abstracta,
   cambiar un `Dictionary` por una `List`) y volver a compilar para ver qué pasa.
3. Dejar el **ejercicio propuesto** del README del módulo como taller.
