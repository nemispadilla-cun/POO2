using System.Text.Encodings.Web;
using Cun.Poo.CunExpressApi.Servicios;

// API REST de CunExpress (G7): el "mesero y la cocina" que consume la demo de consola.
//
//   api.bat                      -> arranca en http://localhost:5070
//   http://localhost:5070        -> abre Swagger para probar cada endpoint
//
// Cada recurso es un CONTROLADOR (una clase); las dependencias se registran
// aqui y ASP.NET Core las entrega por el constructor (inyeccion de dependencias).

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping);

// Singleton: una sola instancia para toda la vida de la API (G4).
// Los datos viven en memoria: al apagar la API se pierden (en G6 irian a una base).
builder.Services.AddSingleton<RepositorioUsuarios>();
builder.Services.AddSingleton<ServicioTokens>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Bitacora del SERVIDOR: cada solicitud que llega y el codigo con que se respondio.
// Mirar esta terminal junto a la del cliente muestra las dos puntas de la conversacion.
app.Use(async (contexto, siguiente) =>
{
    await siguiente();
    Console.WriteLine($"[api] {contexto.Request.Method,-6} {contexto.Request.Path}{contexto.Request.QueryString} -> {contexto.Response.StatusCode}");
});

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.Run();
