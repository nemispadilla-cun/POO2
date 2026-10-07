package co.edu.cun.poo.modulo02_encapsulamiento;

/**
 * ENCAPSULAMIENTO: ocultar los datos internos y exponer solo operaciones
 * controladas. El objeto es el único responsable de mantener sus reglas
 * (invariantes). Aquí la regla es: la nota está siempre entre 0.0 y 5.0.
 *
 * final: nadie puede heredar de esta clase y saltarse las validaciones
 * sobreescribiendo setValor().
 */
public final class Calificacion {

    public static final double NOTA_MINIMA = 0.0;
    public static final double NOTA_MAXIMA = 5.0;
    public static final double NOTA_APROBATORIA = 3.0;

    // private: solo esta clase puede tocar estos atributos directamente.
    private final String asignatura;   // final: se asigna una vez y no cambia
    private double valor;

    public Calificacion(String asignatura, double valor) {
        if (asignatura == null || asignatura.isBlank()) {
            throw new IllegalArgumentException("La asignatura es obligatoria.");
        }
        this.asignatura = asignatura;
        setValor(valor);   // reutilizamos la validación del setter
    }

    // ---------- GETTERS: lectura controlada ----------
    public String getAsignatura() {
        return asignatura;
    }

    public double getValor() {
        return valor;
    }

    // ---------- SETTER con validación ----------
    // No hay setAsignatura(): decidimos que la asignatura no se puede cambiar.
    public void setValor(double valor) {
        if (valor < NOTA_MINIMA || valor > NOTA_MAXIMA) {
            throw new IllegalArgumentException(
                    "Nota inválida: " + valor + ". Debe estar entre "
                            + NOTA_MINIMA + " y " + NOTA_MAXIMA + ".");
        }
        this.valor = valor;
    }

    // ---------- Comportamiento derivado del estado ----------
    // El cliente no calcula si aprobó; le pregunta al objeto.
    public boolean esAprobatoria() {
        return valor >= NOTA_APROBATORIA;
    }

    public String getConcepto() {
        if (valor >= 4.5) return "Excelente";
        if (valor >= 4.0) return "Sobresaliente";
        if (valor >= 3.0) return "Aceptable";
        return "Insuficiente";
    }

    @Override
    public String toString() {
        return asignatura + ": " + valor + " (" + getConcepto() + ")";
    }
}
