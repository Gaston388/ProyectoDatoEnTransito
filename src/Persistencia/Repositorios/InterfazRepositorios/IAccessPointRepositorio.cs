using Persistencia.Entidades;

namespace Persistencia.Repositorios.InterfazRepositorios
{
    public interface IAccessPointRepositorio
    {
        int Agregar(AccessPoint accessPoint);
        AccessPoint? ObtenerPorId(int id);
        List<AccessPoint> Listar();
        void Eliminar(int id);
        void Actualizar(AccessPoint accessPoint);
    }
}