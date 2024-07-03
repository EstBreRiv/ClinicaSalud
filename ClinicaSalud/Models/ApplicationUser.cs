using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ClinicaSalud.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public int Cedula { get; set;}

        [Required]
        public string Nombre { get; set;}

        [Required]
        public string Apellidos { get; set;}

        [Required]
        public bool IsBlocked { get; set; }
    }
}
