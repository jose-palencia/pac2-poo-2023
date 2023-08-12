using Dapper;
using ManejoPresupesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupesto.Servicios
{
    public class RepositorioTiposCuenta : IRepositorioTiposCuenta
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<RepositorioTiposCuenta> logger;
        private readonly string connectionString;

        public RepositorioTiposCuenta(IConfiguration configuration,
            ILogger<RepositorioTiposCuenta> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId) 
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>
                (@"
                SELECT 
	                Id,
	                Nombre,
	                Orden
                FROM TipoCuenta 
                WHERE Id = @id AND UsuarioId = @usuarioId;
                ", new { id, usuarioId });
        }

        public async Task Editar(TipoCuenta modelo) 
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync
                (@"
                UPDATE TipoCuenta
                    SET Nombre = @nombre
                WHERE Id = @id;
                ", modelo);
        }

        public async Task Ordenar(IEnumerable<TipoCuenta> modelo)
        {
            var query = "UPDATE TipoCuenta SET Orden = @Orden WHERE Id = @id;";

            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(query, modelo);
        }

        public async Task Crear(TipoCuenta modelo) 
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                "TiposCuentas_Insertar", 
                new { usuarioId = modelo.UsuarioId, nombre = modelo.Nombre },
                commandType: System.Data.CommandType.StoredProcedure);
            modelo.Id = id;
            logger.LogInformation($"Tipo de cuenta creado: Id: {modelo.Id}, Nombre: {modelo.Nombre}");
        }

        public async Task<bool> Existe(string nombre, int usuarioId) 
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"
                    SELECT
	                    1
                    FROM TipoCuenta
                    WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId;
                ", new {nombre, usuarioId});
            return existe == 1;
        }

        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId) 
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<TipoCuenta>(
                @"
                    SELECT 
	                    Id,
	                    Nombre,
	                    Orden
                    FROM TipoCuenta
                    WHERE UsuarioId = @UsuarioId
                    Order by Orden;
                 ", new { usuarioId });

        }

        public async Task Borrar(int id) 
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(
                "DELETE FROM TipoCuenta WHERE Id = @id;", 
                new { id });

        }

        
    }
}
