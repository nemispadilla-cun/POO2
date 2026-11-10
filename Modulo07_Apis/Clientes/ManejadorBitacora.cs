namespace Cun.Poo.Modulo07Apis.Clientes;

/// <summary>
/// Imprime cada solicitud y el codigo de estado que regreso.
///
/// Un DelegatingHandler "envuelve" al manejador que sigue en la cadena: es el
/// patron DECORATOR de G4 aplicado a HttpClient. Agrega comportamiento
/// (registrar el trafico) sin tocar el codigo de los clientes.
/// </summary>
public class ManejadorBitacora : DelegatingHandler
{
    public ManejadorBitacora(HttpMessageHandler interno) : base(interno)
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage solicitud, CancellationToken cancelacion)
    {
        Uri uri = solicitud.RequestUri!;
        string destino = uri.IsLoopback ? uri.PathAndQuery : uri.Host + uri.AbsolutePath;
        string linea = $"{solicitud.Method,-6} {destino}";
        HttpResponseMessage respuesta = await base.SendAsync(solicitud, cancelacion);

        Console.WriteLine($"      [http] {linea,-34} <- {(int)respuesta.StatusCode} {respuesta.ReasonPhrase}");
        if (respuesta.Headers.Location is not null)
        {
            Console.WriteLine($"      [http] {"",-34}    Location: {respuesta.Headers.Location}");
        }
        return respuesta;
    }
}
