using Microsoft.AspNetCore.Mvc;
using Portafolio.Models;
using Portafolio.Servicios;
using System.Diagnostics;

namespace Portafolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorioProyectos repositorioProyectos;
        private readonly IConfiguration configuration;
        private readonly IServicioEmail servicioEmail;

        public HomeController(
            ILogger<HomeController> logger, 
            IRepositorioProyectos repositorioProyectos,
            IConfiguration configuration,
            IServicioEmail servicioEmail
            )
        {
            _logger = logger;
            this.repositorioProyectos = repositorioProyectos;
            this.configuration = configuration;
            this.servicioEmail = servicioEmail;
        }

        public IActionResult Index()
        {
            var numeroElementos = configuration.GetValue<int>("card-elements");
            var proyectos = repositorioProyectos.ObtenerProyectos().Take(numeroElementos).ToList();
            var modelo = new HomeIndexViewModel 
            {
                Proyectos = proyectos
            };

            //_logger.LogInformation(configuration.GetValue<string>("apellido"));
            //_logger.LogTrace("Este es un mensaje de Trace");
            //_logger.LogDebug("Este es un mensaje de Debug");
            //_logger.LogInformation("Este es un mensaje de Information");
            //_logger.LogWarning("Este es un mensaje de Warning");
            //_logger.LogError("Este es un mensaje de Error");
            //_logger.LogCritical("Este es un mensaje de Critical");

            return View(modelo);
        }

        public IActionResult Proyectos() 
        {
            var proyectos = repositorioProyectos.ObtenerProyectos();
            return View(proyectos);
        }

        [HttpGet]
        public IActionResult Contacto()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contacto(ContactoViewModel modelo) 
        {
            _logger.LogInformation("Nombre: " + modelo.Nombre);
            await servicioEmail.Enviar(modelo);
            return RedirectToAction("Gracias");
        }

        public IActionResult Gracias() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}