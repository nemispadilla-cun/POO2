# CunExpressApi · Web API de ASP.NET Core (Temática 7 · G7)

Es el **servidor** del módulo 7: una aplicación web ASP.NET Core, un ejecutable
aparte del proyecto de consola `POO.Fundamentos`. El cliente que la consume es
`Modulo07_Apis` (`ejecutar.bat 7`). Los dos proyectos están en `POO2.sln`.

```bat
api.bat                 :: arranca en http://localhost:5070 (Ctrl+C para apagar)
```

- **Swagger:** <http://localhost:5070/swagger> documenta cada endpoint y permite
  probarlo desde el navegador (botón *Try it out*).
- **`CunExpressApi.http`:** las mismas solicitudes en texto. Se ejecutan desde
  Visual Studio 2022 o desde VS Code con la extensión *REST Client*.
- Cada solicitud que llega se imprime en la terminal: `[api] GET /api/usuarios/1 -> 200`.

```
CunExpressApi/
├── Program.cs           Configuracion: controladores, servicios (DI), Swagger, bitacora
├── appsettings.json     Credenciales OAuth de demo (seccion "OAuth")
├── Controllers/         UsuariosController (CRUD), TokenController (OAuth2), PedidosController,
│                        ClimaController, PostsController, DiagnosticoController (fallas a proposito)
├── Modelos/             UsuarioRegistro, UsuarioEntrada (DTO + validaciones), Pronostico, Post, Error
├── Servicios/           RepositorioUsuarios (en memoria), ServicioTokens
├── Filtros/             RequiereTokenAttribute (protege un controlador con Bearer)
└── Properties/launchSettings.json   Puerto 5070
```

## Endpoints

| Verbo | Ruta | Respuestas | Controlador |
|---|---|---|---|
| GET | `/api/usuarios` | 200 | `UsuariosController.Listar` |
| GET | `/api/usuarios/{id}` | 200, 404 | `UsuariosController.Obtener` |
| POST | `/api/usuarios` | 201 + `Location`, 400 | `UsuariosController.Crear` |
| PUT | `/api/usuarios/{id}` | 200, 400, 404 | `UsuariosController.Actualizar` |
| DELETE | `/api/usuarios/{id}` | 204, 404 | `UsuariosController.Eliminar` |
| POST | `/oauth/token` (formulario) | 200, 400, 401 | `TokenController.Emitir` |
| GET | `/api/pedidos` (requiere `Bearer`) | 200, 401 | `PedidosController.Listar` |
| GET | `/api/clima?zona=Chapinero` | 200, 404 | `ClimaController.Consultar` |
| GET | `/api/posts` | 200 | `PostsController.Listar` |
| GET | `/api/inestable` | 503, 503, 200… | `DiagnosticoController.Inestable` |
| GET | `/api/lento` | 200 a los 2 s | `DiagnosticoController.Lento` |

## La POO dentro de una Web API
- **Un controlador es una clase** y cada método público es un endpoint. Los
  atributos (`[HttpGet("{id:int}")]`) dicen el verbo y la ruta. El tipo de
  retorno decide el código: `Ok` → 200, `CreatedAtAction` → 201,
  `NoContent` → 204, `NotFound` → 404.
- **Inyección de dependencias (G1):** `Program.cs` registra `RepositorioUsuarios`
  y `ServicioTokens` como **Singleton** (G4). ASP.NET Core los entrega por el
  constructor de cada controlador, que nunca hace `new`.
- **Validación declarativa:** `UsuarioEntrada` tiene `[Required]` y
  `[EmailAddress]`. Con `[ApiController]`, si una regla falla la API responde
  `400` sola (formato *ProblemDetails*, RFC 7807) y el método no llega a correr.
- **DTO ≠ entidad:** el cliente envía `UsuarioEntrada` (sin `Id` ni `Telefono`)
  y la API guarda `UsuarioRegistro`. El cliente nunca decide el `Id`.
- **Filtro como atributo:** `[RequiereToken]` revisa la cabecera
  `Authorization: Bearer ...` antes de cualquier acción del controlador. La regla
  de seguridad se escribe una vez y se reutiliza.
- **Concurrencia:** la API atiende varias solicitudes a la vez, en hilos
  distintos. Por eso `RepositorioUsuarios` usa `lock`.

## Equivalencias con Java

| Java (Spring Boot) | C# (ASP.NET Core) |
|---|---|
| `@RestController` + `@RequestMapping("/api/usuarios")` | `[ApiController]` + `[Route("api/usuarios")]` |
| `@GetMapping("/{id}")` / `@PostMapping` | `[HttpGet("{id:int}")]` / `[HttpPost]` |
| `@PathVariable` / `@RequestParam` / `@RequestBody` | parámetro de ruta / `[FromQuery]` / cuerpo (por defecto) |
| `ResponseEntity.ok(...)` / `.created(uri)` | `Ok(...)` / `CreatedAtAction(...)` |
| `@Valid` + `@NotBlank`, `@Email` | `[ApiController]` + `[Required]`, `[EmailAddress]` |
| `@Service` / `@Autowired` | `builder.Services.AddSingleton<T>()` + constructor |
| `application.properties` | `appsettings.json` |
| springdoc-openapi | Swashbuckle (Swagger) |

> **Seguridad:** el `client_secret` está en `appsettings.json` solo porque es una
> demo. En un proyecto real va en `dotnet user-secrets`, en una variable de
> entorno (`OAuth__ClientSecret`) o en un gestor de secretos, y nunca se sube a GitHub.

## Ejercicio propuesto
1. Agrega `[Range]` o `[Phone]` a un campo nuevo de `UsuarioEntrada` y mira en
   Swagger el `400` que produce un valor inválido.
2. Crea `ProductosController` con un CRUD en memoria y su `RepositorioProductos`.
3. Protege `DELETE /api/usuarios/{id}` con `[RequiereToken]` (solo ese método) y
   ajusta el cliente para que envíe el token.
4. Cambia `RepositorioUsuarios` para que guarde en SQLite con EF Core (G6). El
   controlador no debería cambiar.
