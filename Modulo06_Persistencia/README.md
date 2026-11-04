# Módulo 6 · Persistencia de datos en aplicaciones orientadas a objetos (Temática 6 · G6)

El documento maestro usa Java con **Gson** (JSON) y **Hibernate/JPA** (ORM).
Aquí el equivalente en C#:

- **Archivos:** `System.Text.Json` (JSON), `XmlSerializer` (XML) y un CSV escrito
  a mano. Los tres vienen en .NET.
- **Base de datos:** **Entity Framework Core**, el ORM de .NET, sobre **SQLite**.
  SQLite es una base de datos relacional en un solo archivo (`datos/poo2.db`) que
  no necesita instalar ni configurar un servidor.

```bat
ejecutar.bat 6
```

```
Modulo06_Persistencia/
├── Demo06Persistencia.cs
├── Archivos/    ConfiguracionUsuario, Producto, ArchivoJson (JSON), InventarioCsv (CSV)
├── Entidades/   Estudiante (mapeo con atributos), Cafe, ItemCarrito, Pedido + DetallePedido,
│                Viaje, Reserva
├── Datos/       CursoDbContext (el EntityManager; mapeo con Fluent API)
├── Tienda/      ServicioCompra (la transacción del carrito)
└── Reservas/    IReservaDao, ReservaDaoJson, ReservaDaoEf, ReservaService
```

