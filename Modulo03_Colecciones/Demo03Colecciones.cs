namespace Cun.Poo.Modulo03Colecciones;

/// <summary>
/// Tematica 3 (G3): estructuras de datos avanzadas.
/// </summary>
public class Demo03Colecciones : ModuloBase
{
    public override string Titulo =>
        "Estructuras de datos avanzadas: List, Stack, Queue, Set, Dictionary, grafos";

    public override string Descripcion =>
        "Lista de compras, Ctrl+Z, mesa de ayuda, directorio por documento, genericos y Dijkstra en Bogota.";

    public override void Ejecutar()
    {
        // ====================================================================
        Seccion("1. LIST<T>: orden de insercion, duplicados y acceso por indice");
        var compras = new ListaDeCompras();
        compras.Agregar("Pan");
        compras.Agregar("Leche");
        compras.Agregar("Huevos");
        compras.Agregar("Cafe de origen (Quindio)");
        compras.Agregar("Leche");                 // duplicado: la List lo permite
        Console.WriteLine($"   Lista inicial: {compras}");

        compras.Quitar("Leche");                  // quita solo la PRIMERA coincidencia
        Console.WriteLine($"   Tras quitar una leche: {compras}");
        Console.WriteLine($"   Tercer producto (indice 2): {compras.EnPosicion(2)}");
        Nota("Mantiene el orden en que el usuario agrego los productos: es lo intuitivo aqui.");
        Nota("Equivalencias: List<T> en C# es el ArrayList de Java; LinkedList<T> existe en ambos.");

        // ====================================================================
        Seccion("2. STACK<T>: LIFO, el motor de Deshacer (Ctrl+Z)");
        var editor = new EditorDeTexto();
        editor.Escribir("Hola");
        editor.Escribir(" mundo");
        editor.Escribir(", POO II");
        Console.WriteLine($"   Texto actual: \"{editor.Contenido}\"  ({editor.AccionesEnHistorial} estados guardados)");

        editor.Deshacer();
        Console.WriteLine($"   Tras un Ctrl+Z: \"{editor.Contenido}\"");
        editor.Deshacer();
        Console.WriteLine($"   Tras otro Ctrl+Z: \"{editor.Contenido}\"");
        Nota("El ultimo estado que entra es el primero que sale: Push y Pop son O(1).");

        // ====================================================================
        Seccion("3. QUEUE<T>: FIFO, atencion justa en la mesa de ayuda");
        var mesa = new MesaDeAyuda();
        mesa.Recibir(new TicketSoporte("TK-001", "Usuario sin internet"));
        mesa.Recibir(new TicketSoporte("TK-002", "Problema con la facturacion"));
        mesa.Recibir(new TicketSoporte("TK-003", "Senal de TV intermitente"));
        Console.WriteLine($"   Tickets en espera: {mesa.EnEspera}");

        Console.WriteLine($"   Atendiendo -> {mesa.Atender()}");
        Console.WriteLine($"   Atendiendo -> {mesa.Atender()}");
        Console.WriteLine($"   Quedan en espera: [{string.Join(", ", mesa.Pendientes)}]");
        Nota("El primero que llega es el primero en salir: nadie se cuela y nada se pierde.");

        // ====================================================================
        Seccion("4. HASHSET<T>: sin duplicados, pertenencia instantanea");
        var cedulas = new HashSet<string>();
        string[] registros = ["1020304050", "1030405060", "1020304050", "1040506070"];

        foreach (string cedula in registros)
        {
            bool nueva = cedulas.Add(cedula);     // false si ya estaba
            Console.WriteLine($"   Registrar {cedula}: {(nueva ? "aceptada" : "RECHAZADA (duplicada)")}");
        }
        Console.WriteLine($"   Cedulas unicas: {cedulas.Count} de {registros.Length} intentos");
        Nota("Add devuelve false si el elemento ya existia: la unicidad la garantiza la estructura.");

        // ====================================================================
        Seccion("5. DICTIONARY<K,V>: la clave lleva directo al valor");
        var directorio = new DirectorioEstudiantes();
        directorio.Inscribir(new Estudiante("1020304050", "Laura Mesa", "Ing. de Sistemas", 3));
        directorio.Inscribir(new Estudiante("1030405060", "Andres Gil", "Ing. de Sistemas", 5));
        directorio.Inscribir(new Estudiante("1040506070", "Sofia Ruiz", "Ing. Industrial", 2));

        Console.WriteLine($"   Buscar 1030405060 -> {directorio.Buscar("1030405060")}");
        Console.WriteLine($"   Buscar 9999999999 -> {directorio.Buscar("9999999999")?.ToString() ?? "no existe"}");
        Console.WriteLine("   Ordenados por nombre (el Dictionary NO garantiza orden):");
        foreach (Estudiante e in directorio.OrdenadosPorNombre())
        {
            Console.WriteLine($"     {e}");
        }

        Console.WriteLine();
        ComparadorDeRendimiento.BusquedaListaVsDiccionario(200_000);
        Console.WriteLine();
        ComparadorDeRendimiento.InsercionAlInicio(50_000);
        Nota("La tabla de Big O no es teoria: son estos milisegundos, medidos en esta maquina.");

        // ====================================================================
        Seccion("6. GENERICOS: una sola clase, seguridad de tipo al compilar");
        var inventario = new Inventario<Producto>();
        inventario.Agregar(new Producto("P-001", "Teclado mecanico", 189_000m));
        inventario.Agregar(new Producto("P-002", "Monitor 24 pulgadas", 650_000m));
        inventario.Agregar(new Producto("P-003", "Mouse inalambrico", 95_000m));

        Console.WriteLine($"   Productos en stock: {inventario.Cantidad}");
        Console.WriteLine($"   Buscar P-002 -> {inventario.Buscar("P-002")}");
        inventario.Eliminar("P-003");
        Console.WriteLine($"   Tras eliminar P-003 quedan: {inventario.Cantidad}");
        // inventario.Agregar("un string");   // <- NO COMPILA: seguridad de tipo
        Nota("where T : IIdentificable le garantiza al compilador que todo T tiene Codigo.");
        Nota("El Dictionary interno esta encapsulado: se puede cambiar sin romper a quien use la clase.");

        // ====================================================================
        Seccion("7. CASO ENTREGAS ANDINAS S.A.S.: grafo + PriorityQueue (Dijkstra)");
        Grafo bogota = MapaBogota.Construir();
        Console.WriteLine($"   Mapa cargado: {bogota.Vertices} puntos, {bogota.Calles} calles de doble sentido.");
        Console.WriteLine("   Estructura interna: Dictionary<string, List<Arista>>");
        Console.WriteLine();

        var calculadora = new CalculadoraDeRutas(bogota);
        IReadOnlyDictionary<string, ResultadoRuta> rutas =
            calculadora.DesdeOrigen(MapaBogota.CentroDistribucion);

        Console.WriteLine("   Rutas optimas desde el centro de distribucion:");
        foreach (string entrega in MapaBogota.Entregas)
        {
            ResultadoRuta r = rutas[entrega];
            Console.WriteLine($"     {entrega,-10} {r.Minutos,5:F0} min   {r.CaminoLegible}");
        }

        Console.WriteLine();
        ResultadoRuta suba = rutas["Suba"];
        double directa = MapaBogota.MinutosAvenidaDirectaASuba;
        Console.WriteLine($"   Ruta directa Centro -> Suba por la avenida: {directa:F0} min");
        Console.WriteLine($"   Ruta que encuentra Dijkstra:                {suba.Minutos:F0} min");
        Console.WriteLine($"   Ahorro por entrega: {directa - suba.Minutos:F0} min "
                          + $"({(directa - suba.Minutos) / directa:P0})");
        Nota("La ruta mas corta tiene 5 tramos y es mas rapida que la avenida directa.");
        Nota("La PriorityQueue entrega siempre el vertice mas cercano sin visitar: O(log n) por paso.");

        // ====================================================================
        Seccion("8. LINQ: frecuencia de palabras en una sola expresion");
        const string texto = "la programacion orientada a objetos es un paradigma de "
                             + "programacion que usa objetos en sus interacciones";

        var frecuencia = texto
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .GroupBy(palabra => palabra)
            .Where(grupo => grupo.Count() > 1)
            .ToDictionary(grupo => grupo.Key, grupo => grupo.Count());

        Console.WriteLine("   Palabras repetidas:");
        foreach (KeyValuePair<string, int> par in frecuencia.OrderByDescending(p => p.Value))
        {
            Console.WriteLine($"     {par.Key,-14} {par.Value} veces");
        }
        Nota("GroupBy + ToDictionary resuelve en 4 lineas lo que en Java 8 pedia un bucle con containsKey.");

        // ====================================================================
        Seccion("RESUMEN: que estructura elegir");
        Console.WriteLine("   Necesito orden y acceso por indice ......... List<T>          O(1) por indice");
        Console.WriteLine("   Insertar o quitar mucho por los extremos ... LinkedList<T>    O(1) en extremos");
        Console.WriteLine("   Lo ultimo que entra sale primero ........... Stack<T>         LIFO");
        Console.WriteLine("   Lo primero que entra sale primero .......... Queue<T>         FIFO");
        Console.WriteLine("   Sin duplicados, pertenencia rapida ......... HashSet<T>       O(1)");
        Console.WriteLine("   Buscar por una clave unica ................. Dictionary<K,V>  O(1)");
        Console.WriteLine("   Siempre el elemento de menor costo ......... PriorityQueue    O(log n)");
        Console.WriteLine("   Una red de nodos y conexiones .............. Grafo            Dictionary + List");
        Nota("No hay una estructura mejor: hay una mas adecuada para cada problema.");
    }
}
