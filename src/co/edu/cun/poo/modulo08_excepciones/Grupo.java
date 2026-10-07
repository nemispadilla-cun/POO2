package co.edu.cun.poo.modulo08_excepciones;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

import co.edu.cun.poo.modulo03_herencia.Estudiante;

/**
 * Un grupo de un curso con cupo limitado y prerrequisito opcional.
 * Sus métodos LANZAN excepciones cuando una regla del negocio se viola;
 * NO imprimen mensajes ni deciden qué hacer: eso es responsabilidad de quien llama.
 */
public class Grupo {

    private final String codigoCurso;
    private final int cupoMaximo;
    private final String prerrequisito;   // puede ser null
    private final List<Estudiante> matriculados = new ArrayList<>();
    private final Map<Estudiante, Double> notas = new HashMap<>();

    public Grupo(String codigoCurso, int cupoMaximo, String prerrequisito) {
        this.codigoCurso = codigoCurso;
        this.cupoMaximo = cupoMaximo;
        this.prerrequisito = prerrequisito;
    }

    /**
     * throws en la firma: avisa al llamador de que ESTO PUEDE FALLAR y cómo.
     * Como MatriculaException es checked, el llamador está obligado a atenderla.
     */
    public void matricular(Estudiante estudiante, Set<String> cursosAprobados) throws MatriculaException {
        if (matriculados.size() >= cupoMaximo) {
            throw new CupoAgotadoException(codigoCurso, cupoMaximo);
        }
        if (prerrequisito != null && !cursosAprobados.contains(prerrequisito)) {
            throw new PrerrequisitoException(codigoCurso, prerrequisito);
        }
        matriculados.add(estudiante);
    }

    /** Lanza una unchecked: una nota fuera de rango es un error del programador que llama. */
    public void registrarNota(Estudiante estudiante, double nota) {
        if (nota < 0.0 || nota > 5.0) {
            throw new NotaInvalidaException(nota);
        }
        if (!matriculados.contains(estudiante)) {
            throw new IllegalStateException(estudiante.getNombre() + " no está matriculado en " + codigoCurso);
        }
        notas.put(estudiante, nota);
    }

    public int getMatriculados() {
        return matriculados.size();
    }

    public String getCodigoCurso() {
        return codigoCurso;
    }
}
