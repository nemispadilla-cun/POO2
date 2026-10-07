# Módulo 10 · Principios avanzados de POO (Temática 1 · G1)

Corresponde al material **"Principios Avanzados de Programación Orientada a Objetos"** (curso DIS11, temática 1): cohesión, acoplamiento, reutilización, SOLID y manejo de excepciones. Los cuatro ejemplos en texto del recurso están implementados aquí con los mismos nombres. La demo `Demo10PrincipiosDiseno` los recorre en orden y cierra con el mapa SOLID.

| Ejemplo del recurso | Clases en este módulo |
|---|---|
| Cohesión alta: clase `Factura` | `Factura`, `LineaFactura`, `RepositorioFacturas`, `RepositorioFacturasEnMemoria`, contraejemplo `FacturaBajaCohesion` |
| Bajo acoplamiento entre módulos | `ServicioAutenticacion`, `RepositorioUsuarios`, `RepositorioUsuariosSql`, `RepositorioUsuariosMongo` |
| Reutilización: clase `Logger` | `Logger`, `LoggerConsola`, `LoggerMemoria` |
| Manejo de excepciones | `ProcesadorDePagos`, `PagoRechazadoException`, `Pago` |

> Nota sobre la diapositiva "Ejemplo de bajo acoplamiento": allí `RepositorioUsuarios` tiene `buscarPorCredenciales(usuario, clave)`. Aquí el repositorio solo **devuelve el hash almacenado** y es el servicio quien compara. Es una variante más cohesiva: el repositorio guarda y busca, la autenticación autentica. Buen tema de debate en clase.

## 1. Cohesión alta → `Factura`
**Cohesión** mide qué tan relacionados están los métodos de una clase entre sí.

- `Factura` solo tiene métodos de **cálculo de totales**, **descuentos** y **resumen (PDF)**.
- La **persistencia** se delega a `RepositorioFacturas` (interfaz) con `RepositorioFacturasEnMemoria` como implementación.
- `FacturaBajaCohesion` es el contraejemplo: calcula, guarda en BD, envía correo y escribe logs. Cuatro razones para cambiar la misma clase.

> Regla: una clase, una responsabilidad (principio **S** de SOLID).

## 2. Bajo acoplamiento → `ServicioAutenticacion`
**Acoplamiento** mide cuánto depende una clase de los detalles de otra.

- `ServicioAutenticacion` depende de la **interfaz** `RepositorioUsuarios`, nunca de una BD concreta.
- Se cambia `RepositorioUsuariosSql` por `RepositorioUsuariosMongo` **sin tocar** la autenticación.
- Las dependencias llegan por el **constructor** (inyección de dependencias).

> Regla: depende de abstracciones, no de implementaciones (principio **D** de SOLID).

## 3. Reutilización → `Logger`
- `Logger` es una interfaz estándar con `info`, `error` y un `default advertencia`.
- `LoggerConsola` (escritorio) y `LoggerMemoria` (pruebas / móvil) la implementan.
- `ServicioAutenticacion` y `ProcesadorDePagos` usan el **mismo** `Logger` sin modificaciones.

> Regla: programa contra interfaces; lo que depende de una interfaz sirve en cualquier aplicación que la provea.

## 4. Manejo de excepciones → `ProcesadorDePagos`
- `autorizar()` **detecta** y lanza `PagoRechazadoException` con un `Motivo` (enum) y una referencia.
- `procesar()` **captura** esa excepción específica, traduce el motivo a un **mensaje para el usuario**, **registra** el incidente técnico en el `Logger` y **retorna** normalmente.
- El bucle de pagos **no se interrumpe**: cinco pagos, tres rechazados, dos aprobados, cero caídas.

> Regla: quien detecta lanza; quien sabe qué hacer captura. Nunca un `catch` vacío.

## 5. Mapa SOLID sobre los ejemplos
| Principio | Dónde se ve |
|---|---|
| **S** Responsabilidad única | `Factura` calcula; `RepositorioFacturas` persiste. |
| **O** Abierto/Cerrado | Agregar `LoggerArchivo` o `RepositorioUsuariosRedis` no modifica `ServicioAutenticacion`. |
| **L** Sustitución de Liskov | Cualquier `Logger` reemplaza a otro sin cambiar el comportamiento esperado. |
| **I** Segregación de interfaces | `RepositorioUsuarios` expone solo lo que autenticación necesita. |
| **D** Inversión de dependencias | Los servicios dependen de interfaces; las implementaciones se inyectan por constructor. |

## Referencias del recurso (APA 7)
- Cairo Battistutti, O. (2022). *Aprende a programar en Java: De cero al infinito*.
- González, A. H., & Gallo, S. L. (2021). *Herencia y polimorfismo*.
- Hernández, M., & Baquero, L. (2023). *Programación orientada a objetos en Java: Buenas prácticas*. Ediciones de la U.
- Navarro, D., & Rodríguez, S. (2023). *Programación orientada a objetos en Python: Teoría, patrones y prácticas avanzadas*. Alfaomega.
- Vázquez, M. (2020). *Java avanzado: Aplicación de principios orientados a objetos y patrones de diseño*. Pearson.

## Preguntas para la clase
- ¿Qué pruebas unitarias se pueden escribir para `Factura` que serían imposibles para `FacturaBajaCohesion`?
- Si mañana el equipo pasa de MongoDB a Redis, ¿qué archivos se crean y cuáles se modifican?
- ¿Por qué `ProcesadorDePagos.procesar` devuelve un `String` en vez de relanzar la excepción?
- ¿Cuál de los cuatro principios está violando una clase `Utilidades` con 40 métodos estáticos sin relación?

## Ejercicio propuesto
Crear `NotificadorCorreo` y `NotificadorSms` que implementen `Notificador { void enviar(String destino, String mensaje); }`, e inyectarlo en `ProcesadorDePagos` para avisar al cliente cuando su pago es rechazado. La clase `ProcesadorDePagos` no debe saber si es correo o SMS.
