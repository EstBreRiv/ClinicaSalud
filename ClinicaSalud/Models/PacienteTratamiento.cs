using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaSalud.Models
{
    public class PacienteTratamiento
    {
        public int PacienteID { get; set; }

        public int TratamientoID { get; set; }

        [ForeignKey("PacienteID")]
        public Paciente Paciente { get; set; }

        [ForeignKey("TratamientoID")]
        public Tratamiento Tratamiento { get; set; }
    }
}
