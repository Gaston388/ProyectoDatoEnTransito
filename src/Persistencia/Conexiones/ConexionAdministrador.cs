using MySqlConnector;
namespace Persistencia.Conexiones
{
    public class ConexionAdministrador
    {
        private readonly string connectionString = "Server=localhost;Database=5to_DatosTransito;user ID=Administrador;Password=Admin1234!;";
        public MySqlConnection CrearConexion()
        {
            return new MySqlConnection(connectionString);
        }
    }
}