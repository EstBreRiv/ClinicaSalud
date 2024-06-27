using System.ComponentModel.DataAnnotations;

namespace ClinicaSalud.Models
{
    public class Medicamento
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
