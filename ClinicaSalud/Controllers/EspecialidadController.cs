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
            //trae todas los modelos incluyendo el eager loading del make, osea carga la informacion de la marca asociada
            var modelList = _unitOfWork.Especialidad.GetAll();
            //retorna la informacion en formato json
            return Json(new { data = modelList });
        }

    }
}
