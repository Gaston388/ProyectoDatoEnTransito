using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Aplicacion.Servicios
{
    public class FirewallServicio : IFirewallServicio
    {
        private readonly IFirewallRepositorio repositorio;

        public FirewallServicio(IFirewallRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public Firewall Crear(FirewallRequestDto solicitud)
        {
            Firewall firewall = new Firewall(
                solicitud.Nombre,
                solicitud.DireccionIp,
                solicitud.DireccionMAC,
                solicitud.Encendido,
                solicitud.FiltradoActivo,
                solicitud.PoliticaPredeterminada,
                solicitud.CantidadReglas,
                solicitud.BloqueaTraficoEntrante,
                solicitud.BloqueaTraficoSaliente,
                solicitud.Tipo,
                solicitud.Latencia
            );

            repositorio.Agregar(firewall);

            return firewall;
        }

        public List<Firewall> Listar()
        {
            return repositorio.Listar();
        }

        public Firewall? BuscarPorId(int id)
        {
            return repositorio.ObtenerPorId(id);
        }
        public void Eliminar(int id)
        {
            repositorio.Eliminar(id);
        }
        public void Actualizar(int id, ActualizarFirewallRequestDto solicitud)
        {
            Firewall? firewall = repositorio.ObtenerPorId(id);

            if (firewall is null)
            {
                return;
            }
            firewall.Nombre = solicitud.Nombre;
            firewall.DireccionIp = solicitud.DireccionIp;
            firewall.DireccionMAC = solicitud.DireccionMAC;
            firewall.Encendido = solicitud.Encendido;
            firewall.FiltradoActivo = solicitud.FiltradoActivo;
            firewall.PoliticaPredeterminada = solicitud.PoliticaPredeterminada;
            firewall.CantidadReglas = solicitud.CantidadReglas;
            firewall.BloqueaTraficoEntrante = solicitud.BloqueaTraficoEntrante;
            firewall.BloqueaTraficoSaliente = solicitud.BloqueaTraficoSaliente;
            firewall.Tipo = solicitud.Tipo;
            firewall.Latencia = solicitud.Latencia;

            repositorio.Actualizar(firewall);
        }
    }
}