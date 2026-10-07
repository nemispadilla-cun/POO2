package co.edu.cun.poo.modulo09_integrador;

import java.util.ArrayList;
import java.util.List;
import java.util.Set;

import co.edu.cun.poo.modulo03_herencia.Estudiante;
import co.edu.cun.poo.modulo06_composicion.Curso;
import co.edu.cun.poo.modulo07_colecciones.Repositorio;
import co.edu.cun.poo.modulo08_excepciones.CupoAgotadoException;
import co.edu.cun.poo.modulo08_excepciones.MatriculaException;
import co.edu.cun.poo.modulo08_excepciones.PrerrequisitoException;

/**
 * FACHADA del caso integrador: orquesta a las demás clases.
 * Fíjate en que casi no tiene lógica propia: DELEGA en objetos especializados.
 */
public class SistemaMatriculas {

    private static final double VALOR_CREDITO = 180_000;
    private static final int CUPO_POR_CURSO = 3;

    private final Repositorio<Estudiante> estudiantes = new Repositorio<>();   // módulo 7
    private final List<Matricula> matriculas = new ArrayList<>();
    private PoliticaDescuento politica = PoliticaDescuento.SIN_DESCUENTO;      // strategy

    public void registrarEstudiante(Estudiante e) {
        estudiantes.guardar(e);
    }

    public void usarPolitica(PoliticaDescuento politica) {
        this.politica = politica;
    }

    /**
     * Regla de negocio completa: valida cupo y prerrequisito (lanzando las
     * excepciones del módulo 8), calcula el valor con descuento y registra.
     */
    public Matricula matricular(String documento, Curso curso, int creditos,
                                String prerrequisito, Set<String> aprobados) throws MatriculaException {

        Estudiante estudiante = estudiantes.buscarPorDocumento(documento)
                .orElseThrow(() -> new IllegalArgumentException("No existe estudiante con documento " + documento));

        if (curso.getInscritos().size() >= CUPO_POR_CURSO) {
            throw new CupoAgotadoException(curso.getCodigo(), CUPO_POR_CURSO);
        }
        if (prerrequisito != null && !aprobados.contains(prerrequisito)) {
            throw new PrerrequisitoException(curso.getCodigo(), prerrequisito);
        }

        double valorBase = creditos * VALOR_CREDITO;
        double valorFinal = valorBase * (1 - politica.porcentaje(estudiante));

        curso.inscribir(estudiante);                                   // módulo 6
        Matricula matricula = new Matricula(estudiante, curso, valorFinal);
        matriculas.add(matricula);
        return matricula;
    }

    public List<Matricula> matriculasDe(String documento) {
        return matriculas.stream()
                .filter(m -> m.getEstudiante().getDocumento().equals(documento))
                .toList();
    }

    public double totalRecaudado() {
        return matriculas.stream().mapToDouble(Matricula::getValorPagado).sum();
    }

    public List<Matricula> getMatriculas() {
        return List.copyOf(matriculas);
    }
}
