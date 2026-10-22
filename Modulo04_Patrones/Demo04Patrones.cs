using Cun.Poo.Modulo04Patrones.Comportamiento;
using Cun.Poo.Modulo04Patrones.Creacionales;
using Cun.Poo.Modulo04Patrones.Estructurales;

namespace Cun.Poo.Modulo04Patrones;

/// <summary>
/// Tematica 4 (G4): patrones de diseno orientados a objetos.
///
/// Las subcarpetas siguen la estructura que pide el documento maestro
/// (un paquete por categoria): Creacionales, Estructurales y Comportamiento.
/// </summary>
public class Demo04Patrones : ModuloBase
{
    public override string Titulo =>
        "Patrones de diseno: creacionales, estructurales y de comportamiento";

    public override string Descripcion =>
        "Singleton, Factory Method, Builder, Adapter, Composite, Decorator, Observer y Strategy con casos colombianos.";

    public override void Ejecutar()
    {
        // ====================================================================
        Seccion("1. SINGLETON: la configuracion de la fintech se carga una sola vez");
        AppConfig a = AppConfig.Instancia;
        AppConfig b = AppConfig.Instancia;              // otro "modulo" pide la configuracion
        Console.WriteLine($"   Archivo: {a.Origen}");
        Console.WriteLine($"   Aplicacion: {a.Obtener("Aplicacion")}");
        Console.WriteLine($"   API central de riesgo: {a.ObtenerOculto("CentralRiesgo.ApiKey")}");
        Console.WriteLine($"   Tasa mensual: {decimal.Parse(a.Obtener("Credito.TasaMensual")):P1}");
        Console.WriteLine($"   a y b son el mismo objeto? {ReferenceEquals(a, b)}");
        Console.WriteLine($"   Veces que se leyo el archivo: {AppConfig.VecesCargada}");
        // var otra = new AppConfig();   // <- NO COMPILA: el constructor es privado
        Nota("Constructor privado + Lazy<T>: una instancia, segura con varios hilos.");

        // ====================================================================
        Seccion("2. FACTORY METHOD: reportes de cartera en PDF, Excel o CSV");
        List<CreditoCartera> cartera =
        [
            new("Ana Rojas", 1_200_000m, 0),
            new("Luis Pardo", 850_000m, 45),
            new("Marta Rios", 2_300_000m, 12),
        ];

        string formato = a.Obtener("Reportes.FormatoPorDefecto");
        Console.WriteLine($"   Formato configurado: {formato}");
        Console.WriteLine($"   {GeneradorReporte.Para(formato).Exportar(cartera)}");
        Console.WriteLine("   Gerencia pide los otros formatos:");
        foreach (string otro in new[] { "Excel", "CSV" })
        {
            Console.WriteLine($"   {GeneradorReporte.Para(otro).Exportar(cartera)}");
        }
        Nota("El cliente nunca hace new ReportePdf(): pide y la fabrica decide la clase concreta.");
        Nota("Nuevo formato XML = 2 clases nuevas y 1 linea en Para(). Abierto/Cerrado.");

        // ====================================================================
        Seccion("3. BUILDER: una pizza paso a paso, sin un constructor de 8 parametros");
        Pizza hawaiana = new Pizza.Builder()
            .ConMasa("delgada")
            .ConSalsa("tomate")
            .ConQueso("mozzarella")
            .ConIngrediente("jamon")
            .ConIngrediente("pina")
            .Build();
        Console.WriteLine($"   Pizza creada: {hawaiana}");

        try
        {
            var exagerada = new Pizza.Builder();
            foreach (string i in new[] { "jamon", "pina", "tocineta", "maiz", "pollo", "chorizo" })
            {
                exagerada.ConIngrediente(i);
            }
            exagerada.Build();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"   Pizza rechazada: {ex.Message}");
        }
        Nota("Build() valida ANTES de crear: nunca existe una pizza a medio armar.");

        // ====================================================================
        Seccion("4. ADAPTER: el mismo carrito cobra con PayU y con Wompi");
        IPasarelaPago[] pasarelas =
        [
            new PayUAdapter(new PayUApi()),
            new WompiAdapter(new WompiApi(), "ventas@modacol.example"),
        ];
        foreach (IPasarelaPago pasarela in pasarelas)
        {
            var carrito = new CarritoDeCompras(pasarela);
            carrito.Agregar("Chaqueta de jean", 189_900m);
            carrito.Agregar("Tenis blancos", 239_900m);
            Console.WriteLine($"   {carrito.Pagar()}");
        }
        Nota("PayU usa pesos y texto; Wompi centavos y Guid. Cada adaptador traduce lo suyo.");
        Nota("El carrito no tiene ni un if (esPayU). MercadoPago = un adaptador mas.");

