namespace Cun.Poo.Modulo04Patrones.Creacionales;

public class GeneradorReporteExcel : GeneradorReporte
{
    protected override IReporte CrearReporte() => new ReporteExcel();
}

public class ReporteExcel : IReporte
{
    public string Formato => "Excel";

    public string Generar(IReadOnlyList<CreditoCartera> cartera) =>
        $"[XLSX] Hoja 'Cartera': {cartera.Count} filas, columnas Cliente | Saldo | DiasMora";
}
