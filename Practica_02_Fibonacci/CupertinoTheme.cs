using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/// <summary>
/// Apariencia Cupertino con tipografía Roboto e iconos Material Symbols Rounded.
/// Las dos fuentes están integradas en el ejecutable; no requieren instalación.
/// </summary>
internal static class CupertinoTheme
{
    private static readonly Color Fondo = Color.FromArgb(245, 245, 247);
    private static readonly Color Texto = Color.FromArgb(29, 29, 31);
    private static readonly Color Secundario = Color.FromArgb(110, 110, 115);
    private static readonly Color Azul = Color.FromArgb(0, 122, 255);
    private static readonly List<IntPtr> MemoriaFuentes = new();
    private static readonly PrivateFontCollection Roboto = CargarFuente("Roboto-Regular.ttf");
    private static readonly PrivateFontCollection Material = CargarFuente("MaterialSymbols-Practicas.ttf");

    public static void Aplicar(Form formulario, string simbolo)
    {
        formulario.BackColor = Fondo;
        formulario.ForeColor = Texto;
        formulario.Font = new Font(Roboto.Families[0], 10.5f);
        formulario.AutoScaleMode = AutoScaleMode.Dpi;

        var tabla = formulario.Controls.OfType<TableLayoutPanel>().Single();
        tabla.BackColor = Fondo;
        tabla.Padding = new Padding(30);
        var titulo = tabla.Controls.OfType<Label>().First(l => l.Font.Size >= 17);

        foreach (Control control in tabla.Controls)
        {
            if (control is Label etiqueta && !ReferenceEquals(etiqueta, titulo))
            {
                etiqueta.Font = new Font(Roboto.Families[0], 10.5f);
                etiqueta.ForeColor = Secundario;
            }
            else if (control is TextBox caja)
            {
                caja.Font = new Font(Roboto.Families[0], caja.Multiline ? 11.5f : 11f);
                caja.BackColor = Color.White;
                caja.ForeColor = Texto;
                caja.BorderStyle = BorderStyle.FixedSingle;
                caja.Margin = new Padding(4, 5, 4, 5);
                if (!caja.Multiline) caja.Width = Math.Max(caja.Width, 210);
            }
            else if (control is Button boton)
            {
                boton.Font = new Font(Roboto.Families[0], 11f, FontStyle.Bold);
                boton.Height = 44;
                boton.Width = Math.Max(205, boton.Width);
                boton.AutoSize = false;
                boton.FlatStyle = FlatStyle.Flat;
                boton.FlatAppearance.BorderSize = 0;
                boton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 220);
                boton.BackColor = Azul;
                boton.ForeColor = Color.White;
                boton.Image = CrearIcono(simbolo, Color.White);
            }
        }

        var salida = tabla.Controls.OfType<TextBox>().Single(c => c.Multiline);
        int filaSalida = tabla.GetRow(salida);
        tabla.Controls.Remove(salida);
        var tarjeta = new CupertinoCard { Dock = DockStyle.Fill, Padding = new Padding(16), Margin = new Padding(3, 4, 3, 3) };
        salida.Dock = DockStyle.Fill;
        salida.BorderStyle = BorderStyle.None;
        salida.Margin = Padding.Empty;
        tarjeta.Controls.Add(salida);
        tabla.Controls.Add(tarjeta, 0, filaSalida);
        tabla.SetColumnSpan(tarjeta, 2);

        tabla.Controls.Remove(titulo);
        var cabecera = new Panel { Dock = DockStyle.Fill, Height = 62, MinimumSize = new Size(0, 62), BackColor = Fondo, Margin = new Padding(0, 0, 0, 12) };
        var icono = new CupertinoIconBadge
        {
            Text = simbolo, Font = new Font(Material.Families[0], 25f),
            ForeColor = Azul,
            Location = new Point(0, 2), Size = new Size(52, 52)
        };
        titulo.Font = new Font(Roboto.Families[0], 19f, FontStyle.Bold);
        titulo.ForeColor = Texto;
        titulo.Margin = Padding.Empty;
        titulo.Location = new Point(68, 12);
        cabecera.Controls.Add(icono);
        cabecera.Controls.Add(titulo);
        tabla.Controls.Add(cabecera, 0, 0);
        tabla.SetColumnSpan(cabecera, 2);
    }

    private static PrivateFontCollection CargarFuente(string nombre)
    {
        var ensamblado = Assembly.GetExecutingAssembly();
        string recurso = ensamblado.GetManifestResourceNames().Single(n => n.EndsWith(nombre, StringComparison.Ordinal));
        using Stream flujo = ensamblado.GetManifestResourceStream(recurso)!;
        using var datos = new MemoryStream();
        flujo.CopyTo(datos);
        byte[] bytes = datos.ToArray();
        IntPtr memoria = Marshal.AllocCoTaskMem(bytes.Length);
        Marshal.Copy(bytes, 0, memoria, bytes.Length);
        MemoriaFuentes.Add(memoria);
        var fuentes = new PrivateFontCollection();
        fuentes.AddMemoryFont(memoria, bytes.Length);
        return fuentes;
    }

    private static Bitmap CrearIcono(string simbolo, Color color)
    {
        var imagen = new Bitmap(28, 28);
        using var grafico = Graphics.FromImage(imagen);
        grafico.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        using var brocha = new SolidBrush(color);
        using var fuente = new Font(Material.Families[0], 21f);
        using var formato = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        grafico.DrawString(simbolo, fuente, brocha, new RectangleF(0, -1, 28, 28), formato);
        return imagen;
    }

}

