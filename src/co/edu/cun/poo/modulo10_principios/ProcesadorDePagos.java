package co.edu.cun.poo.modulo10_principios;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/**
 * MANEJO DE EXCEPCIONES en una capa de negocio.
 *
 * autorizar() DETECTA los problemas y lanza PagoRechazadoException.
 * procesar()  CAPTURA esa excepción específica, traduce el motivo a un
 *             mensaje para el usuario, registra el incidente con el Logger
 *             y deja que la aplicación SIGA con el siguiente pago.
 *
 * Reutiliza la interfaz Logger: otra muestra de bajo acoplamiento.
 */
public class ProcesadorDePagos {

    private static final double SALDO_DISPONIBLE_SIMULADO = 500_000;

    private final Logger logger;
    private final List<String> aprobados = new ArrayList<>();

    public ProcesadorDePagos(Logger logger) {
        this.logger = logger;
    }

    /** Devuelve el mensaje que se mostraría al usuario. Nunca lanza: el flujo continúa. */
    public String procesar(Pago pago) {
        try {
            autorizar(pago);
            aprobados.add(pago.referencia());
            logger.info("Pago " + pago.referencia() + " aprobado por $" + String.format("%,.0f", pago.monto()));
            return "Pago aprobado. Gracias por su compra.";

        } catch (PagoRechazadoException e) {
            logger.error("Incidente en pago " + e.getReferencia() + ": " + e.getMessage(), e);
            return mensajeParaUsuario(e);   // retroalimentación clara, sin detalles técnicos
        }
    }

    /** Simula la pasarela: lanza una excepción específica por cada regla violada. */
    private void autorizar(Pago pago) throws PagoRechazadoException {
        if (pago.monto() <= 0) {
            throw new PagoRechazadoException(PagoRechazadoException.Motivo.MONTO_INVALIDO,
                    pago.referencia(), "el monto debe ser mayor que cero");
        }
        if (pago.tarjeta().startsWith("0000")) {
            throw new PagoRechazadoException(PagoRechazadoException.Motivo.TARJETA_BLOQUEADA,
                    pago.referencia(), "la tarjeta está bloqueada");
        }
        if (pago.monto() > SALDO_DISPONIBLE_SIMULADO) {
            throw new PagoRechazadoException(PagoRechazadoException.Motivo.FONDOS_INSUFICIENTES,
                    pago.referencia(), "fondos insuficientes");
        }
    }

    private String mensajeParaUsuario(PagoRechazadoException e) {
        switch (e.getMotivo()) {
            case FONDOS_INSUFICIENTES:
                return "Su tarjeta no tiene fondos suficientes. Intente con otro medio de pago.";
            case TARJETA_BLOQUEADA:
                return "Su tarjeta está bloqueada. Comuníquese con su banco.";
            case MONTO_INVALIDO:
                return "El monto ingresado no es válido. Verifique e intente de nuevo.";
            default:
                return "No pudimos procesar su pago. Intente más tarde.";
        }
    }

    public List<String> getAprobados() {
        return Collections.unmodifiableList(aprobados);
    }
}
