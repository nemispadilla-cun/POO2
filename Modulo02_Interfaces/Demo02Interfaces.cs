namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// Tematica 2 (G2): interfaces y clases abstractas.
/// </summary>
public class Demo02Interfaces : ModuloBase
{
    public override string Titulo =>
        "Interfaces y clases abstractas: contratos, moldes y Strategy";

    public override string Descripcion =>
        "Ecosistema de pagos, personajes de videojuego, flota de EntregaYa y notificaciones con lambdas.";

    public override void Ejecutar()
    {
        // ====================================================================
        Seccion("1. INTERFAZ = CONTRATO: el checkout no conoce ninguna pasarela");
        var checkout = new Checkout();

        // La lista es del tipo de la INTERFAZ, no de las clases concretas.
        List<IMetodoDePago> medios =
        [
            new PagoPse("Bancolombia"),
            new PagoNequi("300 123 4567"),
            new PagoEfecty(),
        ];

        foreach (IMetodoDePago medio in medios)
        {
            checkout.Cobrar(medio, 85_000m);     // enlace dinamico
        }
        checkout.Cobrar(new PagoEfecty(), 12_000m);   // rechazado por su propio minimo

        Console.WriteLine("    Recibos emitidos: [" + string.Join(", ", checkout.Recibos) + "]");
        Nota("Agregar DaviPlata es crear una clase que implemente IMetodoDePago: Checkout no se toca.");
        Nota("Efecty sobreescribe el metodo default MontoValido(): el contrato admite excepciones a la regla.");

        // ====================================================================
        Seccion("2. CLASE ABSTRACTA = MOLDE: lo comun se escribe una sola vez");
        // var personaje = new Personaje("X", 100, 100);  // <- NO COMPILA
        List<Personaje> party =
        [
            new Mago("Elara"),
            new Guerrero("Bruno"),
            new Arquero("Nayeli"),
        ];

        foreach (Personaje p in party)
        {
            p.JugarTurno("el norte");           // template method heredado
        }
        Console.WriteLine();
        party.ForEach(p => Console.WriteLine("   " + p));

        party[0].RecibirDanio(45);              // metodo concreto heredado
        Nota("Moverse(), RecibirDanio() y JugarTurno() viven en Personaje: cero duplicacion.");
        Nota("UsarHabilidadEspecial() es abstracto: el compilador OBLIGA a cada subclase a definirlo.");
        Nota("new Personaje(...) no compila: una clase abstracta no se puede instanciar.");

        // ====================================================================
        Seccion("3. LAS DOS JUNTAS: Vehiculo abstracta + IRastreableGps interfaz");
        var dron = new Dron("DRN-07");
        dron.Arrancar();                        // viene de la clase abstracta
        dron.EnviarAlertaPosicion();            // viene de la interfaz
        Console.WriteLine($"   SOAT del dron: {Dinero.Cop(dron.CalcularSoat())}"
                          + $"  |  SOAT de una moto: {Dinero.Cop(new Moto("ABC12D").CalcularSoat())}");
        Nota("Herencia: UNA clase base. Interfaces: TODAS las que haga falta.");
        Nota("La clase abstracta aporta estado y formulas; la interfaz solo publica el contrato.");

        // ====================================================================
        Seccion("4. CASO ENTREGAYA S.A.S.: el if/else frente al patron Strategy");
        var pedido = new Pedido("PED-9034", "Laura Mesa", new Ruta("Chapinero", "Suba", 12.5));

        Console.WriteLine("   Diseno rigido (depende de un string con el tipo):");
        var rigido = new ServicioDeDespachoRigido();
        Console.WriteLine("     " + rigido.AsignarPedido(pedido, "Moto"));
        Console.WriteLine("     " + rigido.AsignarPedido(pedido, "Bicicleta"));
        Console.WriteLine("     " + rigido.AsignarPedido(pedido, "Dron"));   // no soportado

        Console.WriteLine();
        Console.WriteLine("   Diseno flexible (depende de la interfaz IEstrategiaDeTransporte):");
        var despacho = new ServicioDeDespacho();
        List<IEstrategiaDeTransporte> flota = [new Moto("ABC12D"), new Bicicleta("BIC-004"), dron];
        foreach (IEstrategiaDeTransporte medio in flota)
        {
            Console.WriteLine("     " + despacho.AsignarPedido(pedido, medio));
        }
        Nota("El dron entro a la flota sin modificar ServicioDeDespacho: solo se creo una clase.");
        Nota("El servicio rigido tuvo que rechazarlo: habria que abrirlo y agregarle otro else-if.");

        // ====================================================================
        Seccion("5. UN SOLO METODO: interfaz funcional en Java, DELEGADO en C#");
        var servicio = new ServicioNotificaciones();

        servicio.SetNotificador(new NotificadorEmail());
        servicio.NotificarUsuario("laura@cun.edu.co", "Bienvenida a la plataforma.");

        servicio.SetNotificador(new NotificadorSms());
        servicio.NotificarUsuario("300 123 4567", "Su codigo de verificacion es 123456.");

        // En C# la lambda encaja en un Action<string,string>: no hace falta
        // ninguna anotacion ni una interfaz especial.
        Action<string, string> push = (destino, mensaje) =>
            Console.WriteLine($"   [Push -> {destino}] {mensaje}");
        servicio.SetNotificador(push);
        servicio.NotificarUsuario("app-movil", "Su pedido PED-9034 va en camino.");

        // El metodo default compone dos notificadores en uno.
        INotificador email = new NotificadorEmail();
        servicio.SetNotificador(INotificador.Desde(push).Y(email));
        servicio.NotificarUsuario("laura@cun.edu.co", "Su pedido fue entregado.");

        Nota("Java exige @FunctionalInterface para usar lambdas; C# usa delegados (Action / Func).");
        Nota("ServicioNotificaciones cambia de comportamiento en ejecucion sin modificar su codigo.");

        // ====================================================================
        Seccion("RESUMEN: clase abstracta o interfaz?");
        Console.WriteLine("   Clase abstracta -> clases MUY relacionadas que comparten estado y codigo.");
        Console.WriteLine("                      Personaje, Vehiculo.  Herencia simple.");
        Console.WriteLine("   Interfaz        -> clases que NO tienen por que estar relacionadas");
        Console.WriteLine("                      y solo comparten un comportamiento.");
        Console.WriteLine("                      IMetodoDePago, IEstrategiaDeTransporte, INotificador.");
        Console.WriteLine("                      Implementacion multiple.");
        Nota("Regla practica: \"es un\" -> clase abstracta;  \"sabe hacer\" -> interfaz.");
    }
}
