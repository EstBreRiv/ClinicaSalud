using System.ComponentModel.DataAnnotations;

namespace ClinicaSalud.Models
{
    public class Tratamiento
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }
    }
}
