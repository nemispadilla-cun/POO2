namespace Cun.Poo.Modulo06Persistencia.Archivos;

/// <summary>
/// Producto del inventario que se guarda en JSON y en CSV (seccion 4.3 del
/// recurso). Es un record: dos productos con los mismos datos son iguales,
/// lo que sirve para comprobar que el objeto leido es igual al guardado.
/// </summary>
public record Producto(string Id, string Nombre, decimal Precio);
