using ClinicaSalud.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSalud.Areas.Medicina.Controllers
{
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
    }
}
