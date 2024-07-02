using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaSalud.Models
{
    public class Especialidad
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
