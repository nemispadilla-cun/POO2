using Cun.Poo.Modulo05Gui.Componentes;
using Cun.Poo.Modulo05Gui.Mvc;
using Cun.Poo.Modulo05Gui.PuntoDeVenta;

namespace Cun.Poo.Modulo05Gui;

/// <summary>
/// Tematica 5 (G5): desarrollo de interfaces graficas con orientacion a objetos.
///
/// La consola no puede mostrar ventanas, asi que las secciones 1 a 4 usan una
/// mini libreria de componentes (Componentes/) para ver POR DENTRO lo que hace
/// un framework: eventos, contenedores, layouts y MVC. La seccion 5 senala la
/// ventana real de Windows Forms, que reutiliza el mismo modelo y controlador.
/// </summary>
public class Demo05Gui : ModuloBase
{
    public override string Titulo =>
        "Interfaces graficas: eventos, componentes, layouts y MVC";

    public override string Descripcion =>
        "Delegacion de eventos, contenedores y layouts, MVC con una calculadora y un punto de venta, y la misma logica en Windows Forms.";

    public override void Ejecutar()
    {
        // ====================================================================
        Seccion("1. DELEGACION DE EVENTOS: el cajero automatico");
        var clave = new CampoTexto("txtClave", esClave: true);
        var teclado = "1234".Select(c => new Boton(c.ToString())).ToList();
        foreach (Boton tecla in teclado)
        {
            tecla.Click += (sender, e) => clave.Texto += e.Texto;   // OYENTE: agrega el digito
        }
        foreach (int i in new[] { 3, 1, 0, 2 })
        {
            teclado[i].SimularClic();                               // el usuario digita 4-2-1-3
        }
        Console.WriteLine($"   Clave en pantalla: {clave.Dibujar()}  ({clave.Texto.Length} digitos guardados)");

        decimal saldo = 850_000m;
        var retiro = new Boton("Retiro");
        var consulta = new Boton("Consulta de saldo");
        retiro.Click += (sender, e) =>
        {
            saldo -= 200_000m;
            Console.WriteLine($"   [Retiro]    Entregando {Dinero.Cop(200_000m)}");
        };
        consulta.Click += (sender, e) => Console.WriteLine($"   [Saldo]     Su saldo es {Dinero.Cop(saldo)}");

        // Una fuente puede tener varios oyentes, y un oyente atender varias fuentes.
        EventHandler<ClicEventArgs> auditoria =
            (sender, e) => Console.WriteLine($"   [Auditoria] clic en '{e.Texto}'");
        retiro.Click += auditoria;
        consulta.Click += auditoria;

        // El orden lo decide el usuario, no el programa:
        consulta.SimularClic();
        retiro.SimularClic();
        retiro.Click -= auditoria;                                  // se retira el oyente
        consulta.Click -= auditoria;
        consulta.SimularClic();
        Nota("Fuente = Boton, objeto evento = ClicEventArgs, oyente = el metodo registrado con +=.");
        Nota("Cada boton activa solo SUS oyentes: el retiro no ejecuta codigo de la consulta.");

        // ====================================================================
        Seccion("2. CONTENEDORES Y LAYOUTS: el layout ubica, no el programador");
        var teclas = new Panel("pnlTeclas", new LayoutRejilla(4, 4));
        foreach (char t in "789/456*123-C0=+")
        {
            teclas.Agregar(new Boton(t.ToString()));
        }
        teclas.Redimensionar(280, 160);
        Imprimir(teclas);

        Ventana caja = CrearVentanaCaja();
        caja.Redimensionar(360, 200);
        Imprimir(caja);
        caja.Redimensionar(220, 260);                               // el usuario angosta la ventana
        Imprimir(caja);
        Nota("Nadie escribio coordenadas: la rejilla estira, el flujo reacomoda al redimensionar.");
        Nota("Ventana ES UN Panel (herencia) y un Panel guarda Componentes (Composite de G4).");

        // ====================================================================
        Seccion("3. MVC: la calculadora sin logica en la vista");
        var vista = new VistaConsolaCalculadora();
        new ControladorCalculadora(vista, new CalculadoraLogica());
        vista.Presionar("12+30=");
        vista.Presionar("C9/0=");
        Nota("Modelo: CalculadoraLogica. Vista: ICalculadoraVista. Controlador: el switch de teclas.");
        Nota("El modelo no tiene ni un Console ni un Button: se puede probar solo (G8).");

        // ====================================================================
        Seccion("4. CASO POS: tinto y pan de bono en Tostao'");
        var pedido = new Pedido();
        new VistaTicket(pedido);                                    // dos vistas del mismo modelo
        new PantallaCliente(pedido);
        var controladorCaja = new ControladorCaja(pedido);
        foreach (Boton boton in caja.Hijos.OfType<Boton>())
        {
            if (Menu.TryGetValue(boton.Texto, out decimal precio))
            {
                controladorCaja.ConectarProducto(boton, new Producto(boton.Texto, precio));
            }
            else
            {
                controladorCaja.ConectarPago(boton, boton.Texto);
            }
        }

        Console.WriteLine("   8:05 a.m. en Bogota. Ana toca Tinto, Pan de bono y Nequi:");
        BotonDe(caja, "Tinto").SimularClic();
        BotonDe(caja, "Pan de bono").SimularClic();
        BotonDe(caja, "Nequi").SimularClic();
        try
        {
            BotonDe(caja, "Empanada").SimularClic();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"   Otro clic en Empanada: {ex.Message}");
        }
        Nota("Un solo oyente atiende todos los productos: mira el sender y busca su Producto.");
        Nota("Ticket y pantalla del cliente escuchan el evento Cambio: el pedido no las conoce.");

        // ====================================================================
        Seccion("5. LA VENTANA REAL: el mismo MVC en Windows Forms");
        Console.WriteLine("   Modulo05_GUI/CalculadoraWinForms/FormCalculadora.cs");
        Console.WriteLine("      public class FormCalculadora : Form, ICalculadoraVista");
        Console.WriteLine("   Reutiliza sin cambios: CalculadoraLogica, ControladorCalculadora, ICalculadoraVista");
        Console.WriteLine("   Para abrirla (Windows):  ventana.bat");
        Console.WriteLine("                     o:  dotnet run --project Modulo05_GUI/CalculadoraWinForms");
        Nota("Cambiar la consola por una ventana no toco ni una linea del modelo: ese es el valor de MVC.");
    }

    /// <summary>Precios de la vitrina. Los botones que no estan aqui son medios de pago.</summary>
    private static readonly Dictionary<string, decimal> Menu = new()
    {
        ["Tinto"] = 2_500m,
        ["Pan de bono"] = 3_000m,
        ["Empanada"] = 3_500m,
        ["Jugo de mora"] = 6_000m,
    };

    private static Ventana CrearVentanaCaja()
    {
        var ventana = new Ventana("Tostao' - Caja 1", new LayoutFlujo());
        ventana.Agregar(new Etiqueta("Caja 1:"));
        foreach (string producto in Menu.Keys)
        {
            ventana.Agregar(new Boton(producto));
        }
        ventana.Agregar(new Boton("Nequi"));
        ventana.Agregar(new Boton("Efectivo"));
        return ventana;
    }

    private static Boton BotonDe(Panel panel, string texto) =>
        panel.Hijos.OfType<Boton>().First(b => b.Texto == texto);

    private static void Imprimir(Panel panel)
    {
        Console.WriteLine($"   {panel.Dibujar()}");
        foreach (string fila in panel.Renderizar())
        {
            Console.WriteLine("      " + fila);
        }
    }
}
