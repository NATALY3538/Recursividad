using System;
using System.Collections.Generic;
using System.Numerics;

namespace Practica_02_Fibonacci
{
    /// <summary>
    /// Proporciona métodos para generar la serie y términos de Fibonacci mediante algoritmos recursivos.
    /// </summary>
    public static class FibonacciRecursivo
    {
        /// <summary>
        /// Límite máximo razonable de términos para evitar profundidad excesiva de llamadas y consumo de memoria.
        /// </summary>
        public const int MaxLimiteTerminos = 1000;

        /// <summary>
        /// Calcula recursivamente el k-ésimo término de Fibonacci con memoización para evitar la explosión exponencial O(2^n).
        /// Casos base: F(0) = 0, F(1) = 1.
        /// Paso recursivo: F(k) = F(k - 1) + F(k - 2).
        /// </summary>
        /// <param name="k">Índice del término a calcular (k >= 0).</param>
        /// <param name="memo">Diccionario de memoización de estados ya evaluados.</param>
        /// <returns>Valor del término k en BigInteger.</returns>
        public static BigInteger CalcularTermino(int k, Dictionary<int, BigInteger> memo)
        {
            if (k < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(k), "El índice no puede ser negativo.");
            }

            // Casos base
            if (k == 0) return BigInteger.Zero;
            if (k == 1) return BigInteger.One;

            // Búsqueda en memoización
            if (memo.TryGetValue(k, out BigInteger valor))
            {
                return valor;
            }

            // Paso recursivo genuino
            BigInteger resultado = CalcularTermino(k - 1, memo) + CalcularTermino(k - 2, memo);
            memo[k] = resultado;
            return resultado;
        }

        /// <summary>
        /// Obtiene una lista con los primeros n términos de la serie de Fibonacci generados recursivamente.
        /// Para n = 0, devuelve una lista vacía.
        /// </summary>
        /// <param name="n">Cantidad de términos deseados (n >= 0 y n &lt;= MaxLimiteTerminos).</param>
        /// <returns>Lista con los primeros n términos.</returns>
        public static List<BigInteger> ObtenerSerie(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "La cantidad de términos debe ser mayor o igual a 0.");
            }

            if (n > MaxLimiteTerminos)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(n), 
                    $"La cantidad solicitada ({n}) excede el límite máximo seguro ({MaxLimiteTerminos})."
                );
            }

            var resultado = new List<BigInteger>(n);
            if (n == 0)
            {
                return resultado;
            }

            var memo = new Dictionary<int, BigInteger>();
            // Construcción recursiva de la lista asegurando orden F(0), F(1), ..., F(n-1)
            ConstruirSerieRecursiva(n - 1, memo, resultado);
            return resultado;
        }

        /// <summary>
        /// Método recursivo auxiliar que asegura la generación y adición secuencial de términos de 0 a k.
        /// </summary>
        private static void ConstruirSerieRecursiva(int k, Dictionary<int, BigInteger> memo, List<BigInteger> acumulador)
        {
            if (k < 0) return;

            if (k > 0)
            {
                ConstruirSerieRecursiva(k - 1, memo, acumulador);
            }

            acumulador.Add(CalcularTermino(k, memo));
        }
    }
}
