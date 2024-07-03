using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSalud.Areas.Administracion.Controllers
{
    [Area("Administracion")]
    public class ApplicationUserController : Controller
    {
        private IUnitOfWork _unitOfWork;

        #region Constructor

        public ApplicationUserController(IUnitOfWork unitOfWork)
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
            var modelList = _unitOfWork.ApplicationUser.GetAll();
            return Json(new { data = modelList });
        }

        [HttpGet]
        public IActionResult Upsert(int? Cedula)
        {

            ApplicationUser modelo = new ApplicationUser();

            if (Cedula == null || Cedula <= 0)
            {
                return View(modelo);
            }


            modelo = _unitOfWork.ApplicationUser.Get(x => x.Cedula == Cedula);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);

        }

        [HttpPost]
        public IActionResult Upsert(ApplicationUser _applicationUser)
        {

            if (ModelState.IsValid)
            {
                if (_applicationUser.Cedula == 0)
                    _unitOfWork.ApplicationUser.Add(_applicationUser);
                else
                    _unitOfWork.ApplicationUser.Update(_applicationUser);

                _unitOfWork.save();
                //agregar tempdata
            }
            else
            {
                //tempdata error
            }
            return RedirectToAction("Index");
        }
        public IActionResult ToggleBlock(int? cedula)
        {
            ApplicationUser user = _unitOfWork.ApplicationUser.Get(x => x.Cedula == cedula);
            if (user != null)
            {
                user.IsBlocked = !user.IsBlocked;
                _unitOfWork.save();
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
