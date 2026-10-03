using Dapper;
using MySqlConnector;
using Persistencia.Conexiones;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Persistencia.Repositorios
{
    public class RouterRepositorio : IRouterRepositorio
    {
        private readonly ConexionDesarrollador conexion;

        public RouterRepositorio(ConexionDesarrollador conexion)
        {
            this.conexion = conexion;
        }

        public void Agregar(Router router)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                INSERT INTO routers
                (
                    id,
                    nombre,
                    direccion_ip,
                    direccion_mac,
                    encendido,
                    latencia,
                    reglas,
                    paquetes_bloqueados,
                    paquetes_permitidos
                )
                VALUES
                (
                    @Id,
                    @Nombre,
                    @DireccionIp,
                    @DireccionMAC,
                    @Encendido,
                    @Latencia,
                    @Reglas,
                    @PaquetesBloqueados,
                    @PaquetesPermitidos
                );";

            connection.Execute(sql, router);
        }

        public Router ObtenerPorId(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    id AS Id,
                    nombre AS Nombre,
                    direccion_ip AS DireccionIp,
                    direccion_mac AS DireccionMAC,
                    encendido AS Encendido,
                    latencia AS Latencia,
                    reglas AS Reglas,
                    paquetes_bloqueados AS PaquetesBloqueados,
                    paquetes_permitidos AS PaquetesPermitidos
                FROM routers
                WHERE id = @id;";

            return connection.QueryFirst<Router>(sql, new { id });
        }

        public List<Router> Listar()
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    id AS Id,
                    nombre AS Nombre,
                    direccion_ip AS DireccionIp,
                    direccion_mac AS DireccionMAC,
                    encendido AS Encendido,
                    latencia AS Latencia,
                    reglas AS Reglas,
                    paquetes_bloqueados AS PaquetesBloqueados,
                    paquetes_permitidos AS PaquetesPermitidos
                FROM routers;";

            return connection.Query<Router>(sql).ToList();
        }

        public void Eliminar(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                DELETE FROM routers
                WHERE id = @id;";

            connection.Execute(sql, new { id });
        }
    }
}