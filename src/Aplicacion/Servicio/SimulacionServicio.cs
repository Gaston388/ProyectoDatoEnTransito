using Persistencia.Entidades;
using Aplicacion.Interfaces;
namespace Aplicacion.Servicios
{
    public class SimulacionServicio : ISimulacionServicio
    {
        public void AgregarDispositivo(Simulacion simulacion,DispositivoRed dispositivo)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }
            if (dispositivo == null)
            {
                throw new ArgumentNullException(nameof(dispositivo));
            }
            simulacion.AgregarDispositivo(dispositivo);
        }
        public void AgregarPaquete(Simulacion simulacion,PaqueteRed paquete)
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
        public void ProcesarPaquete(Simulacion simulacion,PaqueteRed paquete)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            if (paquete == null)
            {
                throw new ArgumentNullException(nameof(paquete));
            }
            simulacion.ProcesarPaquete(paquete);
        }
    }
}
