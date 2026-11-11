using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Aplicacion.Servicios
{
    public class PaqueteServicio : IPaqueteServicio
    {
        private readonly IPaqueteRedRepositorio _paqueteRepositorio;

        public PaqueteServicio(IPaqueteRedRepositorio paqueteRepositorio)
        {
            _paqueteRepositorio = paqueteRepositorio
            ?? throw new ArgumentNullException(nameof(paqueteRepositorio));
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

            simulacion.AgregarPaquete(paquete);
            _paqueteRepositorio.Agregar(paquete);
        }
    }
}