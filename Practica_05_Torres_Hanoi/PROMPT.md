# Prompt para agente ejecutor: práctica 5, Torres de Hanói

Implementa únicamente el ejercicio 5 del documento `Practica2-Recursividad 032026.pdf`: leer el número de discos que desee el usuario y describir, mediante recursividad, todos los movimientos para trasladar la torre de la varilla de origen a la de destino. Deben respetarse las tres reglas del enunciado: mover un solo disco a la vez, no poner uno grande sobre uno pequeño y mover únicamente el disco superior de cada varilla.

## Entrega y estructura

- Usa C# WinForms y crea una aplicación gráfica independiente dentro de `Practica_05_Torres_Hanoi/`. No dependas de otras prácticas.
- Entrega el código fuente visible (`.cs` y `.csproj`), un `README.md` con instrucciones de compilación y uso, y un ejecutable Windows `.exe` generado mediante `dotnet publish` para `win-x64` en `publish/`.
- Coloca el formulario y la validación en `Program.cs` y el algoritmo en una clase separada llamada `HanoiRecursivo`, en `HanoiRecursivo.cs`.
- La ventana debe tener apariencia Cupertino: fondo claro, espacios amplios, tarjetas blancas y controles redondeados. Usa Roboto e iconos Material Design integrados para que funcionen sin fuentes instaladas.

## Comportamiento

- Solicita un entero positivo de discos. Rechaza cero, negativos, fracciones y texto; permite repetir la entrada.
- Identifica claramente las varillas `A` (origen), `B` (auxiliar) y `C` (destino). Numera cada movimiento y especifica el disco, la varilla de salida y la de llegada.
- Implementa el caso base de un disco y el paso recursivo clásico: mover `n-1` discos al auxiliar, mover el disco mayor al destino y mover los `n-1` discos restantes al destino. No uses una lista fija de movimientos ni una solución exclusivamente iterativa.
- Muestra al final el total de movimientos, que debe ser `2^n - 1`. Debido al crecimiento exponencial de la salida, define y documenta un límite de discos razonable; valida ese límite antes de empezar a imprimir.

## Verificación y cierre

- Comprueba `n = 1` (1 movimiento `A -> C`), `n = 2` (3 movimientos), `n = 3` (7 movimientos) y el rechazo de `n = 0`. Verifica que ningún movimiento viola las reglas de las varillas.
- Ejecuta `dotnet build` y publica un `.exe` con `dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish`. Comprueba que el ejecutable existe; si el entorno permite ejecutarlo, prueba un caso desde el `.exe`.
- En la respuesta final, indica la ruta del `.exe`, la ruta de los archivos fuente y los comandos de ejecución. No entregues solo fragmentos de código.
