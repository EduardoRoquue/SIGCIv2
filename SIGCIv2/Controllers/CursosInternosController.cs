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
    }
}
