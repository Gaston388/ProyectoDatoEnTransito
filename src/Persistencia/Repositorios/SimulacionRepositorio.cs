    using Persistencia.Repositorios.InterfazRepositorios;
using Persistencia.Entidades;
using Persistencia.Conexiones;
using MySqlConnector;
using Dapper;

namespace Persistencia.Repositorios
{
    public class SimulacionRepositorio : ISimulacionRepositorio
    {
        private readonly ConexionDesarrollador conexion;
        public SimulacionRepositorio(ConexionDesarrollador conexion)
        {
            this.conexion = conexion;
        }
        public Simulacion BuscarPorId(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();
            string sql = @"
            SELECT
            id AS ID
            FROM simulaciones
            WHERE id = @id;";
            return connection.QueryFirst<Simulacion>(sql, new { id });
        }
        public List<Simulacion> Listar()
        {
            using MySqlConnection connection = conexion.CrearConexion();
            string sql = @"
            SELECT 
            id AS ID
            FROM simulaciones;";
            return connection.Query<Simulacion>(sql).ToList();

        }
    }
}