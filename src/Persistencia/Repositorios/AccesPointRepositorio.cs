using Dapper;
using MySqlConnector;
using Persistencia.Conexiones;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Persistencia.Repositorios
{
    public class AccessPointRepositorio : IAccessPointRepositorio
    {
        private readonly ConexionDesarrollador conexion;

        public AccessPointRepositorio(ConexionDesarrollador conexion)
        {
            this.conexion = conexion;
        }

        public int Agregar(AccessPoint accessPoint)
        {
            using MySqlConnection connection = conexion.CrearConexion();
            connection.Open();

            using MySqlTransaction transaccion = connection.BeginTransaction();

            try
            {
                string sqlDispositivo = @"
                    INSERT INTO dispositivos
                    (nombre, direccion_ip, direccion_mac, encendido, latencia)
                    VALUES
                    (@Nombre, @DireccionIp, @DireccionMAC, @Encendido, @Latencia);

                    SELECT LAST_INSERT_ID();
                ";

                int dispositivoId = connection.ExecuteScalar<int>(
                    sqlDispositivo, accessPoint, transaccion);

                string sqlAccessPoint = @"
                    INSERT INTO access_points
                    (
                        dispositivo_id,
                        ssid,
                        seguridad,
                        canal,
                        maximo_dispositivos
                    )
                    VALUES
                    (
                        @DispositivoId,
                        @Ssid,
                        @Seguridad,
                        @Canal,
                        @MaximoDispositivos
                    );
                ";

                connection.Execute(sqlAccessPoint, new
                {
                    DispositivoId = dispositivoId,
                    accessPoint.Ssid,
                    accessPoint.Seguridad,
                    accessPoint.Canal,
                    accessPoint.MaximoDispositivos
                }, transaccion);

                transaccion.Commit();

                accessPoint.Id = dispositivoId;

                return dispositivoId;
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public AccessPoint? ObtenerPorId(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    d.id AS Id,
                    d.nombre AS Nombre,
                    d.direccion_ip AS DireccionIp,
                    d.direccion_mac AS DireccionMAC,
                    d.encendido AS Encendido,
                    a.ssid AS Ssid,
                    a.seguridad AS Seguridad,
                    a.canal AS Canal,
                    a.maximo_dispositivos AS MaximoDispositivos,
                    d.latencia AS Latencia
                FROM access_points a
                INNER JOIN dispositivos d ON d.id = a.dispositivo_id
                WHERE d.id = @id;
            ";

            return connection.QueryFirstOrDefault<AccessPoint>(sql, new { id });
        }

        public List<AccessPoint> Listar()
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    d.id AS Id,
                    d.nombre AS Nombre,
                    d.direccion_ip AS DireccionIp,
                    d.direccion_mac AS DireccionMAC,
                    d.encendido AS Encendido,
                    a.ssid AS Ssid,
                    a.seguridad AS Seguridad,
                    a.canal AS Canal,
                    a.maximo_dispositivos AS MaximoDispositivos,
                    d.latencia AS Latencia
                FROM access_points a
                INNER JOIN dispositivos d ON d.id = a.dispositivo_id;
            ";

            return connection.Query<AccessPoint>(sql).ToList();
        }

        public void Eliminar(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();
            connection.Open();

            using MySqlTransaction transaccion = connection.BeginTransaction();

            try
            {
                string sqlAccessPoint = @"
                    DELETE FROM access_points
                    WHERE dispositivo_id = @id;
                ";

                connection.Execute(sqlAccessPoint, new { id }, transaccion);

                string sqlDispositivo = @"
                    DELETE FROM dispositivos
                    WHERE id = @id;
                ";

                connection.Execute(sqlDispositivo, new { id }, transaccion);

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }
        public void Actualizar(AccessPoint accessPoint)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sqlDispositivo = @"
                UPDATE dispositivos
                SET nombre = @Nombre,
                    direccion_ip = @DireccionIp,
                    direccion_mac = @DireccionMAC,
                    encendido = @Encendido,
                    latencia = @Latencia
                WHERE id = @Id;
            ";

            connection.Execute(sqlDispositivo, accessPoint);

            string sqlAccessPoint = @"
                UPDATE access_points
                SET ssid = @Ssid,
                    seguridad = @Seguridad,
                    canal = @Canal,
                    maximo_dispositivos = @MaximoDispositivos
                WHERE dispositivo_id = @Id;
            ";

            connection.Execute(sqlAccessPoint, accessPoint);
        }
    }
}