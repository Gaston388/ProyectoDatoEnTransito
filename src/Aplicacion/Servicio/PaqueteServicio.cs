using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Aplicacion.Servicios
{
    public class PaqueteServicio : IPaqueteServicio
    {
        private readonly IPaqueteRedRepositorio _paqueteRepositorio;
        private readonly ISimulacionRepositorio _simulacionRepositorio;

        public PaqueteServicio(IPaqueteRedRepositorio paqueteRepositorio, ISimulacionRepositorio simulacionRepositorio)
        {
            _paqueteRepositorio = paqueteRepositorio
            ?? throw new ArgumentNullException(nameof(paqueteRepositorio));
            _simulacionRepositorio = simulacionRepositorio
            ?? throw new ArgumentNullException(nameof(simulacionRepositorio));
        }

        public void CrearPaquete(Simulacion simulacion, PaqueteRed paquete)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion), "La simulación no puede ser nula.");
            }

            if (paquete == null)
            {
                throw new ArgumentNullException(nameof(paquete), "El paquete no puede ser nulo.");
            }
            if (simulacion.Id <= 0)
            {
                throw new ArgumentException("La simulación debe tener un ID válido.", nameof(simulacion));
            }

            Simulacion? simulacionExistente = _simulacionRepositorio.BuscarPorId(simulacion.Id);
            if (simulacionExistente == null)
            {
                throw new InvalidOperationException("No existe esa simulacion");
            }
            if (simulacionExistente.Finalizado)
            {
                throw new InvalidOperationException("No se puede agregar un paquete a una simulación finalizada.");
            }
            int paqueteId = _paqueteRepositorio.Agregar(paquete, simulacion.Id);   
            paquete.Id = paqueteId;
            simulacion.AgregarPaquete(paquete);
        }
        public void ActualizarResultado(PaqueteRed paquete)
        {
            if (paquete == null)
            {
                throw new ArgumentNullException(nameof(paquete), "El paquete no puede ser nulo.");
            }
            if (paquete.Id <= 0)
            {
                throw new ArgumentException(
                    "El paquete debe tener un ID válido.",
                    nameof(paquete));
            }

            PaqueteRed? paqueteExistente = _paqueteRepositorio.ObtenerPorId(paquete.Id);
            if (paqueteExistente == null)
            {
                throw new InvalidOperationException("No existe ese paquete");
            }

            paquete.TtlFinal = paquete.Ttl;

            _paqueteRepositorio.ActualizarResultado(paquete);
        }

        public PaqueteRed? ObtenerPorSimulacionId(int simulacionId)
        {
            if (simulacionId <= 0)
                throw new ArgumentOutOfRangeException(nameof(simulacionId));

            return _paqueteRepositorio.ObtenerPorSimulacionId(simulacionId);
        }

        public void ActualizarDatos(PaqueteRed paquete)
        {
            if (paquete == null)
                throw new ArgumentNullException(nameof(paquete));

            if (paquete.Id <= 0)
                throw new ArgumentException("El paquete debe tener un ID válido.");

            _paqueteRepositorio.ActualizarDatos(paquete);
        }

    }
}