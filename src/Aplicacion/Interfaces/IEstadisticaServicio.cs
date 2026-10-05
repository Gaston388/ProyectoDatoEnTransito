using Persistencia.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IEstadisticaServicio
    {
        void ConsultarEstadisticas(Simulacion simulacion);
        void MostrarEstadisticasDispositivos();
        void MostrarEstadisticasSimulaciones();
    }
}