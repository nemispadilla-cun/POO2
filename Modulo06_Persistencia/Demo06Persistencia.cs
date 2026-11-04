using System.Xml.Serialization;
using Cun.Poo.Modulo06Persistencia.Archivos;
using Cun.Poo.Modulo06Persistencia.Datos;
using Cun.Poo.Modulo06Persistencia.Entidades;
using Cun.Poo.Modulo06Persistencia.Reservas;
using Cun.Poo.Modulo06Persistencia.Tienda;
using Microsoft.EntityFrameworkCore;

namespace Cun.Poo.Modulo06Persistencia;

/// <summary>
/// Tematica 6 (G6): persistencia de datos en aplicaciones orientadas a objetos.
///
/// Secciones 1 y 2: archivos (JSON, XML, CSV) con la biblioteca estandar.
/// Secciones 3 a 6: base de datos relacional con un ORM (EF Core + SQLite),
/// el equivalente en .NET de Hibernate/JPA.
///
/// Todo se guarda en la carpeta datos/ junto al ejecutable. Se borra al
/// empezar, para que cada ejecucion muestre la misma salida.
/// </summary>
public class Demo06Persistencia : ModuloBase
{
    public override string Titulo =>
        "Persistencia: archivos JSON/XML/CSV, ORM, transacciones y DAO";

    public override string Descripcion =>
        "Serializar objetos a archivos, CRUD con EF Core (el Hibernate de .NET), LINQ vs SQL, una transaccion con rollback y el patron DAO.";

    private static readonly string CarpetaDatos = Path.Combine(AppContext.BaseDirectory, "datos");

    public override void Ejecutar()
    {
        if (Directory.Exists(CarpetaDatos))
        {
            Directory.Delete(CarpetaDatos, recursive: true);
        }
        Directory.CreateDirectory(CarpetaDatos);

        SeccionJson();
        SeccionXmlYCsv();
        SeccionOrmCrud();
        SeccionConsultas();
        SeccionTransaccion();
        SeccionDao();
    }

    // ========================================================================
    private static void SeccionJson()
    {
        Seccion("1. SERIALIZAR A JSON: la app que recuerda tus preferencias");
        var config = new ConfiguracionUsuario { Idioma = "es-CO", Tema = "oscuro", TamanoFuente = 14 };
        string ruta = Ruta("configuracion.json");
        ArchivoJson.Guardar(config, ruta);                          // la app se cierra...
        Console.WriteLine("   Al cerrar la app se guarda datos/configuracion.json:");
        Imprimir(File.ReadAllText(ruta));

        config = null;                                              // ...la RAM se libera
        ConfiguracionUsuario recuperada = ArchivoJson.Cargar<ConfiguracionUsuario>(ruta);
        Console.WriteLine("   Al abrirla de nuevo se reconstruye un objeto NUEVO:");
        Console.WriteLine($"      {recuperada}  -> modo oscuro: {(recuperada.EsModoOscuro ? "si" : "no")}");

        // La lista del recurso (Gson): en C# basta con Cargar<List<Producto>>, sin TypeToken.
        List<Producto> inventario =
        [
            new("CAF-001", "Café Nariño", 35_000m),
            new("PAN-002", "Mogolla Chicharrona", 5_000m),
        ];
        string rutaInventario = Ruta("inventario.json");
        ArchivoJson.Guardar(inventario, rutaInventario);
        List<Producto> leidos = ArchivoJson.Cargar<List<Producto>>(rutaInventario);
        Console.WriteLine($"   Inventario: {leidos.Count} productos leidos de datos/inventario.json, " +
                          $"iguales a los guardados: {(leidos.SequenceEqual(inventario) ? "si" : "no")}");
        Nota("Serializar = objeto en RAM -> texto en disco. Deserializar = disco -> objeto nuevo.");
        Nota("EsModoOscuro tiene [JsonIgnore]: es un calculo, no estado. No se guarda.");
    }

