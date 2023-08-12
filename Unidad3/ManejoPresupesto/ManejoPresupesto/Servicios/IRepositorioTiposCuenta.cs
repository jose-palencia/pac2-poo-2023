using ManejoPresupesto.Models;

namespace ManejoPresupesto.Servicios
{
    public interface IRepositorioTiposCuenta
    {
        Task Borrar(int id);
        Task Crear(TipoCuenta modelo);
        Task Editar(TipoCuenta modelo);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuenta> modelo);
    }
}