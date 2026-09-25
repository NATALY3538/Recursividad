# Práctica 3: Máximo Común Divisor - MCD (Recursivo)

Este proyecto corresponde a la implementación del **Ejercicio 3** del instrumento de evaluación `Practica2-Recursividad 032026.pdf` (Estructura de Datos, Licenciatura en Ingeniería en Tecnologías de la Información e Innovación Digital, DSM).

> **Enunciado original:**  
> *«Realiza un programa que calcule el MCD (Máximo Común Divisor) de dos números del tipo entero».*

---

## 1. Estructura del Proyecto

El proyecto está desarrollado en C# (.NET 10) de forma independiente y modular:

```text
Practica_03_MCD/
├── Practica_03_MCD.csproj  # Archivo de configuración del proyecto C#
├── McdRecursivo.cs        # Clase dedicada con el algoritmo de Euclides recursivo
├── Program.cs             # Interfaz de consola, validaciones y control de usuario
├── README.md              # Documentación de compilación, ejecución y pruebas
├── PROMPT.md              # Especificación del prompt original del ejercicio
└── publish/               # Directorio con el ejecutable autocontenido win-x64
    └── Practica_03_MCD.exe
```

---

## 2. Lógica Recursiva y Diseño

La lógica de cálculo reside en la clase [`McdRecursivo`](McdRecursivo.cs):

- **Algoritmo de Euclides recursivo:**
  - **Caso base:** Cuando el divisor $b = 0$, el resultado es $|a|$.
  - **Paso recursivo:** Si $b \neq 0$, se invoca recursivamente: $\text{Euclides}(b, a \pmod b)$.
- **Tratamiento de números negativos:** Las magnitudes se normalizan mediante valor absoluto (`BigInteger.Abs`) para garantizar que el resultado sea siempre no negativo ($\ge 0$) y sin riesgo de desbordamiento de enteros (como el desbordamiento clásico de `Math.Abs(int.MinValue)`).
- **Tratamiento de ceros:**
  - $\text{MCD}(0, x) = |x|$ para todo $x \neq 0$.
  - $\text{MCD}(x, 0) = |x|$ para todo $x \neq 0$.
  - $\text{MCD}(0, 0)$ está matemáticamente indeterminado/indefinido; el programa lo rechaza explícitamente y solicita ingresar nuevos valores.

---

## 3. Validaciones

- Rechaza entradas vacías.
- Rechaza cadenas alfanuméricas o caracteres inválidos.
- Rechaza números fraccionarios o decimales (`4.2`, `10/3`).
- Rechaza el caso $(0, 0)$ informando la indeterminación matemática.
- Permite reintentar de inmediato en el flujo interactivo de consola.

---

## 4. Instrucciones de Compilación y Publicación

### Requisitos
- SDK de .NET 10.0 o superior instalado.
- Sistema Operativo Windows x64.

### Compilar el proyecto
Desde la carpeta `Practica_03_MCD/`:
```bash
dotnet build -c Release
```

### Publicar ejecutable autocontenido (.exe)
```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish
```

---

## 5. Instrucciones de Ejecución

### Modo Interactivo (predeterminado)
```bash
.\publish\Practica_03_MCD.exe
# O bien:
dotnet run -c Release
```

### Modo Directo (pasando argumentos)
```bash
.\publish\Practica_03_MCD.exe 48 18
# O bien:
dotnet run -c Release --no-build -- 48 18
```

---

## 6. Casos de Prueba Verificados

| Entrada (a, b) | Salida Esperada | Comportamiento Observado | Estado |
| :--- | :--- | :--- | :--- |
| `48, 18` | `MCD(48, 18) = 6` | `MCD(48, 18) = 6` | Correcto |
| `-48, 18` | `MCD(-48, 18) = 6` | `MCD(-48, 18) = 6` | Correcto |
| `0, 7` | `MCD(0, 7) = 7` | `MCD(0, 7) = 7` | Correcto |
| `7, 0` | `MCD(7, 0) = 7` | `MCD(7, 0) = 7` | Correcto |
| `0, 0` | Rechazo por indeterminación | Rechazado con mensaje de indeterminación | Correcto |
| `48.5, 18` | Rechazo por número decimal | Rechazado con mensaje de error | Correcto |
| `abc, 18` | Rechazo por texto | Rechazado con mensaje de error | Correcto |
