using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaSalud.Models
{
    public class PacienteMedicamento
    {
        public int PacienteID { get; set; }

        public int MedicamentoID { get; set; }

        [ForeignKey("PacienteID")]
        public Paciente Paciente { get; set; }

        [ForeignKey("MedicamentoID")]
        public Medicamento Medicamento { get; set; }

    }
}
