using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSalud.Areas.Medicina.Controllers
{
    [Area("Medicina")]
    public class MedicamentoController : Controller
    {
        private IUnitOfWork _unitOfWork;

        #region Constructor

        public MedicamentoController(IUnitOfWork unitOfWork)
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
            var modelList = _unitOfWork.Medicamento.GetAll();
            return Json(new { data = modelList });
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            Medicamento modelo = new Medicamento();

            if (id == null || id <= 0)
            {
                return View(modelo);
            }


            modelo = _unitOfWork.Medicamento.Get(x => x.ID == id);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);

        }

        [HttpPost]
        public IActionResult Upsert(Medicamento _medicamento)
        {

            if (ModelState.IsValid)
            {
                if (_medicamento.ID == 0)
                    _unitOfWork.Medicamento.Add(_medicamento);
                else
                    _unitOfWork.Medicamento.Update(_medicamento);

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
            Medicamento modelo = _unitOfWork.Medicamento.Get(x => x.ID == id);

            if (modelo == null)
            {
                return Json(new { success = false, message = "Error al eliminar el medicamento" });
            }

            _unitOfWork.Medicamento.Remove(modelo);
            _unitOfWork.save();

            return Json(new { success = true, message = "Eliminado correctamente" });
        }


        

        #endregion
    }
}

