namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// CONTRATO (interfaz). Dice QUE debe poder hacer cualquier metodo de pago,
/// nunca COMO lo hace. Es el "estandar USB" del checkout.
///
/// Una interfaz no tiene estado: no puede declarar campos de instancia.
/// </summary>
public interface IMetodoDePago
{
    /// <summary>Constante del contrato (en C# los campos de interfaz son const/static).</summary>
    const decimal MontoMinimo = 1_000m;

    /// <summary>Cada implementacion conecta con su propia pasarela.</summary>
    bool ProcesarPago(decimal monto);

    /// <summary>Estado legible de la ultima transaccion.</summary>
    string ObtenerEstado();

    /// <summary>Nombre comercial que ve el cliente en el checkout.</summary>
    string Nombre { get; }

    /// <summary>
    /// Metodo DEFAULT (C# 8): regla comun a todas las implementaciones.
    /// Se hereda sin escribirla y se puede sobreescribir si hace falta.
    /// </summary>
    bool MontoValido(decimal monto) => monto >= MontoMinimo;

    /// <summary>Metodo STATIC: utilidad del contrato, no de una implementacion.</summary>
    static string FormatearCop(decimal monto) => Dinero.Cop(monto);
}
