using Microsoft.AspNetCore.Mvc;
using SIGCIv2.Models;
using SIGCIv2.Servicios;

namespace SIGCIv2.Controllers
{
    public class CursosInternosController:Controller
    {
        private readonly IRepositorioCursosInternos repositorioCursosInternos;

        public CursosInternosController(IRepositorioCursosInternos repositorioCursosInternos)
        {
            this.repositorioCursosInternos = repositorioCursosInternos;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var internos = await repositorioCursosInternos.Obtener();
            return View(internos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CursosInternos cursosInternos)
        {
            if (!ModelState.IsValid)
            {
                return View(cursosInternos);
            }

            var existeCurso = await repositorioCursosInternos.Existe(cursosInternos.Clave);
            if (existeCurso)
            {
                ModelState.AddModelError(nameof(cursosInternos.Clave),
                    $"El curso con clave {cursosInternos.Clave} ya existe");
                return View(cursosInternos);
            }

            await repositorioCursosInternos.Crear(cursosInternos);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<ActionResult> Editar(int IdInterno)
        {
            var existeCurso = await repositorioCursosInternos.ObtenerTipo(IdInterno);
            if (existeCurso is null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            return View(existeCurso);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(CursosInternos cursosInternos)
        {
            var existeCurso = await repositorioCursosInternos.Existe(cursosInternos.Clave);
            if (cursosInternos is null)
            {
                return RedirectToAction("Index", "Home");
            }
            await repositorioCursosInternos.Actualizar(cursosInternos);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Borrar(int id)
        {
            var cursosInternos = await repositorioCursosInternos.ObtenerTipo(id);
            if (cursosInternos is null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            return View(cursosInternos);
        }

        [HttpPost]
        public async Task<ActionResult> Borrar(CursosInternos cursosInternos)
        {
            var existeCurso = await repositorioCursosInternos.Existe(cursosInternos.Clave);
            if (cursosInternos is null)
            {
                return RedirectToAction("Index", "Home");
            }
            await repositorioCursosInternos.Borrar(cursosInternos);
            return RedirectToAction("Index");
        }

    }
}
