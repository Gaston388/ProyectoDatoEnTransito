using Persistencia.Entidades;

namespace Persistencia.Repositorios.InterfazRepositorios
{
    public interface IFirewallRepositorio
    {
        int Agregar(Firewall firewall);
        Firewall? ObtenerPorId(int id);
        List<Firewall> Listar();
        void Eliminar(int id);
        void Actualizar(Firewall firewall);
    }
}