using Persistencia.Entidades;

namespace Persistencia.Repositorios.InterfazRepositorios
{
    public interface IRouterRepositorio
    {
        void Agregar(Router router);
        Router ObtenerPorId(int id);
        List <Router> Listar();
        void Eliminar (int id);
    }
}