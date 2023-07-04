using Dapper;
using Microsoft.Data.SqlClient;
using SIGCIv2.Models;

namespace SIGCIv2.Servicios
{
    public interface IRepositorioCursosExternos
    {
        Task Actualizar(CursosExternos cursosExternos);
        Task Crear(CursosExternos cursosExternos);
        Task<IEnumerable<CursosExternos>> Obtener();
        Task<CursosExternos> ObtenerId(int IdExterno);
    }

    public class RepositorioCursosExternos: IRepositorioCursosExternos
    {
        private readonly string connectionString;

        public RepositorioCursosExternos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(CursosExternos cursosExternos)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"
            INSERT INTO CUR_EXTERNOS (NOMBRE, INICIO, TERMINO, HORAS, DIAS, INSCRITOS, PROVEEDOR, OBJETIVO, COSTO,
            TIPO, MODALIDAD, INSTRUCTOR, EVALUACION)
            VALUES (@NombreCurso, @Inicio, @Termino, @Horas, @Dias, @Inscritos, @Proveedor, @Objetivo, @Costo,
            @Tipo, @Modalidad, @Instructor, @Evaluacion); SELECT SCOPE_IDENTITY();", cursosExternos);

            cursosExternos.IdCursoExterno = id;
        }

        public async Task<IEnumerable<CursosExternos>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<CursosExternos>(
                @"SELECT IdExterno, NOMBRE, INICIO, TERMINO, HORAS, DIAS, INSCRITOS, PROVEEDOR, OBJETIVO, COSTO,
            TIPO, MODALIDAD, INSTRUCTOR, EVALUACION FROM CUR_EXTERNOS;");
        }

        public async Task Actualizar(CursosExternos cursosExternos)
        {
            using var connection = new SqlConnection(connectionString); ;
            await connection.ExecuteAsync($@"UPDATE CUR_EXTERNOS SET
            NOMBRE = @NombreCurso, INICIO = @Inicio, TERMINO = @Termino, HORAS = @Horas, DIAS = @Dias, INSCRITOS = @Inscritos,
            PROVEEDOR = @Proveedor, OBJETIVO = @Objetivo, COSTO = @Costo,
            TIPO = @Tipo, MODALIDAD = @Modalidad, INSTRUCTOR = @Instructor, EVALUACION = @Evaluacion
            WHERE IdExterno = @IdCursoExterno", cursosExternos);
        }

        public async Task<CursosExternos> ObtenerId(int IdExterno)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<CursosExternos>
                (@"SELECT NOMBRE, INICIO, TERMINO, HORAS, DIAS, INSCRITOS, PROVEEDOR, OBJETIVO, COSTO,
            TIPO, MODALIDAD, INSTRUCTOR, EVALUACION FROM CUR_EXTERNOS WHERE IdExterno = @IdCursoExterno", new { IdExterno });
        }



    }
}
