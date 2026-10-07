# Módulo 11 · Interfaces y Clases Abstractas (Temática 2 · G2)

Corresponde al documento maestro **"Interfaces y Clases Abstractas"** (INGS0806, temática 2). Los ejemplos que el recurso presenta en texto están implementados aquí, ejecutables y con los mismos nombres. La demo `Demo11InterfacesYAbstractas` los recorre en orden y cierra con el criterio de decisión.

| Ejemplo del recurso | Clases en este módulo |
|---|---|
| Ecosistema de pagos en Colombia | `MetodoDePago`, `PagoPSE`, `PagoNequi`, `PagoEfecty`, `Checkout` |
| Héroes y villanos en un videojuego | `Personaje`, `Mago`, `Guerrero`, `Arquero` |
| `Vehiculo` abstracta + `RastreableGPS` | `Vehiculo`, `Moto`, `Bicicleta`, `Dron`, `RastreableGPS` |
| Caso de estudio "EntregaYa S.A.S." | `EstrategiaDeTransporte`, `ServicioDeDespacho`, contraejemplo `ServicioDeDespachoRigido`, `Ruta`, `Pedido` |
| Ejercicio guiado de notificaciones | `Notificador`, `NotificadorEmail`, `NotificadorSms`, `ServicioNotificaciones` |

## 1. Interfaz = contrato → `MetodoDePago`
Una interfaz dice **qué** se debe poder hacer, nunca **cómo**. Es el "estándar USB" del checkout.

- `Checkout` opera sobre el tipo `MetodoDePago`; no conoce PSE, Nequi ni Efecty.
- `MONTO_MINIMO` es `public static final` por el solo hecho de estar en una interfaz.
- `montoValido()` es un método **`default`**: regla común que todas heredan sin escribirla.
- `PagoEfecty` **sobreescribe** ese default porque su mínimo de recaudo es distinto.
- `formatearCOP()` es **`static`**: pertenece al contrato, no a una implementación.

> Agregar DaviPlata mañana es crear una clase nueva. `Checkout` no se abre.

## 2. Clase abstracta = molde → `Personaje`
Una clase abstracta es un **molde semi-construido**: comparte estado y código, y deja sin construir lo que cambia.

- Estado compartido: `puntosDeVida`, `nivelDeEnergia` (una interfaz **no** puede tener esto).
- Métodos concretos heredados: `moverse()`, `recibirDanio()`, `estaVivo()`.
- Métodos abstractos: `usarHabilidadEspecial()` y `costoEnergia()`; el compilador obliga a definirlos.
- `jugarTurno()` es un **template method** `final`: el esqueleto del turno es igual para todos y solo el paso variable se delega hacia abajo.
- `new Personaje(...)` **no compila**: una clase abstracta no se instancia.

## 3. Las dos juntas → `Vehiculo` + `RastreableGPS`
- `Vehiculo` es abstracta **e implementa** `EstrategiaDeTransporte`: escribe la fórmula una sola vez y obliga a cada subclase a aportar solo sus números (`velocidadPromedioKmh()`, `costoPorKm()`, `calcularSOAT()`).
- `Dron extends Vehiculo implements RastreableGPS`: herencia simple **más** todos los contratos que haga falta.

> Herencia: una sola superclase. Interfaces: las que se necesiten.

## 4. Caso de estudio "EntregaYa S.A.S." → Strategy
- `ServicioDeDespachoRigido` decide con `if/else` sobre un `String`. Cada vehículo nuevo obliga a abrir y volver a probar la clase más crítica del sistema: viola **Abierto/Cerrado**.
- `ServicioDeDespacho` depende solo de `EstrategiaDeTransporte`. En la demo el `Dron` entra a la flota **sin tocar** el servicio; el rígido tiene que rechazarlo.
- Esto es el patrón **Strategy**: una familia de algoritmos intercambiables detrás de una abstracción.

## 5. Interfaz funcional → `Notificador`
- `@FunctionalInterface`: **un solo método abstracto**, y por eso se puede escribir como **lambda**.
- `y(Notificador otro)` es un `default` que compone dos notificadores en uno.
- `silencioso()` es un `static` que devuelve la implementación "no hacer nada", útil en pruebas.
- `ServicioNotificaciones` cambia de medio en tiempo de ejecución sin modificar su código.

## ¿Clase abstracta o interfaz?
| | Clase abstracta | Interfaz |
|---|---|---|
| Atributos de estado | Sí | No (solo constantes) |
| Constructor | Sí | No |
| Métodos concretos | Sí | Solo `default` / `static` |
| Cuántas por clase | **Una** (`extends`) | **Varias** (`implements`) |
| Se instancia | No | No |
| Cuándo usarla | Clases muy relacionadas que comparten estado y código | Clases que no tienen por qué estar relacionadas y solo comparten un comportamiento |

> Regla práctica: **"es un"** → clase abstracta; **"sabe hacer"** → interfaz.

## Referencias del recurso (APA 7)
- Barnes, D. J., & Kölling, M. (2017). *Programación orientada a objetos con Java usando BlueJ*. Pearson Educación.
- Blasco, F. (2019). *Programación Orientada a Objetos en JAVA*. RA-MA Editorial.
- Ceballos Sierra, F. J. (2015). *Java 2: curso de programación* (4.ª ed.). RA-MA Editorial.
- Flórez Fernández, H. A. (2012). *Programación orientada a objetos usando java*. Ecoe Ediciones.
- Joyanes Aguilar, L. (2020). *Fundamentos de programación: algoritmos, estructura de datos y objetos*. McGraw-Hill.
- López Goytia, J. L. (2015). *Programación orientada a objetos C++ y Java: un acercamiento interdisciplinario*. Grupo Editorial Patria.
- Moreno Pérez, J. C. (2015). *Programación orientada a objetos*. RA-MA Editorial.
- Vegas, J. (2022). *Java 17 Programación Avanzada*. RA-MA Editorial.
- Vélez Serrano, J. F., Sánchez, A., & Pimentel, E. (2011). *Diseñar y programar, todo es empezar*. Dykinson.

## Preguntas para la clase
- `Personaje` tiene `puntosDeVida`. ¿Por qué eso solo es posible en una clase abstracta y no en una interfaz?
- Si `Bicicleta` y `Dron` no comparten nada con `Moto` salvo el contrato, ¿sigue valiendo la pena la clase abstracta `Vehiculo`?
- ¿Qué pasa si se agrega un segundo método abstracto a `Notificador`? ¿Qué código deja de compilar y por qué?
- ¿Por qué `jugarTurno()` es `final`? ¿Qué se rompería si una subclase lo sobreescribiera?
- En `ServicioDeDespachoRigido`, ¿cuántos archivos hay que modificar para agregar una scooter? ¿Y en `ServicioDeDespacho`?

## Ejercicio propuesto
La gerencia de EntregaYa quiere cobrar un **recargo por lluvia** y entregar con **camioneta refrigerada** para pedidos de farmacia.

1. Crear `TransporteCamioneta` extendiendo `Vehiculo` (velocidad 30 km/h, 700 COP/km, SOAT 850.000).
2. Crear la interfaz `RequiereRefrigeracion { double temperaturaMinima(); }` e implementarla **solo** en la camioneta.
3. Agregar al contrato `EstrategiaDeTransporte` un método `default double recargoPorClima(Ruta ruta)` que devuelva 0, y sobreescribirlo en `Bicicleta` (la lluvia la afecta más).
4. Demostrar en el `main` que `ServicioDeDespacho` **no se modificó** en ningún punto.

Entrega: código + una frase que justifique, para cada tipo nuevo, por qué se eligió interfaz o clase abstracta.
