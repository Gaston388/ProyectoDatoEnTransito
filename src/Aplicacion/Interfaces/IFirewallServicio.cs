using Aplicacion.DTOs;
using Persistencia.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IFirewallServicio
    {
        Firewall Crear(FirewallRequestDto solicitud);
        List<Firewall> Listar();
        Firewall? BuscarPorId(int id);
        void Eliminar(int id);
        void Actualizar(int id, ActualizarFirewallRequestDto solicitud);
    }
}