internal static class FiguraRedondeada
{
    public static GraphicsPath Crear(Rectangle area, int radio)
    {
        int d = radio * 2;
        var camino = new GraphicsPath();
        camino.AddArc(area.Left, area.Top, d, d, 180, 90);
        camino.AddArc(area.Right - d, area.Top, d, d, 270, 90);
        camino.AddArc(area.Right - d, area.Bottom - d, d, d, 0, 90);
        camino.AddArc(area.Left, area.Bottom - d, d, d, 90, 90);
        camino.CloseFigure();
        return camino;
    }
}

internal sealed class CupertinoCard : Panel
{
    public CupertinoCard() => SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.Clear(Parent?.BackColor ?? Color.White);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var camino = FiguraRedondeada.Crear(new Rectangle(0, 0, Width - 1, Height - 1), 18);
        using var fondo = new SolidBrush(Color.White);
        e.Graphics.FillPath(fondo, camino);
    }
}

internal sealed class CupertinoIconBadge : Control
{
    public CupertinoIconBadge() => SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.Clear(Parent?.BackColor ?? Color.White);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var camino = FiguraRedondeada.Crear(new Rectangle(0, 0, Width - 1, Height - 1), 15);
        using var fondo = new SolidBrush(Color.White);
        e.Graphics.FillPath(fondo, camino);
        e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        using var tinta = new SolidBrush(ForeColor);
        using var centro = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        e.Graphics.DrawString(Text, Font, tinta, new RectangleF(0, -2, Width, Height), centro);
    }
}

internal sealed class CupertinoButton : Button
{
    private bool encima;
    private bool presionado;

    public CupertinoButton()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        Cursor = Cursors.Hand;
    }

    protected override void OnMouseEnter(EventArgs e) { encima = true; Invalidate(); base.OnMouseEnter(e); }
    protected override void OnMouseLeave(EventArgs e) { encima = false; presionado = false; Invalidate(); base.OnMouseLeave(e); }
    protected override void OnMouseDown(MouseEventArgs e) { presionado = true; Invalidate(); base.OnMouseDown(e); }
    protected override void OnMouseUp(MouseEventArgs e) { presionado = false; Invalidate(); base.OnMouseUp(e); }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.Clear(Parent?.BackColor ?? Color.White);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var camino = new GraphicsPath();
        int r = 16, d = r * 2;
        camino.AddArc(0, 0, d, d, 180, 90);
        camino.AddArc(Width - d - 1, 0, d, d, 270, 90);
        camino.AddArc(Width - d - 1, Height - d - 1, d, d, 0, 90);
        camino.AddArc(0, Height - d - 1, d, d, 90, 90);
        camino.CloseFigure();
        using var fondo = new SolidBrush(presionado ? Color.FromArgb(0, 89, 190) : encima ? Color.FromArgb(0, 105, 220) : BackColor);
        e.Graphics.FillPath(fondo, camino);

        int anchoIcono = Image?.Width ?? 0;
        int anchoTexto = TextRenderer.MeasureText(Text, Font, Size.Empty, TextFormatFlags.NoPadding).Width;
        int espacio = anchoIcono > 0 ? 8 : 0;
        int x = Math.Max(8, (Width - anchoIcono - espacio - anchoTexto) / 2);
        if (Image is not null)
        {
            e.Graphics.DrawImage(Image, x, (Height - Image.Height) / 2);
            x += anchoIcono + espacio;
        }
        TextRenderer.DrawText(e.Graphics, Text, Font, new Rectangle(x, 0, Width - x - 4, Height), ForeColor,
            TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
    }
}
