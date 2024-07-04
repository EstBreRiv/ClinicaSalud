using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
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

        [DisplayName("Link de resultado de examen medico")]
        public string? PictureURL { get; set; }

        [ValidateNever]
        public string? DescripcionExamen { get; set; }

        [ValidateNever]
        public string? HistorialClinico { get; set; }

    }
}
