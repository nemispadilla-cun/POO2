package co.edu.cun.poo.modulo11_interfaces;

import java.util.List;

import co.edu.cun.poo.Modulo;

/**
 * Cinco ejemplos prácticos de INTERFACES Y CLASES ABSTRACTAS (Temática 2 · G2):
 *
 *   1. Interfaz = contrato        -> MetodoDePago + PagoPSE / PagoNequi / PagoEfecty
 *   2. Clase abstracta = molde    -> Personaje + Mago / Guerrero / Arquero
 *   3. Las dos juntas             -> Vehiculo (abstracta) + RastreableGPS (interfaz)
 *   4. Caso EntregaYa S.A.S.      -> ServicioDeDespachoRigido vs ServicioDeDespacho
 *   5. Interfaz funcional         -> Notificador con lambdas, default y static
 */
public class Demo11InterfacesYAbstractas implements Modulo {

    @Override
    public String titulo() {
        return "Interfaces y clases abstractas: contratos, moldes y Strategy";
    }

    @Override
    public String descripcion() {
        return "Ecosistema de pagos, personajes de videojuego, flota de EntregaYa y notificaciones con lambdas.";
    }

    @Override
    public void ejecutar() {

        // =====================================================================
        seccion("1. INTERFAZ = CONTRATO: el checkout no conoce ninguna pasarela");
        Checkout checkout = new Checkout();

        // La lista es del tipo de la INTERFAZ, no de las clases concretas.
        List<MetodoDePago> medios = List.of(
                new PagoPSE("Bancolombia"),
                new PagoNequi("300 123 4567"),
                new PagoEfecty()
        );

        for (MetodoDePago medio : medios) {
            checkout.cobrar(medio, 85_000);     // enlace dinámico: cada uno hace lo suyo
        }
        checkout.cobrar(new PagoEfecty(), 12_000);   // rechazado por su propio mínimo

        System.out.println(" Recibos emitidos: " + checkout.recibos());
        nota("Agregar DaviPlata es crear una clase que implemente MetodoDePago: Checkout no se toca.");
        nota("Efecty sobreescribe el método default montoValido(): el contrato permite excepciones a la regla.");

        // =====================================================================
        seccion("2. CLASE ABSTRACTA = MOLDE: lo común se escribe una sola vez");
        // Personaje personaje = new Personaje("X", 100, 100);  // <- NO COMPILA
        List<Personaje> party = List.of(
                new Mago("Elara"),
                new Guerrero("Bruno"),
                new Arquero("Nayeli")
        );

        for (Personaje p : party) {
            p.jugarTurno("el norte");           // template method heredado
        }
        System.out.println();
        party.forEach(p -> System.out.println("   " + p));

        Personaje mago = party.get(0);
        mago.recibirDanio(45);                  // método concreto heredado
        nota("moverse(), recibirDanio() y jugarTurno() viven en Personaje: cero duplicación en las subclases.");
        nota("usarHabilidadEspecial() es abstracto: el compilador OBLIGA a cada subclase a definirlo.");
        nota("new Personaje(...) no compila: una clase abstracta no se puede instanciar.");

        // =====================================================================
        seccion("3. LAS DOS JUNTAS: Vehiculo abstracta + RastreableGPS interfaz");
        Dron dron = new Dron("DRN-07");
        dron.arrancar();                        // viene de la clase abstracta
        dron.enviarAlertaPosicion();            // viene de la interfaz
        System.out.println("   SOAT del dron: " + MetodoDePago.formatearCOP(dron.calcularSOAT())
                + "  |  SOAT de una moto: " + MetodoDePago.formatearCOP(new Moto("ABC12D").calcularSOAT()));
        nota("Herencia: UNA superclase. Interfaces: TODAS las que haga falta.");
        nota("La clase abstracta aporta estado y fórmulas; la interfaz solo publica el contrato.");

        // =====================================================================
        seccion("4. CASO ENTREGAYA S.A.S.: el if/else frente al patrón Strategy");
        Pedido pedido = new Pedido("PED-9034", "Laura Mesa",
                new Ruta("Chapinero", "Suba", 12.5));

        System.out.println(" Diseño rígido (depende de un String con el tipo):");
        ServicioDeDespachoRigido rigido = new ServicioDeDespachoRigido();
        System.out.println("   " + rigido.asignarPedido(pedido, "Moto"));
        System.out.println("   " + rigido.asignarPedido(pedido, "Bicicleta"));
        System.out.println("   " + rigido.asignarPedido(pedido, "Dron"));   // no soportado

        System.out.println();
        System.out.println(" Diseño flexible (depende de la interfaz EstrategiaDeTransporte):");
        ServicioDeDespacho despacho = new ServicioDeDespacho();
        List<EstrategiaDeTransporte> flota = List.of(
                new Moto("ABC12D"),
                new Bicicleta("BIC-004"),
                dron                                   // el mismo dron de la sección 3
        );
        for (EstrategiaDeTransporte medio : flota) {
            System.out.println("   " + despacho.asignarPedido(pedido, medio));
        }
        nota("El dron entró a la flota sin modificar ServicioDeDespacho: solo se creó una clase.");
        nota("El servicio rígido tuvo que rechazarlo: habría que abrirlo y agregarle otro else-if.");

        // =====================================================================
        seccion("5. INTERFAZ FUNCIONAL: un solo método abstracto habilita las lambdas");
        ServicioNotificaciones servicio = new ServicioNotificaciones();

        servicio.setNotificador(new NotificadorEmail());
        servicio.notificarUsuario("laura@cun.edu.co", "¡Bienvenida a la plataforma!");

        servicio.setNotificador(new NotificadorSms());
        servicio.notificarUsuario("300 123 4567", "Su código de verificación es 123456.");

        // Misma interfaz, escrita como lambda: no hace falta una clase nueva.
        Notificador push = (destino, mensaje) ->
                System.out.println("   [Push -> " + destino + "] " + mensaje);
        servicio.setNotificador(push);
        servicio.notificarUsuario("app-móvil", "Su pedido PED-9034 va en camino.");

        // El método default compone dos notificadores en uno.
        servicio.setNotificador(push.y(new NotificadorEmail()));
        servicio.notificarUsuario("laura@cun.edu.co", "Su pedido fue entregado.");

        nota("Notificador tiene UN método abstracto: por eso puede escribirse como lambda.");
        nota("ServicioNotificaciones cambia de comportamiento en ejecución sin modificar su código.");

        // =====================================================================
        seccion("RESUMEN: ¿clase abstracta o interfaz?");
        System.out.println("   Clase abstracta -> clases MUY relacionadas que comparten estado y código.");
        System.out.println("                      Personaje, Vehiculo.  Herencia simple.");
        System.out.println("   Interfaz        -> clases que NO tienen por qué estar relacionadas");
        System.out.println("                      y solo comparten un comportamiento.");
        System.out.println("                      MetodoDePago, EstrategiaDeTransporte, Notificador.");
        System.out.println("                      Implementación múltiple.");
        nota("Regla práctica: \"es un\" -> clase abstracta;  \"sabe hacer\" -> interfaz.");
    }
}
