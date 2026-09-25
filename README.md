# Prácticas de recursividad en C#

Cinco aplicaciones gráficas independientes basadas en `Practica2-Recursividad 032026.pdf`. Cada práctica incluye el código fuente, una clase con la lógica recursiva, instrucciones de uso y un ejecutable Windows x64 en `publish/`.

Las ventanas usan una apariencia Cupertino con fondo claro, tarjetas blancas y botones azules redondeados. La tipografía Roboto y los iconos Material Symbols Rounded están integrados en cada proyecto.

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

El `.exe` de cada carpeta `publish/` es una aplicación gráfica autocontenida para Windows x64. La compresión de archivo único está habilitada en cada proyecto; cada ejecutable publicado ocupa aproximadamente 49 MB. Los archivos `bin/`, `obj/` y símbolos `.pdb` quedan fuera del repositorio mediante `.gitignore`. Los ejecutables sí se conservan para que la entrega incluya programas listos para usar.

Los [prompts de implementación](PROMPTS.md) y el PDF de la actividad también están incluidos para facilitar la revisión.

## Recursos gráficos

Cada proyecto contiene en `Resources/` un subconjunto de [Roboto](https://github.com/google/fonts/tree/main/ofl/roboto) bajo la licencia SIL Open Font License 1.1 y un subconjunto de [Material Symbols Rounded](https://github.com/google/material-design-icons) bajo Apache 2.0, con copias de ambas licencias. Las fuentes están integradas en cada `.exe` para que la interfaz se vea igual sin instalaciones adicionales.
