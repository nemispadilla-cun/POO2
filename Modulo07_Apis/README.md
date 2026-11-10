# Módulo 7 · Conexión y consumo de APIs (Temática 7 · G7)

El documento maestro usa Java con `java.net.http.HttpClient` y **Gson**. Aquí el
equivalente en C#: **`HttpClient`** y **`System.Text.Json`**, ambos incluidos en
.NET. No se agrega ningún paquete nuevo (la sección 7 reutiliza EF Core del módulo 6).

```bat
ejecutar.bat 7
```

Para que la clase funcione **sin internet** y la salida sea siempre la misma, la
demo levanta su propia API REST en `localhost` (`ServidorApiLocal`, "la cocina"
del restaurante) y la consume. La sección 8 llama a **APIs reales** y solo corre
si se activa:

```bat
set POO2_EN_VIVO=1 && ejecutar.bat 7
```
```bash
POO2_EN_VIVO=1 ./ejecutar.sh 7
```

```
Modulo07_Apis/
├── Demo07Apis.cs
├── Servidor/     ServidorApiLocal (la API de la demo, con HttpListener)
├── Modelos/      Usuario, TokenAcceso, PedidoEnvio, TasaRepresentativa
├── Clientes/     JsonApi, ClienteUsuarios (CRUD), ClienteOAuth, ClienteTrm, ApiException,
│                 ManejadorBitacora y ManejadorReintentos (DelegatingHandler = Decorator)
├── CunExpress/   IClienteClima, ClienteClimaHttp, GestorRutas, Entrega, ReporteClima
└── Taller/       Publicacion, PublicacionesDbContext (API -> SQLite)
```

| Sección | Ejemplo del recurso | Tipos en este módulo |
|---|---|---|
| 1. Anatomía de una solicitud | Endpoint, recurso, verbo, código de estado (1.1, Figura 1) | `HttpClient`, `HttpRequestMessage`, `HttpResponseMessage` |
| 2. Deserializar | `gson.fromJson(json, Usuario.class)` (4.2) | `Usuario`, `JsonSerializer`, `GetFromJsonAsync` |
| 3. CRUD | GET, POST, PUT, DELETE (1.1) y serialización (1.4) | `ClienteUsuarios` |
| 4. Errores | Familias 2xx, 4xx, 5xx (1.1) | `ApiException`, `EnsureSuccessStatusCode`, `ManejadorReintentos`, `Timeout` |
| 5. APIs protegidas | OAuth2 y token de acceso (4.3) | `ClienteOAuth`, `TokenAcceso`, cabecera `Authorization: Bearer` |
| 6. Caso CunExpress | `GestorRutas` + `ClienteClima` (3.1) | `IClienteClima`, `ClienteClimaHttp`, `GestorRutas` |
| 7. Taller | API → `List<Post>` → tabla `posts` (4.4) | `Publicacion`, `PublicacionesDbContext` |
| 8. En vivo | JSONPlaceholder (4.1) y TRM de datos.gov.co (2.3) | `ClienteTrm`, `TasaRepresentativa` |

## Equivalencias Java (recurso) → C#

| Java | C# / .NET | Nota |
|---|---|---|
| `HttpClient.newHttpClient()` | `new HttpClient()` | Se crea una vez y se reutiliza |
| `HttpRequest.newBuilder().uri(...).GET().build()` | `new HttpRequestMessage(HttpMethod.Get, "api/...")` | O el atajo `GetAsync` |
| `client.send(request, BodyHandlers.ofString())` | `await client.SendAsync(request)` | Asíncrono con `async`/`await` |
| `client.sendAsync(...)` + `CompletableFuture` | `Task<T>` + `await` | |
| `response.statusCode()` | `(int)response.StatusCode` | Enum `HttpStatusCode` |
| `response.body()` | `await response.Content.ReadAsStringAsync()` | |
| Gson `fromJson(json, Usuario.class)` | `JsonSerializer.Deserialize<Usuario>(json)` | O `ReadFromJsonAsync<Usuario>()` |
| Gson `toJson(obj)` | `JsonSerializer.Serialize(obj)` | `PostAsJsonAsync` lo hace solo |
| `Post[]` / `TypeToken<List<Post>>` | `List<Publicacion>` | El genérico basta |
| `@SerializedName("access_token")` | `[JsonPropertyName("access_token")]` | Clave del JSON ≠ nombre de la propiedad |
| `.header("Authorization", "Bearer " + t)` | `Headers.Authorization = new AuthenticationHeaderValue("Bearer", t)` | |
| `HttpTimeoutException` | `TaskCanceledException` | `client.Timeout` |
| `IOException` / `ConnectException` | `HttpRequestException` | Sin código de estado |
| Interceptor de OkHttp | `DelegatingHandler` | Cadena de decoradores |

