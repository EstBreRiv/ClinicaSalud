using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicaSalud.Models.ViewModels
{
    public class MedicoVM
    {
        [ValidateNever]
        public Medico medico { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> especialidades { get; set; }

        [ValidateNever]
        public List<int> SelectedEspecialidades { get; set; } = new List<int>();

        [ValidateNever]
        public List<Especialidad> ListaEspecialidades { get; set; } = new List<Especialidad>();
    }
}
