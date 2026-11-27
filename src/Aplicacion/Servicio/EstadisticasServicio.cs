using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Aplicacion.Servicios
{
    public class EstadisticaServicio : IEstadisticaServicio
    {
        private readonly IEstadisticaRepositorio _estadisticaRepositorio;

        public EstadisticaServicio(IEstadisticaRepositorio estadisticaRepositorio)
        {
            _estadisticaRepositorio = estadisticaRepositorio;
        }

        // Devuelve la información de una simulación recibida.
        public Simulacion ConsultarEstadisticas(Simulacion simulacion)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            return simulacion;
        }

        // Obtiene las estadísticas de los dispositivos desde la base de datos.
        public List<dynamic> ObtenerEstadisticasDispositivos()
        {
            return _estadisticaRepositorio.ObtenerEstadisticasDispositivos();
        }

        // Obtiene las estadísticas generales de las simulaciones.
        public List<dynamic> ObtenerEstadisticasSimulaciones()
        {
            return _estadisticaRepositorio.ObtenerEstadisticasSimulaciones();
        }
    }
}