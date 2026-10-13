# Módulo 1 · Principios avanzados de POO (Temática 1 · G1)

Cohesión, acoplamiento, reutilización, SOLID y manejo de excepciones, en C#.

| Ejemplo del recurso | Tipos en este módulo |
|---|---|
| Cohesión alta: clase `Factura` | `Factura`, `LineaFactura`, `IRepositorioFacturas`, `RepositorioFacturasEnMemoria`, contraejemplo `FacturaBajaCohesion` |
| Bajo acoplamiento entre módulos | `ServicioAutenticacion`, `IRepositorioUsuarios`, `RepositorioUsuariosSql`, `RepositorioUsuariosMongo` |
| Reutilización: clase `Logger` | `ILogger`, `LoggerConsola`, `LoggerMemoria` |
| Manejo de excepciones | `ProcesadorDePagos`, `PagoRechazadoException`, `Pago` |

## 1. Cohesión alta → `Factura`
- Solo tiene métodos de **cálculo**, **descuento** y **resumen**.
- La **persistencia** se delega a `IRepositorioFacturas`.
- `FacturaBajaCohesion` es el contraejemplo: calcula, guarda, envía correo y
  escribe logs. Cuatro razones para cambiar la misma clase.

> Regla: una clase, una responsabilidad (principio **S** de SOLID).

## 2. Bajo acoplamiento → `ServicioAutenticacion`
- Depende de la **interfaz** `IRepositorioUsuarios`, nunca de una BD concreta.
- Se cambia SQL por MongoDB **sin tocar** la autenticación.
- Las dependencias llegan por el **constructor primario** de C# 12.

> Regla: depende de abstracciones, no de implementaciones (**D** de SOLID).

## 3. Reutilización → `ILogger`
- Interfaz pequeña con `Info`, `Error` y un **método default** `Advertencia`.
- `LoggerConsola` (escritorio) y `LoggerMemoria` (pruebas / móvil).
- Detalle de C#: un método default de interfaz **solo es visible a través de una
  referencia del tipo de la interfaz**. En la demo se ve el truco:
  `ILogger comoInterfaz = loggerMemoria; comoInterfaz.Advertencia(...)`.

## 4. Manejo de excepciones → `ProcesadorDePagos`
- `Autorizar` detecta y lanza `PagoRechazadoException` con un `enum Motivos`.
- `Procesar` captura esa excepción específica, traduce el motivo con una
  **expresión `switch`**, registra el incidente y retorna normalmente.
- Cinco pagos, tres rechazados, dos aprobados, cero caídas.

> Regla: quien detecta lanza; quien sabe qué hacer captura. Nunca un `catch` vacío.

## Preguntas para la clase
- ¿Qué pruebas se pueden escribir para `Factura` que serían imposibles para `FacturaBajaCohesion`?
- Si el equipo pasa de MongoDB a Redis, ¿qué archivos se crean y cuáles se modifican?
- ¿Por qué `Procesar` devuelve un `string` en vez de relanzar la excepción?

## Ejercicio propuesto
Crear `INotificador` con `NotificadorCorreo` y `NotificadorSms`, e inyectarlo en
`ProcesadorDePagos` para avisar al cliente cuando su pago es rechazado.
`ProcesadorDePagos` no debe saber si es correo o SMS.
