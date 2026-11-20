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
    }
}