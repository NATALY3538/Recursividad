using System.Globalization;
using System.Numerics;
using System.Windows.Forms;

namespace Practica_03_MCD;

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
    private readonly TextBox entradaA = new() { Width = 220 };
    private readonly TextBox entradaB = new() { Width = 220 };
    private readonly TextBox resultado = new()
    {
        Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical,
        Dock = DockStyle.Fill, Font = new Font("Consolas", 12)
    };

    public MainForm()
    {
        Text = "Práctica 3 - Máximo común divisor";
        MinimumSize = new Size(650, 420);
        Size = new Size(850, 580);
        StartPosition = FormStartPosition.CenterScreen;

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20), ColumnCount = 2, RowCount = 5 };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int i = 0; i < 4; i++) panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var titulo = new Label { Text = "MCD mediante Euclides recursivo", AutoSize = true, Font = new Font("Segoe UI", 17, FontStyle.Bold), Margin = new Padding(0, 0, 0, 18) };
        panel.Controls.Add(titulo, 0, 0);
        panel.SetColumnSpan(titulo, 2);
        panel.Controls.Add(new Label { Text = "Primer número entero:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
        panel.Controls.Add(entradaA, 1, 1);
        panel.Controls.Add(new Label { Text = "Segundo número entero:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 2);
        panel.Controls.Add(entradaB, 1, 2);
        var calcular = new CupertinoButton { Text = "Calcular MCD", AutoSize = true, Margin = new Padding(0, 16, 0, 16) };
        calcular.Click += Calcular;
        panel.Controls.Add(calcular, 0, 3);
        panel.SetColumnSpan(calcular, 2);
        panel.Controls.Add(resultado, 0, 4);
        panel.SetColumnSpan(resultado, 2);
        Controls.Add(panel);
        CupertinoTheme.Aplicar(this, "\ue9ef"); // Material Symbols: tag
        AcceptButton = calcular;
    }

    private void Calcular(object? sender, EventArgs e)
    {
        if (!BigInteger.TryParse(entradaA.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var a)
            || !BigInteger.TryParse(entradaB.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var b))
        {
            MessageBox.Show(this, "Ingresa dos números enteros válidos.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (a.IsZero && b.IsZero)
        {
            MessageBox.Show(this, "El MCD de 0 y 0 no está definido.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        resultado.Text = $"MCD({a}, {b}) = {McdRecursivo.Calcular(a, b)}";
    }
}