    // ========================================================================
    private static void SeccionXmlYCsv()
    {
        Seccion("2. EL MISMO OBJETO EN XML, Y UN INVENTARIO EN CSV");
        var config = new ConfiguracionUsuario { Idioma = "es-CO", Tema = "oscuro", TamanoFuente = 14 };
        var serializador = new XmlSerializer(typeof(ConfiguracionUsuario));
        string rutaXml = Ruta("configuracion.xml");
        using (var escritor = new StreamWriter(rutaXml))
        {
            var sinEspaciosDeNombres = new XmlSerializerNamespaces([System.Xml.XmlQualifiedName.Empty]);
            serializador.Serialize(escritor, config, sinEspaciosDeNombres);
        }
        Console.WriteLine("   datos/configuracion.xml (mas verboso, pero validable con un XSD):");
        Imprimir(string.Join('\n', File.ReadAllLines(rutaXml).Skip(1)));   // sin la linea <?xml ...?>

        using (var lector = new StreamReader(rutaXml))
        {
            var desdeXml = (ConfiguracionUsuario)serializador.Deserialize(lector)!;
            Console.WriteLine($"   Leido del XML: {desdeXml}");
        }

        List<Producto> inventario =
        [
            new("CAF-001", "Café Nariño", 35_000m),
            new("PAN-002", "Mogolla Chicharrona", 5_000m),
            new("ARE-003", "Arepa de choclo", 4_500m),
        ];
        string rutaCsv = Ruta("inventario.csv");
        InventarioCsv.Guardar(inventario, rutaCsv);
        Console.WriteLine("   datos/inventario.csv (texto plano, columnas separadas por ';'):");
        Imprimir(File.ReadAllText(rutaCsv).TrimEnd());
        List<Producto> desdeCsv = InventarioCsv.Cargar(rutaCsv);
        Console.WriteLine($"   Leidos del CSV: {desdeCsv.Count} productos, valor del inventario " +
                          $"{Dinero.Cop(desdeCsv.Sum(p => p.Precio))}");
        Nota("JSON y XML reconstruyen el objeto solos. En CSV cada columna se convierte a mano.");
    }

    // ========================================================================
    private static void SeccionOrmCrud()
    {
        Seccion("3. ORM: la clase Estudiante se vuelve tabla, y CRUD sin SQL");
        using (var db = new CursoDbContext(RutaBaseDeDatos))
        {
            db.Database.EnsureCreated();                            // crea las tablas desde las clases
            Console.WriteLine("   EF Core leyo los atributos de Estudiante y genero esta tabla:");
            Imprimir(TablaEstudiantes(db.Database.GenerateCreateScript()));

            // CREATE: em.persist(nuevo)  ->  db.Add(nuevo) + SaveChanges()
            db.Estudiantes.Add(new Estudiante("202401", "Carlos Moncada", "Ingenieria de Software"));
            db.SaveChanges();
            Console.WriteLine("   CREATE  Add + SaveChanges         -> INSERT INTO estudiantes ...");
        }

        // Un contexto NUEVO: el objeto que llega viene del disco, no de la RAM anterior.
        using (var db = new CursoDbContext(RutaBaseDeDatos))
        {
            Estudiante encontrado = db.Estudiantes.Find("202401")!; // READ: em.find(...)
            Console.WriteLine($"   READ    Find(\"202401\")            -> {encontrado}");

            encontrado.Carrera = "Ingenieria de Sistemas";          // UPDATE: se cambia el OBJETO...
            db.SaveChanges();                                       // ...y el ORM detecta el cambio
            Console.WriteLine($"   UPDATE  cambiar propiedad + Save  -> {db.Estudiantes.Find("202401")}");

            db.Estudiantes.Remove(encontrado);                      // DELETE: em.remove(...)
            db.SaveChanges();
            Console.WriteLine($"   DELETE  Remove + SaveChanges      -> quedan {db.Estudiantes.Count()} estudiantes");
        }
        Nota("No escribimos ni una linea de SQL: el ORM traduce objetos a filas y viceversa.");
        Nota("Find y SaveChanges son el find/merge/commit de Hibernate; using cierra la sesion.");
    }

