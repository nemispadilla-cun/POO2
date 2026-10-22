namespace Cun.Poo.Modulo04Patrones.Estructurales;

public enum EstadoPago
{
    Aprobado,
    Pendiente,
    Rechazado,
}

/// <summary>
/// ADAPTER: el OBJETIVO (target). La interfaz que el carrito entiende.
///
/// Caso del recurso: un e-commerce de moda cobra con PayU y quiere sumar
/// Wompi. Las dos APIs son incompatibles entre si; esta interfaz es el
/// "enchufe colombiano" al que ambas se adaptan.
/// </summary>
public interface IPasarelaPago
{
    string Nombre { get; }

    /// <summary>Devuelve el id de la transaccion.</summary>
    string IniciarPago(decimal monto, string descripcion);

    EstadoPago VerificarEstado(string idTransaccion);
}
