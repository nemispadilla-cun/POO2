using Cun.Poo.CunExpressApi.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Cun.Poo.CunExpressApi.Controllers;

/// <summary>
/// GET /api/posts: un arreglo JSON de publicaciones, como el endpoint
/// /posts de JSONPlaceholder que usa el taller 4.4 del recurso.
/// </summary>
[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private static readonly string[] Titulos =
    [
        "Como llegar a la CUN en TransMilenio", "Receta de ajiaco santafereno", "Tips para el parcial de POO II",
        "Las mejores ciclovias de Bogota", "Que es una API REST", "Ferias de emprendimiento en Chapinero",
        "Guia de cafes especiales de Colombia", "Como preparar una entrevista tecnica", "Datos abiertos del Distrito",
        "Proyectos finales de Ingenieria de Sistemas", "Rutas del SITP hacia Suba", "Becas y convenios 2026",
    ];

    [HttpGet]
    public List<Post> Listar() =>
        Titulos.Select((titulo, i) => new Post(i % 4 + 1, i + 1, titulo, $"Contenido de la publicacion {i + 1}."))
               .ToList();
}
