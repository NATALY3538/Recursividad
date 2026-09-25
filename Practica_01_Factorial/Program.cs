using System;
using System.Globalization;
using System.Numerics;

namespace Practica_01_Factorial
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Si se pasa un argumento por línea de comandos, procesarlo directamente (modo batch/prueba)
            if (args.Length > 0)
            {
                ProcesarEntrada(args[0]);
                return;
            }

            // Modo interactivo
            Console.WriteLine("==================================================");
            Console.WriteLine("  PRÁCTICA 1: CÁLCULO DE FACTORIAL (RECURSIVO)    ");
            Console.WriteLine("==================================================");
            Console.WriteLine($"Calcula n! mediante recursión pura (0 <= n <= {FactorialRecursivo.MaxLimiteSeguro}).");
            Console.WriteLine();

            bool continuar = true;
            while (continuar)
            {
                Console.Write("Ingrese un número entero n (>= 0): ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Error: La entrada no puede estar vacía. Intente nuevamente.\n");
                    continue;
                }

                if (!ValidarYCalcular(input.Trim(), out _))
                {
                    Console.WriteLine("Por favor, ingrese un valor válido.\n");
                    continue;
                }

                Console.Write("\n¿Desea calcular otro número? (s/n): ");
                string? resp = Console.ReadLine();
                if (resp == null || !resp.Trim().Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    continuar = false;
                }
                Console.WriteLine();
            }

            Console.WriteLine("Programa finalizado. ¡Hasta luego!");
        }

        public static bool ValidarYCalcular(string input, out BigInteger resultado)
        {
            resultado = BigInteger.Zero;

            // Detectar si ingresó una fracción o decimal
            if (input.Contains('.') || input.Contains(',') || input.Contains('/'))
            {
                Console.WriteLine("Error: No se admiten fracciones ni números decimales. Debe ser un entero.");
                return false;
            }

            // Verificar si es un entero válido
            if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out int n))
            {
                Console.WriteLine("Error: La entrada no es un número entero válido.");
                return false;
            }

            // Validar no negatividad
            if (n < 0)
            {
                Console.WriteLine($"Error: El número ({n}) es negativo. El factorial solo está definido para n >= 0.");
                return false;
            }

            // Validar límite seguro
            if (n > FactorialRecursivo.MaxLimiteSeguro)
            {
                Console.WriteLine($"Error: El número ({n}) excede el límite máximo seguro de {FactorialRecursivo.MaxLimiteSeguro}.");
                return false;
            }

            // Calcular recursivamente
            try
            {
                resultado = FactorialRecursivo.Calcular(n);
                Console.WriteLine($"Operación: {n}!");
                Console.WriteLine($"Resultado: {n}! = {resultado}");
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
            ValidarYCalcular(input, out _);
        }
    }
}
