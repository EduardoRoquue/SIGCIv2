using Microsoft.AspNetCore.Mvc;
using SIGCIv2.Models;
using SIGCIv2.Servicios;

namespace SIGCIv2.Controllers
{
    public class CursosExternosController : Controller
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


        [HttpGet]
        public async Task<ActionResult> Editar(int id)
        {
            var cursosExternos = await repositorioCursosExternos.ObtenerId(id);
            if (cursosExternos is null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            return View(cursosExternos);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(CursosExternos cursosExternos)
        {
            var cursoExterno = await repositorioCursosExternos.ObtenerId(cursosExternos.IdExterno);
            //if (trabajadorExiste is null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            await repositorioCursosExternos.Actualizar(cursosExternos);
            return RedirectToAction("Index");
        }



    }
}
