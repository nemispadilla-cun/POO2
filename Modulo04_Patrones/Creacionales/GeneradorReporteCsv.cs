namespace Cun.Poo.Modulo04Patrones.Creacionales;

public class GeneradorReporteCsv : GeneradorReporte
{
    protected override IReporte CrearReporte() => new ReporteCsv();
}

public class ReporteCsv : IReporte
{
    public string Formato => "CSV";

    public string Generar(IReadOnlyList<CreditoCartera> cartera) =>
        "[CSV]  " + string.Join(" | ", cartera.Select(c => $"{c.Cliente};{c.Saldo:0};{c.DiasMora}"));
}
