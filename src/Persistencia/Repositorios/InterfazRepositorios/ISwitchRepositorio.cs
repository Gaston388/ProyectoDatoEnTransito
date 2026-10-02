using Persistencia.Entidades;

namespace Persistencia.Repositorios.InterfazRepositorios
{
    public interface ISwitchRepositorio
    {
        void Agregar(Switch _switch);
        Switch ObtenerPorId(int id);
        List <Switch> Listar();
        void Eliminar (int id);
    }
}