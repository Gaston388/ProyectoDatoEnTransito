using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Aplicacion.Servicios
{
    public class SimulacionServicio : ISimulacionServicio
    {
        private readonly ISimulacionRepositorio? _simulacionRepositorio;

        public SimulacionServicio(ISimulacionRepositorio simulacionRepositorio)
        {
            _simulacionRepositorio = simulacionRepositorio;
        }
        public SimulacionServicio()
        {
            _simulacionRepositorio = null ;
        }
        public void AgregarDispositivo(Simulacion simulacion, DispositivoRed dispositivo)
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

        public void AgregarPaquete(Simulacion simulacion, PaqueteRed paquete)
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

        public void ProcesarPaquete(Simulacion simulacion, PaqueteRed paquete)
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

        public Simulacion BuscarPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que 0.");
            }

            return _simulacionRepositorio.BuscarPorId(id);
        }

        public List<Simulacion> Listar()
        {
            return _simulacionRepositorio.Listar();
        }
    }
}