using Dapper;
using Microsoft.Data.SqlClient;
using SIGCIv2.Models;

namespace SIGCIv2.Servicios
{
    public interface IRepositorioInstructores
    {
        Task Crear(Instructor instructor);
        Task<bool> NoExiste(int InstructorExp);
        Task<IEnumerable<Instructor>> Obtener();
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
                VALUES (@InstructorExp, @Correo, @Extension, @Horario, @Descanso, @Telefono, @Telefono2, @Calificacion, @Gerencia, @Materias, @Actualizacion, @Calificacion2)
                SELECT SCOPE_IDENTITY();", instructor);

            instructor.IdInstructor = id;
        }

        public async Task<bool> NoExiste(int InstructorExp)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>($"SELECT 1 FROM TRABAJADORES WHERE EXPEDIENTE = @InstructorExp;",
                new {  InstructorExp });
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



    }
}
