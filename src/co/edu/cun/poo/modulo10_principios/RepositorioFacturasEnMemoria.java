package co.edu.cun.poo.modulo10_principios;

import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;

/** Implementación sencilla para la demo. Una versión SQL implementaría la MISMA interfaz. */
public class RepositorioFacturasEnMemoria implements RepositorioFacturas {

    private final Map<String, Factura> almacen = new LinkedHashMap<>();

    @Override
    public void guardar(Factura factura) {
        almacen.put(factura.getNumero(), factura);
    }

    @Override
    public Optional<Factura> buscarPorNumero(String numero) {
        return Optional.ofNullable(almacen.get(numero));
    }

    @Override
    public List<Factura> listar() {
        return new ArrayList<>(almacen.values());
    }
}
