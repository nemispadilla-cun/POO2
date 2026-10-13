namespace Cun.Poo.Modulo01Principios;

/// <summary>Implementacion de prueba: un diccionario en memoria.</summary>
public class RepositorioFacturasEnMemoria : IRepositorioFacturas
{
    private readonly Dictionary<string, Factura> _facturas = [];

    public void Guardar(Factura factura) => _facturas[factura.Numero] = factura;

    public Factura? BuscarPorNumero(string numero) =>
        _facturas.TryGetValue(numero, out Factura? f) ? f : null;

    public IReadOnlyList<Factura> Listar() => _facturas.Values.ToList();
}
