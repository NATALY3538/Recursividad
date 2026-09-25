using System;
using System.Numerics;

namespace Practica_03_MCD
{
    /// <summary>
    /// Proporciona métodos para calcular el Máximo Común Divisor (MCD) utilizando el algoritmo de Euclides recursivo.
    /// </summary>
    public static class McdRecursivo
    {
        /// <summary>
        /// Calcula el MCD de dos números enteros utilizando el algoritmo de Euclides de manera recursiva.
        /// Acepta números positivos, negativos y cero.
        /// Para cualquier x != 0, MCD(0, x) = MCD(x, 0) = |x|.
        /// Si ambos son cero, arroja ArgumentException pues el MCD no está definido.
        /// </summary>
        /// <param name="a">Primer número entero.</param>
        /// <param name="b">Segundo número entero.</param>
        /// <returns>El Máximo Común Divisor no negativo como <see cref="BigInteger"/>.</returns>
        /// <exception cref="ArgumentException">Si a == 0 y b == 0.</exception>
        public static BigInteger Calcular(BigInteger a, BigInteger b)
        {
            if (a.IsZero && b.IsZero)
            {
                throw new ArgumentException("El MCD de (0, 0) no está definido (es indeterminado matemáticamente).");
            }

            // Normalización a magnitudes absolutas para soportar negativos de forma segura sin overflow
            BigInteger absA = BigInteger.Abs(a);
            BigInteger absB = BigInteger.Abs(b);

            return EuclidesRecursivo(absA, absB);
        }

        /// <summary>
        /// Implementación formal del algoritmo recursivo de Euclides.
        /// Caso base: b == 0 -> retorna a.
        /// Paso recursivo: Euclides(b, a % b).
        /// </summary>
        private static BigInteger EuclidesRecursivo(BigInteger a, BigInteger b)
        {
            // Caso base
            if (b.IsZero)
            {
                return a;
            }

            // Paso recursivo con el residuo
            return EuclidesRecursivo(b, a % b);
        }
    }
}
