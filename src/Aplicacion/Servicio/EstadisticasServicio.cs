using Persistencia.Entidades;
using Aplicacion.Interfaces;

namespace Aplicacion.Servicios
{
    public class EstadisticasSErvicio : IEstadisticaServicio
    {
        public void ConsultarEstadisticas(Simulacion simulacion)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }
            //las estadisticas se implementan cuando tenga cp y repo.
        }
    }
}