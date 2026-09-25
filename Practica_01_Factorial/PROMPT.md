# Prompt para agente ejecutor: práctica 1, factorial

Implementa únicamente el ejercicio 1 del documento `Practica2-Recursividad 032026.pdf`: «Realiza un programa que calcule el valor Factorial de un número mayor o igual a cero utilizando una función o método recursivo».

## Entrega y estructura

- Usa C# y crea un proyecto de consola independiente dentro de `Practica_01_Factorial/`. No dependas de otras prácticas.
- Entrega el código fuente visible (`.cs` y `.csproj`), un `README.md` con instrucciones de compilación y uso, y un ejecutable Windows `.exe` generado mediante `dotnet publish` para `win-x64` en `publish/`.
- Coloca la interacción de consola en `Program.cs` y toda la lógica de factorial en una clase separada llamada `FactorialRecursivo`, en `FactorialRecursivo.cs`. `Program.cs` debe llamar a esa clase.
- El cálculo del factorial debe ser realmente recursivo: casos base para 0 y 1; paso recursivo `n * Factorial(n - 1)`. No sustituyas ese cálculo por un bucle ni por una función de biblioteca.

## Comportamiento

- Solicita un entero `n >= 0`; rechaza texto, fracciones y negativos con un mensaje claro y permite volver a intentarlo.
- Usa `System.Numerics.BigInteger` para evitar desbordamiento de `int`/`long` en el resultado. Define y documenta un límite de entrada razonable para no agotar la pila con la recursión; valida ese límite antes de calcular.
- Muestra la operación y el resultado. Para `0`, muestra `0! = 1`.

## Verificación y cierre

- Comprueba al menos `0! = 1`, `1! = 1`, `5! = 120` y la recuperación tras una entrada inválida.
- Ejecuta `dotnet build` y publica un `.exe` con `dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish`. Comprueba que el ejecutable existe; si el entorno permite ejecutarlo, prueba un caso desde el `.exe`.
- En la respuesta final, indica la ruta del `.exe`, la ruta de los archivos fuente y los comandos de ejecución. No entregues solo fragmentos de código.
