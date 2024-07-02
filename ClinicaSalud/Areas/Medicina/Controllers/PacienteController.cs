using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using ClinicaSalud.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicaSalud.Areas.Medicina.Controllers
{
    [Area("Medicina")]
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





        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            ClinicaSalud.Models.Paciente modelo = new ClinicaSalud.Models.Paciente();

            if (id == null || id <= 0)
            {
                return View(modelo);
            }


            modelo = _unitOfWork.Paciente.Get(x => x.ID == id);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);

        }

        [HttpPost]
        public IActionResult Upsert(ClinicaSalud.Models.Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                if (paciente.ID == 0)
                {
                    _unitOfWork.Paciente.Add(paciente);

                }
                else { 
                    _unitOfWork.Paciente.Update(paciente);
                }

                _unitOfWork.save();

                TempData["success"] = "Make created successfully";
            }
            return RedirectToAction("Index");
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

        [HttpGet]
        public IActionResult AgregarMedicamento(int? id) {
            PacienteMedicamentoVM model = new PacienteMedicamentoVM();

            model.Paciente = new Models.Paciente();

            if(id == null || id <= 0)
                return NotFound();

            model.Paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            model.MedicamentoList = _unitOfWork.Medicamento.GetAll().Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.ID.ToString()
            });

            return View(model);
        }
    }
}
