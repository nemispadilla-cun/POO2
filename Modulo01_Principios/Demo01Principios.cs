namespace Cun.Poo.Modulo01Principios;

/// <summary>
/// Tematica 1 (G1): cohesion, acoplamiento, reutilizacion, SOLID y excepciones.
/// </summary>
public class Demo01Principios : ModuloBase
{
    public override string Titulo =>
        "Principios avanzados de POO: cohesion, acoplamiento, reutilizacion, excepciones";

    public override string Descripcion =>
        "Factura cohesiva, autenticacion desacoplada por interfaz, Logger reutilizable, ProcesadorDePagos.";

    public override void Ejecutar()
    {
        ILogger logger = new LoggerConsola("app");

        // ====================================================================
        Seccion("1. COHESION ALTA: la clase Factura solo sabe de facturas");
        var factura = new Factura("F-0001", "Ana Torres");
        factura.AgregarLinea("Matricula POO II (4 creditos)", 1, 720_000m);
        factura.AgregarLinea("Carne estudiantil", 1, 15_000m);
        factura.AgregarLinea("Guia de laboratorio", 2, 12_500m);
        factura.AplicarDescuento(0.10m);
        Console.WriteLine(factura.GenerarResumen());

        IRepositorioFacturas repositorio = new RepositorioFacturasEnMemoria();
        repositorio.Guardar(factura);
        Console.WriteLine($"   Guardada en el repositorio. Facturas almacenadas: {repositorio.Listar().Count}");
        Nota("Todos los metodos de Factura tratan sobre calcular, descontar o resumir. Nada mas.");
        Nota("FacturaBajaCohesion mezcla calculo + BD + correo + log: cuatro razones para cambiarla.");

        // ====================================================================
        Seccion("2. BAJO ACOPLAMIENTO: autenticacion que no conoce la base de datos");
        var authSql = new ServicioAutenticacion(new RepositorioUsuariosSql(), logger);
        var authMongo = new ServicioAutenticacion(new RepositorioUsuariosMongo(), logger);

        Console.WriteLine("   Con SQL   -> ana/cun2026:   " + authSql.Autenticar("ana", "cun2026"));
        Console.WriteLine("   Con SQL   -> ana/incorrecta:" + authSql.Autenticar("ana", "incorrecta"));
        Console.WriteLine("   Con Mongo -> ana/cun2026:   " + authMongo.Autenticar("ana", "cun2026"));
        Console.WriteLine("   Con Mongo -> luis (no existe en Mongo): " + authMongo.Autenticar("luis", "poo-rocks"));
        Nota("Cambiamos de SQL a MongoDB sin tocar ServicioAutenticacion: depende de la INTERFAZ.");
        Nota("Las dependencias entran por el constructor (inyeccion de dependencias).");

        // ====================================================================
        Seccion("3. REUTILIZACION: un Logger, muchas aplicaciones");
        var loggerMovil = new LoggerMemoria();
        var authMovil = new ServicioAutenticacion(new RepositorioUsuariosMongo(), loggerMovil);
        authMovil.Autenticar("carla", "mongo123");
        authMovil.Autenticar("nadie", "x");

        Console.WriteLine("   Historial capturado por LoggerMemoria:");
        foreach (string registro in loggerMovil.Registros)
        {
            Console.WriteLine("     " + registro);
        }

        // El metodo default de la interfaz, visible solo por la referencia ILogger.
        ILogger comoInterfaz = loggerMovil;
        comoInterfaz.Advertencia("cupo de sesiones casi lleno");
        Console.WriteLine("   Tras usar el metodo default: " + loggerMovil.Registros[^1]);
        Nota("LoggerConsola sirve para escritorio; LoggerMemoria para pruebas o movil. La interfaz es la misma.");

        // ====================================================================
        Seccion("4. MANEJO DE EXCEPCIONES: el flujo no se interrumpe");
        var procesador = new ProcesadorDePagos(logger);
        List<Pago> pagos =
        [
            new("P-101", "4111222233334444", 250_000m),   // aprobado
            new("P-102", "0000111122223333", 80_000m),    // tarjeta bloqueada
            new("P-103", "4111222233334444", 900_000m),   // fondos insuficientes
            new("P-104", "4111222233334444", -5m),        // monto invalido
            new("P-105", "5500000000000004", 120_000m),   // aprobado
        ];

        var aprobados = new List<string>();
        foreach (Pago pago in pagos)
        {
            string respuesta = procesador.Procesar(pago);
            Console.WriteLine($"   Usuario ve -> {pago.Referencia}: {respuesta}");
            if (respuesta.StartsWith("Pago aprobado", StringComparison.Ordinal))
            {
                aprobados.Add(pago.Referencia);
            }
        }
        Console.WriteLine("   Pagos aprobados: [" + string.Join(", ", aprobados) + "]");
        Nota("Cinco pagos, tres rechazados, dos aprobados, cero caidas del sistema.");
        Nota("Quien detecta lanza; quien sabe que hacer captura. Nunca un catch vacio.");
    }
}
