using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaSalud.Models
{
    public class PacientePadecimiento
    {
        public int PacienteID { get; set; }

        public int PadecimientoID { get; set; }

        [ForeignKey("PacienteID")]
        public Paciente Paciente { get; set; }

        [ForeignKey("PadecimientoID")]
        public Padecimiento Padecimiento { get; set; }
    }
}
