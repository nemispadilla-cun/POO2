using System.Globalization;

namespace Cun.Poo;

/// <summary>
/// Formato de pesos colombianos, independiente de la configuracion regional
/// de la maquina (el proyecto usa InvariantGlobalization para que la salida
/// sea identica en Windows, Linux y macOS).
/// </summary>
public static class Dinero
{
    public static string Cop(decimal monto) =>
        "$" + monto.ToString("#,##0", CultureInfo.InvariantCulture).Replace(',', '.') + " COP";
}
