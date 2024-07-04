using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClinicaSalud.Areas.Paciente.Controllers
{
    [Area("Paciente")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<IdentityUser> _userManager;
        private IUnitOfWork _unitOfWork;
        private static Models.Paciente pacienteActual;
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var usuario = _userManager.GetUserAsync(HttpContext.User);
            var usuario2 = this.User;

            if (usuario2 != null)
            {
                if (usuario2.IsInRole(ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Paciente))
                {
                    var usuario3 = _unitOfWork.ApplicationUser.Get(x => x.Email == usuario2.Identity.Name);
                    pacienteActual = _unitOfWork.Paciente.Get(x => x.Cedula == usuario3.Cedula);

                    return View(pacienteActual);
                }
            }

            Models.Paciente paciente = new Models.Paciente();
            //var correoUsuario = _unitOfWork.Paciente.Get(x => x.);
            //var usuario3 = _userManager.GetUserId(usuario2);
            return View(paciente);
        }

        public IActionResult Login()
        {
            var aux = pacienteActual;
            return View();
        }

        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Paciente )]
        public IActionResult MostrarMedicamentos(int? id)
        {

            var MedicamentosGenerales = _unitOfWork.Medicamento.GetAll();

            var MedicamentosPaciente = _unitOfWork.PacienteMedicamento.Get(x => x.PacienteID == id);

            foreach (var item in MedicamentosGenerales)
            {
                //item.Nombre = item.Nombre + " " + item.Presentacion;
            }
            Models.Paciente paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            return View(paciente);
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
