using System;
using System.Globalization;

namespace Practica_04_Cambio_Minimo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            if (args.Length >= 2)
            {
                ProcesarEntrada(args[0], args[1]);
                return;
            }

            Console.WriteLine("==================================================");
            Console.WriteLine("  PRÁCTICA 4: CAMBIO MÍNIMO DE MONEDAS (RECURSIVO)");
            Console.WriteLine("==================================================");
            Console.WriteLine("Calcula el cambio devolviendo el mínimo número de piezas (monedas).");
            Console.WriteLine($"Denominaciones: $100, $50, $20, $10, $5, $1, 50¢, 20¢, 1¢.");
            Console.WriteLine($"Límite máximo de cambio: ${CambioMinimoRecursivo.MaxCambioPesos:F2} MXN.");
            Console.WriteLine();

            bool continuar = true;
            while (continuar)
            {
                decimal precio = SolicitarMonto("Ingrese el total o precio de compra ($): ");
                decimal pago;

                while (true)
                {
                    pago = SolicitarMonto("Ingrese el pago recibido ($): ");
                    if (pago < precio)
                    {
                        Console.WriteLine($"Error: El pago (${pago:F2}) es menor al precio (${precio:F2}). Ingrese un pago suficiente.\n");
                        continue;
                    }
                    break;
                }

                if (ValidarYEjecutar(precio, pago, out ResultadoCambio? resultado) && resultado != null)
                {
                    ImprimirResultado(resultado);
                }

                Console.Write("\n¿Desea realizar otro cálculo? (s/n): ");
                string? resp = Console.ReadLine();
                if (resp == null || !resp.Trim().Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    continuar = false;
                }
                Console.WriteLine();
            }

            Console.WriteLine("Programa finalizado. ¡Hasta luego!");
        }

        private static decimal SolicitarMonto(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Error: La entrada no puede estar vacía. Intente de nuevo.");
                    continue;
                }

                if (ParsearMonto(input.Trim(), out decimal monto, out string errorMsg))
                {
                    return monto;
                }

                Console.WriteLine($"Error: {errorMsg}");
            }
        }

        public static bool ParsearMonto(string input, out decimal monto, out string errorMsg)
        {
            monto = 0m;
            errorMsg = string.Empty;

            // Reemplazar coma por punto para admitir ambos separadores decimales
            string normalizada = input.Replace(',', '.');

            // Verificar caracteres válidos
            if (!decimal.TryParse(normalizada, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal valor))
            {
                errorMsg = "La entrada no es una cantidad numérica válida.";
                return false;
            }

            if (valor < 0)
            {
                errorMsg = "La cantidad no puede ser negativa.";
                return false;
            }

            // Validar que no tenga más de 2 decimales
            int idxPunto = normalizada.IndexOf('.');
            if (idxPunto >= 0)
            {
                int decimales = normalizada.Length - idxPunto - 1;
                if (decimales > 2)
                {
                    errorMsg = $"La cantidad tiene {decimales} decimales. Solo se permiten hasta 2 decimales (centavos).";
                    return false;
                }
            }

            monto = valor;
            return true;
        }

        public static bool ValidarYEjecutar(decimal precio, decimal pago, out ResultadoCambio? resultado)
        {
            resultado = null;
            try
            {
                resultado = CambioMinimoRecursivo.CalcularCambio(precio, pago);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public static void ImprimirResultado(ResultadoCambio res)
        {
            Console.WriteLine("\n================== DESGLOSE DE CAMBIO ==================");
            Console.WriteLine($"Total compra:  ${res.Precio:F2}");
            Console.WriteLine($"Pago recibido: ${res.Pago:F2}");
            Console.WriteLine($"Cambio vuelto: ${res.CambioTotal:F2}");
            Console.WriteLine("Monedas a entregar (mínimo número de piezas):");
            foreach (var d in res.Detalle)
            {
                string palabraMoneda = d.CantidadMonedas == 1 ? "moneda" : "monedas";
                Console.WriteLine($"  ▪ {d.CantidadMonedas} {palabraMoneda} de {d.Descripcion}");
            }
            Console.WriteLine($"Total de monedas: {res.TotalMonedas}");
            Console.WriteLine("========================================================\n");
        }

        private static void ProcesarEntrada(string strPrecio, string strPago)
        {
            if (!ParsearMonto(strPrecio, out decimal precio, out string errPrecio))
            {
                Console.WriteLine($"Error en precio: {errPrecio}");
                return;
            }

            if (!ParsearMonto(strPago, out decimal pago, out string errPago))
            {
                Console.WriteLine($"Error en pago: {errPago}");
                return;
            }

            if (pago < precio)
            {
                Console.WriteLine($"Error: El pago (${pago:F2}) es menor al precio (${precio:F2}). Pago insuficiente.");
                return;
            }

            if (ValidarYEjecutar(precio, pago, out ResultadoCambio? resultado) && resultado != null)
            {
                ImprimirResultado(resultado);
            }
        }
    }
}
