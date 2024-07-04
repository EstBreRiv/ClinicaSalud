using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace ClinicaSalud.Models.ViewModels
{
    public class PacienteTratamientoVM
    {
        public Paciente Paciente { get; set; }

        [DisplayName("Lista de Tratamientos")]
        public IEnumerable<SelectListItem> TratamientoList { get; set; }
        public Tratamiento Tratamiento { get; set; }

        public int TratamientoID { get; set; }

        public List<Tratamiento> ListaTratamientos { get; set; }
    }
}
