using Dapper;
using ManejoPresupesto.Models;
using ManejoPresupesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace ManejoPresupesto.Controllers
{
    public class TiposCuentaController : Controller
    {
        private readonly ILogger<TiposCuentaController> logger;
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IRepositorioTiposCuenta repositorioTiposCuenta;

        public TiposCuentaController(
            ILogger<TiposCuentaController> logger,
            IServicioUsuarios servicioUsuarios,
            IRepositorioTiposCuenta repositorioTiposCuenta)
        {
            this.logger = logger;
            this.servicioUsuarios = servicioUsuarios;
            this.repositorioTiposCuenta = repositorioTiposCuenta;
        }

        public async Task<IActionResult> Index() 
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tiposCuenta = await repositorioTiposCuenta.Obtener(usuarioId);

            return View(tiposCuenta);
        }

        [HttpGet]
        public IActionResult Crear() 
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoCuenta modelo) 
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            modelo.UsuarioId = servicioUsuarios.ObtenerUsuarioId();

            var yaExisteTipoCuenta = await repositorioTiposCuenta.Existe(modelo.Nombre, modelo.UsuarioId);

            if (yaExisteTipoCuenta)
            {
                ModelState
                    .AddModelError
                    (nameof(modelo.Nombre), $"El nombre {modelo.Nombre} ya existe.");
                return View(modelo);
            }

            await repositorioTiposCuenta.Crear(modelo);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id) 
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuenta.ObtenerPorId(id, usuarioId);
            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
            return View(tipoCuenta);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(TipoCuenta modelo) 
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuentaExiste = await repositorioTiposCuenta.ObtenerPorId(modelo.Id, usuarioId);

            if (tipoCuentaExiste is null)
            {
                return RedirectToAction("NoEcontrado", "Home");
            }

            await repositorioTiposCuenta.Editar(modelo);

            return RedirectToAction("Index");

        }


        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre) 
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var yaExisteTipoCuenta = await repositorioTiposCuenta.Existe(nombre, usuarioId);
            if (yaExisteTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe.");
            }

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int id) 
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuenta.ObtenerPorId(id, usuarioId);

            if (tipoCuenta is null) 
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(tipoCuenta);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarTipoCuenta(int id) 
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();

            var tipoCuenta = await repositorioTiposCuenta.ObtenerPorId(id, usuarioId);

            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioTiposCuenta.Borrar(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Ordenar([FromBody] int[] ids) 
        {

            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuentas = await repositorioTiposCuenta.Obtener(usuarioId);

            var idsTiposCuenta = tipoCuentas.Select(x => x.Id);

            var idsTiposCuentaNoPertenecenAlUsuario = ids.Except(idsTiposCuenta).ToList();

            if (idsTiposCuentaNoPertenecenAlUsuario.Count > 0)
            {
                return Forbid();
            }

            var tiposCuentasOrdenados = ids.Select((valor, indice) =>
                new TipoCuenta() { Id = valor, Orden = indice +1 } ).AsEnumerable();

            await repositorioTiposCuenta.Ordenar(tiposCuentasOrdenados);

            return Ok();
        }
    }
}
