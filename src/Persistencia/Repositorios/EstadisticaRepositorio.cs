using Dapper;
using System.Data;
using Persistencia.Conexiones;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Persistencia.Repositorios
{
    public class EstadisticaRepositorio : IEstadisticaRepositorio
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public EstadisticaRepositorio(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<dynamic> ObtenerEstadisticasDispositivos()
        {
            using IDbConnection connection = _connectionFactory.CrearConnection();

            // Llama al Stored Procedure
            return connection.Query("CALL sp_estadisticas_dispositivos()").ToList();
        }

        public List<dynamic> ObtenerEstadisticasSimulaciones()
        {
            using IDbConnection connection = _connectionFactory.CrearConnection();

            // Llama al Stored Procedure
            return connection.Query("CALL sp_estadisticas_simulaciones()").ToList();
        }
    }
}