using Microsoft.AspNetCore.Mvc;
using SIGCIv2.Models;
using SIGCIv2.Servicios;

namespace SIGCIv2.Controllers
{
    public class InstructoresController: Controller
    {
        private readonly  IRepositorioInstructores repositorioInstructores;

        public InstructoresController(IRepositorioInstructores repositorioInstructores)
        {
            this.repositorioInstructores = repositorioInstructores;
        }

        [HttpGet]
        public async Task<IActionResult> Index ()
        {
            var instructor = await repositorioInstructores.Obtener();
            return View(instructor);
        }

        [HttpGet]
        public  IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                return View(instructor);
            }

            //Validaciones en el Repositorio
            var noExisteExpediente = await repositorioInstructores.NoExiste(instructor.Expediente);

            if (noExisteExpediente)
            {
                ModelState.AddModelError(nameof(instructor.Expediente),
                    $"El expediente {instructor.Expediente} no existe");
                return View(instructor);
            }

            await repositorioInstructores.Crear(instructor);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> VerificarExpediente(int trabajadorExp)
        {
            var noExisteExpediente = await repositorioInstructores.NoExiste(trabajadorExp);

            if (noExisteExpediente)
            {
                return Json($"El expediente {trabajadorExp} no existe");
            }
            return Json(true);
        }

    }
}
