using Persistencia.Entidades;

namespace Persistencia.Repositorios.InterfazRepositorios
{
    public interface ISimulacionRepositorio
    {
        Simulacion BuscarPorId(int id);
        List<Simulacion> Listar();
    }
}