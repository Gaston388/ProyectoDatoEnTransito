using Persistencia.Entidades;

namespace Persistencia.Repositorios.InterfazRepositorios
{
    public interface IPaqueteRedRepositorio
    {
        void Agregar(PaqueteRed paqueteRed);
        PaqueteRed ObtenerPorId(int id);
        List <PaqueteRed> Listar();
    }
}