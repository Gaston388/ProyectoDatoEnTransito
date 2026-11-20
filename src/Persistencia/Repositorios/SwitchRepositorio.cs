using Dapper;
using MySqlConnector;
using Persistencia.Conexiones;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Persistencia.Repositorios
{
    public class SwitchRepositorio : ISwitchRepositorio
    {
        private readonly ConexionDesarrollador conexion;

        public SwitchRepositorio(ConexionDesarrollador conexion)
        {
            this.conexion = conexion;
        }

        public int Agregar(Switch switchRed)
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
                    sqlDispositivo, switchRed, transaccion);

                string sqlSwitch = @"
                    INSERT INTO switches
                    (dispositivo_id, cantidad_puertos, puertos_ocupados, vlan_activa, cantidad_vlan)
                    VALUES
                    (@DispositivoId, @CantidadPuertos, @PuertosOcupados, @VlanActiva, @CantidadVlan);
                ";

                connection.Execute(sqlSwitch, new
                {
                    DispositivoId = dispositivoId,
                    switchRed.CantidadPuertos,
                    switchRed.PuertosOcupados,
                    switchRed.VlanActiva,
                    switchRed.CantidadVlan
                }, transaccion);

                transaccion.Commit();

                switchRed.Id = dispositivoId;
                return dispositivoId;
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public Switch? ObtenerPorId(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    d.id AS Id,
                    d.nombre AS Nombre,
                    d.direccion_ip AS DireccionIp,
                    d.direccion_mac AS DireccionMAC,
                    d.encendido AS Encendido,
                    s.cantidad_puertos AS CantidadPuertos,
                    s.puertos_ocupados AS PuertosOcupados,
                    s.vlan_activa AS VlanActiva,
                    s.cantidad_vlan AS CantidadVlan,
                    d.latencia AS Latencia
                FROM switches s
                INNER JOIN dispositivos d ON d.id = s.dispositivo_id
                WHERE d.id = @id;
            ";

            return connection.QueryFirstOrDefault<Switch>(sql, new { id });
        }

        public List<Switch> Listar()
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    d.id AS Id,
                    d.nombre AS Nombre,
                    d.direccion_ip AS DireccionIp,
                    d.direccion_mac AS DireccionMAC,
                    d.encendido AS Encendido,
                    s.cantidad_puertos AS CantidadPuertos,
                    s.puertos_ocupados AS PuertosOcupados,
                    s.vlan_activa AS VlanActiva,
                    s.cantidad_vlan AS CantidadVlan,
                    d.latencia AS Latencia
                FROM switches s
                INNER JOIN dispositivos d ON d.id = s.dispositivo_id;
            ";

            return connection.Query<Switch>(sql).ToList();
        }

        public void Eliminar(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();
            connection.Open();

            using MySqlTransaction transaccion = connection.BeginTransaction();

            try
            {
                string sqlSwitch = @"
                    DELETE FROM switches
                    WHERE dispositivo_id = @id;
                ";

                connection.Execute(sqlSwitch, new { id }, transaccion);

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
        public void Actualizar(Switch switchRed)
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

            connection.Execute(sqlDispositivo, switchRed);

            string sqlSwitch = @"
                UPDATE switches
                SET cantidad_puertos = @CantidadPuertos,
                    puertos_ocupados = @PuertosOcupados,
                    vlan_activa = @VlanActiva,
                    cantidad_vlan = @CantidadVlan
                WHERE dispositivo_id = @Id;
            ";

            connection.Execute(sqlSwitch, switchRed);
        }
    }
}