using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using WorkersApp;
using WorkersApp.Models;

namespace WorkersApp.Repository
{
    public class TrabajadorRepository
    {
        private readonly DbConnection _dbConnection;

        public TrabajadorRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<Trabajador>> GetById(string? dni)
        {
            using var connection = _dbConnection.CreateConnection();
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_find_trabajador_by_dni", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@dni", SqlDbType.VarChar, 8).Value = (object?)dni ?? DBNull.Value;

            using var reader = await command.ExecuteReaderAsync();

            List<Trabajador> trabajadores = new();

            while (await reader.ReadAsync())
            {
                trabajadores.Add(new Trabajador
                {
                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                    Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                    Dni = reader.GetString(reader.GetOrdinal("dni")),
                    Edad = reader.GetInt32(reader.GetOrdinal("edad"))
                });
            }

            return trabajadores;
        }

        public async Task<bool> save(Trabajador trabajador)
        {
            using var connection = _dbConnection.CreateConnection();
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_insert_trabajador", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = trabajador.Nombre;
            command.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = trabajador.Apellido;
            command.Parameters.Add("@dni", SqlDbType.Char, 8).Value = trabajador.Dni;
            command.Parameters.Add("@edad", SqlDbType.Int).Value = trabajador.Edad;

            int filasAgregadas = await command.ExecuteNonQueryAsync();

            return filasAgregadas > 0;
        }


    }
}