    // ========================================================================
    private static void SeccionConsultas()
    {
        Seccion("4. CONSULTAS: LINQ es el HQL de C#");
        using var db = new CursoDbContext(RutaBaseDeDatos);
        db.Estudiantes.AddRange(
            new Estudiante("202402", "Ana Rojas", "Ingenieria de Sistemas"),
            new Estudiante("202403", "Luis Pardo", "Ingenieria de Sistemas"),
            new Estudiante("202404", "Maria Cuellar", "Ingenieria de Software"),
            new Estudiante("202405", "Jorge Rincon", "Contaduria Publica"),
            new Estudiante("202406", "Sofia Medina", "Ingenieria de Sistemas"));
        db.SaveChanges();

        string carrera = "Ingenieria de Sistemas";
        IQueryable<string> nombres = db.Estudiantes
            .Where(e => e.Carrera == carrera)                       // propiedades, no columnas
            .OrderBy(e => e.Nombre)
            .Select(e => e.Nombre);
        Console.WriteLine("   LINQ:  db.Estudiantes.Where(e => e.Carrera == carrera).Select(e => e.Nombre)");
        Console.WriteLine("   SQL que genera EF Core (ToQueryString):");
        Imprimir(nombres.ToQueryString());
        Console.WriteLine($"   Resultado: {string.Join(", ", nombres)}");

        // HQL: SELECT e.carrera, COUNT(e) FROM Estudiante e GROUP BY e.carrera
        var porCarrera = db.Estudiantes
            .GroupBy(e => e.Carrera)
            .Select(g => new { Carrera = g.Key, Cantidad = g.Count() })
            .OrderByDescending(x => x.Cantidad)
            .ToList();
        Console.WriteLine("   Agrupacion: estudiantes por carrera");
        foreach (var fila in porCarrera)
        {
            Console.WriteLine($"      {fila.Carrera,-24} {fila.Cantidad}");
        }
        Nota("La consulta se escribe sobre la clase Estudiante; la tabla 'estudiantes' la pone el ORM.");
        Nota("El valor viaja como parametro (@carrera), nunca pegado al texto: no hay inyeccion SQL.");
    }

