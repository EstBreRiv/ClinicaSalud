using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaSalud.Models
{
    public class MedicoEspecialidad
    {
        public int MedicoID { get; set; }

        public int especialidadID { get; set; }

        [ForeignKey("MedicoID")]
        public Medico Medico { get; set; }

        [ForeignKey("especialidadID")]
        public Especialidad Especialidad { get; set; }
    }
}
