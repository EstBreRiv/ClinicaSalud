using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicaSalud.Models
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        [DisplayName("Numero de colegiado")]
        public string NumeroColegiado { get; set; }

        [DisplayName("Link de fotografia")]
        public string? FotografiaUrl { get; set; }

    }

}

