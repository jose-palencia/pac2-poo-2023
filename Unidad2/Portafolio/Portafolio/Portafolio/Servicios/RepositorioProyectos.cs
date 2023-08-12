using Portafolio.Models;

namespace Portafolio.Servicios
{
    public class RepositorioProyectos : IRepositorioProyectos
    {
        public List<Proyecto> ObtenerProyectos()
        {
            return new List<Proyecto>
                { new Proyecto
                    {
                    Titulo = "La Prensa",
                    Descripcion = "Aplicacion de Planilla realizado en ASP.Net Core",
                    ImagenUrl = "/images/pic-laprensa.png",
                    Link = "http://laprensa.hn"
                    },
                  new Proyecto
                    {
                    Titulo = "Diunsa",
                    Descripcion = "E-Comerce realizado en React",
                    ImagenUrl = "/images/pic-diunsa.png",
                    Link = "https://diunsa.hn"
                    },
                  new Proyecto
                    {
                    Titulo = "UNAH",
                    Descripcion = "Sistema de Matricula en ASP. Net con Oracle",
                    ImagenUrl = "/images/pic-unah.png",
                    Link = "https://unah.edu.hn"
                    },
                  new Proyecto
                    {
                    Titulo = "JetStereo",
                    Descripcion = "Sistema de Cotizacion en React",
                    ImagenUrl = "/images/pic-jetstereo.png",
                    Link = "https://jetstereo.com"
                    },
                };
        }
    }
}
