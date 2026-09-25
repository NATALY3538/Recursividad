using System.Globalization;
using System.Windows.Forms;

namespace Practica_02_Fibonacci;

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
        Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both,
        Dock = DockStyle.Fill, Font = new Font("Consolas", 11), WordWrap = true
    };

    public MainForm()
    {
        Text = "Práctica 2 - Serie de Fibonacci";
        MinimumSize = new Size(700, 430);
        Size = new Size(850, 580);
        StartPosition = FormStartPosition.CenterScreen;

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20), ColumnCount = 2, RowCount = 4 };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int i = 0; i < 3; i++) panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var titulo = new Label { Text = "Serie de Fibonacci recursiva", AutoSize = true, Font = new Font("Segoe UI", 17, FontStyle.Bold), Margin = new Padding(0, 0, 0, 18) };
        panel.Controls.Add(titulo, 0, 0);
        panel.SetColumnSpan(titulo, 2);
        panel.Controls.Add(new Label { Text = $"Cantidad de términos (0 a {FibonacciRecursivo.MaxLimiteTerminos}):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
        panel.Controls.Add(entrada, 1, 1);
        var generar = new CupertinoButton { Text = "Generar serie", AutoSize = true, Margin = new Padding(0, 16, 0, 16) };
        generar.Click += Generar;
        panel.Controls.Add(generar, 0, 2);
        panel.SetColumnSpan(generar, 2);
        panel.Controls.Add(resultado, 0, 3);
        panel.SetColumnSpan(resultado, 2);
        Controls.Add(panel);
        CupertinoTheme.Aplicar(this, "\ue24a"); // Material Symbols: functions
        AcceptButton = generar;
    }

    private void Generar(object? sender, EventArgs e)
    {
        if (!int.TryParse(entrada.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int n)
            || n < 0 || n > FibonacciRecursivo.MaxLimiteTerminos)
        {
            MessageBox.Show(this, $"Ingresa una cantidad entera entre 0 y {FibonacciRecursivo.MaxLimiteTerminos}.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            entrada.Focus();
            return;
        }

        var serie = FibonacciRecursivo.ObtenerSerie(n);
        resultado.Text = n == 0
            ? "Serie vacía (0 términos)."
            : $"F(0) = 0, F(1) = 1\r\n\r\n{string.Join(", ", serie)}";
    }
}
