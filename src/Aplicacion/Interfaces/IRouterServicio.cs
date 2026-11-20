using Aplicacion.DTOs;
using Persistencia.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IRouterServicio
    {
        Router Crear(RouterRequestDto solicitud);
        Router? ObtenerPorId(int id);
        List<Router> Listar();
        void Eliminar(int id);
        void Actualizar(int id, ActualizarRouterRequestDto solicitud);
    }
    
}