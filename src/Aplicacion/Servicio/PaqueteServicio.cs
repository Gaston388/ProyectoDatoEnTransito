using Persistencia.Entidades;
using Aplicacion.Interfaces;

namespace Aplicacion.Servicio
{
    public class PaqueteServicio : IPaqueteServicio
    {
        public void CrearPaquete(Simulacion simulacion, PaqueteRed paquete)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }
            if (paquete == null)
            {
                throw new ArgumentNullException(nameof(paquete));
            }
            simulacion.AgregarPaquete(paquete);
        }
    }
}