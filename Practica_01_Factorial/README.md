# Práctica 1: Factorial de un Número (Recursivo)

Este proyecto corresponde a la implementación del **Ejercicio 1** del instrumento de evaluación `Practica2-Recursividad 032026.pdf` (Estructura de Datos, Licenciatura en Ingeniería en Tecnologías de la Información e Innovación Digital, DSM).

> **Enunciado original:**  
> *«Realiza un programa que calcule el valor Factorial de un número mayor o igual a cero utilizando una función o método recursivo».*

---

## 1. Estructura del Proyecto

El proyecto está desarrollado en C# (.NET 10) de forma completamente independiente:

```text
Practica_01_Factorial/
├── Practica_01_Factorial.csproj  # Archivo de configuración del proyecto C#
├── FactorialRecursivo.cs        # Clase dedicada con la lógica puramente recursiva
├── Program.cs                   # Interfaz de consola, validaciones y control de usuario
├── README.md                    # Documentación de compilación, ejecución y pruebas
├── PROMPT.md                    # Especificación del prompt original del ejercicio
└── publish/                     # Directorio de salida con el ejecutable autocontenido
    └── Practica_01_Factorial.exe
```

---

## 2. Lógica Recursiva y Diseño

Toda la lógica de cálculo reside en la clase [`FactorialRecursivo`](FactorialRecursivo.cs):

- **Casos base:**
  - $0! = 1$
  - $1! = 1$
- **Paso recursivo:**
  - $n! = n \times (n - 1)!$
- **Tipo de datos:** Se utiliza `System.Numerics.BigInteger` para permitir el cálculo de factoriales de números enteros grandes sin sufrir desbordamiento aritmético (`overflow`).
- **Límite de recursión:** Se define `MaxLimiteSeguro = 1000` para acotar la profundidad de llamadas. El tiempo de ejecución depende del equipo.

---

## 3. Validaciones

El programa valida rigurosamente las entradas:
- Rechaza entradas vacías.
- Rechaza cadenas de texto alfanuméricas.
- Rechaza fracciones o números con punto/coma decimal (`3.5`, `4/2`).
- Rechaza números negativos ($n < 0$).
- Rechaza valores que superen el límite seguro ($n > 1000$).
- En modo interactivo, informa el error específico y permite reintentar inmediatamente.

---

## 4. Instrucciones de Compilación y Publicación

### Requisitos
- SDK de .NET 10.0 o superior instalado.
- Sistema Operativo Windows x64 (para ejecutar la compilación nativa publicada).

### Compilar el proyecto
Desde la carpeta `Practica_01_Factorial/`:
```bash
dotnet build -c Release
```

### Publicar ejecutable autocontenido (.exe)
Para generar el ejecutable único e independiente en la carpeta `publish/`:
```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish
```

---

## 5. Instrucciones de Ejecución

### Modo Interactivo (predeterminado)
Hacer doble clic en `publish/Practica_01_Factorial.exe` o ejecutarlo desde la terminal:
```bash
.\publish\Practica_01_Factorial.exe
```

### Modo Directo (pasando argumento)
```bash
.\publish\Practica_01_Factorial.exe 5
```

---

## 6. Casos de Prueba Verificados

| Entrada | Salida Esperada | Comportamiento Observado | Estado |
| :--- | :--- | :--- | :--- |
| `0` | `0! = 1` | `Operación: 0!`<br>`Resultado: 0! = 1` | Correcto |
| `1` | `1! = 1` | `Operación: 1!`<br>`Resultado: 1! = 1` | Correcto |
| `5` | `5! = 120` | `Operación: 5!`<br>`Resultado: 5! = 120` | Correcto |
| `-3` | Rechazo con mensaje de error | Rechazado con mensaje claro de error | Correcto |
| `3.14` | Rechazo de número decimal | Rechazado con mensaje claro de error | Correcto |
| `abc` | Rechazo de texto | Rechazado con mensaje claro de error | Correcto |
| `1001` | Rechazo por exceder límite seguro | Rechazado con mensaje de límite | Correcto |
