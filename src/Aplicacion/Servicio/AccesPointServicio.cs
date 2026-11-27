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
            //guardamos el access point en la base de datos                
            repositorio.Agregar(accessPoint);
            //devolvemos el objeto creado
            return accessPoint;
        }
        //obtiene y devuelve la lista de todos los accesspoint registrados
        public List<AccessPoint> Listar()
        {
            return repositorio.Listar();
        }
        // busca un accesspoint por su identificador, el signo ? indica que puede devolver null
        public AccessPoint? BuscarPorId(int id)
        {
            return repositorio.ObtenerPorId(id);
        }
        //solicita al repositorio eliminar el access point por su id
        public void Eliminar(int id)
        {
            repositorio.Eliminar(id);
        }
        // recibe el id del objeto y un dto con los nuevos datos para actualizar un A.C existente
        public void Actualizar(int id, ActualizarAccessPointRequestDto solicitud)
        {
            //buscamos el access point
            AccessPoint? accessPoint = repositorio.ObtenerPorId(id);
            //si no existe, terminamos sin aplicar cambios
            if (accessPoint is null)
                return;
            //remplazamos los valores actuales por los datos recibidos de el dto
            accessPoint.Nombre = solicitud.Nombre;
            accessPoint.DireccionIp = solicitud.DireccionIp;
            accessPoint.DireccionMAC = solicitud.DireccionMAC;
            accessPoint.Encendido = solicitud.Encendido;
            accessPoint.Ssid = solicitud.Ssid;
            accessPoint.Seguridad = solicitud.Seguridad;
            accessPoint.Canal = solicitud.Canal;
            accessPoint.MaximoDispositivos = solicitud.MaximoDispositivos;
            accessPoint.Latencia = solicitud.Latencia;
            //enviamos el objeto modificado 
            repositorio.Actualizar(accessPoint);
        }
    }
}