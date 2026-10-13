namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// MOLDE SEMI-CONSTRUIDO (clase abstracta).
///
/// Reune el estado y el comportamiento que comparten todos los personajes y
/// deja sin construir lo que cambia en cada uno. Al tener partes sin construir
/// NO se puede instanciar: "new Personaje(...)" no compila.
///
/// Diferencia clave con una interfaz: aqui SI hay estado y constructor.
/// </summary>
public abstract class Personaje(string nombre, int vidaInicial, int energiaInicial)
{
    protected string Nombre { get; } = nombre;

    protected int PuntosDeVida { get; private set; } = vidaInicial;

    protected int NivelDeEnergia { get; private set; } = energiaInicial;

    public bool EstaVivo => PuntosDeVida > 0;

    // ---------- Metodos CONCRETOS: iguales para todos, se escriben una vez ----------

    public void Moverse(string direccion) =>
        Console.WriteLine($"   {Nombre} se mueve hacia {direccion}.");

    public void RecibirDanio(int cantidad)
    {
        PuntosDeVida -= cantidad;
        if (PuntosDeVida <= 0)
        {
            PuntosDeVida = 0;
            Console.WriteLine($"   {Nombre} ha sido derrotado.");
        }
        else
        {
            Console.WriteLine($"   {Nombre} recibe {cantidad} de danio (quedan {PuntosDeVida} PV).");
        }
    }

    // ---------- Metodos ABSTRACTOS: cada subclase DEBE implementarlos ----------

    public abstract void UsarHabilidadEspecial();

    public abstract int CostoEnergia { get; }

    // ---------- Template method: el esqueleto del turno es comun ----------

    /// <summary>
    /// En C# un metodo no virtual ya no se puede sobreescribir, asi que el
    /// orden del turno queda cerrado (el equivalente del "final" de Java).
    /// </summary>
    public void JugarTurno(string direccion)
    {
        Moverse(direccion);
        if (NivelDeEnergia >= CostoEnergia)
        {
            UsarHabilidadEspecial();            // enlace dinamico
            NivelDeEnergia -= CostoEnergia;
        }
        else
        {
            Console.WriteLine($"   {Nombre} no tiene energia suficiente "
                              + $"({NivelDeEnergia}/{CostoEnergia}) y descansa.");
            NivelDeEnergia += 20;
        }
    }

    public override string ToString() =>
        $"{Nombre,-9} PV:{PuntosDeVida,-4} Energia:{NivelDeEnergia,-4}";
}
