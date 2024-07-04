using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSalud.Areas.Medicina.Controllers
{
    [Area("Medicina")]
    [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]
    public class PadecimientoController : Controller
    {
        private IUnitOfWork _unitOfWork;

        #region Constructor

        public PadecimientoController(IUnitOfWork unitOfWork)
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
            var modelList = _unitOfWork.Padecimiento.GetAll();
            return Json(new { data = modelList });
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            Padecimiento modelo = new Padecimiento();

            if (id == null || id <= 0)
            {
                return View(modelo);
            }


            modelo = _unitOfWork.Padecimiento.Get(x => x.ID == id);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);

        }

        [HttpPost]
        public IActionResult Upsert(Padecimiento _padecimiento)
        {

            if (ModelState.IsValid)
            {
                if (_padecimiento.ID == 0)
                    _unitOfWork.Padecimiento.Add(_padecimiento);
                else
                    _unitOfWork.Padecimiento.Update(_padecimiento);

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
            Padecimiento modelo = _unitOfWork.Padecimiento.Get(x => x.ID == id);

            if (modelo == null)
            {
                return Json(new { success = false, message = "Error al eliminar el padecimiento" });
            }

            _unitOfWork.Padecimiento.Remove(modelo);
            _unitOfWork.save();

            return Json(new { success = true, message = "Eliminado correctamente" });
        }

        #endregion
    }
}
