# Práctica 5: Torres de Hanói (Recursivo)

Este proyecto corresponde a la implementación del **Ejercicio 5** del instrumento de evaluación `Practica2-Recursividad 032026.pdf` (Estructura de Datos, Licenciatura en Ingeniería en Tecnologías de la Información e Innovación Digital, DSM).

> **Enunciado original:**  
> *«Las Torres de Hanói es un rompecabezas o juego matemático inventado en 1883 por el matemático francés Éduard Lucas... El juego consiste en pasar todos los discos de la varilla ocupada (es decir la que posee la torre) a una de las otras varillas vacantes. Para realizar este objetivo, es necesario seguir tres simples reglas:*  
> *1. Sólo se puede mover un disco cada vez.*  
> *2. Un disco de mayor tamaño no puede descansar sobre uno más pequeño que él mismo.*  
> *3. Sólo puedes desplazar el disco que se encuentre arriba en cada varilla.*  
> *Utilizando recursividad realiza un programa recursivo que lea el número de discos que el usuario desee y describa los movimientos para llevar todas las torres del origen al destino».*

---

## 1. Estructura del Proyecto

El proyecto está desarrollado en C# (.NET 10) de forma independiente y modular:

```text
Practica_05_Torres_Hanoi/
├── Practica_05_Torres_Hanoi.csproj  # Archivo de configuración del proyecto C#
├── HanoiRecursivo.cs                # Clase con el algoritmo recursivo y verificador de reglas
├── Program.cs                       # Interfaz de consola, validaciones y salida ordenada
├── README.md                        # Documentación completa y casos de prueba
├── PROMPT.md                        # Especificación del prompt original del ejercicio
└── publish/                         # Directorio con el ejecutable autocontenido win-x64
    └── Practica_05_Torres_Hanoi.exe
```

---

## 2. Lógica Recursiva y Diseño

Toda la lógica de resolución reside en la clase [`HanoiRecursivo`](HanoiRecursivo.cs):

### Identificación de Varillas
- **Varilla `A`:** Origen (donde residen inicialmente los $n$ discos apilados de mayor a menor).
- **Varilla `B`:** Auxiliar o de apoyo.
- **Varilla `C`:** Destino final.

### Algoritmo Recursivo Clásico
- **Caso Base ($n = 1$):**
  Se traslada directamente el disco 1 de la varilla origen a la varilla destino.
- **Paso Recursivo ($n > 1$):**
  1. Se trasladan recursivamente los $n - 1$ discos superiores desde la varilla `origen` hasta la varilla `auxiliar`, empleando `destino` como apoyo intermedio.
  2. Se traslada el disco mayor $n$ directamente de la varilla `origen` a la varilla `destino`.
  3. Se trasladan recursivamente los $n - 1$ discos desde la varilla `auxiliar` hasta la varilla `destino`, empleando `origen` como apoyo intermedio.

### Verificación de Reglas y Total Teórico
- El número total de movimientos exacto para $n$ discos es siempre $2^n - 1$.
- La clase incluye un método formal `ValidarReglas` que simula las 3 pilas de discos paso a paso para confirmar que:
  - Nunca se traslade más de un disco a la vez.
  - Nunca se coloque un disco sobre otro de menor tamaño.
  - Solo se extraiga el disco de la cúspide de cada varilla.
- **Límite seguro de entrada:** Se establece `MaxLimiteDiscos = 15` para la salida en consola ($2^{15} - 1 = 32,767$ movimientos), evitando el bloqueo o congelamiento de la consola del sistema.

---

## 3. Validaciones

- Rechaza entradas vacías o en blanco.
- Rechaza texto o símbolos.
- Rechaza números decimales o fracciones (`3.2`, `5/2`).
- Rechaza $n = 0$ y cantidades negativas ($n < 0$).
- Rechaza cantidades que excedan el límite seguro ($n > 15$).
- Permite reintentar de forma inmediata ante cualquier entrada inválida.

---

## 4. Instrucciones de Compilación y Publicación

### Requisitos
- SDK de .NET 10.0 o superior instalado.
- Sistema Operativo Windows x64.

### Compilar el proyecto
Desde la carpeta `Practica_05_Torres_Hanoi/`:
```bash
dotnet build -c Release
```

### Publicar ejecutable autocontenido (.exe)
```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish
```

---

## 5. Instrucciones de Ejecución

### Modo Interactivo
```bash
.\publish\Practica_05_Torres_Hanoi.exe
# O bien:
dotnet run -c Release
```

### Modo Directo (pasando número de discos como argumento)
```bash
.\publish\Practica_05_Torres_Hanoi.exe 3
# O bien:
dotnet run -c Release --no-build -- 3
```

---

## 6. Casos de Prueba Verificados

### Caso $n = 1$
- **Total movimientos:** $1$ ($2^1 - 1 = 1$).
- **Secuencia:**
  - `Paso 1: Mover disco 1 de varilla A a varilla C`
- **Estado:** Correcto.

### Caso $n = 2$
- **Total movimientos:** $3$ ($2^2 - 1 = 3$).
- **Secuencia:**
  - `Paso 1: Mover disco 1 de varilla A a varilla B`
  - `Paso 2: Mover disco 2 de varilla A a varilla C`
  - `Paso 3: Mover disco 1 de varilla B a varilla C`
- **Estado:** Correcto.

### Caso $n = 3$
- **Total movimientos:** $7$ ($2^3 - 1 = 7$).
- **Secuencia:**
  - `Paso 1: Mover disco 1 de varilla A a varilla C`
  - `Paso 2: Mover disco 2 de varilla A a varilla B`
  - `Paso 3: Mover disco 1 de varilla C a varilla B`
  - `Paso 4: Mover disco 3 de varilla A a varilla C`
  - `Paso 5: Mover disco 1 de varilla B a varilla A`
  - `Paso 6: Mover disco 2 de varilla B a varilla C`
  - `Paso 7: Mover disco 1 de varilla A a varilla C`
- **Estado:** Correcto.

### Casos Inválidos
- $n = 0$: Rechazado con mensaje de error (debe ser $\ge 1$).
- $n = -3$: Rechazado con mensaje de error.
- $n = 2.5$: Rechazado por decimal.
- $n = texto$: Rechazado por texto no numérico.
- $n = 16$: Rechazado por exceder límite seguro.