## Lo que muestra la salida
- **Anatomía:** `GET /api/usuarios/1` regresa `200 OK`, `application/json` y un
  cuerpo de texto. Es solo texto hasta que lo deserializamos.
- **Deserializar:** el JSON trae `"telefono"` y la clase `Usuario` no lo tiene:
  se ignora sin error. El servidor guarda sus datos con **su propia** clase; los
  dos lados solo comparten el JSON.
- **CRUD:** el `POST` no envía `id` (lo decide el servidor) y regresa `201 Created`
  con la cabecera `Location: /api/usuarios/4`. `DELETE` regresa `204 No Content`
  y el `GET` siguiente, `404`. `ClienteUsuarios` convierte el 404 en `null`.
- **Errores:** un email inválido da `400` y una `ApiException` con el mensaje del
  servidor. `/api/inestable` falla dos veces con `503` y el `ManejadorReintentos`
  lo logra al tercer intento. `/api/lento` se cancela a los 500 ms. Sin servidor
  no hay código HTTP: el error es de red (`ConnectionRefused`).
- **OAuth2:** sin token, `401` con `WWW-Authenticate: Bearer`. Con un secreto
  falso, `401 invalid_client`. Con el bueno llega un token y los pedidos.
- **CunExpress:** las entregas en zonas con lluvia intensa (Chapinero, Kennedy)
  salen al final con aviso al cliente. Fontibón no tiene estación (`404`) y sale
  sin pronóstico. Se hace una llamada por **zona**, no por paquete.
- **Taller:** 12 publicaciones pasan de JSON a objetos y de objetos a la tabla
  `posts` de `datos/api_posts.db`, que se puede abrir con DB Browser for SQLite.
- **En vivo:** JSONPlaceholder devuelve el usuario con `Nombre` **vacío**, porque
  esa API dice `"name"` y no `"nombre"`. La TRM llega con `"valor"` como texto
  (`"3297.07"`), y por eso `TasaRepresentativa` usa `AllowReadingFromString`.

## Preguntas para la clase
- ¿Qué ventajas tiene que `GestorRutas` reciba un `IClienteClima` y no un
  `ClienteClimaHttp`? ¿Cómo lo probarías sin internet? (lo haremos en G8)
- ¿Por qué el `ManejadorReintentos` reintenta un `503` pero no un `400`? ¿Qué
  pasaría si reintentara un `POST` que sí alcanzó a crear el usuario?
- `ManejadorBitacora` y `ManejadorReintentos` se envuelven uno al otro. ¿Qué
  patrón de G4 es? ¿Qué cambia en la salida si se invierte el orden?
- ¿Dónde guardarías el `client_secret` de CunExpress en un proyecto real? ¿Qué
  pasa si se sube a un repositorio público de GitHub?
- El recurso pregunta por la ética al consumir APIs con datos personales. ¿Qué
  datos de este módulo son personales? ¿Qué **no** deberíamos pedirle a una API?

## Ejercicio propuesto
1. **Mapeo:** crea `UsuarioPlaceholder` con `[JsonPropertyName("name")]` y
   `[JsonPropertyName("username")]` y vuelve a correr la sección 8 con
   `POO2_EN_VIVO=1`. Ahora el nombre ya no debe salir vacío.
2. **Recurso nuevo:** agrega al servidor `GET /api/productos/{codigo}` y escribe
   `ClienteProductos` con `ObtenerAsync` (404 → `null`).
3. **Clima falso:** escribe `ClienteClimaFijo : IClienteClima`, que responda
   "lluvia intensa" en todas las zonas, y pásalo a `GestorRutas`. ¿Cuántas
   entregas quedan con aviso? No debe cambiar ni una línea de `GestorRutas`.
4. **Token automático:** escribe `ManejadorToken : DelegatingHandler`, que pida
   el token la primera vez, lo guarde y lo agregue a cada solicitud.
5. **TRM:** usa `ClienteTrm` para mostrar en pesos una lista de precios en dólares
   (por ejemplo, tres licencias de software).
