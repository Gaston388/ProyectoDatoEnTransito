using MySqlConnector;
namespace Persistencia.Conexiones
{
    public class ConexionAdministrador
    {
        private readonly string connectionString = "Server=localhost;Database=datos_transito;user ID=administrador;Password=Admin1234!;";
        public MySqlConnection CrearConexion()
        {
            return new MySqlConnection(connectionString);
        }
    }
}