using System;
using System.Collections.Generic;

namespace Practica_04_Cambio_Minimo
{
    /// <summary>
    /// Representa el resultado del cálculo de cambio mínimo.
    /// </summary>
    public class ResultadoCambio
    {
        public decimal Precio { get; }
        public decimal Pago { get; }
        public decimal CambioTotal { get; }
        public int TotalMonedas { get; }
        public IReadOnlyList<DenominacionInfo> Detalle { get; }

        public ResultadoCambio(decimal precio, decimal pago, decimal cambioTotal, int totalMonedas, List<DenominacionInfo> detalle)
        {
            Precio = precio;
            Pago = pago;
            CambioTotal = cambioTotal;
            TotalMonedas = totalMonedas;
            Detalle = detalle.AsReadOnly();
        }
    }

    /// <summary>
    /// Información sobre cada una de las 9 denominaciones oficiales y su cantidad de monedas.
    /// </summary>
    public class DenominacionInfo
    {
        public string Descripcion { get; }
        public int ValorCentavos { get; }
        public int CantidadMonedas { get; }

        public DenominacionInfo(string descripcion, int valorCentavos, int cantidadMonedas)
        {
            Descripcion = descripcion;
            ValorCentavos = valorCentavos;
            CantidadMonedas = cantidadMonedas;
        }
    }

    /// <summary>
    /// Algoritmo recursivo con búsqueda acotada (Branch and Bound) y memoización para cambio mínimo.
    /// Garantiza la combinación óptima con el mínimo número de monedas para todas las 9 denominaciones.
    /// </summary>
    public static class CambioMinimoRecursivo
    {
        /// <summary>
        /// Límite máximo de cambio procesable en pesos ($10,000.00 pesos = 1,000,000 de centavos).
        /// </summary>
        public const decimal MaxCambioPesos = 10000.00m;

        // Las 9 denominaciones especificadas en centavos
        public static readonly int[] Denominaciones = new int[]
        {
            10000, // 100 pesos
            5000,  // 50 pesos
            2000,  // 20 pesos
            1000,  // 10 pesos
            500,   // 5 pesos
            100,   // 1 peso
            50,    // 50 centavos
            20,    // 20 centavos
            1      // 1 centavo
        };

        public static readonly string[] NombresDenominaciones = new string[]
        {
            "100 pesos",
            "50 pesos",
            "20 pesos",
            "10 pesos",
            "5 pesos",
            "1 peso",
            "50 centavos",
            "20 centavos",
            "un centavo"
        };

        /// <summary>
        /// Calcula el cambio óptimo minimizando el número total de monedas mediante recursión acotada.
        /// </summary>
        public static ResultadoCambio CalcularCambio(decimal precio, decimal pago)
        {
            if (precio < 0)
                throw new ArgumentOutOfRangeException(nameof(precio), "El precio no puede ser negativo.");

            if (pago < 0)
                throw new ArgumentOutOfRangeException(nameof(pago), "El pago no puede ser negativo.");

            if (decimal.Round(precio, 2) != precio || decimal.Round(pago, 2) != pago)
                throw new ArgumentException("El precio y el pago deben tener como máximo dos decimales.");

            if (pago < precio)
                throw new InvalidOperationException($"El pago ({pago:F2}) es insuficiente para cubrir el precio ({precio:F2}).");

            decimal cambioDecimal = pago - precio;

            if (cambioDecimal > MaxCambioPesos)
                throw new ArgumentOutOfRangeException(
                    nameof(pago), 
                    $"El cambio (${cambioDecimal:F2}) excede el límite máximo procesable de ${MaxCambioPesos:F2}."
                );

            int centavosTotal = (int)(cambioDecimal * 100m);

            if (centavosTotal == 0)
            {
                var detalleCero = new List<DenominacionInfo>();
                for (int i = 0; i < Denominaciones.Length; i++)
                {
                    detalleCero.Add(new DenominacionInfo(NombresDenominaciones[i], Denominaciones[i], 0));
                }
                return new ResultadoCambio(precio, pago, 0.00m, 0, detalleCero);
            }

            var memo = new Dictionary<(int, int), (int, int[])>();
            (int minMonedas, int[] distribucion) = ResolverRecursivo(0, centavosTotal, memo);

            // Validar que la suma en centavos coincida exactamente
            int sumaVerificacion = 0;
            for (int i = 0; i < Denominaciones.Length; i++)
            {
                sumaVerificacion += distribucion[i] * Denominaciones[i];
            }

            if (sumaVerificacion != centavosTotal)
            {
                throw new InvalidOperationException($"Error de consistencia interna: la suma ({sumaVerificacion}) no coincide con el cambio ({centavosTotal}).");
            }

            var listaDetalle = new List<DenominacionInfo>();
            for (int i = 0; i < Denominaciones.Length; i++)
            {
                listaDetalle.Add(new DenominacionInfo(NombresDenominaciones[i], Denominaciones[i], distribucion[i]));
            }

            return new ResultadoCambio(precio, pago, cambioDecimal, minMonedas, listaDetalle);
        }

