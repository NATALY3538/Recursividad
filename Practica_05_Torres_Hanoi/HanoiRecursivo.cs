using System;
using System.Collections.Generic;

namespace Practica_05_Torres_Hanoi
{
    /// <summary>
    /// Representa un movimiento individual de un disco entre dos varillas.
    /// </summary>
    public class MovimientoHanoi
    {
        public int NumeroPaso { get; }
        public int NumeroDisco { get; }
        public char VarillaOrigen { get; }
        public char VarillaDestino { get; }

        public MovimientoHanoi(int numeroPaso, int numeroDisco, char varillaOrigen, char varillaDestino)
        {
            NumeroPaso = numeroPaso;
            NumeroDisco = numeroDisco;
            VarillaOrigen = varillaOrigen;
            VarillaDestino = varillaDestino;
        }

        public override string ToString()
        {
            return $"Paso {NumeroPaso}: Mover disco {NumeroDisco} de varilla {VarillaOrigen} a varilla {VarillaDestino}";
        }
    }

    /// <summary>
    /// Proporciona la resolución recursiva clásica para el rompecabezas de las Torres de Hanói.
    /// </summary>
    public static class HanoiRecursivo
    {
        /// <summary>
        /// Límite máximo de discos para impresión en consola para evitar congelamiento de la terminal.
        /// Con 15 discos se generan 2^15 - 1 = 32,767 movimientos.
        /// </summary>
        public const int MaxLimiteDiscos = 15;

        /// <summary>
        /// Resuelve el problema de las Torres de Hanói para n discos y retorna la secuencia ordenada de movimientos.
        /// </summary>
        /// <param name="n">Número de discos (n >= 1 y n &lt;= MaxLimiteDiscos).</param>
        /// <param name="origen">Identificador de la varilla de origen (por defecto 'A').</param>
        /// <param name="auxiliar">Identificador de la varilla auxiliar (por defecto 'B').</param>
        /// <param name="destino">Identificador de la varilla de destino (por defecto 'C').</param>
        /// <returns>Lista con todos los movimientos requeridos.</returns>
        public static List<MovimientoHanoi> Resolver(int n, char origen = 'A', char auxiliar = 'B', char destino = 'C')
        {
            if (n < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "El número de discos debe ser un entero positivo (n >= 1).");
            }

            if (n > MaxLimiteDiscos)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(n), 
                    $"El número de discos ({n}) excede el límite máximo seguro ({MaxLimiteDiscos}) para salida por consola."
                );
            }

            var movimientos = new List<MovimientoHanoi>((1 << n) - 1);
            int contadorPasos = 0;

            EjecutarHanoi(n, origen, auxiliar, destino, movimientos, ref contadorPasos);

            return movimientos;
        }

        /// <summary>
        /// Algoritmo recursivo clásico de Torres de Hanói:
        /// - Caso base (n = 1): Mover directamente el disco 1 de origen a destino.
        /// - Paso recursivo (n > 1):
        ///   1. Mover n - 1 discos de origen a auxiliar utilizando destino como apoyo.
        ///   2. Mover el disco n de origen a destino.
        ///   3. Mover los n - 1 discos de auxiliar a destino utilizando origen como apoyo.
        /// </summary>
        private static void EjecutarHanoi(
            int n, 
            char origen, 
            char auxiliar, 
            char destino, 
            List<MovimientoHanoi> lista, 
            ref int contadorPasos)
        {
            // Caso base: un solo disco
            if (n == 1)
            {
                contadorPasos++;
                lista.Add(new MovimientoHanoi(contadorPasos, 1, origen, destino));
                return;
            }

            // Paso 1: Mover la sub-torre de n-1 discos de origen a auxiliar
            EjecutarHanoi(n - 1, origen, destino, auxiliar, lista, ref contadorPasos);

            // Paso 2: Mover el disco n (el más grande de la sub-torre actual) al destino
            contadorPasos++;
            lista.Add(new MovimientoHanoi(contadorPasos, n, origen, destino));

            // Paso 3: Mover la sub-torre de n-1 discos de auxiliar a destino
            EjecutarHanoi(n - 1, auxiliar, origen, destino, lista, ref contadorPasos);
        }

        /// <summary>
        /// Valida formalmente que una secuencia de movimientos respete las 3 reglas del enunciado:
        /// 1. Sólo se puede mover un disco cada vez.
        /// 2. Un disco de mayor tamaño no puede descansar sobre uno más pequeño.
        /// 3. Sólo se puede desplazar el disco que se encuentre arriba en cada varilla.
        /// </summary>
        public static bool ValidarReglas(int n, List<MovimientoHanoi> movimientos, out string mensajeError)
        {
            mensajeError = string.Empty;
            var varillas = new Dictionary<char, Stack<int>>
            {
                ['A'] = new Stack<int>(),
                ['B'] = new Stack<int>(),
                ['C'] = new Stack<int>()
            };

            // Inicializar varilla A con los discos de mayor a menor (el mayor abajo)
            for (int d = n; d >= 1; d--)
            {
                varillas['A'].Push(d);
            }

            int pasoEsperado = 1;
            foreach (var mov in movimientos)
            {
                if (mov.NumeroPaso != pasoEsperado)
                {
                    mensajeError = $"Secuencia de pasos incorrecta en paso {mov.NumeroPaso} (se esperaba {pasoEsperado}).";
                    return false;
                }
                pasoEsperado++;

                // Regla 3: Sólo se puede desplazar el disco superior
                if (varillas[mov.VarillaOrigen].Count == 0)
                {
                    mensajeError = $"En el paso {mov.NumeroPaso}, la varilla origen {mov.VarillaOrigen} está vacía.";
                    return false;
                }

                int discoSuperior = varillas[mov.VarillaOrigen].Pop();
                if (discoSuperior != mov.NumeroDisco)
                {
                    mensajeError = $"En el paso {mov.NumeroPaso}, se intentó mover el disco {mov.NumeroDisco}, pero arriba de la varilla {mov.VarillaOrigen} estaba el disco {discoSuperior}.";
                    return false;
                }

                // Regla 2: No colocar un disco grande sobre uno más pequeño
                if (varillas[mov.VarillaDestino].Count > 0)
                {
                    int discoDestino = varillas[mov.VarillaDestino].Peek();
                    if (discoSuperior > discoDestino)
                    {
                        mensajeError = $"En el paso {mov.NumeroPaso}, se intentó colocar el disco mayor {discoSuperior} sobre el disco menor {discoDestino} en la varilla {mov.VarillaDestino}.";
                        return false;
                    }
                }

                varillas[mov.VarillaDestino].Push(discoSuperior);
            }

            // Verificar estado final: todos los discos deben estar en C en orden
            if (varillas['C'].Count != n)
            {
                mensajeError = $"Al final, la varilla destino C no contiene los {n} discos.";
                return false;
            }

            return true;
        }
    }
}
