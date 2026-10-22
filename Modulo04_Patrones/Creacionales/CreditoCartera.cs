namespace Cun.Poo.Modulo04Patrones.Creacionales;

/// <summary>Un credito de la cartera de la fintech: lo que muestran los reportes.</summary>
public record CreditoCartera(string Cliente, decimal Saldo, int DiasMora)
{
    public bool EnMora => DiasMora > 30;
}
