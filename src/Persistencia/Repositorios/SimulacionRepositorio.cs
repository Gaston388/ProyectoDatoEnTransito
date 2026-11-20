    using Persistencia.Repositorios.InterfazRepositorios;
using Persistencia.Entidades;
using Persistencia.Conexiones;
using MySqlConnector;
using Dapper;

namespace Persistencia.Repositorios
{
    public class SimulacionRepositorio : ISimulacionRepositorio
    {
        private readonly ConexionDesarrollador conexion;
        public SimulacionRepositorio(ConexionDesarrollador conexion)
        {
            this.conexion = conexion;
        }
        public Simulacion? BuscarPorId(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();
            string sql = @"
            SELECT
            id AS ID,
            fecha_inicio AS FechaInicio,
            fecha_fin AS FechaFin,
            finalizado As Finalizado,
            exitoso AS Exitoso
            FROM simulaciones
            WHERE id = @id;";
            return connection.QueryFirstOrDefault<Simulacion>(sql, new { id });
        }
        public List<Simulacion> Listar()
        {
            using MySqlConnection connection = conexion.CrearConexion();
            string sql = @"
            SELECT 
            id AS ID,
            fecha_inicio AS FechaInicio,
            fecha_fin AS FechaFin,
            finalizado As Finalizado,
            exitoso AS Exitoso
            FROM simulaciones;";
            return connection.Query<Simulacion>(sql).ToList();

        }
        public int Agregar(Simulacion simulacion)
        {
            using MySqlConnection connection = conexion.CrearConexion();
            string sql = @"
            INSERT INTO simulaciones (fecha_inicio, finalizado)
            VALUES (@FechaInicio, @Finalizado);
            SELECT LAST_INSERT_ID();";
            return connection.ExecuteScalar<int>(sql, new
            {
                simulacion.FechaInicio,
                simulacion.Finalizado
            });
        }
        public void Finalizar(int id, bool exitoso)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                UPDATE simulaciones
                SET fecha_fin = @FechaFin,
                    finalizado = true,
                    exitoso = @Exitoso
                    WHERE id = @Id;
                    ";
                connection.Execute(sql, new
                {
                    Id = id,
                    FechaFin = DateTime.Now,
                    Exitoso = exitoso
            });
            
        }
        public void Eliminar(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();
            connection.Open();

            using var transaccion = connection.BeginTransaction();

            try
            {
                string sql = @"
                    DELETE FROM simulacion_paquetes
                    WHERE simulacion_id = @Id;

                    DELETE FROM recorridos
                    WHERE simulacion_id = @Id;

                    DELETE FROM simulaciones
                    WHERE id = @Id;
                ";

                connection.Execute(sql, new { Id = id }, transaccion);

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public void AgregarPaso(int simulacionId, int dispositivoId, int orden, int latencia, int ttlFinal)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                INSERT INTO recorridos
                (simulacion_id, dispositivo_id, orden, latencia, ttl_final)
                VALUES
                (@SimulacionId, @DispositivoId, @Orden, @Latencia, @TtlFinal);
            ";

            connection.Execute(sql, new
            {
                SimulacionId = simulacionId,
                DispositivoId = dispositivoId,
                Orden = orden,
                Latencia = latencia,
                TtlFinal = ttlFinal
            });
        }

        public void EliminarPasos(int simulacionId)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                DELETE FROM recorridos
                WHERE simulacion_id = @SimulacionId;
            ";

            connection.Execute(sql, new { SimulacionId = simulacionId });
        }
        public void Reiniciar(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                UPDATE simulaciones
                SET fecha_fin = NULL,
                    finalizado = false,
                    exitoso = NULL
                WHERE id = @Id;";

            connection.Execute(sql, new { Id = id });
        }
    }
}