        // ====================================================================
        Seccion("5. COMPOSITE: archivos y carpetas se tratan igual");
        var raiz = new Carpeta("C:");
        var documentos = new Carpeta("Documentos");
        documentos.Agregar(new Archivo("reporte.docx", 512))
                  .Agregar(new Carpeta("Universidad").Agregar(new Archivo("taller-poo.pdf", 1_024)));
        raiz.Agregar(documentos)
            .Agregar(new Archivo("vacaciones.jpg", 2_048));

        raiz.Imprimir();
        Console.WriteLine($"   Tamano total del disco: {raiz.Tamano} KB");
        Nota("Tamano de una Carpeta = suma recursiva de sus hijos, sin preguntar si son hojas.");

        // ====================================================================
        Seccion("6. DECORATOR: personalizar el cafe sin crear una clase por combinacion");
        IBebida pedido1 = new ConCanela(new ConCremaBatida(new Tinto()));
        IBebida pedido2 = new ConArequipe(new ConCremaBatida(new Latte()));
        Console.WriteLine($"   {pedido1.Descripcion,-38} {Dinero.Cop(pedido1.Precio)}");
        Console.WriteLine($"   {pedido2.Descripcion,-38} {Dinero.Cop(pedido2.Precio)}");
        Nota("Cada adicion envuelve a la anterior: 2 bases + 3 adiciones = 5 clases, no 16.");
        Nota("En .NET: new StreamReader(new BufferedStream(new FileStream(...))) es un Decorator.");

        // ====================================================================
        Seccion("7. OBSERVER: la estacion avisa sola a todos sus dispositivos");
        var estacion = new EstacionMeteorologica();
        var web = new RegistroWeb();
        estacion.RegistrarObservador(new DisplayPantalla());
        estacion.RegistrarObservador(new DisplayCelular());
        estacion.RegistrarObservador(web);
        estacion.MedicionCambio += (t, h) =>               // la version idiomatica de C#
        {
            if (t >= 30)
            {
                Console.WriteLine($"   [Alerta]   Calor extremo: {t:0.0} C");
            }
        };

        estacion.SetMediciones(25.5, 90);
        Console.WriteLine("   ...cambia el clima...");
        estacion.SetMediciones(31.2, 40);
        Console.WriteLine($"   Historico web: [{string.Join(", ", web.Historico)}]");
        Nota("La estacion no conoce pantallas ni celulares: solo IObservador. Uno a muchos.");
        Nota("En C#, 'event' + delegado es el Observer del lenguaje (como las notificaciones push).");

        // ====================================================================
        Seccion("8. STRATEGY: las promociones del supermercado cambian sin tocar la caja");
        var caja = new CajaRegistradora();
        caja.Registrar(new LineaCompra("Leche entera x6", "Lacteos", 22_800m, 1));
        caja.Registrar(new LineaCompra("Queso campesino", "Lacteos", 9_500m, 1));
        caja.Registrar(new LineaCompra("Galletas de soda", "Galleteria", 4_200m, 3));
        caja.Registrar(new LineaCompra("Cafe molido 500 g", "Despensa", 18_900m, 1));
        Console.WriteLine($"   Compra bruta: {Dinero.Cop(caja.Bruto)}");

        IEstrategiaDescuento[] promociones =
        [
            new SinDescuento(),
            new DescuentoPorcentual("Lacteos", 20),
            new DescuentoPagueLleve("Galleteria", pague: 2, lleve: 3),
            new DescuentoCombinado(new DescuentoPorcentual("Lacteos", 20), new DescuentoTarjetaFidelidad(10)),
        ];
        foreach (IEstrategiaDescuento promocion in promociones)
        {
            caja.UsarPromocion(promocion);
            Console.WriteLine($"   {caja.Resumen()}");
        }
        Nota("Mercadeo inventa la promocion; desarrollo solo escribe una clase nueva.");
        Nota("Ya lo usaste en el modulo 2: IEstrategiaDeTransporte en EntregaYa es otro Strategy.");

        // ====================================================================
        Seccion("RESUMEN: que problema resuelve cada patron");
        Console.WriteLine("   CREACIONALES    Singleton ...... una sola instancia compartida");
        Console.WriteLine("                   Factory Method . crear sin acoplarse a la clase concreta");
        Console.WriteLine("                   Builder ........ objetos complejos paso a paso");
        Console.WriteLine("   ESTRUCTURALES   Adapter ........ hacer compatibles interfaces distintas");
        Console.WriteLine("                   Composite ...... tratar igual a la parte y al todo");
        Console.WriteLine("                   Decorator ...... agregar funciones envolviendo");
        Console.WriteLine("   COMPORTAMIENTO  Observer ....... avisar a muchos cuando algo cambia");
        Console.WriteLine("                   Strategy ....... intercambiar algoritmos en ejecucion");
        Nota("Un patron no es codigo para copiar: es una solucion probada a un problema recurrente.");
    }
}
