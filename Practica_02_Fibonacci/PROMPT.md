# Prompt para agente ejecutor: práctica 2, Fibonacci

Implementa únicamente el ejercicio 2 del documento `Practica2-Recursividad 032026.pdf`: «Realiza un programa que de la serie de Fibonacci utilizando recursividad».

## Entrega y estructura

- Usa C# y crea un proyecto de consola independiente dentro de `Practica_02_Fibonacci/`. No dependas de otras prácticas.
- Entrega el código fuente visible (`.cs` y `.csproj`), un `README.md` con instrucciones de compilación y uso, y un ejecutable Windows `.exe` generado mediante `dotnet publish` para `win-x64` en `publish/`.
- Coloca la interacción de consola en `Program.cs` y toda la lógica recursiva en una clase separada llamada `FibonacciRecursivo`, en `FibonacciRecursivo.cs`.

## Comportamiento

- Interpreta la entrada como **cantidad de términos**: para `n = 5`, muestra `0, 1, 1, 2, 3`. Indica al usuario que la serie empieza en `F(0) = 0` y `F(1) = 1`.
- Solicita un entero `n >= 0`; si `n = 0`, muestra una serie vacía sin error. Rechaza entradas inválidas y permite volver a intentarlo.
- Calcula los valores con una función o método recursivo con casos base y paso recursivo. Puedes usar memoización para evitar trabajo exponencial, pero no reemplaces la recursión por un cálculo exclusivamente iterativo.
- Usa `System.Numerics.BigInteger` para los valores. Establece y documenta un límite de términos razonable para proteger tiempo, memoria y profundidad de pila.

## Verificación y cierre

- Comprueba las salidas para `n = 0`, `n = 1`, `n = 2` y `n = 6` (`0, 1, 1, 2, 3, 5`), además de una entrada inválida.
- Ejecuta `dotnet build` y publica un `.exe` con `dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish`. Comprueba que el ejecutable existe; si el entorno permite ejecutarlo, prueba un caso desde el `.exe`.
- En la respuesta final, indica la ruta del `.exe`, la ruta de los archivos fuente y los comandos de ejecución. No entregues solo fragmentos de código.
