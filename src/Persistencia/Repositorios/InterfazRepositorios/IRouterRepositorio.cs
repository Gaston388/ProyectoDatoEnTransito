using Persistencia.Entidades;

namespace Persistencia.Repositorios.InterfazRepositorios
{
    public interface IRouterRepositorio
    {
        int Agregar(Router router);
        Router ObtenerPorId(int id);
        List <Router> Listar();
        void Eliminar (int id);
        void Actualizar (Router router);
    }
}