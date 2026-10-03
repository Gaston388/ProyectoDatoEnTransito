using Dapper;
using MySqlConnector;
using Persistencia.Conexiones;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Persistencia.Repositorios
{
    public class AccesPointRepositorio : IAccessPointRepositorio
    {
        private readonly ConexionDesarrollador conexion;

        public AccesPointRepositorio(ConexionDesarrollador conexion)
        {
            this.conexion = conexion;
        }

        public void Agregar(AccessPoint accessPoint)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                INSERT INTO access_points
                (
                    id,
                    nombre,
                    direccion_ip,
                    direccion_mac,
                    encendido,
                    latencia,
                    ssid,
                    seguridad,
                    canal,
                    maximo_dispositivos
                )
                VALUES
                (
                    @Id,
                    @Nombre,
                    @DireccionIp,
                    @DireccionMAC,
                    @Encendido,
                    @Latencia,
                    @Ssid,
                    @Seguridad,
                    @Canal,
                    @MaximoDispositivos
                );";

            connection.Execute(sql, accessPoint);
        }

        public AccessPoint ObtenerPorId(int id)
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
                    ssid AS Ssid,
                    seguridad AS Seguridad,
                    canal AS Canal,
                    maximo_dispositivos AS MaximoDispositivos
                FROM access_points
                WHERE id = @id;";

            return connection.QueryFirst<AccessPoint>(sql, new { id });
        }

        public List<AccessPoint> Listar()
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
                    ssid AS Ssid,
                    seguridad AS Seguridad,
                    canal AS Canal,
                    maximo_dispositivos AS MaximoDispositivos
                FROM access_points;";

            return connection.Query<AccessPoint>(sql).ToList();
        }

        public void Eliminar(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                DELETE FROM access_points
                WHERE id = @id;";

            connection.Execute(sql, new { id });
        }
    }
}