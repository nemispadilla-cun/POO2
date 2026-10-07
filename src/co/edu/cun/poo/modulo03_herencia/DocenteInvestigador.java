package co.edu.cun.poo.modulo03_herencia;

/**
 * HERENCIA MULTINIVEL: DocenteInvestigador -> Docente -> Persona -> Object.
 * Cada nivel aporta algo y puede refinar lo del anterior.
 */
public class DocenteInvestigador extends Docente {

    private final String grupoInvestigacion;
    private static final double BONIFICACION = 0.20;   // 20 % extra

    public DocenteInvestigador(String nombre, String documento, String especialidad,
                               double salarioBase, String grupoInvestigacion) {
        super(nombre, documento, especialidad, salarioBase);
        this.grupoInvestigacion = grupoInvestigacion;
    }

    @Override
    public double calcularSalario() {
        return super.calcularSalario() * (1 + BONIFICACION);
    }

    @Override
    public String presentarse() {
        return super.presentarse() + " Investigo en el grupo " + grupoInvestigacion + ".";
    }
}
