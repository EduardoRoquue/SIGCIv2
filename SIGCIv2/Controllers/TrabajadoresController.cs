using Microsoft.AspNetCore.Mvc;
using SIGCIv2.Models;
using SIGCIv2.Servicios;
using System.Security.Cryptography;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SIGCIv2.Controllers
{
    public class TrabajadoresController: Controller
    {
        private readonly IRepositorioTrabajadores repositorioTrabajadores;

        public TrabajadoresController(IRepositorioTrabajadores repositorioTrabajadores)
        {
            this.repositorioTrabajadores = repositorioTrabajadores;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trabajador = await repositorioTrabajadores.Obtener();
            return View(trabajador);
        }
        

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Trabajador trabajador)
        {
            //validaciones por atributo
            if (!ModelState.IsValid)
            {
                return View(trabajador);
            }
            //Validaciones en el repositorio
            var yaExisteExpediente = await repositorioTrabajadores.Existe(trabajador.Expediente);
            if (yaExisteExpediente)
            {
                ModelState.AddModelError(nameof(trabajador.Expediente),
                    $"El expediente {trabajador.Expediente} ya existe");
                return View(trabajador);
            }
            await repositorioTrabajadores.Crear(trabajador);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Editar(int id)
        {
            var trabajador = await repositorioTrabajadores.ObtenerTipo(id);
            if (trabajador is null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            return View(trabajador);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(Trabajador trabajador)
        {
            var trabajadorExiste = await repositorioTrabajadores.ObtenerTipo(trabajador.Expediente);
            if (trabajadorExiste is null)
            {
                return RedirectToAction("Index", "Home");
            }
            await repositorioTrabajadores.Actualizar(trabajador);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Borrar(int id)
        {
            var trabajador = await repositorioTrabajadores.ObtenerTipo(id);
            if (trabajador is null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            return View(trabajador);
        }

        [HttpPost]
        public async Task<ActionResult> Borrar(Trabajador trabajador)
        {
            var trabajadorExiste = await repositorioTrabajadores.ObtenerTipo(trabajador.Expediente);
            if (trabajadorExiste is null)
            {
                return RedirectToAction("Index", "Home");
            }
            await repositorioTrabajadores.Borrar(trabajador);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> VerificarExpediente(int expediente, string nombre)
        {
            var yaExisteExpediente = await repositorioTrabajadores.Existe(expediente);
            if (yaExisteExpediente)
            {
                return Json($"El expediente {expediente} ya existe está registrado");
            }
            return Json(true);
        }
    }
}
