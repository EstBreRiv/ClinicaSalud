using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace ClinicaSalud.Models.ViewModels
{
    public class PacienteMedicamentoVM
    {
        public Paciente Paciente { get; set; }

        [DisplayName("Lista de Medicamentos")]
        public IEnumerable<SelectListItem> MedicamentoList { get; set; }
        public Medicamento Medicamento { get; set; }

        public int MedicamentoID { get; set; }

        public List<Medicamento> ListaMedicamentos { get; set; }
    }
}