Cada ejecución borra y vuelve a crear la carpeta `datos/` junto al ejecutable
(`bin/Release/net8.0/datos/`). Así la salida es siempre la misma. Ábrela después
de correr el módulo para ver los archivos `.json`, `.xml`, `.csv` y la base `poo2.db`.
La base se puede abrir con [DB Browser for SQLite](https://sqlitebrowser.org/) o
con la extensión *SQLite Viewer* de VS Code.

| Sección | Ejemplo del recurso | Tipos en este módulo |
|---|---|---|
| 1. Serializar a JSON | Configuración de una app (2.1) e inventario con Gson (4.3) | `ConfiguracionUsuario`, `Producto`, `ArchivoJson` |
| 2. XML y CSV | Mismo objeto en XML (2.1), archivos planos (3.2) | `XmlSerializer`, `InventarioCsv` |
| 3. ORM: mapeo y CRUD | Entidad `Estudiante` con Hibernate (4.1) | `Estudiante`, `CursoDbContext`, `Add`/`Find`/`SaveChanges`/`Remove` |
| 4. Consultas | HQL vs SQL, `GROUP BY` (4.2) | LINQ + `ToQueryString()` |
| 5. Transacción | Carrito de "Cafés de mi Tierra" (2.2) | `ServicioCompra`, `Cafe`, `ItemCarrito`, `Pedido` |
| 6. Capas y DAO | Transportes Andinos S.A.S. (3.1) y el JOIN de HQL (4.2) | `IReservaDao`, `ReservaDaoJson`, `ReservaDaoEf`, `ReservaService` |

## El paquete de EF Core

Es el único paquete NuGet del curso. Está en `POO.Fundamentos.csproj` y
`dotnet build` lo descarga solo la primera vez (necesita internet):

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.11" />
```

En un proyecto nuevo se agrega con `dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.11`
o desde *Administrar paquetes NuGet* en Visual Studio. Para SQL Server se usa
`Microsoft.EntityFrameworkCore.SqlServer` y `UseSqlServer(...)`; el resto del código
no cambia.

> **Sobre `EnsureCreated()`:** crea las tablas a partir de las clases y sirve para
> una demo. En un proyecto real se usan **migraciones**
> (`dotnet ef migrations add Inicial` y `dotnet ef database update`), que guardan
> la historia de los cambios del esquema.

## Equivalencias Java (recurso) → C#

| Java | C# / .NET | Nota |
|---|---|---|
| Gson `gson.toJson(obj)` | `JsonSerializer.Serialize(obj)` | Incluido en .NET, sin paquete |
| `gson.fromJson(json, tipo)` + `TypeToken` | `JsonSerializer.Deserialize<List<Producto>>(json)` | El genérico reemplaza al `TypeToken` |
| `transient` / `@Expose` | `[JsonIgnore]` / `[XmlIgnore]` | Atributo que no se guarda |
| JAXB | `XmlSerializer` | |
| Hibernate / JPA | Entity Framework Core | El ORM |
| `hibernate.cfg.xml` | `OnConfiguring` (o `appsettings.json`) | Cadena de conexión y proveedor |
| `SessionFactory` | `DbContextOptions` | Se crea una vez |
| `EntityManager` / `Session` | `DbContext` | Uno por unidad de trabajo, con `using` |
| `@Entity @Table(name=...)` | `[Table("...")]` o `modelo.Entity<T>().ToTable(...)` | Atributos o Fluent API |
| `@Id` / `@Column(name=...)` | `[Key]` / `[Column("...")]` | |
| `em.persist(obj)` | `db.Add(obj)` + `db.SaveChanges()` | |
| `em.find(Clase.class, id)` | `db.Estudiantes.Find(id)` | |
| `em.merge(obj)` | cambiar la propiedad + `SaveChanges()` | El contexto detecta el cambio |
| `em.remove(obj)` | `db.Remove(obj)` + `SaveChanges()` | |
| `getTransaction().begin()/commit()/rollback()` | `Database.BeginTransaction()` / `Commit()` / `Rollback()` | `SaveChanges` solo ya es una transacción |
| HQL `SELECT e FROM Estudiante e WHERE ...` | LINQ `db.Estudiantes.Where(e => ...)` | Revisado por el compilador |
| `GROUP BY` + `COUNT` en HQL | `GroupBy(...).Select(g => g.Count())` | |
| `JOIN r.viaje v` | `r.Viaje.Id` o `Include(r => r.Viaje)` | El ORM escribe el `INNER JOIN` |
| `ReservaDAO` / `ReservaDAOImpl` | `IReservaDao` / `ReservaDaoEf` | Mismo patrón |

## Lo que muestra la salida
- **JSON:** la configuración se guarda como `{"idioma":"es-CO","tema":"oscuro","tamanoFuente":14}`
  y vuelve como un objeto **nuevo**. `EsModoOscuro` no aparece en el archivo
  porque es un cálculo, no estado. La lista de productos se lee sin `TypeToken`.
- **XML y CSV:** el mismo objeto en XML, con las etiquetas del recurso. En el
  CSV cada columna se convierte a mano (`decimal.Parse`), que es justo lo que los
  serializadores evitan.
- **ORM:** EF Core genera `CREATE TABLE "estudiantes"` con las columnas
  `codigo_estudiante` y `nombre_completo` que piden los atributos. Carlos Moncada
  se crea, se lee **desde otro contexto** (viene del disco), cambia de carrera y se borra.
- **Consultas:** `ToQueryString()` muestra el SQL que genera la consulta LINQ.
  El valor de la carrera va como **parámetro**, lo que impide la inyección SQL.
- **Transacción:** la compra de Laura confirma (COMMIT por $134.000). La de
  Andrés falla en el segundo café y el ROLLBACK deshace el pedido y el stock que
  ya se habían escrito. El carrito de Andrés sigue intacto.
- **DAO:** el **mismo** `ReservaService` vende 3 sillas y rechaza la cuarta,
  primero con un archivo JSON y después con la base de datos. Lo único que cambia
  es el DAO que recibe por el constructor.

## Preguntas para la clase
- ¿Por qué la configuración de la app va en un archivo y el carrito de compras
  en una base de datos? ¿Qué pasaría con `ReservaDaoJson` si dos taquillas
  venden tiquetes al mismo tiempo?
- En la sección 5, ¿qué quedaría en la base si se quitara `transaccion.Rollback()`
  y el `catch`? Pruébalo.
- `Pedido` tiene una `List<DetallePedido>`. ¿Cómo se guarda esa lista en una
  base relacional, que no tiene listas? Eso es el **desajuste de impedancia**.
- ¿Qué principio SOLID (G1) cumple `ReservaService` al recibir un `IReservaDao`?
  ¿Qué patrón de G4 se parece a cambiar el DAO en tiempo de ejecución?
- El recurso pregunta por la Ley de *Habeas Data* (Ley 1581 de 2012). ¿Qué datos
  de este módulo son personales? ¿Qué harías en la capa de persistencia para protegerlos?

## Ejercicio propuesto
1. **Archivos:** agrega a `ConfiguracionUsuario` una lista de "archivos recientes"
   y comprueba que JSON y XML la guardan sin cambiar `ArchivoJson`.
2. **CRUD:** crea la entidad `Materia` (código, nombre, créditos), agrégala a
   `CursoDbContext` y haz su CRUD. Consulta con LINQ las materias de más de 3 créditos.
3. **Transacción:** agrega a `ServicioCompra` el método `CancelarPedido(id)`, que
   devuelva el stock y borre el pedido en una sola transacción.
4. **DAO:** escribe `ReservaDaoCsv : IReservaDao` y pásalo a `ReservaService`
   sin modificar el servicio.
5. **Relación muchos a muchos:** modela `Estudiante` ↔ `Materia` (inscripciones)
   y mira en `GenerateCreateScript()` la tabla intermedia que crea el ORM.