    // ========================================================================
    private static void SeccionTransaccion()
    {
        Seccion("5. TRANSACCION: el carrito de Cafes de mi Tierra");
        using var db = new CursoDbContext(RutaBaseDeDatos);
        var narino = new Cafe { Nombre = "Cafe Narino", Region = "Narino", Precio = 35_000m, Stock = 10 };
        var huila = new Cafe { Nombre = "Cafe Huila", Region = "Huila", Precio = 32_000m, Stock = 3 };
        var sierra = new Cafe { Nombre = "Cafe Sierra Nevada", Region = "Magdalena", Precio = 38_000m, Stock = 5 };
        db.Cafes.AddRange(narino, huila, sierra);
        db.SaveChanges();
        var tienda = new ServicioCompra(db);

        Console.WriteLine("   Laura agrega 2 Cafe Narino y 2 Cafe Huila, y paga:");
        tienda.AgregarAlCarrito("Laura", narino, 2);
        tienda.AgregarAlCarrito("Laura", huila, 2);
        Pedido pedido = tienda.FinalizarCompra("Laura");
        Console.WriteLine($"      COMMIT   pedido #{pedido.Id} por {Dinero.Cop(pedido.Total)} ({pedido.Detalles.Count} detalles)");
        ImprimirStock(db);

        Console.WriteLine("   Andres agrega 1 Cafe Sierra Nevada y 3 Cafe Huila (solo queda 1):");
        tienda.AgregarAlCarrito("Andres", sierra, 1);
        tienda.AgregarAlCarrito("Andres", huila, 3);
        try
        {
            tienda.FinalizarCompra("Andres");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"      ROLLBACK {ex.Message}");
        }
        ImprimirStock(db);
        Console.WriteLine($"      Pedidos en la base: {db.Pedidos.Count()}. " +
                          $"El carrito de Andres sigue con {tienda.VerCarrito("Andres").Count} items.");
        Nota("El pedido de Andres alcanzo a insertarse y a descontar la Sierra Nevada: el rollback lo deshizo.");
        Nota("SQLite bloquea toda la base al escribir; en PostgreSQL o SQL Server se usa SELECT ... FOR UPDATE.");
    }

    // ========================================================================
    private static void SeccionDao()
    {
        Seccion("6. CAPAS Y DAO: Transportes Andinos sin sobreventa");
        using var db = new CursoDbContext(RutaBaseDeDatos);
        var viaje = new Viaje { Ruta = "Bogota - Tunja", PlacaBus = "WFK-215", Capacidad = 3 };
        db.Viajes.Add(viaje);
        db.SaveChanges();
        Console.WriteLine($"   {viaje}");

        var daoEf = new ReservaDaoEf(db);
        List<IReservaDao> daos = [new ReservaDaoJson(Ruta("reservas.json")), daoEf];
        foreach (IReservaDao dao in daos)
        {
            var servicio = new ReservaService(dao);                 // la MISMA logica de negocio
            Console.WriteLine($"   ReservaService con {dao.Tecnologia}:");
            foreach (string pasajero in new[] { "Ana", "Carlos", "Lucia", "Pedro" })
            {
                try
                {
                    Reserva r = servicio.CrearReserva(viaje, pasajero);
                    Console.WriteLine($"      Reserva confirmada: {r}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"      {pasajero}: {ex.Message}");
                }
            }
            Console.WriteLine($"      Pasajeros: {string.Join(", ", servicio.Pasajeros(viaje).Select(r => r.Pasajero))}");
        }

        Console.WriteLine("   El JOIN del recurso en LINQ, y el SQL que genera:");
        Console.WriteLine("      HQL:  SELECT r FROM Reserva r JOIN r.viaje v WHERE v.id = :viajeId");
        Console.WriteLine("      LINQ: db.Reservas.Where(r => r.Viaje.Id == viajeId)");
        Imprimir(daoEf.ConsultaPorViaje(viaje.Id).ToQueryString());
        Nota("ReservaService no cambio entre JSON y base de datos: solo cambio el DAO que recibe.");
        Nota("Presentacion -> Negocio (ReservaService) -> Datos (IReservaDao): cada capa habla con la de abajo.");
    }

    // ------------------------------------------------------------------------
    private static string RutaBaseDeDatos => Ruta("poo2.db");

    private static string Ruta(string archivo) => Path.Combine(CarpetaDatos, archivo);

    /// <summary>Imprime un texto de varias lineas con sangria.</summary>
    private static void Imprimir(string texto)
    {
        foreach (string linea in texto.Replace("\r", "").Split('\n'))
        {
            Console.WriteLine("      " + linea);
        }
    }

    /// <summary>Del script completo, solo el CREATE TABLE de estudiantes.</summary>
    private static string TablaEstudiantes(string script)
    {
        int inicio = script.IndexOf("CREATE TABLE \"estudiantes\"", StringComparison.Ordinal);
        int fin = script.IndexOf(");", inicio, StringComparison.Ordinal) + 2;
        return script[inicio..fin];
    }

    private static void ImprimirStock(CursoDbContext db)
    {
        string stock = string.Join(" | ", db.Cafes.AsNoTracking().OrderBy(c => c.Id)
                                              .Select(c => c.Nombre + ": " + c.Stock));
        Console.WriteLine($"      Stock -> {stock}");
    }
}
