namespace Cun.Poo.Modulo04Patrones.Estructurales;

/// <summary>
/// DECORATOR: el decorador abstracto. ES una IBebida y ADEMAS TIENE una
/// IBebida adentro. Cada adicion envuelve a la anterior y le suma lo suyo,
/// sin tocar el cafe base. Analogia: personalizar un cafe en Juan Valdez.
///
/// Alternativa con herencia: TintoConCrema, TintoConCremaYCanela,
/// LatteConArequipe... una clase por combinacion. Con 2 bases y 3 adiciones
/// ya son 16 clases. Con Decorator son 2 + 3.
/// </summary>
public abstract class DecoradorBebida : IBebida
{
    protected DecoradorBebida(IBebida envuelta) => Envuelta = envuelta;

    protected IBebida Envuelta { get; }

    public abstract string Descripcion { get; }

    public abstract decimal Precio { get; }
}

public class ConCremaBatida(IBebida envuelta) : DecoradorBebida(envuelta)
{
    public override string Descripcion => Envuelta.Descripcion + " + crema batida";

    public override decimal Precio => Envuelta.Precio + 2_000m;
}

public class ConArequipe(IBebida envuelta) : DecoradorBebida(envuelta)
{
    public override string Descripcion => Envuelta.Descripcion + " + arequipe";

    public override decimal Precio => Envuelta.Precio + 2_500m;
}

public class ConCanela(IBebida envuelta) : DecoradorBebida(envuelta)
{
    public override string Descripcion => Envuelta.Descripcion + " + canela";

    public override decimal Precio => Envuelta.Precio + 800m;
}
