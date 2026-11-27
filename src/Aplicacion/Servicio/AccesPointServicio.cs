using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Aplicacion.Servicios
{
    public class AccessPointServicio : IAccessPointServicio
    {
        private readonly IAccessPointRepositorio repositorio;

        public AccessPointServicio(IAccessPointRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }
        //recibe los datos del access point mediante un dto
        public AccessPoint Crear(AccessPointRequestDto solicitud)
        {
            //creamos un Access Point utilizando los datos recibidos
            AccessPoint accessPoint = new AccessPoint(
                solicitud.Nombre,
                solicitud.DireccionIp,
                solicitud.DireccionMAC,
                solicitud.Encendido,
                solicitud.Ssid,
                solicitud.Seguridad,
                solicitud.Canal,
                solicitud.MaximoDispositivos,
                solicitud.Latencia
            );

            repositorio.Agregar(accessPoint);

            return accessPoint;
        }

        public List<AccessPoint> Listar()
        {
            return repositorio.Listar();
        }

        public AccessPoint? BuscarPorId(int id)
        {
            return repositorio.ObtenerPorId(id);
        }
        public void Eliminar(int id)
        {
            repositorio.Eliminar(id);
        }
        public void Actualizar(int id, ActualizarAccessPointRequestDto solicitud)
        {
            AccessPoint? accessPoint = repositorio.ObtenerPorId(id);

            if (accessPoint is null)
                return;

            accessPoint.Nombre = solicitud.Nombre;
            accessPoint.DireccionIp = solicitud.DireccionIp;
            accessPoint.DireccionMAC = solicitud.DireccionMAC;
            accessPoint.Encendido = solicitud.Encendido;
            accessPoint.Ssid = solicitud.Ssid;
            accessPoint.Seguridad = solicitud.Seguridad;
            accessPoint.Canal = solicitud.Canal;
            accessPoint.MaximoDispositivos = solicitud.MaximoDispositivos;
            accessPoint.Latencia = solicitud.Latencia;

            repositorio.Actualizar(accessPoint);
        }
    }
}