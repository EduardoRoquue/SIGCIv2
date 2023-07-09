using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SIGCIv2.Models;

namespace SIGCIv2.Servicios
{
    public interface IRepositorioTrabajadores
    {
        Task Actualizar(Trabajador trabajador);
        Task Borrar(Trabajador trabajador);
        Task Crear(Trabajador trabajador);
        Task<bool> Existe(int Expediente);
        Task<IEnumerable<Trabajador>> Obtener();
        Task<Trabajador> ObtenerTipo(int Expediente);
    }
    public class RepositorioTrabajadores : IRepositorioTrabajadores
    {
        private readonly string connectionString;
        public RepositorioTrabajadores(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Trabajador trabajador)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"
            INSERT INTO TRABAJADORES (EXPEDIENTE, NOMBRE, CATSTC,GERENCIA, COORDINACION, GENERO, CalidadLaboral, ESTATUS, SECCIONAL, DISCAPACIDAD)
            VALUES (@Expediente, @Nombre, @CATSTC, @Gerencia, @Coordinacion, @Genero, @CalidadLaboral, @Estatus, @Seccional, @Discapacidad);
            SELECT SCOPE_IDENTITY();", trabajador);
            trabajador.IdTrabajador = id;
        }

        public async Task<bool> Existe(int Expediente)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>($"SELECT 1 FROM TRABAJADORES WHERE EXPEDIENTE = @Expediente;",
            new { Expediente });
            return existe == 1;
        }
        
        public async Task<IEnumerable<Trabajador>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Trabajador>(
                //@"SELECT IdTrabajador, EXPEDIENTE, NOMBRE, CATSTC, GERENCIA, COORDINACION, GENERO, CalidadLaboral, ESTATUS, SECCIONAL, DISCAPACIDAD FROM TRABAJADORES;");
                @"sp_CatalogoTrabajadores",
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task Actualizar(Trabajador trabajador)
        {
            using var connection = new SqlConnection(connectionString); ;
            await connection.ExecuteAsync($@"UPDATE TRABAJADORES SET
            NOMBRE = @Nombre, CATSTC = @CATSTC, GERENCIA = @Gerencia, COORDINACION = @Coordinacion, GENERO = @Genero, CalidadLaboral = @CalidadLaboral, ESTATUS = @Estatus, SECCIONAL = @Seccional, DISCAPACIDAD = @Discapacidad
            WHERE EXPEDIENTE = @Expediente", trabajador);
        }

        public async Task Borrar(Trabajador trabajador)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync($@"DELETE FROM INSTRUCTORES WHERE TrabajadorExp = @Expediente;
            DELETE FROM TRABAJADORES WHERE EXPEDIENTE = @Expediente;", trabajador);
        }

        public async Task<Trabajador> ObtenerTipo(int Expediente)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Trabajador>(
                 @"SELECT EXPEDIENTE, NOMBRE, CATSTC,GERENCIA, COORDINACION, GENERO, CalidadLaboral, ESTATUS, SECCIONAL, DISCAPACIDAD
                FROM TRABAJADORES WHERE EXPEDIENTE = @Expediente", new { Expediente });
        }

    }


}
