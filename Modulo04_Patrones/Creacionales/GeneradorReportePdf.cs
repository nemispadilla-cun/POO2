namespace Cun.Poo.Modulo04Patrones.Creacionales;

public class GeneradorReportePdf : GeneradorReporte
{
    protected override IReporte CrearReporte() => new ReportePdf();
}

public class ReportePdf : IReporte
{
    public string Formato => "PDF";

    public string Generar(IReadOnlyList<CreditoCartera> cartera) =>
        $"[PDF]  Informe para gerencia: {cartera.Count} creditos, "
        + $"saldo {Dinero.Cop(cartera.Sum(c => c.Saldo))}, {cartera.Count(c => c.EnMora)} en mora";
}
