using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ClinicaSalud.Models.ViewModels
{
    public class PacienteVM
    {
        [ValidateNever]
        public Paciente paciente { get; set; }
    }
}
