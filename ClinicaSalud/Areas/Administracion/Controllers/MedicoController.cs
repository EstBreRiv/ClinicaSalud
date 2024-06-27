using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSalud.Areas.Administracion.Controllers
{
    [Area("Administracion")]
    public class MedicoController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;

        #region Constructor
        public MedicoController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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

        public IActionResult Details()
        {
            return View();
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var modelList = _unitOfWork.Medico.GetAll();
            return Json(new { data = modelList });
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            Medico modelo = new Medico();

            if (id == null || id <= 0)
            {
                return View(modelo);
            }


            modelo = _unitOfWork.Medico.Get(x => x.Id == id);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);

        }

        [HttpPost]
        public IActionResult Upsert(Medico _medico, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString(); //Para generar un string aleatorio muy poco probable de repetirse 
                    string extension = Path.GetExtension(file.FileName);
                    var uploads = Path.Combine(wwwRootPath, @"images\medicos");

                    if (_medico.FotografiaUrl != null) //Update
                    {
                        var oldImageUrl = Path.Combine(wwwRootPath, _medico.FotografiaUrl);

                        if (System.IO.File.Exists(oldImageUrl))
                            System.IO.File.Delete(oldImageUrl);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    _medico.FotografiaUrl = @"images\medicos\" + fileName + extension;

                }

                if (_medico.Id == 0)
                    _unitOfWork.Medico.Add(_medico);
                else
                    _unitOfWork.Medico.Update(_medico);

                _unitOfWork.save();
                //agregar tempdata
            }
            else
            {
                //tempdata error
            }
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Medico modelo = _unitOfWork.Medico.Get(x => x.Id == id);

            if (modelo == null)
            {
                return Json(new { success = false, message = "Error al eliminar medico" });
            }

            _unitOfWork.Medico.Remove(modelo);
            _unitOfWork.save();

            return Json(new { success = true, message = "Eliminado correctamente" });
        }

        #endregion
    }
}
