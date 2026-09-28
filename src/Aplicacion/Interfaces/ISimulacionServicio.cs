using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface ISimulacionServicio
    {
        public interface ISimulacionService
        {
            void AgregarDispositivo(Simulacion simulacion,DispositivoRed dispositivo);
            void AgregarPaquete(Simulacion simulacion,PaqueteRed paquete);
            void ProcesarPaquete(Simulacion simulacion,PaqueteRed paquete);
        }
    }
}