namespace Cun.Poo.CunExpressApi.Modelos;

/// <summary>
/// Como guarda la API a un usuario. Tiene un Telefono que el cliente de
/// consola no conoce: cliente y servidor NO comparten clases, solo el JSON.
/// Esta API podria estar escrita en Java o Python y el cliente no cambiaria.
/// </summary>
public record UsuarioRegistro(int Id, string Nombre, string Email, string Ciudad, string Telefono);
