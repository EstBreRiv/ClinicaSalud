using System.ComponentModel.DataAnnotations;

namespace ClinicaSalud.Models
{
    public class Paciente
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public int Cedula { get; set; }



    }
}
