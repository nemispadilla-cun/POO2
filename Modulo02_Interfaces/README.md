# Módulo 2 · Interfaces y clases abstractas (Temática 2 · G2)

| Ejemplo del recurso | Tipos en este módulo |
|---|---|
| Ecosistema de pagos en Colombia | `IMetodoDePago`, `PagoPse`, `PagoNequi`, `PagoEfecty`, `Checkout` |
| Héroes y villanos en un videojuego | `Personaje`, `Mago`, `Guerrero`, `Arquero` |
| `Vehiculo` abstracta + GPS | `Vehiculo`, `Moto`, `Bicicleta`, `Dron`, `IRastreableGps` |
| Caso "EntregaYa S.A.S." | `IEstrategiaDeTransporte`, `ServicioDeDespacho`, contraejemplo `ServicioDeDespachoRigido` |
| Ejercicio guiado de notificaciones | `INotificador`, `NotificadorEmail`, `NotificadorSms`, `ServicioNotificaciones` |

## 1. Interfaz = contrato → `IMetodoDePago`
- `Checkout` opera sobre el tipo `IMetodoDePago`: no conoce PSE, Nequi ni Efecty.
- `MontoMinimo` es `const` dentro de la interfaz.
- `MontoValido()` es un **método `default`**; `PagoEfecty` lo **sobreescribe**
  porque su mínimo de recaudo es distinto.
- `FormatearCop()` es **`static`** en la interfaz.

## 2. Clase abstracta = molde → `Personaje`
- Estado compartido: `PuntosDeVida`, `NivelDeEnergia` (una interfaz **no** puede tenerlo).
- Métodos concretos heredados: `Moverse`, `RecibirDanio`.
- Miembros abstractos: `UsarHabilidadEspecial()` y `CostoEnergia`.
- `JugarTurno()` es un **template method**: al no ser `virtual`, ninguna subclase
  puede alterar el orden del turno (el `final` de Java).
- `new Personaje(...)` **no compila**.

## 3. Las dos juntas → `Vehiculo` + `IRastreableGps`
- `Vehiculo` es abstracta **e implementa** `IEstrategiaDeTransporte`: escribe la
  fórmula una sola vez y obliga a cada subclase a aportar solo sus números.
- `Dron : Vehiculo, IRastreableGps`: herencia simple **más** los contratos que haga falta.

## 4. Caso "EntregaYa S.A.S." → Strategy
- `ServicioDeDespachoRigido` decide con `if/else` sobre un `string`: viola **Abierto/Cerrado**.
- `ServicioDeDespacho` depende solo de `IEstrategiaDeTransporte`. En la demo el
  `Dron` entra a la flota **sin tocar** el servicio; el rígido lo rechaza.

## 5. Un solo método: interfaz funcional en Java, **delegado** en C#
Diferencia importante entre los dos lenguajes:

| | Java | C# |
|---|---|---|
| Lambda sobre un contrato | necesita `@FunctionalInterface` (1 método abstracto) | cualquier **delegado**: `Action`, `Func`, o uno propio |
| Declaración | `interface Notificador { void enviar(...); }` | `Action<string,string>` ya existe |
| Composición | método `default` que devuelve otra lambda | `+=` sobre el delegado, o un método default |

En el módulo conviven las dos formas: la interfaz `INotificador` para el modelo
de dominio y `INotificador.Desde(Action<string,string>)` como puente para poder
pasar lambdas.

## ¿Clase abstracta o interfaz?
| | Clase abstracta | Interfaz |
|---|---|---|
| Atributos de estado | Sí | No (solo `const`/`static`) |
| Constructor | Sí | No |
| Métodos concretos | Sí | Solo `default` / `static` |
| Cuántas por clase | **Una** | **Varias** |
| Se instancia | No | No |

> Regla práctica: **"es un"** → clase abstracta; **"sabe hacer"** → interfaz.

## Ejercicio propuesto
1. `TransporteCamioneta : Vehiculo` (30 km/h, 700 COP/km, SOAT 850.000).
2. Interfaz `IRequiereRefrigeracion` implementada **solo** por la camioneta.
3. Agregar al contrato un `default decimal RecargoPorClima(Ruta ruta)` que
   devuelva 0 y sobreescribirlo en `Bicicleta`.
4. Demostrar que `ServicioDeDespacho` **no se modificó**.
