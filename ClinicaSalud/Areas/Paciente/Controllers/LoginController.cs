using ClinicaSalud.Data.Repository;
using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;



namespace ClinicaSalud.Areas.Paciente.Controllers
{
    [Area("Paciente")]
    [EnableCors("CorsPolicy")]
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<IdentityUser> _userManager;
        private IUnitOfWork _unitOfWork;
        private static Models.Paciente pacienteActual;

        public LoginController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, IUnitOfWork IunitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = IunitOfWork;
        }

        [HttpPost]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Viene nulo");
            }

            var user = _unitOfWork.ApplicationUser.Get(v => v.Email == request.Email);
            user.UserName = request.Email;
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                ApplicationUser user1 = _unitOfWork.ApplicationUser.Get(v => v.Email == request.Email);

                // Verifica si el usuario está bloqueado
                if (user1 != null && user1.IsBlocked)
                {
                    // Usuario bloqueado
                    _logger.LogWarning("La cuenta del usuario está bloqueada.");
                    // Invalida el inicio de sesión y retorna un mensaje adecuado
                    ModelState.AddModelError(string.Empty, "La cuenta del usuario está bloqueada.");
                    return BadRequest("Usuario bloqueado");
                }
                // Aquí puedes realizar cualquier lógica adicional que necesites
                // Como por ejemplo, buscar al paciente en la base de datos
                pacienteActual = _unitOfWork.Paciente.Get(x => x.Cedula == user.Cedula);

                if (pacienteActual != null)
                {
                    return Ok(new { usuarioId = pacienteActual.ID});
                }
            }

            return Unauthorized();
        }
    }
}
