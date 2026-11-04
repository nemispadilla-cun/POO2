using System.Globalization;

namespace Cun.Poo.Modulo06Persistencia.Archivos;

/// <summary>
/// Persistencia en un archivo plano CSV, sin librerias: cada objeto es una
/// linea y cada atributo una columna separada por ';'.
///
/// Aqui el programador hace a mano lo que JSON y XML hacen solos: decidir el
/// orden de las columnas, convertir cada texto a su tipo y validar.
/// Es una pequena muestra del "desajuste" entre objetos y filas.
/// </summary>
public static class InventarioCsv
{
    private const string Encabezado = "id;nombre;precio";

    public static void Guardar(IEnumerable<Producto> productos, string ruta)
    {
        var lineas = new List<string> { Encabezado };
        foreach (Producto p in productos)
        {
            // InvariantCulture: el precio se escribe igual en cualquier pais.
            lineas.Add($"{p.Id};{p.Nombre};{p.Precio.ToString(CultureInfo.InvariantCulture)}");
        }
        File.WriteAllLines(ruta, lineas);
    }

    public static List<Producto> Cargar(string ruta)
    {
        var productos = new List<Producto>();
        foreach (string linea in File.ReadLines(ruta).Skip(1))      // salta el encabezado
        {
            string[] columnas = linea.Split(';');
            if (columnas.Length != 3)
            {
                throw new InvalidDataException($"Linea mal formada: '{linea}'");
            }
            decimal precio = decimal.Parse(columnas[2], CultureInfo.InvariantCulture);
            productos.Add(new Producto(columnas[0], columnas[1], precio));
        }
        return productos;
    }
}
