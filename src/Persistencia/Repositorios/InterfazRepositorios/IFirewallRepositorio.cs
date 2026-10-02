using Persistencia.Entidades;

namespace Persistencia.Repositorios.InterfazRepositorios
{
    public interface IFirewallRepositorio
    {
        void Agregar(Firewall firewall);
        Firewall ObtenerPorId(int id);
        List <Firewall> Listar();
        void Eliminar (int id);
    }
}