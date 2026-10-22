namespace Cun.Poo.Modulo04Patrones.Creacionales;

/// <summary>
/// El PRODUCTO del Factory Method: lo unico que el codigo cliente conoce
/// de un reporte, sea PDF, Excel o CSV.
/// </summary>
public interface IReporte
{
    string Formato { get; }

    string Generar(IReadOnlyList<CreditoCartera> cartera);
}
