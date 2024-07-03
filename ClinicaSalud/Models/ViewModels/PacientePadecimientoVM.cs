using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicaSalud.Models.ViewModels
{
    public class PacientePadecimientoVM
    {
        public Paciente Paciente { get; set; }
        public IEnumerable<SelectListItem> PadecimientoList { get; set; }
        public Padecimiento Padecimiento { get; set; }

        public int PadecimientoID { get; set; }

        public List<Padecimiento> ListaPadecimientos { get; set; }
    }
}
