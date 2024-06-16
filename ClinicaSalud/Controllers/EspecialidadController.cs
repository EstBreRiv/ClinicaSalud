using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSalud.Controllers
{
    public class EspecialidadController : Controller
    {

        private IUnitOfWork _unitOfWork;

        public EspecialidadController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var modelList = _unitOfWork.Especialidad.GetAll();
            return Json(new { data = modelList });
        }

    }
}
