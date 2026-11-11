using Persistencia.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IPaqueteServicio
    {
        void CrearPaquete(Simulacion simulacion, PaqueteRed paquete);
    }
}