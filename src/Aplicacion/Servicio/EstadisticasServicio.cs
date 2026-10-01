using Persistencia.Entidades;
using Aplicacion.Interfaces;

namespace Aplicacion.Servicio
{
    public class EstadisticasSErvicio
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