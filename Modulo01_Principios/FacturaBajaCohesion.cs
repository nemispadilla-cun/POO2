namespace Cun.Poo.Modulo01Principios;

/// <summary>
/// CONTRAEJEMPLO. Esta clase calcula, guarda en base de datos, envia correos
/// y escribe logs: cuatro razones distintas para modificarla.
///
/// Sintomas de baja cohesion:
///   - El nombre necesita una "y": calcula Y guarda Y envia.
///   - No se puede probar el calculo sin base de datos ni correo.
///   - Un cambio en el proveedor de correo rompe el calculo del total.
/// </summary>
public class FacturaBajaCohesion
{
    public decimal CalcularTotal() => 0m;

    public void GuardarEnBaseDeDatos() { /* SQL aqui */ }

    public void EnviarPorCorreo(string destinatario) { /* SMTP aqui */ }

    public void EscribirLog(string mensaje) { /* archivo aqui */ }
}
