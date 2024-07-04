using ClinicaSalud.Data.Repository;
using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;
using ClinicaSalud.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicaSalud.Areas.Administracion.Controllers
{
    [Area("Administracion")]
    [Authorize(Roles = ClinicaSalud.Utilities.ClinicaSaludRoles.Role_Admin)]

    public class MedicoController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;

        #region Constructor
        public MedicoController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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

        public IActionResult Details(int id)
        {
            //Medico medico = _unitOfWork.Medico.Get(v => v.Id == id);

            MedicoVM model = new MedicoVM();

            model.medico = _unitOfWork.Medico.Get(x => x.Id == id);
            //model.ListaEspecialidades = _unitOfWork.MedicoEspecialidad;

            var lista = _unitOfWork.MedicoEspecialidad.GetAll();

            foreach (var item in lista)
            {
                if (item.MedicoID == id)
                {
                    //var especialidad = _unitOfWork.MedicoEspecialidad.Get(x => x.MedicoID == id);
                    Especialidad varieble = _unitOfWork.Especialidad.Get(x => x.ID == item.especialidadID);
                    model.ListaEspecialidades.Add(varieble);
                }
            }


            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var modelList = _unitOfWork.Medico.GetAll();
            return Json(new { data = modelList });
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            MedicoVM modelo = new()
            {
                medico = new Medico(),
                especialidades = _unitOfWork.Especialidad.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.ID.ToString()
                }).ToList()
            };
        

            if (id == null || id <= 0)
            {
                return View(modelo);
            }


            modelo.medico = _unitOfWork.Medico.Get(m => m.Id == id);

            if (modelo == null)
            {
                return NotFound();
            }



            return View(modelo);

        }


        [HttpPost]
        public IActionResult Upsert(MedicoVM _medico, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString(); //Para generar un string aleatorio muy poco probable de repetirse 
                    string extension = Path.GetExtension(file.FileName);
                    var uploads = Path.Combine(wwwRootPath, @"images\medicos");

                    if (_medico.medico.FotografiaUrl != null) //Update
                    {
                        var oldImageUrl = Path.Combine(wwwRootPath, _medico.medico.FotografiaUrl);

                        if (System.IO.File.Exists(oldImageUrl))
                            System.IO.File.Delete(oldImageUrl);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    _medico.medico.FotografiaUrl = @"images\medicos\" + fileName + extension;

                }

                

                if (_medico.medico.Id == 0)
                    _unitOfWork.Medico.Add(_medico.medico);
                else
                    _unitOfWork.Medico.Update(_medico.medico);

                _unitOfWork.save();

                var listaEspecialidades = _unitOfWork.MedicoEspecialidad.GetAll();

                foreach (var item in listaEspecialidades)
                {
                    if (item.MedicoID == _medico.medico.Id && _medico.SelectedEspecialidades.Contains(item.especialidadID))
                    {
                        return RedirectToAction("Index");
                    }
                }


                foreach (var especialidad in _medico.SelectedEspecialidades) {

                   

                    MedicoEspecialidad me = new MedicoEspecialidad
                    {
                        MedicoID = _medico.medico.Id,
                        especialidadID = especialidad
                    };

                    _unitOfWork.MedicoEspecialidad.Add(me);
                }
               
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
            Medico modelo = _unitOfWork.Medico.Get(x => x.Id == id);

            if (modelo == null)
            {
                return Json(new { success = false, message = "Error al eliminar medico" });
            }

            _unitOfWork.Medico.Remove(modelo);
            _unitOfWork.save();

            return Json(new { success = true, message = "Eliminado correctamente" });
        }

        #endregion
    }
}
