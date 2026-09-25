# Práctica 4: Cambio Mínimo de Monedas (Recursivo)

Este proyecto corresponde a la implementación del **Ejercicio 4** del instrumento de evaluación `Practica2-Recursividad 032026.pdf` (Estructura de Datos, Licenciatura en Ingeniería en Tecnologías de la Información e Innovación Digital, DSM).

> **Enunciado original:**  
> *«Escribe una aplicación en C# que indique el número de monedas de cada cantidad que deben de devolverse como parte de una operación comercial de tal forma que se devuelvan el mínimo numero de piezas (monedas) posibles. Las denominaciones de monedas con las que se cuenta son las siguientes:*  
> *▪ 100 pesos*  
> *▪ 50 pesos*  
> *▪ 20 pesos*  
> *▪ 10 pesos*  
> *▪ 5 pesos*  
> *▪ 1 peso*  
> *▪ 50 centavos*  
> *▪ 20 centavos*  
> *▪ 1 centavo*  
> *Por ejemplo, si se ingresa la cantidad de 73.26 pesos y se paga con 100 pesos el sistema debe devolver que el vuelto o cambio es de 26.74 y debe de entregarse de la siguiente manera, que cumple que se utiliza el mínimo número de monedas posible:*  
> *▪ 0 monedas de 100 pesos*  
> *▪ 0 monedas de 50 pesos*  
> *▪ 1 moneda de 20 pesos*  
> *▪ 0 monedas de 10 pesos*  
> *▪ 1 moneda de 5 pesos*  
> *▪ 1 moneda de 1 peso*  
> *▪ 1 moneda de 50 centavos*  
> *▪ 1 moneda de 20 centavos*  
> *▪ 4 monedas de un centavo».*

---

## 1. Estructura del Proyecto

El proyecto está desarrollado en C# (.NET 10) de forma independiente y estructurada:

```text
Practica_04_Cambio_Minimo/
├── Practica_04_Cambio_Minimo.csproj  # Configuración del proyecto C#
├── CambioMinimoRecursivo.cs          # Clase con el algoritmo recursivo acotado y memoización
├── Program.cs                         # Interfaz de consola, validaciones y presentación
├── README.md                          # Documentación completa y casos de prueba
├── PROMPT.md                          # Requisitos del prompt original
└── publish/                           # Directorio del ejecutable autocontenido win-x64
    └── Practica_04_Cambio_Minimo.exe
```

---

## 2. Lógica Recursiva y Diseño del Algoritmo

Toda la lógica de optimización reside en la clase [`CambioMinimoRecursivo`](CambioMinimoRecursivo.cs):

### ¿Por qué no un algoritmo voraz (greedy)?
El algoritmo voraz (tomar siempre la moneda más grande disponible) **falla** con este sistema de denominaciones.  
Por ejemplo, para devolver **60 centavos**:
- Enfoque voraz: Toma 50¢ + diez monedas de 1¢ = **11 monedas**.
- Enfoque óptimo real: Toma tres monedas de 20¢ ($3 \times 20 = 60$) = **3 monedas**.

### Búsqueda Recursiva Acotada con Memoización
Para encontrar el mínimo global garantizado de monedas sin riesgo de desbordamiento de pila:
- **Estructura recursiva sobre denominaciones:** La recursión se realiza sobre el índice de denominación ($0 \le \text{idx} < 9$). Por ende, la profundidad de la pila de llamadas es como máximo **9 llamadas**, independiente del importe total en centavos.
- **Caso base:** Cuando el residuo es cero o cuando se alcanza la denominación final de 1 centavo (que cubre el remanente de manera directa).
- **Límite de combinaciones:** Si varias monedas de una denominación equivalen a una sola moneda mayor, esa cantidad de monedas pequeñas nunca puede formar parte de una solución óptima. La búsqueda omite únicamente esos conteos.
- **Memoización de Estados:** Se registran los resultados de tuplas `(índice_moneda, centavos_restantes)` en un diccionario para reutilizar subproblemas idénticos.
- **Regla determinista de desempate:** Ante múltiples combinaciones con igual cantidad mínima total de monedas, el algoritmo explora las cantidades de mayor a menor, favoreciendo deterministamente la mayor denominación (orden lexicográfico descendente).
- **Límite máximo seguro:** Se establece y valida un tope máximo de cambio procesable de `$10,000.00` pesos (1,000,000 de centavos).

---

## 3. Manejo de Moneda y Separador Decimal

- **Tipo de dato:** Se usa exclusivamente `decimal` para las entradas monetarias y se convierte inmediatamente a centavos enteros (`int`) para los cálculos, garantizando precisión absoluta sin errores de coma flotante (`float`/`double`).
- **Separadores decimales aceptados:** El programa acepta de forma transparente tanto el punto `.` como la coma `,` (por ejemplo, `73.26` o `73,26`).
- **Validaciones:**
  - Rechaza cantidades negativas.
  - Rechaza más de dos decimales (los centavos solo tienen hasta 2 cifras decimales).
  - Rechaza pagos inferiores al precio de compra.
  - Si el cambio es exactamente cero, muestra 0 piezas para cada una de las 9 denominaciones.

---

## 4. Instrucciones de Compilación y Publicación

### Requisitos
- SDK de .NET 10.0 o superior instalado.
- Sistema Operativo Windows x64.

### Compilar el proyecto
Desde la carpeta `Practica_04_Cambio_Minimo/`:
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
.\publish\Practica_04_Cambio_Minimo.exe
# O bien:
dotnet run -c Release
```

### Modo Directo (precio y pago como argumentos)
```bash
.\publish\Practica_04_Cambio_Minimo.exe 73.26 100.00
# O bien:
dotnet run -c Release --no-build -- 73.26 100.00
```

---

## 6. Casos de Prueba Verificados

### Caso 1: Ejemplo oficial del enunciado (PDF)
- **Precio:** `73.26` | **Pago:** `100.00` | **Cambio:** `26.74`
- **Resultado obtenido:**
  - 0 monedas de 100 pesos
  - 0 monedas de 50 pesos
  - 1 moneda de 20 pesos
  - 0 monedas de 10 pesos
  - 1 moneda de 5 pesos
  - 1 moneda de 1 peso
  - 1 moneda de 50 centavos
  - 1 moneda de 20 centavos
  - 4 monedas de un centavo
  - **Total de monedas:** 9 monedas.
  - **Suma exacta:** $20.00 + 5.00 + 1.00 + 0.50 + 0.20 + 0.04 = 26.74$.
  - **Estado:** Correcto (coincidencia idéntica al PDF).

### Caso 2: Prueba de no-voracidad (60 centavos)
- **Precio:** `0.40` | **Pago:** `1.00` | **Cambio:** `0.60`
- **Resultado:** 3 monedas de 20 centavos (las demás en 0). Total: 3 monedas.  
- **Estado:** Correcto (supera al voraz que daría 11 monedas).

### Caso 3: Cambio cero
- **Precio:** `50.00` | **Pago:** `50.00` | **Cambio:** `0.00`
- **Resultado:** 0 monedas de todas las denominaciones. Total: 0 monedas.  
- **Estado:** Correcto.

### Caso 4: Pago insuficiente
- **Precio:** `100.00` | **Pago:** `80.00`
- **Resultado:** Rechazado con mensaje claro de error.  
- **Estado:** Correcto.

### Caso 5: Más de dos decimales
- **Precio:** `10.999` | **Pago:** `20.00`
- **Resultado:** Rechazado por tener más de 2 decimales.  
- **Estado:** Correcto.
