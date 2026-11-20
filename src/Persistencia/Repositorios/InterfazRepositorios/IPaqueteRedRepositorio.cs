using Persistencia.Entidades;

namespace Persistencia.Repositorios.InterfazRepositorios
{
    public interface IPaqueteRedRepositorio
    {
        int Agregar(PaqueteRed paqueteRed, int simulacionId);
        PaqueteRed? ObtenerPorId(int id);
        List <PaqueteRed> Listar();
        void ActualizarResultado(PaqueteRed paquete);
    }
}