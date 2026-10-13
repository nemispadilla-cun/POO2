namespace Cun.Poo.Modulo02Interfaces;

/// <summary>
/// CONTRATO del caso de estudio "EntregaYa S.A.S.".
///
/// Cualquier medio de transporte que quiera entrar a la flota debe saber
/// responder dos preguntas. Nada mas. Esto es el patron STRATEGY.
/// </summary>
public interface IEstrategiaDeTransporte
{
    /// <summary>Tiempo estimado de la entrega, en minutos.</summary>
    double CalcularTiempoEstimado(Ruta ruta);

    /// <summary>Costo del envio, en pesos colombianos.</summary>
    decimal CalcularCostoEnvio(Ruta ruta);

    /// <summary>Identificacion legible del medio.</summary>
    string Medio { get; }
}
