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

        public int Agregar(Firewall firewall)
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
                    sqlDispositivo, firewall, transaccion);

                string sqlFirewall = @"
                    INSERT INTO firewalls
                    (
                        dispositivo_id,
                        filtrado_activo,
                        politica_predeterminada,
                        cantidad_reglas,
                        bloquea_trafico_entrante,
                        bloquea_trafico_saliente,
                        tipo
                    )
                    VALUES
                    (
                        @DispositivoId,
                        @FiltradoActivo,
                        @PoliticaPredeterminada,
                        @CantidadReglas,
                        @BloqueaTraficoEntrante,
                        @BloqueaTraficoSaliente,
                        @Tipo
                    );
                ";

                connection.Execute(sqlFirewall, new
                {
                    DispositivoId = dispositivoId,
                    firewall.FiltradoActivo,
                    firewall.PoliticaPredeterminada,
                    firewall.CantidadReglas,
                    firewall.BloqueaTraficoEntrante,
                    firewall.BloqueaTraficoSaliente,
                    firewall.Tipo
                }, transaccion);

                transaccion.Commit();

                firewall.Id = dispositivoId;

                return dispositivoId;
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public Firewall? ObtenerPorId(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    d.id AS Id,
                    d.nombre AS Nombre,
                    d.direccion_ip AS DireccionIp,
                    d.direccion_mac AS DireccionMAC,
                    d.encendido AS Encendido,
                    f.filtrado_activo AS FiltradoActivo,
                    f.politica_predeterminada AS PoliticaPredeterminada,
                    f.cantidad_reglas AS CantidadReglas,
                    f.bloquea_trafico_entrante AS BloqueaTraficoEntrante,
                    f.bloquea_trafico_saliente AS BloqueaTraficoSaliente,
                    f.tipo AS Tipo,
                    d.latencia AS Latencia
                FROM firewalls f
                INNER JOIN dispositivos d ON d.id = f.dispositivo_id
                WHERE d.id = @id;
            ";

            return connection.QueryFirstOrDefault<Firewall>(sql, new { id });
        }

        public List<Firewall> Listar()
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    d.id AS Id,
                    d.nombre AS Nombre,
                    d.direccion_ip AS DireccionIp,
                    d.direccion_mac AS DireccionMAC,
                    d.encendido AS Encendido,
                    f.filtrado_activo AS FiltradoActivo,
                    f.politica_predeterminada AS PoliticaPredeterminada,
                    f.cantidad_reglas AS CantidadReglas,
                    f.bloquea_trafico_entrante AS BloqueaTraficoEntrante,
                    f.bloquea_trafico_saliente AS BloqueaTraficoSaliente,
                    f.tipo AS Tipo,
                    d.latencia AS Latencia
                FROM firewalls f
                INNER JOIN dispositivos d ON d.id = f.dispositivo_id;
            ";

            return connection.Query<Firewall>(sql).ToList();
        }

        public void Eliminar(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();
            connection.Open();

            using MySqlTransaction transaccion = connection.BeginTransaction();

            try
            {
                string sqlFirewall = @"
                    DELETE FROM firewalls
                    WHERE dispositivo_id = @id;
                ";

                connection.Execute(sqlFirewall, new { id }, transaccion);

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
        public void Actualizar(Firewall firewall)
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

            connection.Execute(sqlDispositivo, firewall);

            string sqlFirewall = @"
                UPDATE firewalls
                SET filtrado_activo = @FiltradoActivo,
                    politica_predeterminada = @PoliticaPredeterminada,
                    cantidad_reglas = @CantidadReglas,
                    bloquea_trafico_entrante = @BloqueaTraficoEntrante,
                    bloquea_trafico_saliente = @BloqueaTraficoSaliente,
                    tipo = @Tipo
                WHERE dispositivo_id = @Id;
            ";

            connection.Execute(sqlFirewall, firewall);
        }
    }
}