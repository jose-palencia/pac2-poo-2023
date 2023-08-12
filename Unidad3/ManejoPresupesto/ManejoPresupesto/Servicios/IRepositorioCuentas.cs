using ManejoPresupesto.Models;

namespace ManejoPresupesto.Servicios
{
    public interface IRepositorioCuentas
    {
        Task Crear(Cuenta modelo);
    }
}