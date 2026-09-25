# Prácticas de recursividad en C#

Cinco proyectos de consola independientes basados en `Practica2-Recursividad 032026.pdf`. Cada práctica incluye el código fuente, una clase con la lógica recursiva, instrucciones de uso y un ejecutable Windows x64 en `publish/`.

| Práctica | Proyecto | Clase recursiva |
| --- | --- | --- |
| 1. Factorial | [Practica_01_Factorial](Practica_01_Factorial/README.md) | `FactorialRecursivo` |
| 2. Fibonacci | [Practica_02_Fibonacci](Practica_02_Fibonacci/README.md) | `FibonacciRecursivo` |
| 3. MCD | [Practica_03_MCD](Practica_03_MCD/README.md) | `McdRecursivo` |
| 4. Cambio mínimo | [Practica_04_Cambio_Minimo](Practica_04_Cambio_Minimo/README.md) | `CambioMinimoRecursivo` |
| 5. Torres de Hanói | [Practica_05_Torres_Hanoi](Practica_05_Torres_Hanoi/README.md) | `HanoiRecursivo` |

## Compilar

Se requiere el SDK de .NET 10. Desde la carpeta de la práctica elegida:

```powershell
dotnet build -c Release
```

## Generar el ejecutable

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish
```

El `.exe` de cada carpeta `publish/` funciona en Windows x64 sin instalar .NET. Los archivos `bin/`, `obj/` y símbolos `.pdb` quedan fuera del repositorio mediante `.gitignore`. Los ejecutables sí se conservan para que la entrega incluya programas listos para usar. Cada uno ocupa aproximadamente 74 MB; se recomienda alojarlos como archivos de una versión publicada si el proveedor del repositorio limita el tamaño o se desea mantener ligero el historial Git.

Los [prompts originales](PROMPTS.md) y el PDF de la actividad también están incluidos para facilitar la revisión.
