using MySqlConnector;
using Persistencia.Entidades;
using Dapper;
using Persistencia.Repositorios.InterfazRepositorios;
using Persistencia.Conexiones;

namespace Persistencia.Repositorios
{
    public class PaqueteRedRepositorio : IPaqueteRedRepositorio
    {
        private readonly ConexionDesarrollador conexion;
        public PaqueteRedRepositorio(ConexionDesarrollador conexion)
        {
            this.conexion = conexion;
        }
        public int Agregar(PaqueteRed paqueteRed, int simulacionId)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            connection.Open();

            using MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string sqlPaquete = @"
                    INSERT INTO paquetes
                    (
                        ip_origen, ip_destino, mac_origen, mac_destino,
                        tamano, protocolo, datos, hora_creacion,
                        procesado, latencia_acumulada, ttl_inicial
                    )
                    VALUES
                    (
                        @IpOrigen, @IpDestino, @MacOrigen, @MacDestino,
                        @Tamaño, @Protocolo, @Datos, @HoraCreacion,
                        @Procesado, @LatenciaAcumulada, @Ttl
                    );

                    SELECT LAST_INSERT_ID();
                ";

                int paqueteId = connection.ExecuteScalar<int>(
                    sqlPaquete, paqueteRed, transaction);

                string sqlRelacion = @"
                    INSERT INTO simulacion_paquetes
                    (simulacion_id, paquete_id)
                    VALUES
                    (@SimulacionId, @PaqueteId);
                ";

                connection.Execute(sqlRelacion, new
                {
                    SimulacionId = simulacionId,
                    PaqueteId = paqueteId
                }, transaction);

                transaction.Commit();

                return paqueteId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public PaqueteRed? ObtenerPorId(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    ttl_inicial AS Ttl,
                    ttl_final AS TtlFinal,
                    id AS Id,
                    ip_origen AS IpOrigen,
                    ip_destino AS IpDestino,
                    mac_origen AS MacOrigen,
                    mac_destino AS MacDestino,
                    tamano AS Tamaño,
                    protocolo AS Protocolo,
                    datos AS Datos,
                    hora_creacion AS HoraCreacion,
                    procesado AS Procesado,
                    latencia_acumulada AS LatenciaAcumulada
                FROM paquetes
                WHERE id = @id;
            ";

            return connection.QueryFirstOrDefault<PaqueteRed>(sql, new { id });
        }        
        public List<PaqueteRed> Listar()
        {
            using MySqlConnection connection = conexion.CrearConexion();
            string sql = @"
                SELECT
                    ttl_inicial AS Ttl,
                    ttl_final AS TtlFinal,
                    id AS Id,
                    ip_origen AS IpOrigen,
                    ip_destino AS IpDestino,
                    mac_origen AS MacOrigen,
                    mac_destino AS MacDestino,
                    tamano AS Tamaño,
                    protocolo AS Protocolo,
                    datos AS Datos,
                    hora_creacion AS HoraCreacion,
                    procesado AS Procesado,
                    latencia_acumulada AS LatenciaAcumulada
                FROM paquetes;
            ";

            return connection.Query<PaqueteRed>(sql).ToList();
        }
        public void ActualizarResultado(PaqueteRed paquete)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                UPDATE paquetes
                SET procesado = @Procesado,
                    latencia_acumulada = @LatenciaAcumulada,
                    ttl_final = @TtlFinal
                WHERE id = @Id;
            ";

            connection.Execute(sql, new
            {
                paquete.Procesado,
                paquete.LatenciaAcumulada,
                paquete.TtlFinal,
                paquete.Id
            });
        }

        public PaqueteRed? ObtenerPorSimulacionId(int simulacionId)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    p.ttl_inicial AS Ttl,
                    p.ttl_final AS TtlFinal,
                    p.id AS Id,
                    p.ip_origen AS IpOrigen,
                    p.ip_destino AS IpDestino,
                    p.mac_origen AS MacOrigen,
                    p.mac_destino AS MacDestino,
                    p.tamano AS Tamaño,
                    p.protocolo AS Protocolo,
                    p.datos AS Datos,
                    p.hora_creacion AS HoraCreacion,
                    p.procesado AS Procesado,
                    p.latencia_acumulada AS LatenciaAcumulada
                FROM paquetes p
                INNER JOIN simulacion_paquetes sp
                    ON p.id = sp.paquete_id
                WHERE sp.simulacion_id = @SimulacionId;
            ";

            return connection.QueryFirstOrDefault<PaqueteRed>(
                sql, new { SimulacionId = simulacionId });
        }

        public void ActualizarDatos(PaqueteRed paquete)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                UPDATE paquetes
                SET ip_origen = @IpOrigen,
                    ip_destino = @IpDestino,
                    mac_origen = @MacOrigen,
                    mac_destino = @MacDestino,
                    tamano = @Tamaño,
                    protocolo = @Protocolo,
                    datos = @Datos,
                    hora_creacion = @HoraCreacion,
                    ttl_inicial = @Ttl
                WHERE id = @Id;
            ";

            connection.Execute(sql, paquete);
        }

    }
}
