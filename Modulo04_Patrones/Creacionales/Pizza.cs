namespace Cun.Poo.Modulo04Patrones.Creacionales;

/// <summary>
/// BUILDER: construir un objeto complejo paso a paso.
///
/// Sin Builder la pizza necesitaria un constructor imposible de leer:
///   new Pizza("delgada", "tomate", "mozzarella", true, false, true, ...)
/// Con Builder cada paso tiene nombre y el orden no importa.
///
/// El constructor de Pizza es PRIVADO: la unica forma de obtener una pizza es
/// pasar por Pizza.Builder, que valida antes de construir. Una vez creada,
/// la pizza es inmutable (solo tiene propiedades get).
/// </summary>
public sealed class Pizza
{
    public string Masa { get; }
    public string Salsa { get; }
    public string Queso { get; }
    public IReadOnlyList<string> Ingredientes { get; }

    private Pizza(Builder b)
    {
        Masa = b.Masa;
        Salsa = b.Salsa;
        Queso = b.Queso;
        Ingredientes = b.Ingredientes.ToList();
    }

    public override string ToString() =>
        $"masa {Masa}, salsa {Salsa}, queso {Queso}, con {string.Join(" y ", Ingredientes)}";

    /// <summary>Clase anidada: el equivalente de "public static class PizzaBuilder" en Java.</summary>
    public sealed class Builder
    {
        public const int MaximoIngredientes = 5;

        internal string Masa { get; private set; } = "tradicional";
        internal string Salsa { get; private set; } = "tomate";
        internal string Queso { get; private set; } = "mozzarella";
        internal List<string> Ingredientes { get; } = [];

        // Cada metodo devuelve "this" para poder encadenar llamadas (interfaz fluida).
        public Builder ConMasa(string masa) { Masa = masa; return this; }

        public Builder ConSalsa(string salsa) { Salsa = salsa; return this; }

        public Builder ConQueso(string queso) { Queso = queso; return this; }

        public Builder ConIngrediente(string ingrediente) { Ingredientes.Add(ingrediente); return this; }

        public Pizza Build()
        {
            if (Ingredientes.Count == 0)
            {
                throw new InvalidOperationException("Una pizza necesita al menos un ingrediente.");
            }
            if (Ingredientes.Count > MaximoIngredientes)
            {
                throw new InvalidOperationException(
                    $"Maximo {MaximoIngredientes} ingredientes; se pidieron {Ingredientes.Count}.");
            }
            return new Pizza(this);
        }
    }
}
