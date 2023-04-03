using Microsoft.AspNetCore.Mvc;
using SIGCIv2.Models;
using SIGCIv2.Servicios;

namespace SIGCIv2.Controllers
{
    public class CursosExternosController: Controller
    {
        private readonly IRepositorioCursosExternos repositorioCursosExternos;

        public CursosExternosController(IRepositorioCursosExternos repositorioCursosExternos)
        {
            this.repositorioCursosExternos = repositorioCursosExternos;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var curExternos = await repositorioCursosExternos.Obtener();
            return View(curExternos);
        }

        
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CursosExternos cursosExternos)
        {
            if (!ModelState.IsValid)
            {
                return View(cursosExternos);
            }

            await repositorioCursosExternos.Crear(cursosExternos);
            return RedirectToAction("Index");

        }

    }
}
