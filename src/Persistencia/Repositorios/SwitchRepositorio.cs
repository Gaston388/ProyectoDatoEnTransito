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

        public void Agregar(Switch switchRed)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                INSERT INTO switches
                (
                    id,
                    nombre,
                    direccion_ip,
                    direccion_mac,
                    encendido,
                    latencia,
                    cantidad_puertos,
                    puertos_ocupados,
                    vlan_activa,
                    cantidad_vlan
                )
                VALUES
                (
                    @Id,
                    @Nombre,
                    @DireccionIp,
                    @DireccionMAC,
                    @Encendido,
                    @Latencia,
                    @CantidadPuertos,
                    @PuertosOcupados,
                    @VlanActiva,
                    @CantidadVlan
                );";

            connection.Execute(sql, switchRed);
        }

        public Switch ObtenerPorId(int id)
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
                    cantidad_puertos AS CantidadPuertos,
                    puertos_ocupados AS PuertosOcupados,
                    vlan_activa AS VlanActiva,
                    cantidad_vlan AS CantidadVlan
                FROM switches
                WHERE id = @id;";

            return connection.QueryFirst<Switch>(sql, new { id });
        }

        public List<Switch> Listar()
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
                    cantidad_puertos AS CantidadPuertos,
                    puertos_ocupados AS PuertosOcupados,
                    vlan_activa AS VlanActiva,
                    cantidad_vlan AS CantidadVlan
                FROM switches;";

            return connection.Query<Switch>(sql).ToList();
        }

        public void Eliminar(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                DELETE FROM switches
                WHERE id = @id;";

            connection.Execute(sql, new { id });
        }
    }
}