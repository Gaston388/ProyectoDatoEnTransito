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
    }
}
