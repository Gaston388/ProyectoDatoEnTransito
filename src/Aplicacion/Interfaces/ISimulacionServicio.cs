using Persistencia.Entidades;
using Aplicacion.DTOs;

namespace Aplicacion.Interfaces
{
    public interface ISimulacionServicio
    {
        void AgregarDispositivo(Simulacion simulacion,DispositivoRed dispositivo);
        void AgregarPaquete(Simulacion simulacion,PaqueteRed paquete);
        bool ProcesarPaquete(Simulacion simulacion,PaqueteRed paquete);
        Simulacion? BuscarPorId(int id);
        List <Simulacion> Listar();
        int Agregar();
        void Finalizar (int id, bool exitoso);
        SimulacionResponseDto CrearSimulacion(SimulacionRequestDto solicitud);
    }
}