namespace Cun.Poo.Modulo02Interfaces;

/// <summary>Otra implementacion del MISMO contrato, con logica muy distinta.</summary>
public class PagoNequi(string celular) : IMetodoDePago
{
    private string _estado = "Nequi: sin transacciones.";

    public string Nombre => "Nequi";

    public bool ProcesarPago(decimal monto)
    {
        if (!((IMetodoDePago)this).MontoValido(monto))
        {
            _estado = "Nequi: monto por debajo del minimo.";
            return false;
        }

        // Aqui iria la API de Nequi (notificacion push / QR).
        Console.WriteLine($"     Generando solicitud push a Nequi {celular} "
                          + $"por {IMetodoDePago.FormatearCop(monto)}");
        _estado = "Nequi: esperando confirmacion del usuario.";
        return true;
    }

    public string ObtenerEstado() => _estado;
}
