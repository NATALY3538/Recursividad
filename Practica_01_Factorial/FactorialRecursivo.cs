using System;
using System.Numerics;

namespace Practica_01_Factorial
{
    /// <summary>
    /// Proporciona métodos para el cálculo del factorial utilizando un algoritmo puramente recursivo.
    /// </summary>
    public static class FactorialRecursivo
    {
        /// <summary>
        /// Límite máximo seguro para el cálculo de factorial para evitar desbordamiento de la pila (StackOverflowException).
        /// Un valor de 1000 recursiones cabe con holgura en el tamaño de pila predeterminado (~1MB).
        /// </summary>
        public const int MaxLimiteSeguro = 1000;

        /// <summary>
        /// Calcula el factorial de un número entero n (n >= 0 y n &lt;= MaxLimiteSeguro) de forma recursiva.
        /// Casos base: 0! = 1, 1! = 1.
        /// Paso recursivo: n * Factorial(n - 1).
        /// </summary>
        /// <param name="n">Número entero mayor o igual a 0.</param>
        /// <returns>Valor factorial representado como <see cref="BigInteger"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Si n es negativo o excede MaxLimiteSeguro.</exception>
        public static BigInteger Calcular(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "El número debe ser mayor o igual a 0.");
            }

            if (n > MaxLimiteSeguro)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(n), 
                    $"El número {n} excede el límite máximo seguro ({MaxLimiteSeguro}) para evitar el desbordamiento de pila."
                );
            }

            // Casos base definidos formalmente: 0! = 1 y 1! = 1
            if (n == 0 || n == 1)
            {
                return BigInteger.One;
            }

            // Paso recursivo genuino: n * Factorial(n - 1)
            return (BigInteger)n * Calcular(n - 1);
        }
    }
}
