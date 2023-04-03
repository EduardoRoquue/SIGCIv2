using Dapper;
using Microsoft.Data.SqlClient;
using SIGCIv2.Models;

namespace SIGCIv2.Servicios
{
    public interface IRepositorioCursosExternos
    {
        Task Crear(CursosExternos cursosExternos);
        Task<IEnumerable<CursosExternos>> Obtener();
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

    }
}
