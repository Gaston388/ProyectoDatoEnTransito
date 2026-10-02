using MySqlConnector;

namespace Persistencia.Conexiones
{
    public class ConexionDesarrollador
    {
        private readonly string connectionString = "Server=localhost;Database=datos_transito;user ID=desarrollador;Password=Desarrollador1234!;";
        public MySqlConnection CrearConexion()
        {
            return new MySqlConnection(connectionString);
        }
    }
}