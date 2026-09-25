using System;
using System.Collections.Generic;
using System.Globalization;

namespace Practica_05_Torres_Hanoi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            if (args.Length > 0)
            {
                ProcesarEntrada(args[0]);
                return;
            }

            Console.WriteLine("==================================================");
            Console.WriteLine("  PRÁCTICA 5: TORRES DE HANÓI (RECURSIVO)         ");
            Console.WriteLine("==================================================");
            Console.WriteLine("Describe todos los movimientos para trasladar n discos.");
            Console.WriteLine("Varillas: A (Origen), B (Auxiliar), C (Destino).");
            Console.WriteLine($"Reglas: 1 disco a la vez, nunca grande sobre pequeño, solo disco superior.");
            Console.WriteLine($"Límite máximo seguro para consola: n <= {HanoiRecursivo.MaxLimiteDiscos}.");
            Console.WriteLine();

            bool continuar = true;
            while (continuar)
            {
                Console.Write("Ingrese el número de discos n (entero >= 1): ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Error: La entrada no puede estar vacía. Intente nuevamente.\n");
                    continue;
                }

                if (!ValidarYResolver(input.Trim()))
                {
                    Console.WriteLine("Por favor, ingrese un valor válido.\n");
                    continue;
                }

                Console.Write("\n¿Desea resolver para otra cantidad de discos? (s/n): ");
                string? resp = Console.ReadLine();
                if (resp == null || !resp.Trim().Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    continuar = false;
                }
                Console.WriteLine();
            }

            Console.WriteLine("Programa finalizado. ¡Hasta luego!");
        }

        public static bool ValidarYResolver(string input)
        {
            if (input.Contains('.') || input.Contains(',') || input.Contains('/'))
            {
                Console.WriteLine("Error: No se admiten fracciones ni decimales. Debe ingresar un número entero positivo.");
                return false;
            }

            if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out int n))
            {
                Console.WriteLine("Error: La entrada no es un número entero válido.");
                return false;
            }

            if (n <= 0)
            {
                Console.WriteLine($"Error: El número de discos ({n}) debe ser estrictamente mayor a cero (n >= 1).");
                return false;
            }

            if (n > HanoiRecursivo.MaxLimiteDiscos)
            {
                Console.WriteLine($"Error: El número de discos ({n}) excede el límite máximo seguro ({HanoiRecursivo.MaxLimiteDiscos}).");
                return false;
            }

            try
            {
                List<MovimientoHanoi> movimientos = HanoiRecursivo.Resolver(n);

                Console.WriteLine($"\n--- Secuencia de Movimientos para {n} discos ---");
                Console.WriteLine($"Varilla Origen: A | Varilla Auxiliar: B | Varilla Destino: C\n");

                foreach (var mov in movimientos)
                {
                    Console.WriteLine(mov.ToString());
                }

                long totalEsperado = (1L << n) - 1;
                Console.WriteLine($"\nTotal de movimientos realizados: {movimientos.Count} (2^{n} - 1 = {totalEsperado})");

                // Verificación de integridad de reglas
                if (HanoiRecursivo.ValidarReglas(n, movimientos, out string errorRegla))
                {
                    Console.WriteLine("Verificación de reglas: TODOS LOS MOVIMIENTOS CUMPLEN RIGUROSAMENTE LAS 3 REGLAS.");
                }
                else
                {
                    Console.WriteLine($"ALERTA: Se detectó una violación de reglas: {errorRegla}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante la resolución: {ex.Message}");
                return false;
            }
        }

        private static void ProcesarEntrada(string input)
        {
            ValidarYResolver(input);
        }
    }
}
