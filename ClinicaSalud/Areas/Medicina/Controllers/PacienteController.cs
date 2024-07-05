using ClinicaSalud.data.migrations;
using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using ClinicaSalud.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ClinicaSalud.Areas.Medicina.Controllers
{
    [Area("Medicina")]
    [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Paciente)]
    public class PacienteController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;
        private static int IdPacienteActual;

        #region Constructor

        public PacienteController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var modelList = _unitOfWork.Paciente.GetAll();
            return Json(new { data = modelList });
        }





        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            PacienteVM modelo = new PacienteVM();
            modelo.paciente = new Models.Paciente();

            if (id == null || id <= 0)
            {
                return View(modelo);
            }


            modelo.paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            if (modelo == null)
            {
                return NotFound();
            }

            return View(modelo);

        }

        [HttpPost]
        public IActionResult Upsert(PacienteVM _paciente, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString(); //Para generar un string aleatorio muy poco probable de repetirse 
                    string extension = Path.GetExtension(file.FileName).ToLower(); // Convertir a minúsculas para asegurar la comparación

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf" }; // Extensiones permitidas
                    if (!allowedExtensions.Contains(extension))
                    {
                        TempData["error"] = "File format not supported. Only .jpg, .jpeg, .png, .gif, .pdf are allowed.";
                        return View(_paciente);
                    }

                    var uploads = Path.Combine(wwwRootPath, @"images\resultados");

                    // Verificar y crear la carpeta si no existe
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    if (_paciente.paciente.PictureURL != null) //Update
                    {
                        var oldImageUrl = Path.Combine(wwwRootPath, _paciente.paciente.PictureURL);

                        if (System.IO.File.Exists(oldImageUrl))
                            System.IO.File.Delete(oldImageUrl);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    _paciente.paciente.PictureURL = @"images\resultados\" + fileName + extension;

                }

                if (_paciente.paciente.ID == 0)
                    _unitOfWork.Paciente.Add(_paciente.paciente);
                else
                    _unitOfWork.Paciente.Update(_paciente.paciente);

                _unitOfWork.save();


                TempData["success"] = "Paciente agregado";
            }
            return RedirectToAction("Index");
        }




        public IActionResult Details(int id)
        {
            ClinicaSalud.Models.Paciente paciente = _unitOfWork.Paciente.Get(v => v.ID == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }
        #endregion


        #region Medicamentos
        [HttpGet]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]
        public IActionResult AgregarMedicamento(int? id)
        {
            PacienteMedicamentoVM model = new PacienteMedicamentoVM();

            model.Paciente = new Models.Paciente();

            if (id == null || id <= 0)
                return NotFound();

            model.Paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            var listaMedicamentos = _unitOfWork.Medicamento.GetAll();

            var medicamentosPaciente = _unitOfWork.PacienteMedicamento.GetAll();

            IEnumerable<SelectListItem> MedicamentoList = listaMedicamentos.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.ID.ToString()
            });

            model.MedicamentoList = MedicamentoList;

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]

        public IActionResult AgregarMedicamento(PacienteMedicamentoVM _paciente)
        {
            var medicamentosPaciente = _unitOfWork.PacienteMedicamento.GetAll();



            PacienteMedicamento pacienteMedicamento = new PacienteMedicamento
            {
                PacienteID = _paciente.Paciente.ID,
                MedicamentoID = _paciente.MedicamentoID
            };

            foreach (var item in medicamentosPaciente)
            {

                if (item.MedicamentoID == pacienteMedicamento.MedicamentoID && item.PacienteID == pacienteMedicamento.PacienteID)
                {
                    return RedirectToAction("Index");
                }
            }

            _unitOfWork.PacienteMedicamento.Add(pacienteMedicamento);

            _unitOfWork.save();
            TempData["success"] = "Medicamento agregado al paciente correctamente";

            return RedirectToAction("Index");
        }


        [HttpDelete]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]

        public IActionResult Delete(int? id)
        {
            Models.Paciente modelo = _unitOfWork.Paciente.Get(x => x.ID == id);

            if (modelo == null)
            {
                return Json(new { success = false, message = "Error al eliminar paciente" });
            }

            _unitOfWork.Paciente.Remove(modelo);
            _unitOfWork.save();

            return Json(new { success = true, message = "Eliminado correctamente" });
        }


        [HttpGet]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]

        public IActionResult Medicamentos(int? id)
        {
            var paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            IdPacienteActual = paciente.ID;

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }


        [HttpGet]
        public IActionResult getMedicamentos(int? id)
        {
            var IdActual = IdPacienteActual;

            var medicamentos = _unitOfWork.Medicamento.GetAll();

            var pacientes = _unitOfWork.Paciente.GetAll();

            var medicamentoPaciente = _unitOfWork.PacienteMedicamento.GetAll();

            var listaReturn = new List<Medicamento>();

            foreach (var item in medicamentoPaciente)
            {

                if (item.PacienteID == id)
                {
                    listaReturn.Add(item.Medicamento);
                }
            }

            return Json(new { data = listaReturn });

        }



        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]
        public IActionResult suspenderMedicamento(int? id)
        {

            var IdActual = IdPacienteActual;

            PacienteMedicamento pacienteMedicamento = _unitOfWork.PacienteMedicamento.Get(x => x.PacienteID == IdActual && x.MedicamentoID == id);

            if (pacienteMedicamento == null)
            {
                return NotFound();
            }

            _unitOfWork.PacienteMedicamento.Remove(pacienteMedicamento);

            _unitOfWork.save();
            TempData["success"] = "Medicamento suspendido al paciente correctamente";

            return RedirectToAction("Index");
        }
        #endregion

        #region Tratamientos

        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]
        public IActionResult SuspenderTratamiento(int? id)
        {

            var IdActual = IdPacienteActual;

            PacienteTratamiento pacienteTratamiento = _unitOfWork.PacienteTratamiento.Get(x => x.PacienteID == IdActual && x.TratamientoID == id);

            if (pacienteTratamiento == null)
            {
                return NotFound();
            }

            _unitOfWork.PacienteTratamiento.Remove(pacienteTratamiento);

            _unitOfWork.save();
            TempData["success"] = "Tratamiento suspendido al paciente correctamente";

            return RedirectToAction("Index");
        }

        

        [HttpGet]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]

        public IActionResult AgregarTratamiento(int? id)
        {
            PacienteTratamientoVM model = new PacienteTratamientoVM();

            model.Paciente = new Models.Paciente();

            if (id == null || id <= 0)
                return NotFound();

            model.Paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            var listaTratamientos = _unitOfWork.Tratamiento.GetAll();

            var tratamientosPaciente = _unitOfWork.PacienteTratamiento.GetAll();

            IEnumerable<SelectListItem> TratamientoList = listaTratamientos.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.ID.ToString()
            });

            model.TratamientoList = TratamientoList;

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]

        public IActionResult AgregarTratamiento(PacienteTratamientoVM _tratamiento)
        {
            var tratamientosPaciente = _unitOfWork.PacienteTratamiento.GetAll();



            PacienteTratamiento pacienteTratamiento = new PacienteTratamiento
            {
                PacienteID = _tratamiento.Paciente.ID,
                TratamientoID = _tratamiento.TratamientoID
            };

            foreach (var item in tratamientosPaciente)
            {

                if (item.TratamientoID == pacienteTratamiento.TratamientoID && item.PacienteID == pacienteTratamiento.PacienteID)
                {
                    return RedirectToAction("Index");
                }
            }

            _unitOfWork.PacienteTratamiento.Add(pacienteTratamiento);

            _unitOfWork.save();
            TempData["success"] = "Tratamiento agregado al paciente correctamente";

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]
        public IActionResult Tratamientos(int? id)
        {
            var paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            IdPacienteActual = paciente.ID;


            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }


        [HttpGet]
        public IActionResult getTratamientos(int? id)
        {

            var tratamientos = _unitOfWork.Tratamiento.GetAll();

            var pacientes = _unitOfWork.Paciente.GetAll();

            var tratamientoPaciente = _unitOfWork.PacienteTratamiento.GetAll();

            var listaReturn = new List<Tratamiento>();

            foreach (var item in tratamientoPaciente)
            {

                if (item.PacienteID == id)
                {
                    listaReturn.Add(item.Tratamiento);
                }
            }

            return Json(new { data = listaReturn });

        }

        #endregion

        #region Padecimientos
        [HttpGet]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]

        public IActionResult AgregarPadecimiento(int? id)
        {
            PacientePadecimientoVM model = new PacientePadecimientoVM();

            model.Paciente = new Models.Paciente();

            if (id == null || id <= 0)
                return NotFound();

            model.Paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            var listaPadecimientos = _unitOfWork.Padecimiento.GetAll();

            var padecimientoPaciente = _unitOfWork.PacientePadecimiento.GetAll();

            IEnumerable<SelectListItem> PadecimientoList = listaPadecimientos.Select(i => new SelectListItem
            {
                Text = i.Nombre,
                Value = i.ID.ToString()
            });

            model.PadecimientoList = PadecimientoList;

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]

        public IActionResult AgregarPadecimiento(PacientePadecimientoVM _padecimiento)
        {
            var padecimientosPaciente = _unitOfWork.PacientePadecimiento.GetAll();



            PacientePadecimiento pacientePadecimiento = new PacientePadecimiento
            {
                PacienteID = _padecimiento.Paciente.ID,
                PadecimientoID = _padecimiento.PadecimientoID
            };

            foreach (var item in padecimientosPaciente)
            {

                if (item.PadecimientoID == pacientePadecimiento.PadecimientoID && item.PacienteID == pacientePadecimiento.PacienteID)
                {
                    return RedirectToAction("Index");
                }
            }

            _unitOfWork.PacientePadecimiento.Add(pacientePadecimiento);

            _unitOfWork.save();
            TempData["success"] = "Padecimiento agregado al paciente correctamente";

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]

        public IActionResult Padecimientos(int? id)
        {
            var paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            IdPacienteActual = paciente.ID;


            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }


        [HttpGet]
        public IActionResult getPadecimientos(int? id)
        {

            var padecimientos = _unitOfWork.Padecimiento.GetAll();

            var pacientes = _unitOfWork.Paciente.GetAll();

            var padecimientoPaciente = _unitOfWork.PacientePadecimiento.GetAll();

            var listaReturn = new List<Padecimiento>();

            foreach (var item in padecimientoPaciente)
            {

                if (item.PacienteID == id)
                {
                    listaReturn.Add(item.Padecimiento);
                }
            }

            return Json(new { data = listaReturn });

        }

        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]

        public IActionResult SuspenderPadecimiento(int? id)
        {

            var IdActual = IdPacienteActual;

            PacientePadecimiento pacientePadecimiento = _unitOfWork.PacientePadecimiento.Get(x => x.PacienteID == IdActual && x.PadecimientoID == id);

            if (pacientePadecimiento == null)
            {
                return NotFound();
            }

            _unitOfWork.PacientePadecimiento.Remove(pacientePadecimiento);

            _unitOfWork.save();
            TempData["success"] = "Padecimiento suspendido al paciente correctamente";

            return RedirectToAction("Index");
        }

        #endregion

        [HttpGet]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]

        public IActionResult Examenes(int? id)
        {
            var paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            IdPacienteActual = paciente.ID;

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [HttpGet]
        [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin + "," + ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Medico)]

        public IActionResult HistorialClinico(int? id)
        {
            var paciente = _unitOfWork.Paciente.Get(x => x.ID == id);

            IdPacienteActual = paciente.ID;

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }
    }
}
