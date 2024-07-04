using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace ClinicaSalud.Models.ViewModels
{
    public class PacientePadecimientoVM
    {
        public Paciente Paciente { get; set; }

        [DisplayName("Lista de Padecimientos")]
        public IEnumerable<SelectListItem> PadecimientoList { get; set; }
        public Padecimiento Padecimiento { get; set; }

        public int PadecimientoID { get; set; }

        public List<Padecimiento> ListaPadecimientos { get; set; }
    }
}
