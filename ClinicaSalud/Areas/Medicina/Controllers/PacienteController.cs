using ClinicaSalud.data.migrations;
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
        private IWebHostEnvironment _webHostEnvironment;

        #region Constructor

        public PacienteController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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

            PacienteVM modelo = new PacienteVM();

            if (id == null || id <= 0)
            {
                return View(modelo);
            }


            modelo.paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);

        }

        [HttpPost]
        public IActionResult Upsert(PacienteVM _paciente, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString(); //Para generar un string aleatorio muy poco probable de repetirse 
                    string extension = Path.GetExtension(file.FileName);
                    var uploads = Path.Combine(wwwRootPath, @"images\resultados");

                    if (_paciente.paciente.PictureURL != null) //Update
                    {
                        var oldImageUrl = Path.Combine(wwwRootPath, _paciente.paciente.PictureURL);

                        if (System.IO.File.Exists(oldImageUrl))
                            System.IO.File.Delete(oldImageUrl);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    _paciente.paciente.PictureURL = @"images\resultados\" + fileName + extension;

                }

                if (_paciente.paciente.ID == 0)
                    _unitOfWork.Paciente.Add(_paciente.paciente);
                else
                    _unitOfWork.Paciente.Update(_paciente.paciente);

                _unitOfWork.save();


                TempData["success"] = "Paciente agregado";
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
        public IActionResult AgregarMedicamento(int? id)
        {
            PacienteMedicamentoVM model = new PacienteMedicamentoVM();

            model.Paciente = new Models.Paciente();

            if (id == null || id <= 0)
                return NotFound();

            model.Paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            var listaMedicamentos = _unitOfWork.Medicamento.GetAll();

            var medicamentosPaciente = _unitOfWork.PacienteMedicamento.GetAll();

            IEnumerable<SelectListItem> MedicamentoList = listaMedicamentos.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.ID.ToString()
            });

            model.MedicamentoList = MedicamentoList;

            return View(model);
        }


        [HttpPost]
        public IActionResult AgregarMedicamento(PacienteMedicamentoVM _paciente)
        {
            var medicamentosPaciente = _unitOfWork.PacienteMedicamento.GetAll();



            PacienteMedicamento pacienteMedicamento = new PacienteMedicamento
            {
                PacienteID = _paciente.Paciente.ID,
                MedicamentoID = _paciente.MedicamentoID
            };

            foreach (var item in medicamentosPaciente)
            {

                if (item.MedicamentoID == pacienteMedicamento.MedicamentoID && item.PacienteID == pacienteMedicamento.PacienteID)
                {
                    return RedirectToAction("Index");
                }
            }

            _unitOfWork.PacienteMedicamento.Add(pacienteMedicamento);

            _unitOfWork.save();

            return RedirectToAction("Index");
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Models.Paciente modelo = _unitOfWork.Paciente.Get(x => x.ID == id);

            if (modelo == null)
            {
                return Json(new { success = false, message = "Error al eliminar medico" });
            }

            _unitOfWork.Paciente.Remove(modelo);
            _unitOfWork.save();

            return Json(new { success = true, message = "Eliminado correctamente" });
        }


        [HttpGet]
        public IActionResult Medicamentos(int? id)
        {
            var paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }


        [HttpGet]
        public IActionResult getMedicamentos(int? id)
        {

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
