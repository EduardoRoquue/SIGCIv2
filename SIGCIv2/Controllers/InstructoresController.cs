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

        [HttpGet]
        public async Task<ActionResult> Editar(int id)
        {
            var instructor = await repositorioInstructores.ObtenerTipo(id);
            if (instructor is null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            return View(instructor);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(Instructor instructor)
        {
            var instructorExiste = await repositorioInstructores.ObtenerTipo(instructor.Expediente);
            if (instructorExiste is null)
            {
                return RedirectToAction("Index", "Home");
            }
            await repositorioInstructores.Actualizar(instructor);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Borrar(int id)
        {
            var instructor = await repositorioInstructores.ObtenerTipo(id);
            if (instructor is null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            return View(instructor);
        }

        [HttpPost]
        public async Task<ActionResult> Borrar(Instructor instructor)
        {
            var instructorExiste = await repositorioInstructores.ObtenerTipo(instructor.Expediente);
            if (instructorExiste is null)
            {
                return RedirectToAction("Index", "Home");
            }
            await repositorioInstructores.Borrar(instructor);
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
