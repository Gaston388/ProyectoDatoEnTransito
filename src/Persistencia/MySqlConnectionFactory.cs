using MySqlConnector;
using System.Data;

namespace Persistencia.Repositorios
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory()
        {
            _connectionString = "Server=localhost;Port=3306;Database=datos_transito;User ID=desarrollador;Password=Desarrollador1234!;SslMode=None;Charset=utf8mb4;";
        }

        public IDbConnection CrearConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}