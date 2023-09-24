using Dapper;
using Microsoft.Data.SqlClient;
using NPOI.OpenXmlFormats.Shared;
using SIGCIv2.Models;

namespace SIGCIv2.Servicios
{
    public interface IRepositorioCursosInternos
    {
        Task Actualizar(CursosInternos cursosInternos);
        Task Borrar(CursosInternos cursosInternos);
        Task Crear(CursosInternos interno);
        Task<bool> Existe(string Nombre);
        Task<IEnumerable<CursosInternos>> Obtener();
        Task<CursosInternos> ObtenerTipo(int IdInterno);
    }
    public class RepositorioCursosInternos: IRepositorioCursosInternos
    {
        private readonly string connectionString;

        public RepositorioCursosInternos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<CursosInternos>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<CursosInternos>(
                @"SELECT IdInterno, NOMBRE, CLAVE, INICIO, TERMINO, TIPO, DIRIGIDO
                    FROM CUR_INTERNOS");
        }

        public async Task Crear(CursosInternos cursosInternos)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                $@"INSERT INTO CUR_INTERNOS (NOMBRE, CLAVE, INICIO, TERMINO, TIPO, DIRIGIDO)
                VALUES (@Nombre, @Clave, @Inicio, @Termino, @Tipo, @Dirigido);
                SELECT SCOPE_IDENTITY();", cursosInternos);
            cursosInternos.IdInterno = id;
        }

        public async Task Actualizar(CursosInternos cursosInternos)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync($@"UPDATE CUR_INTERNOS SET
            NOMBRE = @Nombre, CLAVE = @Clave, INICIO = @Inicio, TERMINO = @Termino, TIPO = @Tipo, DIRIGIDO = @Dirigido
            WHERE CLAVE = @Clave", cursosInternos);
        }

        public async Task Borrar(CursosInternos cursosInternos)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM CUR_INTERNOS WHERE CLAVE = @Clave;", cursosInternos);
        }

        public async Task<bool> Existe(string Clave)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>($"SELECT 1 FROM CUR_INTERNOS WHERE CLAVE = @Clave;",
                   new { Clave });
            return existe == 1;
        }
         
        public async Task<CursosInternos> ObtenerTipo(int IdInterno)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<CursosInternos>(
                 @"SELECT IdInterno, NOMBRE, CLAVE, INICIO, TERMINO, TIPO, DIRIGIDO
                FROM CUR_INTERNOS WHERE IdInterno = @IdInterno", new { IdInterno });
        }
    }
}
