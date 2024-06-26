using ClinicaSalud.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSalud.Areas.Medicina.Controllers
{
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
    }
}
