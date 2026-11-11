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
        //este constructor vacio se conserva para no impedir las operaciones en memoria
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

        public bool ProcesarPaquete(Simulacion simulacion, PaqueteRed paquete)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            if (paquete == null)
            {
                throw new ArgumentNullException(nameof(paquete));
            }

            return simulacion.ProcesarPaquete(paquete);
        }

        public Simulacion BuscarPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "El ID debe ser mayor que 0.");
            }
            if(_simulacionRepositorio == null)
            {
                throw new InvalidOperationException("El repositorio de simulación no está inicializado.");
            }
            return _simulacionRepositorio.BuscarPorId(id);
        }

        public List<Simulacion> Listar()
        {
            if(_simulacionRepositorio == null)
            {
                throw new InvalidOperationException("El repositorio de simulación no está inicializado.");
            }

            return _simulacionRepositorio.Listar();
        }
    }
}