using Persistencia.Entidades;
using Aplicacion.Interfaces;

namespace Aplicacion.Interfaces
{
    public interface IEstadisticaServicio
    {
        void ConsultarEstadisticas(Simulacion simulacion);
    }
}