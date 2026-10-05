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
            _paqueteRepositorio = paqueteRepositorio;
        }

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
            _paqueteRepositorio.Agregar(paquete);
        }
    }
}