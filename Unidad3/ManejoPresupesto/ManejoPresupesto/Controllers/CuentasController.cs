using ManejoPresupesto.Models;
using ManejoPresupesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupesto.Controllers
{
    public class CuentasController : Controller
    {
        private readonly IRepositorioTiposCuenta repositorioTiposCuenta;
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IRepositorioCuentas repositorioCuentas;

        public CuentasController(
            IRepositorioTiposCuenta repositorioTiposCuenta,
            IServicioUsuarios servicioUsuarios,
            IRepositorioCuentas repositorioCuentas)
        {
            this.repositorioTiposCuenta = repositorioTiposCuenta;
            this.servicioUsuarios = servicioUsuarios;
            this.repositorioCuentas = repositorioCuentas;
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            
            var modelo = new CuentaCreacionViewModel();

            modelo.TiposCuenta = await ObtenerTiposCuenta(usuarioId);

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CuentaCreacionViewModel modelo) 
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuenta.ObtenerPorId(modelo.TipoCuentaId, usuarioId);

            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            if (!ModelState.IsValid)
            {
                modelo.TiposCuenta = await ObtenerTiposCuenta(usuarioId);
                return View(modelo);
            }

            await repositorioCuentas.Crear(modelo);

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerTiposCuenta(int usuarioId) 
        {
            var tiposCuenta = await repositorioTiposCuenta.Obtener(usuarioId);
            return tiposCuenta
                    .Select(x => new SelectListItem(x.Nombre, x.Id.ToString()))
                    .Prepend(new SelectListItem("Seleccionar un tipo de cuenta", ""));
        }
    }
}
