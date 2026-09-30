using Persistencia.Entidades;
namespace Aplicacion.Interfaces
{
    public interface ISimulacionServicio
    {
        void AgregarDispositivo(Simulacion simulacion,DispositivoRed dispositivo);
        void AgregarPaquete(Simulacion simulacion,PaqueteRed paquete);
        void ProcesarPaquete(Simulacion simulacion,PaqueteRed paquete);

    }
}