using Cun.Poo.Modulo05Gui.Mvc;

namespace Cun.Poo.Modulo05Gui.CalculadoraWinForms;

/// <summary>
/// VISTA de Windows Forms para la calculadora (el CalculadoraUI de NetBeans
/// en el recurso). Solo arma controles y avisa que tecla se presiono: no
/// suma, no resta, no valida. Toda la logica esta en CalculadoraLogica.
///
/// Los controles se crean por codigo para que se vea que cada boton es un
/// objeto. Con el Disenador de Visual Studio se generaria lo mismo en
/// FormCalculadora.Designer.cs, dentro de InitializeComponent().
/// </summary>
public class FormCalculadora : Form, ICalculadoraVista
{
    private readonly TextBox _pantalla;

    public event Action<string>? TeclaPresionada;

    public FormCalculadora()
    {
        Text = "Calculadora MVC - POO II";
        ClientSize = new Size(300, 360);
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 14F);

        // Componente: la pantalla (JTextField txtDisplay en el recurso).
        _pantalla = new TextBox
        {
            Dock = DockStyle.Top,
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Right,
            Font = new Font("Consolas", 22F),
        };

        // Contenedor con layout de rejilla 4x4 (JPanel + GridLayout en Swing).
        var rejilla = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 4 };
        for (int i = 0; i < 4; i++)
        {
            rejilla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            rejilla.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        }

        foreach (char tecla in "789/456*123-C0=+")
        {
            var boton = new Button { Text = tecla.ToString(), Dock = DockStyle.Fill };
            boton.Click += AlPresionarBoton;    // un solo manejador para los 16 botones
            rejilla.Controls.Add(boton);
        }

        // Dock = Fill se agrega primero para que Top quede encima.
        Controls.Add(rejilla);
        Controls.Add(_pantalla);
    }

    private void AlPresionarBoton(object? sender, EventArgs e)
    {
        var boton = (Button)sender!;            // sender = la FUENTE del evento
        TeclaPresionada?.Invoke(boton.Text);    // la vista solo avisa
    }

    public void MostrarPantalla(string texto) => _pantalla.Text = texto;

    public void MostrarError(string mensaje) =>
        MessageBox.Show(this, mensaje, "Calculadora", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
