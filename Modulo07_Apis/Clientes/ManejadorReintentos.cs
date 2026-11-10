using System.Net;

namespace Cun.Poo.Modulo07Apis.Clientes;

/// <summary>
/// Reintenta cuando el SERVIDOR falla de forma pasajera (502, 503, 504).
///
/// Los 4xx no se reintentan: si el error es del cliente (datos malos, sin
/// permiso), repetir la misma solicitud dara el mismo error. Entre intentos
/// espera cada vez mas (espera progresiva), para no saturar al servidor.
/// En proyectos reales se usa la libreria Polly, que hace esto mismo.
/// </summary>
public class ManejadorReintentos : DelegatingHandler
{
    private readonly int _maxIntentos;

    public ManejadorReintentos(HttpMessageHandler interno, int maxIntentos = 3) : base(interno)
    {
        _maxIntentos = maxIntentos;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage solicitud, CancellationToken cancelacion)
    {
        for (int intento = 1; ; intento++)
        {
            HttpResponseMessage respuesta = await base.SendAsync(solicitud, cancelacion);
            if (!EsPasajero(respuesta.StatusCode) || intento == _maxIntentos)
            {
                return respuesta;
            }
            int espera = 100 * intento;
            Console.WriteLine($"      [reintento] fallo pasajero, espero {espera} ms (intento {intento} de {_maxIntentos})");
            respuesta.Dispose();
            await Task.Delay(espera, cancelacion);
        }
    }

    private static bool EsPasajero(HttpStatusCode codigo) =>
        codigo is HttpStatusCode.ServiceUnavailable or HttpStatusCode.BadGateway or HttpStatusCode.GatewayTimeout;
}
