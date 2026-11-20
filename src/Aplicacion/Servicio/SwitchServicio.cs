using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Aplicacion.Servicios
{
    public class SwitchServicio : ISwitchServicio
    {
        private readonly ISwitchRepositorio repositorio;

        public SwitchServicio(ISwitchRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public Switch Crear(SwitchRequestDto solicitud)
        {
            Switch switchRed = new Switch(
                solicitud.Nombre,
                solicitud.DireccionIp,
                solicitud.DireccionMAC,
                solicitud.Encendido,
                solicitud.CantidadPuertos,
                solicitud.PuertosOcupados,
                solicitud.VlanActiva,
                solicitud.CantidadVlan,
                solicitud.Latencia
            );

            repositorio.Agregar(switchRed);

            return switchRed;
        }

        public List<Switch> Listar()
        {
            return repositorio.Listar();
        }

        public Switch? BuscarPorId(int id)
        {
            return repositorio.ObtenerPorId(id);
        }
        public void Eliminar(int id)
        {
            repositorio.Eliminar(id);
        }
        public void Actualizar(int id, ActualizarSwitchRequestDto solicitud)
        {
            Switch? switchRed = repositorio.ObtenerPorId(id);

            if (switchRed is null)
            {
                return;
            }
            switchRed.Nombre = solicitud.Nombre;
            switchRed.DireccionIp = solicitud.DireccionIp;
            switchRed.DireccionMAC = solicitud.DireccionMAC;
            switchRed.Encendido = solicitud.Encendido;
            switchRed.CantidadPuertos = solicitud.CantidadPuertos;
            switchRed.PuertosOcupados = solicitud.PuertosOcupados;
            switchRed.VlanActiva = solicitud.VlanActiva;
            switchRed.CantidadVlan = solicitud.CantidadVlan;
            switchRed.Latencia = solicitud.Latencia;

            repositorio.Actualizar(switchRed);
        }
    }
}