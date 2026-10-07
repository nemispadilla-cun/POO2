package co.edu.cun.poo.modulo10_principios;

import java.util.List;

import co.edu.cun.poo.Modulo;

/**
 * Cuatro ejemplos prácticos de PRINCIPIOS DE DISEÑO orientado a objetos:
 *
 *   1. Cohesión alta          -> Factura vs FacturaBajaCohesion, RepositorioFacturas
 *   2. Bajo acoplamiento      -> ServicioAutenticacion + RepositorioUsuarios (SQL / Mongo)
 *   3. Reutilización          -> Logger (LoggerConsola, LoggerMemoria) usado por varios módulos
 *   4. Manejo de excepciones  -> ProcesadorDePagos + PagoRechazadoException
 */
public class Demo10PrincipiosDiseno implements Modulo {

    @Override
    public String titulo() {
        return "Principios de diseño: cohesión, acoplamiento, reutilización, excepciones";
    }

    @Override
    public String descripcion() {
        return "Factura cohesiva, autenticación desacoplada por interfaz, Logger reutilizable, ProcesadorDePagos.";
    }

    @Override
    public void ejecutar() {
        Logger logger = new LoggerConsola("app");

        // =====================================================================
        seccion("1. COHESIÓN ALTA: la clase Factura solo sabe de facturas");
        Factura factura = new Factura("F-0001", "Ana Torres");
        factura.agregarLinea("Matrícula POO II (4 créditos)", 1, 720_000);
        factura.agregarLinea("Carné estudiantil", 1, 15_000);
        factura.agregarLinea("Guía de laboratorio", 2, 12_500);
        factura.aplicarDescuento(0.10);
        System.out.println(factura.generarResumenPdf());

        // La persistencia se DELEGA a otro objeto: la factura no sabe guardarse.
        RepositorioFacturas repositorio = new RepositorioFacturasEnMemoria();
        repositorio.guardar(factura);
        System.out.println("Guardada en el repositorio. Facturas almacenadas: " + repositorio.listar().size());
        nota("Todos los métodos de Factura tratan sobre calcular, descontar o resumir. Nada más.");
        nota("FacturaBajaCohesion (abrir el archivo) mezcla cálculo + BD + correo + log: cuatro razones para cambiar.");

        // =====================================================================
        seccion("2. BAJO ACOPLAMIENTO: autenticación que no conoce la base de datos");
        // Misma clase ServicioAutenticacion, dos almacenes distintos.
        ServicioAutenticacion authSql = new ServicioAutenticacion(new RepositorioUsuariosSql(), logger);
        ServicioAutenticacion authMongo = new ServicioAutenticacion(new RepositorioUsuariosMongo(), logger);

        System.out.println("Con SQL   -> ana/cun2026:  " + authSql.autenticar("ana", "cun2026"));
        System.out.println("Con SQL   -> ana/mal:      " + authSql.autenticar("ana", "incorrecta"));
        System.out.println("Con Mongo -> ana/cun2026:  " + authMongo.autenticar("ana", "cun2026"));
        System.out.println("Con Mongo -> luis (no existe en Mongo): " + authMongo.autenticar("luis", "poo-rocks"));
        nota("Cambiamos de SQL a MongoDB sin tocar ServicioAutenticacion: depende de la INTERFAZ RepositorioUsuarios.");
        nota("Las dependencias entran por el constructor (inyección de dependencias).");

        // =====================================================================
        seccion("3. REUTILIZACIÓN: un Logger, muchas aplicaciones");
        // El mismo servicio funciona con OTRO logger sin cambiar una línea.
        LoggerMemoria loggerMovil = new LoggerMemoria();   // simula la app móvil
        ServicioAutenticacion authMovil = new ServicioAutenticacion(new RepositorioUsuariosMongo(), loggerMovil);
        authMovil.autenticar("carla", "mongo123");
        authMovil.autenticar("nadie", "x");
        System.out.println("Historial capturado por LoggerMemoria:");
        loggerMovil.getRegistros().forEach(r -> System.out.println("   " + r));
        nota("LoggerConsola sirve para escritorio; LoggerMemoria para pruebas o móvil. La interfaz Logger es la misma.");
        nota("ServicioAutenticacion y ProcesadorDePagos reutilizan el mismo Logger: se escribió una vez.");

        // =====================================================================
        seccion("4. MANEJO DE EXCEPCIONES: ProcesadorDePagos no interrumpe el flujo");
        ProcesadorDePagos procesador = new ProcesadorDePagos(logger);
        List<Pago> pagos = List.of(
                new Pago("P-101", "4111222233334444", 250_000),   // aprobado
                new Pago("P-102", "0000111122223333", 80_000),    // tarjeta bloqueada
                new Pago("P-103", "4111222233334444", 900_000),   // fondos insuficientes
                new Pago("P-104", "4111222233334444", -5),        // monto inválido
                new Pago("P-105", "5500000000000004", 120_000)    // aprobado
        );
        for (Pago pago : pagos) {
            String respuesta = procesador.procesar(pago);
            System.out.println("   Usuario ve -> " + pago.referencia() + ": " + respuesta);
        }
        System.out.println("Pagos aprobados: " + procesador.getAprobados());
        nota("Se capturó PagoRechazadoException (específica), se informó al usuario en su idioma,");
        nota("se registró el incidente técnico en el log y el bucle siguió con el siguiente pago.");

        // =====================================================================
        seccion("5. Los principios SOLID ya estaban en los ejemplos anteriores");
        System.out.println("   S  Responsabilidad única      -> Factura calcula; RepositorioFacturas guarda.");
        System.out.println("   O  Abierto/Cerrado            -> nuevo Logger o nuevo RepositorioUsuarios: se AGREGA una clase, no se MODIFICA el servicio.");
        System.out.println("   L  Sustitución de Liskov      -> donde se espera un Logger sirve LoggerConsola o LoggerMemoria sin sorpresas.");
        System.out.println("   I  Segregación de interfaces  -> RepositorioUsuarios solo pide lo que autenticación necesita (2 métodos), no 20.");
        System.out.println("   D  Inversión de dependencias  -> ServicioAutenticacion depende de interfaces; las implementaciones se inyectan.");
        nota("Cohesión alta + bajo acoplamiento + reutilización son el RESULTADO de aplicar SOLID.");
    }
}
