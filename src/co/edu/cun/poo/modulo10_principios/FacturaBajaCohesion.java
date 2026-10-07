package co.edu.cun.poo.modulo10_principios;

/**
 * CONTRAEJEMPLO de COHESIÓN BAJA: una clase que hace de todo.
 * Calcula, se guarda en la base de datos, envía correos y escribe logs.
 *
 * Problemas:
 *   - Cambiar la base de datos obliga a tocar la clase Factura.
 *   - No se puede probar el cálculo sin una BD ni un servidor de correo.
 *   - Cuatro razones distintas para modificar la misma clase.
 *
 * (Los métodos están vacíos a propósito; el punto es la FORMA de la clase.)
 */
public class FacturaBajaCohesion {

    public double calcularTotal() { return 0; }

    public void guardarEnBaseDeDatos() { /* abre conexión JDBC, arma el INSERT... */ }

    public void enviarPorCorreo(String destinatario) { /* configura SMTP, adjunta PDF... */ }

    public void escribirLog(String mensaje) { /* abre archivo de log, escribe, cierra... */ }
}
