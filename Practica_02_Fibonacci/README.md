# Práctica 2: Serie de Fibonacci (Recursiva)

Este proyecto corresponde a la implementación del **Ejercicio 2** del instrumento de evaluación `Practica2-Recursividad 032026.pdf` (Estructura de Datos, Licenciatura en Ingeniería en Tecnologías de la Información e Innovación Digital, DSM).

> **Enunciado original:**  
> *«Realiza un programa que de la serie de Fibonacci utilizando recursividad».*

---

## 1. Estructura del Proyecto

El proyecto está desarrollado en C# (.NET 10) de forma totalmente autónoma e independiente:

```text
Practica_02_Fibonacci/
├── Practica_02_Fibonacci.csproj  # Archivo de configuración del proyecto C#
├── FibonacciRecursivo.cs        # Clase dedicada con la lógica recursiva y memoización
├── Program.cs                   # Interfaz de consola, validaciones y control de usuario
├── README.md                    # Documentación de compilación, ejecución y pruebas
├── PROMPT.md                    # Especificación del prompt original del ejercicio
└── publish/                     # Directorio con el ejecutable autocontenido win-x64
    └── Practica_02_Fibonacci.exe
```

---

## 2. Lógica Recursiva y Diseño

La lógica de cálculo reside en la clase [`FibonacciRecursivo`](FibonacciRecursivo.cs):

- **Definición de términos:**
  - $F(0) = 0$
  - $F(1) = 1$
  - $F(k) = F(k - 1) + F(k - 2)$ para $k \ge 2$.
- **Interpretación de la entrada ($n$):** Representa la **cantidad de términos** solicitados de la serie.
  - Para $n = 0$: Serie vacía `(vacía, 0 términos)`.
  - Para $n = 1$: `0`.
  - Para $n = 2$: `0, 1`.
  - Para $n = 5$: `0, 1, 1, 2, 3`.
  - Para $n = 6$: `0, 1, 1, 2, 3, 5`.
- **Recursión y Memoización:** Se implementa un método genuinamente recursivo con casos base y paso recursivo, utilizando un diccionario de memoización para registrar los estados intermedios y evitar el tiempo exponencial $\mathcal{O}(2^n)$, manteniendo una complejidad lineal $\mathcal{O}(n)$.
- **Tipo de datos:** Se usa `System.Numerics.BigInteger` para los valores de la serie, garantizando precisión arbitraria sin desbordamiento numérico.
- **Límite seguro de términos:** Se establece `MaxLimiteTerminos = 1000`. Esto protege contra el consumo excesivo de memoria y la profundidad de la pila de llamadas.

---

## 3. Validaciones

- Rechaza entradas vacías o en blanco.
- Rechaza texto alfanumérico.
- Rechaza números fraccionarios o decimales (`2.5`, `3/4`).
- Rechaza cantidades negativas ($n < 0$).
- Rechaza valores que superen el límite seguro ($n > 1000$).
- Permite reintentar de forma inmediata ante cualquier error en modo interactivo.

---

## 4. Instrucciones de Compilación y Publicación

### Requisitos
- SDK de .NET 10.0 o superior instalado.
- Sistema Operativo Windows x64.

### Compilar el proyecto
Desde la carpeta `Practica_02_Fibonacci/`:
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
.\publish\Practica_02_Fibonacci.exe
```
O directamente con `dotnet`:
```bash
dotnet run -c Release
```

### Modo Directo (pasando argumento)
```bash
.\publish\Practica_02_Fibonacci.exe 6
# O bien:
dotnet run -c Release --no-build -- 6
```

---

## 6. Casos de Prueba Verificados

| Entrada | Salida Esperada | Comportamiento Observado | Estado |
| :--- | :--- | :--- | :--- |
| `n = 0` | Serie vacía | `Serie generada: (vacía, 0 términos)` | Correcto |
| `n = 1` | `0` | `Serie generada: 0` | Correcto |
| `n = 2` | `0, 1` | `Serie generada: 0, 1` | Correcto |
| `n = 5` | `0, 1, 1, 2, 3` | `Serie generada: 0, 1, 1, 2, 3` | Correcto |
| `n = 6` | `0, 1, 1, 2, 3, 5` | `Serie generada: 0, 1, 1, 2, 3, 5` | Correcto |
| `n = -4` | Rechazo por número negativo | Error reportado y solicitud de nuevo valor | Correcto |
| `n = 3.5` | Rechazo por número decimal | Error reportado y solicitud de nuevo valor | Correcto |
| `n = texto` | Rechazo por formato no numérico | Error reportado y solicitud de nuevo valor | Correcto |
