using MySqlConnector;

namespace Persistencia.Conexiones
{
    public class ConexionDesarrollador
    {
        private readonly string connectionString = "Server=localhost;Database=5to_DatosTransito;user ID=5to_agbd;Password=Trigg3rs!;";
        public MySqlConnection CrearConexion()
        {
            return new MySqlConnection(connectionString);
        }
    }
}