using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Aplicacion.Servicios
{
    public class RouterServicio : IRouterServicio
    {
        private readonly IRouterRepositorio repositorio;

        public RouterServicio(IRouterRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public Router Crear(RouterRequestDto solicitud)
        {
            Router router = new Router(
                solicitud.Nombre,
                solicitud.DireccionIp,
                solicitud.DireccionMAC,
                solicitud.Encendido,
                solicitud.PaquetesBloqueados,
                solicitud.PaquetesPermitidos,
                solicitud.Latencia
            );

            repositorio.Agregar(router);

            return router;
        }
        public Router? ObtenerPorId(int id)
        {
            return repositorio.ObtenerPorId(id);
        }
        public List<Router> Listar()
        {
            return repositorio.Listar();
        }
        public void Eliminar(int id)
        {
            repositorio.Eliminar(id);
        }
        public void Actualizar(int id, ActualizarRouterRequestDto solicitud)
        {
            Router? router = repositorio.ObtenerPorId(id);

            if (router is null)
            {
                return;
            }
            router.Nombre = solicitud.Nombre;
            router.DireccionIp = solicitud.DireccionIp;
            router.DireccionMAC = solicitud.DireccionMAC;
            router.Encendido = solicitud.Encendido;
            router.PaquetesBloqueados = solicitud.PaquetesBloqueados;
            router.PaquetesPermitidos = solicitud.PaquetesPermitidos;
            router.Latencia = solicitud.Latencia;

            repositorio.Actualizar(router);
        }
    }
}