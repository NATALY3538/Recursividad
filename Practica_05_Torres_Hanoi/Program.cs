using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Practica_05_Torres_Hanoi;

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
        Dock = DockStyle.Fill, Font = new Font("Consolas", 10), WordWrap = false
    };

    public MainForm()
    {
        Text = "Práctica 5 - Torres de Hanói";
        MinimumSize = new Size(750, 540);
        Size = new Size(880, 640);
        StartPosition = FormStartPosition.CenterScreen;

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20), ColumnCount = 2, RowCount = 5 };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int i = 0; i < 4; i++) panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var titulo = new Label { Text = "Torres de Hanói", AutoSize = true, Font = new Font("Segoe UI", 17, FontStyle.Bold), Margin = new Padding(0, 0, 0, 10) };
        panel.Controls.Add(titulo, 0, 0);
        panel.SetColumnSpan(titulo, 2);
        var ayuda = new Label { Text = "A: origen   B: auxiliar   C: destino", AutoSize = true, Margin = new Padding(0, 0, 0, 14) };
        panel.Controls.Add(ayuda, 0, 1);
        panel.SetColumnSpan(ayuda, 2);
        panel.Controls.Add(new Label { Text = $"Discos (1 a {HanoiRecursivo.MaxLimiteDiscos}):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 2);
        panel.Controls.Add(entrada, 1, 2);
        var resolver = new CupertinoButton { Text = "Mostrar movimientos", AutoSize = true, Margin = new Padding(0, 16, 0, 16) };
        resolver.Click += Resolver;
        panel.Controls.Add(resolver, 0, 3);
        panel.SetColumnSpan(resolver, 2);
        panel.Controls.Add(resultado, 0, 4);
        panel.SetColumnSpan(resultado, 2);
        Controls.Add(panel);
        CupertinoTheme.Aplicar(this, "\ue97a"); // Material Symbols: account_tree
        AcceptButton = resolver;
    }

    private void Resolver(object? sender, EventArgs e)
    {
        if (!int.TryParse(entrada.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int n)
            || n < 1 || n > HanoiRecursivo.MaxLimiteDiscos)
        {
            MessageBox.Show(this, $"Ingresa un número entero de discos entre 1 y {HanoiRecursivo.MaxLimiteDiscos}.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            entrada.Focus();
            return;
        }

        var movimientos = HanoiRecursivo.Resolver(n);
        if (!HanoiRecursivo.ValidarReglas(n, movimientos, out string error))
        {
            MessageBox.Show(this, error, "Error de movimientos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var texto = new StringBuilder();
        foreach (var movimiento in movimientos)
            texto.AppendLine(movimiento.ToString());
        texto.AppendLine();
        texto.AppendLine($"Total: {movimientos.Count} movimientos (2^{n} - 1).");
        resultado.Text = texto.ToString();
    }
}
