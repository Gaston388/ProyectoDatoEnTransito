using Dapper;
using MySqlConnector;
using Persistencia.Conexiones;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Persistencia.Repositorios
{
    public class FirewallRepositorio : IFirewallRepositorio
    {
        private readonly ConexionDesarrollador conexion;

        public FirewallRepositorio(ConexionDesarrollador conexion)
        {
            this.conexion = conexion;
        }

        public void Agregar(Firewall firewall)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                INSERT INTO firewalls
                (
                    id,
                    nombre,
                    direccion_ip,
                    direccion_mac,
                    encendido,
                    latencia,
                    filtrado_activo,
                    politica_predeterminada,
                    cantidad_reglas,
                    bloquea_trafico_entrante,
                    bloquea_trafico_saliente,
                    tipo
                )
                VALUES
                (
                    @Id,
                    @Nombre,
                    @DireccionIp,
                    @DireccionMAC,
                    @Encendido,
                    @Latencia,
                    @FiltradoActivo,
                    @PoliticaPredeterminada,
                    @CantidadReglas,
                    @BloqueaTraficoEntrante,
                    @BloqueaTraficoSaliente,
                    @Tipo
                );";

            connection.Execute(sql, firewall);
        }

        public Firewall ObtenerPorId(int id)
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
                    filtrado_activo AS FiltradoActivo,
                    politica_predeterminada AS PoliticaPredeterminada,
                    cantidad_reglas AS CantidadReglas,
                    bloquea_trafico_entrante AS BloqueaTraficoEntrante,
                    bloquea_trafico_saliente AS BloqueaTraficoSaliente,
                    tipo AS Tipo
                FROM firewalls
                WHERE id = @id;";

            return connection.QueryFirst<Firewall>(sql, new { id });
        }

        public List<Firewall> Listar()
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
                    filtrado_activo AS FiltradoActivo,
                    politica_predeterminada AS PoliticaPredeterminada,
                    cantidad_reglas AS CantidadReglas,
                    bloquea_trafico_entrante AS BloqueaTraficoEntrante,
                    bloquea_trafico_saliente AS BloqueaTraficoSaliente,
                    tipo AS Tipo
                FROM firewalls;";

            return connection.Query<Firewall>(sql).ToList();
        }

        public void Eliminar(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                DELETE FROM firewalls
                WHERE id = @id;";

            connection.Execute(sql, new { id });
        }
    }
}