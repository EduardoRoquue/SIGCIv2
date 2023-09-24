using Dapper;
using Microsoft.Data.SqlClient;
using SIGCIv2.Models;

namespace SIGCIv2.Servicios
{
    public interface IRepositorioInstructores
    {
        Task Actualizar(Instructor instructor);
        Task Borrar(Instructor instructor);
        Task Crear(Instructor instructor);
        Task<bool> NoExiste(int Expediente);
        Task<IEnumerable<Instructor>> Obtener();
        Task<Instructor> ObtenerTipo(int Expediente);
    }
    public class RepositorioInstructores: IRepositorioInstructores
    {
        private readonly string connectionString;
        
        public RepositorioInstructores(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Instructor instructor)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"
                INSERT INTO INSTRUCTORES (TrabajadorExp, CORREO, EXT, HORARIO, DESCANSO, Tel1, Tel2, CALIF, GERENCIA, MATERIAS, ACTUALIZACION, CALIF2)
                VALUES (@Expediente, @Correo, @Extension, @Horario, @Descanso, @Tel1, @Tel2, @Calif, @Gerencia, @Materias, @Actualizacion, @Calif2)
                SELECT SCOPE_IDENTITY();", instructor);

            instructor.IdInstructor = id;
        }

        public async Task<bool> NoExiste(int Expediente)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>($"SELECT 1 FROM TRABAJADORES WHERE EXPEDIENTE = @Expediente;",
                new {  Expediente });
            return existe == 0;
        }

        public async Task<IEnumerable<Instructor>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Instructor>(
                @"SELECT IdInstructor,EXPEDIENTE, NOMBRE, CORREO, EXT, HORARIO, DESCANSO, Tel1, Tel2, CALIF, INSTRUCTORES.GERENCIA, MATERIAS, ACTUALIZACION, CALIF2  FROM INSTRUCTORES
	            INNER JOIN TRABAJADORES
	            ON TRABAJADORES.EXPEDIENTE = INSTRUCTORES.TrabajadorExp;");
                //@"sp_CatalogoInstructores",
                //commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Actualizar(Instructor instructor)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE INSTRUCTORES SET
                CORREO = @Correo, EXT = @Extension, HORARIO = @Horario, DESCANSO = @Descanso, Tel1 = @Tel1, Tel2 = @Tel2, CALIF = @CALIF, GERENCIA = @Gerencia,
                MATERIAS = @Materias, ACTUALIZACION = @Actualizacion, Calif2 = @Calif2 WHERE TrabajadorExp = @Expediente", instructor);
        }

        public async Task Borrar(Instructor instructor)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM INSTRUCTORES WHERE TrabajadorExp = @Expediente;", instructor);
        }

        public async Task<Instructor> ObtenerTipo(int Expediente)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Instructor>(
                @"SELECT IdInstructor, EXPEDIENTE, NOMBRE, INSTRUCTORES.CORREO, INSTRUCTORES.EXT, HORARIO, DESCANSO, Tel1, Tel2, CALIF, INSTRUCTORES.GERENCIA, MATERIAS, ACTUALIZACION, CALIF2
                FROM INSTRUCTORES  
                INNER JOIN TRABAJADORES
                ON TRABAJADORES.EXPEDIENTE = INSTRUCTORES.TrabajadorExp
                WHERE EXPEDIENTE = @Expediente", new { Expediente });
        }
    }
}
