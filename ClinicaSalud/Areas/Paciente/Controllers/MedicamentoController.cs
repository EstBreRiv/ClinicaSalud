using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSalud.Areas.Paciente.Controllers
{
    [Area("Paciente")]
    [EnableCors("CorsPolicy")]
    public class MedicamentoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<IdentityUser> _userManager;
        private IUnitOfWork _unitOfWork;
        private static Models.Paciente pacienteActual;

        public MedicamentoController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, IUnitOfWork IunitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = IunitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [EnableCors("CorsPolicy")]
        public IActionResult EnviarMedicamentos(int? id)
        {
            var IdActual = pacienteActual.ID;

            var medicamentos = _unitOfWork.Medicamento.GetAll();

            var pacientes = _unitOfWork.Paciente.GetAll();

            var medicamentoPaciente = _unitOfWork.PacienteMedicamento.GetAll();

            var listaReturn = new List<Medicamento>();

            foreach (var item in medicamentoPaciente)
            {

                if (item.PacienteID == id)
                {
                    listaReturn.Add(item.Medicamento);
                }
            }

            return Json(new { data = listaReturn });

        }
    }
}
