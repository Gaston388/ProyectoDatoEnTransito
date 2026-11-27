using MySqlConnector;
using System.Data;

namespace Persistencia.Repositorios
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory()
        {
            _connectionString = "Server=localhost;Port=3306;Database=5to_DatosTransito;User ID=5to_agbd;Password=Trigg3rs!;SslMode=None;Charset=utf8mb4;";
        }

        public IDbConnection CrearConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}