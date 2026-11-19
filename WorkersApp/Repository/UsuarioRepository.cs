using Microsoft.Data.SqlClient;
using System.Data;
using WorkersApp.Models;

namespace WorkersApp.Repository
{
    public class UsuarioRepository
    {
        private readonly DbConnection _dbConnection;

        public UsuarioRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<UsuarioRequestDto?> GetClaveByUsername(string username)
        {
            using var connection = _dbConnection.CreateConnection();
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_get_usuario_by_username", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@username", SqlDbType.VarChar, 40).Value = username;

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new UsuarioRequestDto
                {
                    Username = reader.GetString(reader.GetOrdinal("username")),
                    Clave = reader.GetString(reader.GetOrdinal("clave"))
                };
            }

            return null;
        }
    }
}
