using System.Text.Json.Serialization;

namespace Cun.Poo.Modulo07Apis.Modelos;

/// <summary>
/// El recurso "usuario" visto desde NUESTRA aplicacion (el POJO del recurso).
///
/// Las propiedades se llaman como las claves del JSON (Nombre -> "nombre")
/// y el serializador las empareja solo. Lo que el JSON traiga de mas (por
/// ejemplo "telefono") se ignora; lo que falte queda con su valor por defecto.
/// </summary>
public class Usuario
{
    /// <summary>
    /// Lo asigna el servidor. Con Id = 0 (objeto nuevo) no se escribe en el
    /// JSON del POST, porque el cliente no decide el Id.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public string Email { get; set; } = "";

    public string Ciudad { get; set; } = "";

    public override string ToString() => $"Usuario{{Id={Id}, Nombre={Nombre}, Email={Email}, Ciudad={Ciudad}}}";
}
