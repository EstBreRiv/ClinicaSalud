using ClinicaSalud.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSalud.Areas.Administracion.Controllers
{
    public class MedicoController : Controller
    {
        private IUnitOfWork _unitOfWork;

        #region Constructor
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region API

        #endregion
    }
}
