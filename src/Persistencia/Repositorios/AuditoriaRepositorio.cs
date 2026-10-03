using Dapper;
using MySqlConnector;
using Persistencia.Conexiones;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Persistencia.Repositorios
{
    public class AuditoriaRepositorio : IAuditoriaRepositorios
    {
        private readonly ConexionDesarrollador conexion;

        public AuditoriaRepositorio(ConexionDesarrollador conexion)
        {
            this.conexion = conexion;
        }

        public List<Auditoria> Listar()
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    id AS Id,
                    accion AS Accion,
                    descripcion AS Descripcion,
                    fecha AS Fecha
                FROM auditorias;";

            return connection.Query<Auditoria>(sql).ToList();
        }
    }
}