namespace Persistencia.Repositorios.InterfazRepositorios
{
    public interface IEstadisticaRepositorio
    {
        List<dynamic> ObtenerEstadisticasDispositivos();
        List<dynamic> ObtenerEstadisticasSimulaciones();
    }
}