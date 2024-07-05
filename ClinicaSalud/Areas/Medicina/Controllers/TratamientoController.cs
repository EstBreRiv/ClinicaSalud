using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSalud.Areas.Medicina.Controllers
{
    [Area("Medicina")]
    [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]
    public class TratamientoController : Controller
    {
        private IUnitOfWork _unitOfWork;

        #region Constructor

        public TratamientoController(IUnitOfWork unitOfWork)
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
            var modelList = _unitOfWork.Tratamiento.GetAll();
            return Json(new { data = modelList });
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            Tratamiento modelo = new Tratamiento();

            if (id == null || id <= 0)
            {
                return View(modelo);
            }


            modelo = _unitOfWork.Tratamiento.Get(x => x.ID == id);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);

        }

        [HttpPost]
        public IActionResult Upsert(Tratamiento _tratamiento)
        {

            if (ModelState.IsValid)
            {
                if (_tratamiento.ID == 0)
                    _unitOfWork.Tratamiento.Add(_tratamiento);
                else
                    _unitOfWork.Tratamiento.Update(_tratamiento);

                _unitOfWork.save();
                TempData["success"] = "Tratamiento creado correctamente";
            }
            else
            {
                TempData["error"] = "Error al crear tratamiento";
            }
            return RedirectToAction("Index");
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Tratamiento modelo = _unitOfWork.Tratamiento.Get(x => x.ID == id);

            if (modelo == null)
            {
                return Json(new { success = false, message = "Error al eliminar el tratamiento" });
            }

            _unitOfWork.Tratamiento.Remove(modelo);
            _unitOfWork.save();

            return Json(new { success = true, message = "Eliminado correctamente" });
        }

        #endregion
    }
}
