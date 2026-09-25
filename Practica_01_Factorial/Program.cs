using System.Globalization;
using System.Windows.Forms;

namespace Practica_01_Factorial;

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
    private readonly TextBox entrada = new() { Width = 180 };
    private readonly TextBox resultado = new()
    {
        Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical,
        Dock = DockStyle.Fill, Font = new Font("Consolas", 11)
    };

    public MainForm()
    {
        Text = "Práctica 1 - Factorial recursivo";
        MinimumSize = new Size(650, 400);
        Size = new Size(850, 580);
        StartPosition = FormStartPosition.CenterScreen;

        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, Padding = new Padding(20), ColumnCount = 2, RowCount = 4
        };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var titulo = new Label { Text = "Factorial mediante recursividad", AutoSize = true, Font = new Font("Segoe UI", 17, FontStyle.Bold), Margin = new Padding(0, 0, 0, 18) };
        panel.Controls.Add(titulo, 0, 0);
        panel.SetColumnSpan(titulo, 2);
        panel.Controls.Add(new Label { Text = $"Número entero (0 a {FactorialRecursivo.MaxLimiteSeguro}):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
        panel.Controls.Add(entrada, 1, 1);
        var calcular = new CupertinoButton { Text = "Calcular factorial", AutoSize = true, Margin = new Padding(0, 16, 0, 16) };
        calcular.Click += Calcular;
        panel.Controls.Add(calcular, 0, 2);
        panel.SetColumnSpan(calcular, 2);
        panel.Controls.Add(resultado, 0, 3);
        panel.SetColumnSpan(resultado, 2);
        Controls.Add(panel);
        CupertinoTheme.Aplicar(this, "\uea5f"); // Material Symbols: calculate
        AcceptButton = calcular;
    }

    private void Calcular(object? sender, EventArgs e)
    {
        if (!int.TryParse(entrada.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int n)
            || n < 0 || n > FactorialRecursivo.MaxLimiteSeguro)
        {
            MessageBox.Show(this, $"Ingresa un número entero entre 0 y {FactorialRecursivo.MaxLimiteSeguro}.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            entrada.Focus();
            return;
        }

        resultado.Text = $"{n}! = {FactorialRecursivo.Calcular(n)}";
    }
}
