namespace Cun.Poo.Modulo07Apis.CunExpress;

/// <summary>
/// Contrato del servicio de clima (el ClienteClima del caso de estudio).
///
/// GestorRutas depende de esta INTERFAZ y no de OpenWeatherMap: si manana se
/// cambia de proveedor, o en las pruebas de G8 se usa un clima falso, el
/// gestor no cambia. Es la "D" de SOLID (G1) y el contrato de G2.
/// </summary>
public interface IClienteClima
{
    /// <summary>Pronostico de la zona, o null si el proveedor no la conoce.</summary>
    Task<ReporteClima?> ConsultarAsync(string zona);
}
