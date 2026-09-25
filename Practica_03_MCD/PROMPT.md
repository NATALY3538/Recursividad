# Prompt para agente ejecutor: práctica 3, MCD

Implementa únicamente el ejercicio 3 del documento `Practica2-Recursividad 032026.pdf`: «Realiza un programa que calcule el MCD (Máximo Común Divisor) de dos números del tipo entero». Esta entrega requiere que el cálculo sea recursivo.

## Entrega y estructura

- Usa C# y crea un proyecto de consola independiente dentro de `Practica_03_MCD/`. No dependas de otras prácticas.
- Entrega el código fuente visible (`.cs` y `.csproj`), un `README.md` con instrucciones de compilación y uso, y un ejecutable Windows `.exe` generado mediante `dotnet publish` para `win-x64` en `publish/`.
- Coloca la interacción de consola en `Program.cs` y el algoritmo en una clase separada llamada `McdRecursivo`, en `McdRecursivo.cs`.

## Comportamiento

- Lee dos números enteros. Rechaza texto y fracciones con mensajes claros y permite repetir la entrada.
- Usa el algoritmo recursivo de Euclides: caso base cuando el segundo valor es cero; paso recursivo con el residuo. El resultado debe ser no negativo.
- Acepta enteros negativos normalizando sus magnitudes sin desbordamiento en el valor mínimo del tipo elegido. `MCD(0, x) = |x|` para `x != 0`; para `(0, 0)`, informa que el MCD no está definido y solicita otros valores.
- Muestra ambos números ingresados y el MCD.

## Verificación y cierre

- Comprueba `MCD(48, 18) = 6`, `MCD(-48, 18) = 6`, `MCD(0, 7) = 7` y el rechazo de `(0, 0)`.
- Ejecuta `dotnet build` y publica un `.exe` con `dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish`. Comprueba que el ejecutable existe; si el entorno permite ejecutarlo, prueba un caso desde el `.exe`.
- En la respuesta final, indica la ruta del `.exe`, la ruta de los archivos fuente y los comandos de ejecución. No entregues solo fragmentos de código.
