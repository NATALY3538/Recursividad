# Prompt para agente ejecutor: práctica 4, cambio mínimo

Implementa únicamente el ejercicio 4 del documento `Practica2-Recursividad 032026.pdf`: una aplicación en C# que, dada una cantidad de compra y el pago recibido, devuelva el cambio con el **mínimo número total de monedas**. El ejercicio lista estas denominaciones: 100, 50, 20, 10, 5 y 1 peso; 50, 20 y 1 centavo. Esta entrega requiere que la lógica de resolución sea recursiva.

## Entrega y estructura

- Crea un proyecto de consola C# independiente dentro de `Practica_04_Cambio_Minimo/`. No dependas de otras prácticas.
- Entrega el código fuente visible (`.cs` y `.csproj`), un `README.md` con instrucciones de compilación y uso, y un ejecutable Windows `.exe` generado mediante `dotnet publish` para `win-x64` en `publish/`.
- Coloca lectura y presentación de datos en `Program.cs`. Coloca el algoritmo recursivo de cambio en una clase separada llamada `CambioMinimoRecursivo`, en `CambioMinimoRecursivo.cs`. Puedes agregar un tipo de resultado si ayuda a mantener el código claro.

## Comportamiento

- Solicita el precio y el pago en pesos con hasta dos decimales. Usa `decimal` para leerlos y convierte a **centavos enteros** antes de calcular; no uses `float` ni `double` para dinero. Acepta de forma clara un separador decimal documentado en el `README.md`.
- Rechaza cantidades negativas, más de dos decimales y pagos inferiores al precio. Si el cambio es cero, muestra cero monedas de cada denominación.
- Calcula una combinación que minimice el número total de monedas para **todas** las denominaciones indicadas. Usa recursividad con memoización o una búsqueda recursiva acotada; evita profundidades de pila proporcionales al número de centavos. No uses el algoritmo voraz de escoger siempre la moneda mayor: con estas denominaciones falla, por ejemplo, para 60 centavos (`3 × 20` usa menos piezas que `50 + 10 × 1`).
- Muestra el cambio en pesos, una línea por cada una de las nueve denominaciones, el número de monedas de cada una y el total de piezas. En empates de mínimo, usa una regla determinista y documéntala.
- Establece un máximo de cambio procesable razonable, documéntalo y valida el límite antes de ejecutar el algoritmo.

## Verificación y cierre

- Comprueba el ejemplo del documento: precio `73.26`, pago `100.00`, cambio `26.74`, con 1 moneda de 20 pesos, 1 de 5 pesos, 1 de 1 peso, 1 de 50 centavos, 1 de 20 centavos y 4 de 1 centavo; las demás cantidades son cero. Total: 9 monedas.
- Comprueba además 60 centavos de cambio (3 monedas de 20 centavos), cambio cero y pago insuficiente. Verifica que el valor de las monedas entregadas suma exactamente el cambio.
- Ejecuta `dotnet build` y publica un `.exe` con `dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish`. Comprueba que el ejecutable existe; si el entorno permite ejecutarlo, prueba un caso desde el `.exe`.
- En la respuesta final, indica la ruta del `.exe`, la ruta de los archivos fuente y los comandos de ejecución. No entregues solo fragmentos de código.
