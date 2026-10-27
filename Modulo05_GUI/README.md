# Módulo 5 · Interfaces gráficas con orientación a objetos (Temática 5 · G5)

El documento maestro trabaja con Java Swing y el diseñador Matisse de NetBeans.
Aquí está en C# en dos partes:

1. **Demo de consola** (`ejecutar.bat 5`): una mini librería de componentes
   para ver *por dentro* lo que hace un framework gráfico, es decir, eventos,
   contenedores, layouts y MVC.
2. **Ventana real en Windows Forms** (`ventana.bat`): una calculadora que
   reutiliza **el mismo modelo y el mismo controlador** que la demo de consola.

```
Modulo05_GUI/
├── Demo05Gui.cs
├── Componentes/          Componente, Boton, Etiqueta, CampoTexto, Panel, Ventana,
│                         ClicEventArgs, ILayout, LayoutFlujo, LayoutRejilla
├── Mvc/                  CalculadoraLogica (modelo), ICalculadoraVista, ControladorCalculadora,
│                         VistaConsolaCalculadora
├── PuntoDeVenta/         Producto, Pedido (modelo), VistaTicket, PantallaCliente, ControladorCaja
└── CalculadoraWinForms/  proyecto aparte (solo Windows): FormCalculadora + Program
```

| Sección | Ejemplo del recurso | Tipos en este módulo |
|---|---|---|
| 1. Delegación de eventos | Cajero automático (2.1) | `Boton` (fuente), `ClicEventArgs` (objeto evento), lambdas con `+=` (oyentes), `CampoTexto` con clave |
| 2. Componentes, contenedores y layouts | Anatomía de una GUI (1.2) y POS (2.2) | `Componente`, `Panel`, `Ventana`, `ILayout`, `LayoutRejilla`, `LayoutFlujo` |
| 3. MVC | Calculadora y su refactorización (4.2 y 4.4) | `CalculadoraLogica`, `ICalculadoraVista`, `ControladorCalculadora`, `VistaConsolaCalculadora` |
| 4. Caso POS | Tinto y pan de bono en Tostao' (2.2) | `Pedido`, `VistaTicket`, `PantallaCliente`, `ControladorCaja` |
| 5. Ventana real | Calculadora Swing (4.2) | `FormCalculadora : Form, ICalculadoraVista` |

## La ventana de Windows Forms

```bat
ventana.bat
:: o bien
dotnet run --project Modulo05_GUI/CalculadoraWinForms
```

Es un proyecto aparte (`net8.0-windows`) porque Windows Forms solo existe en
Windows y el proyecto del curso debe compilar en cualquier sistema. Por eso
`POO.Fundamentos.csproj` lo excluye con `<Compile Remove=...>`.

El proyecto **no copia** el modelo: enlaza los mismos archivos de `Mvc/`.

```xml
<Compile Include="..\Mvc\CalculadoraLogica.cs" Link="Mvc\CalculadoraLogica.cs" />
<Compile Include="..\Mvc\ControladorCalculadora.cs" Link="Mvc\ControladorCalculadora.cs" />
<Compile Include="..\Mvc\ICalculadoraVista.cs" Link="Mvc\ICalculadoraVista.cs" />
```

La única clase nueva es la vista, `FormCalculadora`. Pasar de la consola a una
ventana no cambió ni una línea del modelo ni del controlador.

> Los botones se crean por código, en un ciclo, para que se vea que cada uno es
> un objeto. Con el **Diseñador de Visual Studio** (el equivalente de Matisse)
> se arrastran desde el Cuadro de herramientas y el código se genera en
> `FormX.Designer.cs`, dentro de `InitializeComponent()` (el `initComponents()`
> de NetBeans).

## Equivalencias Java Swing → C# Windows Forms

