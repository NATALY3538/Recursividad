using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Practica_02_Fibonacci
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
            Console.WriteLine("  PRÁCTICA 2: SERIE DE FIBONACCI (RECURSIVA)      ");
            Console.WriteLine("==================================================");
            Console.WriteLine("Genera los primeros n términos de la serie con recursividad.");
            Console.WriteLine($"Convención: F(0) = 0, F(1) = 1. Límite seguro: n <= {FibonacciRecursivo.MaxLimiteTerminos}.");
            Console.WriteLine();

            bool continuar = true;
            while (continuar)
            {
                Console.Write("Ingrese la cantidad de términos n (>= 0): ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Error: La entrada no puede estar vacía. Intente nuevamente.\n");
                    continue;
                }

                if (!ValidarYMostrar(input.Trim()))
                {
                    Console.WriteLine("Por favor, ingrese un valor válido.\n");
                    continue;
                }

                Console.Write("\n¿Desea generar otra serie? (s/n): ");
                string? resp = Console.ReadLine();
                if (resp == null || !resp.Trim().Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    continuar = false;
                }
                Console.WriteLine();
            }

            Console.WriteLine("Programa finalizado. ¡Hasta luego!");
        }

        public static bool ValidarYMostrar(string input)
        {
            // Rechazar decimales y fracciones
            if (input.Contains('.') || input.Contains(',') || input.Contains('/'))
            {
                Console.WriteLine("Error: No se permiten fracciones ni números decimales. Debe ingresar un número entero.");
                return false;
            }

            // Validar que sea entero
            if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out int n))
            {
                Console.WriteLine("Error: La entrada no es un número entero válido.");
                return false;
            }

            // Validar no negatividad
            if (n < 0)
            {
                Console.WriteLine($"Error: La cantidad de términos ({n}) no puede ser negativa.");
                return false;
            }

            // Validar límite máximo
            if (n > FibonacciRecursivo.MaxLimiteTerminos)
            {
                Console.WriteLine($"Error: La cantidad ({n}) excede el límite máximo seguro ({FibonacciRecursivo.MaxLimiteTerminos}).");
                return false;
            }

            try
            {
                List<BigInteger> serie = FibonacciRecursivo.ObtenerSerie(n);
                Console.WriteLine($"Cantidad de términos solicitada: {n}");
                if (n == 0)
                {
                    Console.WriteLine("Serie generada: (vacía, 0 términos)");
                }
                else
                {
                    Console.WriteLine($"Serie generada: {string.Join(", ", serie)}");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante el cálculo: {ex.Message}");
                return false;
            }
        }

        private static void ProcesarEntrada(string input)
        {
            ValidarYMostrar(input);
        }
    }
}
