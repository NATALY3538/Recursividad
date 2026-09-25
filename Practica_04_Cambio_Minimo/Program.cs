using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Practica_04_Cambio_Minimo;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}

public sealed class MainForm : Form
{
    private readonly TextBox entradaPrecio = new() { Width = 180 };
    private readonly TextBox entradaPago = new() { Width = 180 };
    private readonly TextBox resultado = new()
    {
        Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical,
        Dock = DockStyle.Fill, Font = new Font("Consolas", 11)
    };

    public MainForm()
    {
        Text = "Práctica 4 - Cambio mínimo de monedas";
        MinimumSize = new Size(720, 560);
        Size = new Size(880, 700);
        StartPosition = FormStartPosition.CenterScreen;

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20), ColumnCount = 2, RowCount = 6 };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int i = 0; i < 5; i++) panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var titulo = new Label { Text = "Cambio con el mínimo de monedas", AutoSize = true, Font = new Font("Segoe UI", 17, FontStyle.Bold), Margin = new Padding(0, 0, 0, 10) };
        panel.Controls.Add(titulo, 0, 0);
        panel.SetColumnSpan(titulo, 2);
        var ayuda = new Label { Text = "Importes en pesos; usa punto o coma decimal, máximo dos cifras. Cambio máximo: $10,000.00.", AutoSize = true, Margin = new Padding(0, 0, 0, 14) };
        panel.Controls.Add(ayuda, 0, 1);
        panel.SetColumnSpan(ayuda, 2);
        panel.Controls.Add(new Label { Text = "Precio de compra ($):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 2);
        panel.Controls.Add(entradaPrecio, 1, 2);
        panel.Controls.Add(new Label { Text = "Pago recibido ($):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 3);
        panel.Controls.Add(entradaPago, 1, 3);
        var calcular = new CupertinoButton { Text = "Calcular cambio", AutoSize = true, Margin = new Padding(0, 16, 0, 16) };
        calcular.Click += Calcular;
        panel.Controls.Add(calcular, 0, 4);
        panel.SetColumnSpan(calcular, 2);
        panel.Controls.Add(resultado, 0, 5);
        panel.SetColumnSpan(resultado, 2);
        Controls.Add(panel);
        CupertinoTheme.Aplicar(this, "\uef63"); // Material Symbols: payments
        AcceptButton = calcular;
    }

    private static bool IntentarLeerMonto(string texto, out decimal monto)
    {
        string normalizado = texto.Trim().Replace(',', '.');
        monto = 0m;
        if (normalizado.Length == 0 || normalizado.Count(c => c == '.') > 1)
            return false;
        int punto = normalizado.IndexOf('.');
        if (punto >= 0 && normalizado.Length - punto - 1 > 2)
            return false;
        return decimal.TryParse(normalizado,
            NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
            CultureInfo.InvariantCulture, out monto) && monto >= 0m;
    }

    private void Calcular(object? sender, EventArgs e)
    {
        if (!IntentarLeerMonto(entradaPrecio.Text, out decimal precio)
            || !IntentarLeerMonto(entradaPago.Text, out decimal pago))
        {
            MessageBox.Show(this, "Ingresa importes no negativos con máximo dos decimales.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (pago < precio)
        {
            MessageBox.Show(this, "El pago debe ser igual o mayor que el precio.", "Pago insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            ResultadoCambio cambio = CambioMinimoRecursivo.CalcularCambio(precio, pago);
            var texto = new StringBuilder();
            texto.AppendLine($"Precio: ${precio:F2}   Pago: ${pago:F2}");
            texto.AppendLine($"Cambio: ${cambio.CambioTotal:F2}");
            texto.AppendLine();
            foreach (var moneda in cambio.Detalle)
                texto.AppendLine($"{moneda.CantidadMonedas,4} × {moneda.Descripcion}");
            texto.AppendLine();
            texto.AppendLine($"Total de monedas: {cambio.TotalMonedas}");
            resultado.Text = texto.ToString();
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(this, ex.Message, "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(this, ex.Message, "No se pudo calcular", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
