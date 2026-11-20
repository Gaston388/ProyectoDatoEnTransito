using Aplicacion.DTOs;
using Persistencia.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IAccessPointServicio
    {
        AccessPoint Crear(AccessPointRequestDto solicitud);
        List<AccessPoint> Listar();
        AccessPoint? BuscarPorId(int id);
        void Eliminar(int id);
        void Actualizar(int id, ActualizarAccessPointRequestDto solicitud);
    }
}