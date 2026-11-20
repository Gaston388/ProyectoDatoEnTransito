using Persistencia.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IPaqueteServicio
    {
        void CrearPaquete(Simulacion simulacion, PaqueteRed paquete);
        void ActualizarResultado(PaqueteRed paquete);
        PaqueteRed? ObtenerPorSimulacionId(int simulacionId);
        void ActualizarDatos(PaqueteRed paquete);
    }
}