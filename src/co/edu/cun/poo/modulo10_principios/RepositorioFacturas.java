package co.edu.cun.poo.modulo10_principios;

import java.util.List;
import java.util.Optional;

/**
 * La PERSISTENCIA de facturas es otra responsabilidad, así que vive en otro
 * tipo. Es una interfaz para que la Factura y quien la usa no dependan de
 * CÓMO se guarda (memoria, SQL, archivo...).
 */
public interface RepositorioFacturas {

    void guardar(Factura factura);

    Optional<Factura> buscarPorNumero(String numero);

    List<Factura> listar();
}
