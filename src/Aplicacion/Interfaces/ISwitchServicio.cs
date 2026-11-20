using Aplicacion.DTOs;
using Persistencia.Entidades;

namespace Aplicacion.Interfaces
{
    public interface ISwitchServicio
    {
        Switch Crear(SwitchRequestDto solicitud);
        List<Switch> Listar();
        Switch? BuscarPorId(int id);
        void Eliminar(int id);
        void Actualizar(int id, ActualizarSwitchRequestDto solicitud);
    }
}