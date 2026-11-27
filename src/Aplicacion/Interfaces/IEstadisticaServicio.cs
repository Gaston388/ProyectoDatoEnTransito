using Persistencia.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IEstadisticaServicio
    {
        Simulacion ConsultarEstadisticas(Simulacion simulacion);

        List<dynamic> ObtenerEstadisticasDispositivos();

        List<dynamic> ObtenerEstadisticasSimulaciones();
    }
}