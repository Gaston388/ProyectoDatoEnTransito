using Persistencia.Entidades;
using Aplicacion.Interfaces;

namespace Aplicacion.Interfaces
{
    public interface IPaqueteServicio
    {
        void CrearPaquete(Simulacion simulaacion, PaqueteRed paquete);
    }
}