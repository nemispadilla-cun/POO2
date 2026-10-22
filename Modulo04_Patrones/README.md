# Módulo 4 · Patrones de diseño orientados a objetos (Temática 4 · G4)

El documento maestro implementa los patrones en Java con NetBeans y pide un
paquete por categoría. Aquí está resuelto en C# con la misma organización:
una subcarpeta (y un namespace) por categoría.

```
Modulo04_Patrones/
├── Demo04Patrones.cs
├── Creacionales/     Cun.Poo.Modulo04Patrones.Creacionales
├── Estructurales/    Cun.Poo.Modulo04Patrones.Estructurales
└── Comportamiento/   Cun.Poo.Modulo04Patrones.Comportamiento
```

| Patrón | Categoría | Ejemplo del recurso | Tipos en este módulo |
|---|---|---|---|
| **Singleton** | Creacional | Configuración de la fintech de microcréditos | `AppConfig` + `microcreditos.json` |
| **Factory Method** | Creacional | Reportes de cartera PDF / Excel / CSV | `IReporte`, `GeneradorReporte`, `GeneradorReportePdf`/`Excel`/`Csv`, `CreditoCartera` |
| **Builder** | Creacional | Constructor de pizzas (ejercicio 4.2) | `Pizza`, `Pizza.Builder` |
| **Adapter** | Estructural | E-commerce de moda: PayU + Wompi | `IPasarelaPago`, `PayUApi`, `WompiApi`, `PayUAdapter`, `WompiAdapter`, `CarritoDeCompras` |
| **Composite** | Estructural | Sistema de archivos (ejercicio 4.3) | `IComponenteFs`, `Archivo`, `Carpeta` |
| **Decorator** | Estructural | Personalizar el café en Juan Valdez | `IBebida`, `Tinto`, `Latte`, `DecoradorBebida`, `ConCremaBatida`, `ConArequipe`, `ConCanela` |
| **Observer** | Comportamiento | Estación meteorológica (ejercicio 4.4) | `ISujeto`, `IObservador`, `EstacionMeteorologica`, `DisplayPantalla`, `DisplayCelular`, `RegistroWeb` |
| **Strategy** | Comportamiento | Promociones dinámicas del supermercado | `IEstrategiaDescuento`, `SinDescuento`, `DescuentoPorcentual`, `DescuentoPagueLleve`, `DescuentoTarjetaFidelidad`, `DescuentoCombinado`, `CajaRegistradora` |

> **Fe de erratas del recurso:** el documento maestro titula el ejercicio 4.2
> (Builder) como "Estructural" y el 4.3 (Composite) como "Creacional". Es al
> revés: **Builder es creacional** (crea objetos) y **Composite es estructural**
> (compone objetos en árbol). El propio recurso los ubica bien en los paquetes.

## La configuración del Singleton
`AppConfig` lee `Creacionales/microcreditos.json` **una sola vez**. El archivo se
copia junto al ejecutable gracias a este bloque de `POO.Fundamentos.csproj`:

```xml
<None Update="Modulo04_Patrones\Creacionales\microcreditos.json">
  <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
</None>
```

La demo comprueba que dos llamadas a `AppConfig.Instancia` devuelven el mismo
objeto y que el archivo se leyó una vez. Las claves del JSON son de juguete:
en un sistema real los secretos van en variables de entorno o en un almacén
de secretos, nunca en el repositorio.

## Equivalencias Java → C#

| Java (recurso) | C# (este módulo) |
|---|---|
| `getInstance()` con doble verificación | propiedad `Instancia` + `Lazy<T>` |
| clase `final` | clase `sealed` |
| `public static class PizzaBuilder` | clase anidada `Pizza.Builder` |
| `new BufferedReader(new FileReader(...))` | `new StreamReader(new BufferedStream(new FileStream(...)))` |
| `java.util.Observable` / `Observer` | `event` + delegado (`Action<T1,T2>`) |
| interfaz de estrategia | interfaz, o un `Func<...>` si la estrategia es una sola función |

## Lo que muestra la salida
- **Singleton:** `a y b son el mismo objeto? True` · `Veces que se leyo el archivo: 1`.
- **Factory Method:** el formato sale de la configuración (`PDF`); el cliente
  nunca escribe `new ReportePdf()`.
- **Builder:** la pizza hawaiana se arma con llamadas encadenadas; una de 6
  ingredientes es rechazada por `Build()` antes de existir.
- **Adapter:** el mismo `CarritoDeCompras` cobra $429.800 con PayU y con Wompi,
  aunque PayU trabaja en pesos con ids de texto y Wompi en centavos con `Guid`.
- **Composite:** `C:` pesa 3584 KB, calculado de forma recursiva.
- **Decorator:** tinto + crema + canela = $6.300; latte + crema + arequipe = $12.400.
- **Observer:** un cambio de clima actualiza pantalla, celular y web, y además
  dispara la alerta suscrita con `event`.
- **Strategy:** la misma compra de $63.800 paga $57.340, $59.600 o $50.960
  según la promoción del día, sin tocar `CajaRegistradora`.

## Preguntas para la clase
- ¿Por qué un Singleton es más difícil de probar que un objeto que se recibe por
  constructor? ¿Qué haría la inyección de dependencias en su lugar?
- ¿Qué diferencia hay entre `GeneradorReporte.Para("PDF")` (fábrica simple) y el
  método fábrica `CrearReporte()` que sobrescriben las subclases?
- `DescuentoCombinado` es una estrategia hecha de estrategias. ¿Qué otro patrón
  del módulo usa la misma idea?
- ¿Cuándo aplicar un patrón es **sobre-diseño**? Busca un ejemplo en este módulo
  donde una solución más simple bastaría.

## Ejercicio propuesto
1. **Adapter:** agrega `MercadoPagoApi` (inventa su interfaz incompatible) y su
   `MercadoPagoAdapter`. El carrito no debe cambiar ni una línea.
2. **Factory Method:** agrega el formato XML y léelo desde
   `Reportes.FormatoPorDefecto` en `microcreditos.json`.
3. **Strategy:** crea la promoción "martes de frutas y verduras: 15 %" y
   combínala con la tarjeta de la casa.
4. **Observer:** agrega un observador que avise por SMS solo si la humedad
   supera el 85 %, y retíralo con `RemoverObservador` después de la primera alerta.
5. Justifica por escrito, en cinco líneas, qué patrón usarías en tu proyecto
   final y por qué ese y no otro.
