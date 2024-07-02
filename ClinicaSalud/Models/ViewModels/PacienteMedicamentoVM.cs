using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicaSalud.Models.ViewModels
{
    public class PacienteMedicamentoVM
    {
        public Paciente Paciente { get; set; }
        public IEnumerable<SelectListItem> MedicamentoList { get; set; }
        public Medicamento Medicamento { get; set; }

        public int MedicamentoID { get; set; }

        public List<Medicamento> ListaMedicamentos { get; set; }
    }
}