        /// <summary>
        /// Función recursiva que explora las decisiones de cantidad de monedas para cada denominación.
        /// La profundidad máxima de la pila es 9 (una por cada denominación).
        /// Memoiza subproblemas completos; no guarda resultados parciales afectados por una cota global.
        /// Regla de desempate: Al iterar k en orden descendente, ante igualdad de monedas mínimas totales,
        /// se favorece la moneda de mayor denominación.
        /// </summary>
        private static (int, int[]) ResolverRecursivo(
            int idx,
            int remCentavos,
            Dictionary<(int, int), (int, int[])> memo)
        {
            // Caso base 1: cantidad completada exactamente
            if (remCentavos == 0)
            {
                int[] ceros = new int[Denominaciones.Length - idx];
                return (0, ceros);
            }

            // Caso base 2: última denominación (moneda de 1 centavo)
            if (idx == Denominaciones.Length - 1)
            {
                int monedas = remCentavos; // Monedas de 1 centavo necesarias
                return (monedas, new int[] { monedas });
            }

            var clave = (idx, remCentavos);
            if (memo.TryGetValue(clave, out var guardado))
            {
                return guardado;
            }

            int d = Denominaciones[idx];
            int maxK = Math.Min(remCentavos / d, LimiteMonedasEnSolucionOptima(idx));

            int mejorMonedas = int.MaxValue;
            int[]? mejorDistribucion = null;

            // Explorar todos los conteos que pueden pertenecer a una solución óptima.
            for (int k = maxK; k >= 0; k--)
            {
                int resto = remCentavos - k * d;
                var (subMonedas, subDist) = ResolverRecursivo(idx + 1, resto, memo);
                int totalRama = k + subMonedas;
                if (totalRama < mejorMonedas)
                {
                    mejorMonedas = totalRama;
                    mejorDistribucion = new int[Denominaciones.Length - idx];
                    mejorDistribucion[0] = k;
                    Array.Copy(subDist, 0, mejorDistribucion, 1, subDist.Length);
                }
            }

            var resultadoFinal = (mejorMonedas, mejorDistribucion!);
            memo[clave] = resultadoFinal;
            return resultadoFinal;
        }

        // Si una denominación mayor es un múltiplo de la actual, alcanzar ese
        // múltiplo con monedas pequeñas nunca puede ser óptimo: se reemplaza por una sola.
        private static int LimiteMonedasEnSolucionOptima(int idx)
        {
            int limite = int.MaxValue;
            for (int anterior = 0; anterior < idx; anterior++)
            {
                if (Denominaciones[anterior] % Denominaciones[idx] == 0)
                {
                    limite = Math.Min(limite, Denominaciones[anterior] / Denominaciones[idx] - 1);
                }
            }
            return limite;
        }
    }
}
