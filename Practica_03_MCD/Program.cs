using System;
using System.Globalization;
using System.Numerics;

namespace Practica_03_MCD
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
            Console.WriteLine("  PRÁCTICA 3: MÁXIMO COMÚN DIVISOR (RECURSIVO)    ");
            Console.WriteLine("==================================================");
            Console.WriteLine("Calcula el MCD de dos números enteros mediante el algoritmo de Euclides.");
            Console.WriteLine("Se admiten números positivos, negativos y cero. (0, 0) no está definido.");
            Console.WriteLine();

            bool continuar = true;
            while (continuar)
            {
                BigInteger a = SolicitarEntero("Ingrese el primer número entero (a): ");
                BigInteger b = SolicitarEntero("Ingrese el segundo número entero (b): ");

                if (a.IsZero && b.IsZero)
                {
                    Console.WriteLine("\nError: El MCD(0, 0) no está definido matemáticamente.");
                    Console.WriteLine("Por favor, ingrese valores válidos donde al menos uno no sea cero.\n");
                    continue;
                }

                try
                {
                    BigInteger mcd = McdRecursivo.Calcular(a, b);
                    Console.WriteLine("\n--- Resultado ---");
                    Console.WriteLine($"Números ingresados: a = {a}, b = {b}");
                    Console.WriteLine($"MCD({a}, {b}) = {mcd}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al calcular: {ex.Message}");
                }

                Console.Write("\n¿Desea calcular otro MCD? (s/n): ");
                string? resp = Console.ReadLine();
                if (resp == null || !resp.Trim().Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    continuar = false;
                }
                Console.WriteLine();
            }

            Console.WriteLine("Programa finalizado. ¡Hasta luego!");
        }

        private static BigInteger SolicitarEntero(string prompt)
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

                if (input.Contains('.') || input.Contains(',') || input.Contains('/'))
                {
                    Console.WriteLine("Error: No se admiten fracciones ni decimales. Ingrese un número entero.");
                    continue;
                }

                if (BigInteger.TryParse(input.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out BigInteger valor))
                {
                    return valor;
                }

                Console.WriteLine("Error: La entrada no es un número entero válido. Intente de nuevo.");
            }
        }

        public static bool ValidarYCalcular(string inputA, string inputB, out BigInteger resultado)
        {
            resultado = BigInteger.Zero;

            if (inputA.Contains('.') || inputA.Contains(',') || inputA.Contains('/') ||
                inputB.Contains('.') || inputB.Contains(',') || inputB.Contains('/'))
            {
                Console.WriteLine("Error: No se admiten fracciones ni decimales. Ambos deben ser enteros.");
                return false;
            }

            if (!BigInteger.TryParse(inputA.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out BigInteger a))
            {
                Console.WriteLine($"Error: '{inputA}' no es un número entero válido.");
                return false;
            }

            if (!BigInteger.TryParse(inputB.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out BigInteger b))
            {
                Console.WriteLine($"Error: '{inputB}' no es un número entero válido.");
                return false;
            }

            if (a.IsZero && b.IsZero)
            {
                Console.WriteLine("Error: El MCD(0, 0) no está definido. Ambos valores no pueden ser cero simultáneamente.");
                return false;
            }

            try
            {
                resultado = McdRecursivo.Calcular(a, b);
                Console.WriteLine($"Números ingresados: a = {a}, b = {b}");
                Console.WriteLine($"MCD({a}, {b}) = {resultado}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private static void ProcesarEntrada(string inputA, string inputB)
        {
            ValidarYCalcular(inputA, inputB, out _);
        }
    }
}
