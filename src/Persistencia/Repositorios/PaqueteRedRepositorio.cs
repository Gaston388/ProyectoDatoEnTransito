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
        public void Agregar(PaqueteRed paqueteRed)
        {
            using MySqlConnection connection = conexion.CrearConexion();
             string sql = @"
                INSERT INTO paquetes
                (
                    ip_origen,
                    ip_destino,
                    mac_origen,
                    mac_destino,
                    tamano,
                    protocolo,
                    datos,
                    hora_creacion,
                    procesado,
                    latencia_acumulada,
                    ttl_inicial
                )
                VALUES
                (
                    @IpOrigen,
                    @IpDestino,
                    @MacOrigen,
                    @MacDestino,
                    @Tamaño,
                    @Protocolo,
                    @Datos,
                    @HoraCreacion,
                    @Procesado,
                    @LatenciaAcumulada,
                    @TTL
                );";
            connection.Execute(sql, paqueteRed);
        }
        public PaqueteRed ObtenerPorId(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();
             string sql = @"
                SELECT
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
                    latencia_acumulada AS LatenciaAcumulada,
                    ttl_inicial AS TTL
                FROM paquetes
                WHERE id = @id;";
            return connection.QueryFirst<PaqueteRed>(sql, new { id });
        }
        public List<PaqueteRed> Listar()
        {
            using MySqlConnection connection = conexion.CrearConexion();
             string sql = @"
                SELECT
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
                    latencia_acumulada AS LatenciaAcumulada,
                    ttl_inicial AS TTL
                FROM paquetes;";
            return connection.Query<PaqueteRed>(sql).ToList();
        }
    }
}