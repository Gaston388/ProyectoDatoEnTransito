using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Aplicacion.Servicios
{
    public class DispositivoServicio : IDispositivoServicio
    {
        private readonly IAccessPointRepositorio _accessPointRepositorio;
        private readonly IRouterRepositorio _routerRepositorio;
        private readonly IFirewallRepositorio _firewallRepositorio;
        private readonly ISwitchRepositorio _switchRepositorio;

        public DispositivoServicio(
            IAccessPointRepositorio accessPointRepositorio,
            IRouterRepositorio routerRepositorio,
            IFirewallRepositorio firewallRepositorio,
            ISwitchRepositorio switchRepositorio)
        {
            _accessPointRepositorio = accessPointRepositorio;
            _routerRepositorio = routerRepositorio;
            _firewallRepositorio = firewallRepositorio;
            _switchRepositorio = switchRepositorio;
        }

        public void RegistrarDispositivo(Simulacion simulacion, DispositivoRed dispositivo)
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

        public DispositivoRed ObtenerDispositivoPorId(Simulacion simulacion, int id)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "El Id debe ser mayor que 0.");
            }

            foreach (var dispositivo in simulacion.Dispositivos)
            {
                if (dispositivo.Id == id)
                {
                    return dispositivo;
                }
            }

            throw new Exception("No se encontró el dispositivo");
        }

        public void EliminarDispositivo(Simulacion simulacion, DispositivoRed dispositivo)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            if (dispositivo == null)
            {
                throw new ArgumentNullException(nameof(dispositivo));
            }

            simulacion.Dispositivos.Remove(dispositivo);
        }
    }
}