namespace Cun.Poo.Modulo04Patrones.Estructurales;

/// <summary>DECORATOR: el COMPONENTE. Lo que toda bebida sabe decir de si misma.</summary>
public interface IBebida
{
    string Descripcion { get; }

    decimal Precio { get; }
}

/// <summary>Componente concreto: la base del pedido.</summary>
public class Tinto : IBebida
{
    public string Descripcion => "Tinto";

    public decimal Precio => 3_500m;
}

/// <summary>Componente concreto: la base del pedido.</summary>
public class Latte : IBebida
{
    public string Descripcion => "Latte";

    public decimal Precio => 7_900m;
}
