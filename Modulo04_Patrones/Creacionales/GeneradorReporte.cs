namespace Cun.Poo.Modulo04Patrones.Creacionales;

/// <summary>
/// FACTORY METHOD: el CREADOR abstracto.
///
/// Exportar() es igual para todos los formatos; lo que cambia es QUE reporte
/// se crea, y esa decision se delega a las subclases a traves del metodo
/// fabrica CrearReporte(). El cliente nunca escribe "new ReportePdf()".
///
/// Agregar XML manana = una clase ReporteXml + un GeneradorReporteXml.
/// Ningun archivo existente se modifica: principio Abierto/Cerrado.
/// </summary>
public abstract class GeneradorReporte
{
    /// <summary>El metodo fabrica.</summary>
    protected abstract IReporte CrearReporte();

    public string Exportar(IReadOnlyList<CreditoCartera> cartera)
    {
        IReporte reporte = CrearReporte();
        return reporte.Generar(cartera);
    }

    /// <summary>
    /// "Menu del dia": el cliente pide por nombre y la cocina decide que plato
    /// concreto entregar. Es el unico lugar que conoce las clases concretas.
    /// </summary>
    public static GeneradorReporte Para(string formato) => formato.ToUpperInvariant() switch
    {
        "PDF" => new GeneradorReportePdf(),
        "EXCEL" => new GeneradorReporteExcel(),
        "CSV" => new GeneradorReporteCsv(),
        _ => throw new NotSupportedException($"Formato de reporte no soportado: {formato}"),
    };
}
