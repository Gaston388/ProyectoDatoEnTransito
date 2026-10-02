using Persistencia.Repositorios.InterfazRepositorios;
using Persistencia.Entidades;

namespace Persistencia.Repositorios
{
    public interface FirewallRepositorio : IFirewallRepositorio
    {
        public void Agregar(Firewall firewall)
        {
            throw new NotImplementedException();
        }
        public Firewall ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
        public List<Firewall> Listar()
        {
            throw new NotImplementedException();
        }
        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}