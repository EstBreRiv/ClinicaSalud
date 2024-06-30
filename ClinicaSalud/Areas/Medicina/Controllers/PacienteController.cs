using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicaSalud.Areas.Medicina.Controllers
{
    public class PacienteController : Controller
    {
        private IUnitOfWork _unitOfWork;

        #region Constructor

        public PacienteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var modelList = _unitOfWork.Paciente.GetAll();
            return Json(new { data = modelList });
        }

        public IActionResult Details(int id)
        {
            ClinicaSalud.Models.Paciente paciente = _unitOfWork.Paciente.Get(v => v.ID == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }
        #endregion
    }
}
