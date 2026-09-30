using Persistencia.Entidades;
using Aplicacion.Interfaces;
namespace Aplicacion.Interfaces
{
    public interface IDispositivoServicio
    {
        void RegistrarDispositivo(Simulacion simulacion, DispositivoRed dispositivo);
        void EliminarDispositivo(Simulacion simulacion, DispositivoRed dispositivo);
        DispositivoRed ObtenerDispositivoPorId(Simulacion simulacion, int id);
    }
}