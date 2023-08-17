using ManejoPresupesto.Models;

namespace ManejoPresupesto.Servicios
{
    public interface IRepositorioCuentas
    {
        Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
        Task Crear(Cuenta modelo);
        Task<Cuenta> ObtenerPorId(int id, int usuarioId);
    }
}