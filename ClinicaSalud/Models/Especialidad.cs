using System.ComponentModel.DataAnnotations;

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
