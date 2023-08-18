using Dapper;
using ManejoPresupesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupesto.Servicios
{
    public class RepositorioCuentas: IRepositorioCuentas
    {
        private readonly string connectionString;

        public RepositorioCuentas(IConfiguration configuration)
        {
            connectionString = configuration
                .GetConnectionString("DefaultConnection").ToString();
        }

        public async Task Crear(Cuenta modelo) 
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection
                .QuerySingleAsync<int>(
                @"INSERT INTO Cuentas (Nombre, TipoCuentaId, Balance, Descripcion)
                VALUES (@Nombre, @TipoCuentaId, @Balance, @Descripcion);
                SELECT SCOPE_IDENTITY();", modelo);

            modelo.Id = id;
        }

        public async Task<Cuenta> ObtenerPorId(int id, int usuarioId) 
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync
                <Cuenta>
                (@"
                    SELECT 
	                    cu.Id,
	                    cu.Nombre,
	                    cu.Balance,
	                    cu.Descripcion,
	                    cu.TipoCuentaId
                    FROM Cuentas cu
                    INNER JOIN TipoCuenta tc 
                    ON tc.Id = cu.TipoCuentaId
                    WHERE tc.UsuarioId = @UsuarioId AND cu.Id = @Id;
                    ", new { id, usuarioId });
        }

        public async Task Actualizar(CuentaCreacionViewModel modelo) 
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(
                @"
                UPDATE Cuentas
                SET Nombre = @Nombre,
	                Balance = @Balance,
	                Descripcion = @Descripcion,
	                TipoCuentaId = @TipoCuentaId
                WHERE Id = @Id;
                ", modelo);
        }

        public async Task<IEnumerable<Cuenta>> Buscar(int usuarioId) 
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Cuenta>
                (@"SELECT 
	                cue.Id,
	                cue.Nombre,
	                cue.Balance,
	                tcue.Nombre AS TipoCuenta
                FROM Cuentas AS cue
                 INNER JOIN TipoCuenta AS tcue
                 ON cue.TipoCuentaId = tcue.Id
                WHERE tcue.UsuarioId = @UsuarioId
                ORDER BY tcue.Orden;",
                new { usuarioId });
        }
    }
}
