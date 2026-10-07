package co.edu.cun.poo.modulo07_colecciones;

import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.function.Predicate;

import co.edu.cun.poo.modulo03_herencia.Persona;

/**
 * CLASE GENÉRICA con TIPO ACOTADO: T puede ser cualquier subclase de Persona.
 * Escribimos el código UNA vez y sirve para Repositorio<Estudiante>,
 * Repositorio<Docente>, etc., con seguridad de tipos en compilación.
 *
 * Sin genéricos tendríamos que usar Object y hacer casts por todas partes.
 */
public class Repositorio<T extends Persona> {

    // LinkedHashMap: acceso rápido por clave Y conserva el orden de inserción.
    private final Map<String, T> porDocumento = new LinkedHashMap<>();

    public void guardar(T entidad) {
        porDocumento.put(entidad.getDocumento(), entidad);
    }

    /** Optional obliga al que llama a considerar el caso "no existe". */
    public Optional<T> buscarPorDocumento(String documento) {
        return Optional.ofNullable(porDocumento.get(documento));
    }

    public List<T> listar() {
        return new ArrayList<>(porDocumento.values());
    }

    /** Recibe un comportamiento (Predicate) como parámetro: programación funcional. */
    public List<T> filtrar(Predicate<T> criterio) {
        List<T> resultado = new ArrayList<>();
        for (T entidad : porDocumento.values()) {
            if (criterio.test(entidad)) {
                resultado.add(entidad);
            }
        }
        return resultado;
    }

    public int contar() {
        return porDocumento.size();
    }

    // ---------- MÉTODO GENÉRICO (independiente del T de la clase) ----------
    public static <E> String unir(List<E> elementos, String separador) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < elementos.size(); i++) {
            if (i > 0) sb.append(separador);
            sb.append(elementos.get(i));
        }
        return sb.toString();
    }
}
