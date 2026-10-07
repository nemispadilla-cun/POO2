package co.edu.cun.poo.modulo08_excepciones;

import java.util.Set;

import co.edu.cun.poo.Modulo;
import co.edu.cun.poo.modulo03_herencia.Estudiante;

public class Demo08Excepciones implements Modulo {

    @Override
    public String titulo() {
        return "Manejo de excepciones";
    }

    @Override
    public String descripcion() {
        return "try/catch/finally, checked vs unchecked, jerarquía propia, throws, multi-catch, try-with-resources, causa.";
    }

    @Override
    public void ejecutar() {
        Estudiante ana = new Estudiante("Ana Torres", "1023456789", "Ing. de Sistemas", 3);
        Estudiante luis = new Estudiante("Luis Pérez", "1034567890", "Ing. de Sistemas", 1);
        Estudiante carla = new Estudiante("Carla Ruiz", "1045678901", "Ing. Industrial", 3);
        Grupo poo2 = new Grupo("POO2", 2, "POO1");

        seccion("1. Flujo normal y flujo excepcional (checked)");
        intentarMatricular(poo2, ana, Set.of("POO1", "MATE1"));      // OK
        intentarMatricular(poo2, luis, Set.of("MATE1"));             // falta prerrequisito
        intentarMatricular(poo2, carla, Set.of("POO1"));             // OK (cupo 2/2)
        intentarMatricular(poo2, luis, Set.of("POO1"));              // cupo agotado
        nota("Grupo.matricular() solo LANZA. Quién imprime y qué hacer después lo decide el llamador.");

        seccion("2. Capturar por jerarquía: del más específico al más general");
        try {
            poo2.matricular(luis, Set.of("POO1"));
        } catch (CupoAgotadoException e) {
            System.out.println("Específico -> cupo máximo era " + e.getCupoMaximo());
        } catch (MatriculaException e) {
            System.out.println("General    -> " + e.getMessage());
        }
        nota("El orden importa: si MatriculaException fuera primero, el catch de CupoAgotado nunca se ejecutaría (y no compila).");

        seccion("3. Unchecked: errores de programación, no se obliga a capturarlos");
        try {
            poo2.registrarNota(ana, 7.5);
        } catch (NotaInvalidaException e) {
            System.out.println("Capturada igual, aunque el compilador no lo exigía -> " + e.getMessage());
        }
        try {
            poo2.registrarNota(luis, 4.0);   // Luis no está matriculado
        } catch (IllegalStateException e) {
            System.out.println("Estado inválido -> " + e.getMessage());
        }

        seccion("4. finally: se ejecuta SIEMPRE");
        try {
            System.out.println("Intentando registrar nota válida...");
            poo2.registrarNota(ana, 4.5);
            System.out.println("Registrada.");
        } catch (NotaInvalidaException e) {
            System.out.println("No debería llegar aquí.");
        } finally {
            System.out.println("finally: liberar recursos, cerrar archivos, registrar auditoría...");
        }

        seccion("5. try-with-resources: cierre automático");
        try (ConexionSimulada conexion = new ConexionSimulada("servidor-notas")) {
            conexion.enviar("nota de Ana = 4.5");
            conexion.enviar("");   // lanza IllegalArgumentException
            System.out.println("Esta línea no se ejecuta.");
        } catch (IllegalArgumentException e) {
            System.out.println("Error al enviar -> " + e.getMessage());
        }
        nota("Observa que la conexión se cerró ANTES de entrar al catch, sin escribir close().");

        seccion("6. Multi-catch y encadenar la causa");
        try {
            procesarMatriculaCompleta(poo2, luis);
        } catch (IllegalStateException | IllegalArgumentException e) {
            System.out.println("Fallo de aplicación: " + e.getMessage());
            System.out.println("Causa original:      " + e.getCause().getClass().getSimpleName()
                    + " - " + e.getCause().getMessage());
        }
        nota("Envolver una excepción de bajo nivel en una de tu capa conserva la causa para depurar.");

        seccion("7. Buenas prácticas");
        nota("Nunca dejes un catch vacío: como mínimo registra el error.");
        nota("Captura lo más específico posible; evita catch (Exception e) salvo en la frontera de la aplicación.");
        nota("Usa checked para fallas esperables del negocio y unchecked para errores de programación.");
    }

    /** Método auxiliar que atiende la excepción y deja que el programa continúe. */
    private void intentarMatricular(Grupo grupo, Estudiante estudiante, Set<String> aprobados) {
        try {
            grupo.matricular(estudiante, aprobados);
            System.out.println("OK    " + estudiante.getNombre() + " matriculado en " + grupo.getCodigoCurso()
                    + " (" + grupo.getMatriculados() + " inscritos)");
        } catch (MatriculaException e) {
            System.out.println("FALLO " + estudiante.getNombre() + ": " + e.getMessage());
        }
    }

    /** Traduce una excepción de negocio (checked) a una de aplicación (unchecked) conservando la causa. */
    private void procesarMatriculaCompleta(Grupo grupo, Estudiante estudiante) {
        try {
            grupo.matricular(estudiante, Set.of());
        } catch (MatriculaException e) {
            throw new IllegalStateException("No se pudo completar el proceso de matrícula de "
                    + estudiante.getNombre(), e);   // el segundo argumento es la CAUSA
        }
    }
}
