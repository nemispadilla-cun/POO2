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
| 5 | [Interfaces gráficas](Modulo05_GUI/README.md) | **G5** | Programación dirigida por eventos, componentes, contenedores, layouts, MVC, Windows Forms | `Boton`, `LayoutRejilla`, `ControladorCalculadora`, `FormCalculadora` |
| 6 | [Persistencia de datos](Modulo06_Persistencia/README.md) | **G6** | Serialización JSON/XML/CSV, ORM, CRUD, LINQ vs SQL, transacciones, patrón DAO | `ArchivoJson`, `CursoDbContext`, `ServicioCompra`, `IReservaDao` |
| 7 | [Conexión y consumo de APIs](Modulo07_Apis/README.md) | **G7** | HTTP y REST, `HttpClient`, JSON ↔ objetos, CRUD, códigos de estado, reintentos, OAuth2, datos abiertos | `ClienteUsuarios`, `ManejadorReintentos`, `GestorRutas`, `ClienteTrm` |

El repositorio tiene **dos ejecutables**, agrupados en `POO2.sln`:

| Proyecto | Tipo | Para qué |
|---|---|---|
| `POO.Fundamentos.csproj` (raíz) | Consola | Los módulos 1 a 7 (`ejecutar.bat N`) |
| [`CunExpressApi/`](CunExpressApi/README.md) | Aplicación web ASP.NET Core (Web API) | El servidor REST que consume el módulo 7 (`api.bat`) |

## Requisitos
- **SDK de .NET 8 o superior** (`dotnet --version`). Descarga: <https://dotnet.microsoft.com/download>
- Paquetes NuGet: **Entity Framework Core con SQLite** (el ORM, desde el módulo 6) y **Swashbuckle** (Swagger, solo en `CunExpressApi`). `dotnet build` lo descarga solo la primera vez, así que esa primera compilación necesita internet. El resto sale de la biblioteca estándar (también el cliente HTTP del módulo 7). La sección 8 del módulo 7 usa internet solo si se activa con `POO2_EN_VIVO=1`.
- La ventana del módulo 5 (`ventana.bat`) usa Windows Forms, así que solo abre en **Windows**. El resto del proyecto funciona en cualquier sistema.

Si solo tienes .NET 9 o 10, cambia `<TargetFramework>` en `POO.Fundamentos.csproj`.

## Cómo ejecutar

### Windows
```bat
compilar.bat
ejecutar.bat            :: menu interactivo
ejecutar.bat 3          :: solo el modulo 3
ejecutar.bat 1 2        :: varios modulos
ejecutar.bat todos      :: todos en orden
ventana.bat             :: ventana de Windows Forms del modulo 5
api.bat                 :: Web API del modulo 7 (dejarla abierta y, en otra terminal, ejecutar.bat 7)
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
Abre **`POO2.sln`** con **Visual Studio 2022**, **Visual Studio Code** (extensión
C# Dev Kit) o **Rider**. El punto de entrada de la consola es `Program.cs`; el de
la Web API, `CunExpressApi/Program.cs`. Para el módulo 7 configura los **dos**
como proyectos de inicio.

## Arquitectura del proyecto

```
POO-Fundamentos-NET/
├── POO2.sln                  (los dos ejecutables: consola + Web API)
├── POO.Fundamentos.csproj    (ejecutable 1: la consola con los modulos)
├── Program.cs                (menu: List<IModulo>, polimorfismo en accion)
├── IModulo.cs                (el CONTRATO que cumple cada modulo)
├── ModuloBase.cs             (el MOLDE: codigo compartido por los modulos)
├── Dinero.cs                 (formato de pesos colombianos)
├── ventana.bat               (abre la ventana de Windows Forms del modulo 5)
├── api.bat                   (arranca la Web API del modulo 7)
├── Modulo01_Principios/      -> G1
├── Modulo02_Interfaces/      -> G2
├── Modulo03_Colecciones/     -> G3
├── Modulo04_Patrones/        -> G4 (Creacionales/, Estructurales/, Comportamiento/)
├── Modulo05_GUI/             -> G5 (Componentes/, Mvc/, PuntoDeVenta/, CalculadoraWinForms/)
├── Modulo06_Persistencia/    -> G6 (Archivos/, Entidades/, Datos/, Tienda/, Reservas/)
├── Modulo07_Apis/            -> G7, el CLIENTE (Modelos/, Clientes/, CunExpress/, Taller/)
└── CunExpressApi/            -> G7, el SERVIDOR: ejecutable 2, ASP.NET Core Web API
                                 (Controllers/, Modelos/, Servicios/, Filtros/)
```

El propio menú es un ejemplo de lo que enseña: `Program` no conoce ninguna clase
`DemoXX`, solo la interfaz `IModulo`. Y `ModuloBase` es la clase abstracta que
evita que los siete módulos repitan los métodos `Seccion` y `Nota`. Es decir, la
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
| `JFrame` / `JButton` (Swing) | `Form` / `Button` (Windows Forms) | Ver tabla completa en el módulo 5 |
| `addActionListener(...)` | `boton.Click += Manejador;` | Eventos de C# |
| Gson | `System.Text.Json` | Incluido en .NET |
| Hibernate / JPA (`EntityManager`) | Entity Framework Core (`DbContext`) | Ver tabla completa en el módulo 6 |
| HQL | LINQ | Consultas sobre clases, revisadas por el compilador |
| `java.net.http.HttpClient` | `System.Net.Http.HttpClient` | Ver tabla completa en el módulo 7 |
| `CompletableFuture` | `Task<T>` + `async`/`await` | |
| `System.out.println` | `Console.WriteLine` | |
| `String.format` | interpolación `$"..."` | |
| `List<X> l = new ArrayList<>()` | `List<X> l = [];` | Expresión de colección, C# 12 |
| paquete `co.edu.cun.poo` | namespace `Cun.Poo` | |

## Sugerencia de uso en clase
1. Ejecutar el módulo del día con `ejecutar.bat N` y leer la salida junto al código.
2. Cambiar algo en vivo (quitar un `override`, instanciar una clase abstracta,
   cambiar un `Dictionary` por una `List`) y volver a compilar para ver qué pasa.
3. Dejar el **ejercicio propuesto** del README del módulo como taller.
