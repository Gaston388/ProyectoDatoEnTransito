using Persistencia.Entidades;

namespace Persistencia.Repositorios.InterfazRepositorios
{
    public interface ISimulacionRepositorio
    {
        Simulacion? BuscarPorId(int id);
        List<Simulacion> Listar();
        public int Agregar(Simulacion simulacion);
        void Finalizar(int id, bool exitoso);
        void Eliminar(int id);
        void AgregarPaso(int simulacionId, int dispositivoId, int orden, int latencia, int ttlFinal);
        void EliminarPasos(int simulacionId);
    }
}