| Java Swing (recurso) | C# Windows Forms | Nota |
|---|---|---|
| `JFrame` | `Form` | La ventana principal |
| `JPanel` | `Panel`, `FlowLayoutPanel`, `TableLayoutPanel` | En WinForms el layout es un tipo de panel |
| `JButton` / `JLabel` / `JTextField` | `Button` / `Label` / `TextBox` | |
| `JPasswordField` | `TextBox` con `UseSystemPasswordChar = true` | |
| `JComboBox` / `JCheckBox` | `ComboBox` / `CheckBox` | |
| `JTable` + `JScrollPane` | `DataGridView` | Trae su propia barra de desplazamiento |
| `JOptionPane.showMessageDialog` | `MessageBox.Show` | |
| `JDialog` | otro `Form` abierto con `ShowDialog()` | |
| `FlowLayout` / `GridLayout` | `FlowLayoutPanel` / `TableLayoutPanel` | |
| `BorderLayout` | propiedad `Dock` (`Top`, `Fill`, …) | |
| `addActionListener(listener)` | `boton.Click += Manejador;` | Un `event` de C# |
| `actionPerformed(ActionEvent e)` | `void Manejador(object? sender, EventArgs e)` | `sender` es la fuente |
| `e.getSource()` | `sender` | |
| `initComponents()` (Matisse) | `InitializeComponent()` en `.Designer.cs` | |
| `UIManager.setLookAndFeel(...)` | `ApplicationConfiguration.Initialize()` | Estilos visuales del sistema |
| `EventQueue.invokeLater(...)` | `[STAThread]` + `Application.Run(form)` | Arranca el bucle de eventos |
| JavaFX | WPF, .NET MAUI o Avalonia | La generación "moderna" |

## Lo que muestra la salida
- **Eventos:** el teclado del cajero llena un campo de clave que muestra
  `****`. La consulta y el retiro solo ejecutan *sus* oyentes. El oyente de
  auditoría atiende a los dos botones y se retira con `-=`.
- **Layouts:** la rejilla 4x4 ubica las 16 teclas de la calculadora. El
  FlowLayout de la caja de Tostao' reacomoda los botones al angostar la ventana
  de 360 a 220 píxeles. Nadie escribió coordenadas.
- **MVC:** `1 2 + 3 0 =` muestra `42`. `9 / 0` llega a la vista como error
  porque el modelo lo detecta y el controlador lo traduce.
- **POS:** un tinto ($2.500) y un pan de bono ($3.000) suman $5.500, se pagan
  con Nequi y dos vistas (ticket y pantalla del cliente) se actualizan solas.
  Después del pago, el modelo rechaza otro producto.

## Preguntas para la clase
- ¿Qué pasaría con la calculadora si la suma estuviera escrita dentro del
  `Click` del botón "="? ¿Cómo la probarías sin abrir la ventana?
- `ControladorCaja` usa **un solo** manejador para todos los productos. ¿Qué
  ventaja tiene frente a escribir un método por botón? ¿Cuándo sería peor?
- ¿Por qué `ILayout` es un ejemplo del patrón Strategy de G4? ¿Y `Panel`, del
  patrón Composite?
- El recurso pregunta qué industrias en Colombia seguirán dependiendo de
  aplicaciones de escritorio. Da dos ejemplos y justifícalos.

## Ejercicio propuesto
1. **Calculadora:** agrega el punto decimal y una tecla `±` en
   `CalculadoraLogica`. Comprueba que la consola **y** la ventana las aceptan sin
   tocar el controlador más que en el `switch`.
2. **Layout:** crea `LayoutBorde` (norte, sur, centro), el `BorderLayout` de
   Swing, y úsalo en una ventana del cajero.
3. **POS:** agrega un botón "Anular último" y una tercera vista que lleve el
   conteo de tintos vendidos en el día.
4. **Windows Forms:** construye con el Diseñador de Visual Studio la ventana del
   cajero (clave enmascarada, botones Retiro y Consulta de saldo), con un modelo
   `CuentaBancaria` separado de la vista.
