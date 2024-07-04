using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSalud.Areas.Administracion.Controllers
{
    [Area("Administracion")]
    [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin)]

    public class EspecialidadController : Controller
    {

        private IUnitOfWork _unitOfWork;

        #region Constructor

        public EspecialidadController(IUnitOfWork unitOfWork)
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
            var modelList = _unitOfWork.Especialidad.GetAll();
            return Json(new { data = modelList });
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            Especialidad modelo = new Especialidad();

            if (id == null || id <= 0)
            {
                return View(modelo);
            }


            modelo = _unitOfWork.Especialidad.Get(x => x.ID == id);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);

        }

        [HttpPost]
        public IActionResult Upsert(Especialidad _especialidad)
        {

            if (ModelState.IsValid)
            {
                if (_especialidad.ID == 0)
                    _unitOfWork.Especialidad.Add(_especialidad);
                else
                    _unitOfWork.Especialidad.Update(_especialidad);

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
            Especialidad modelo = _unitOfWork.Especialidad.Get(x => x.ID == id);

            if (modelo == null)
            {
                return Json(new { success = false, message = "Error al eliminar especialidad" });
            }

            _unitOfWork.Especialidad.Remove(modelo);
            _unitOfWork.save();

            return Json(new { success = true, message = "Eliminado correctamente" });
        }

        #endregion
    }
}
