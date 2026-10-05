using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Aplicacion.Servicios
{
    public class EstadisticaServicio : IEstadisticaServicio
    {
        private readonly IEstadisticaRepositorio _estadisticaRepositorio;

        public EstadisticaServicio(IEstadisticaRepositorio estadisticaRepositorio)
        {
            _estadisticaRepositorio = estadisticaRepositorio;
        }

        public void ConsultarEstadisticas(Simulacion simulacion)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            Console.WriteLine($"===== Estadísticas de la simulación {simulacion.Id} =====");
            Console.WriteLine($"Dispositivos: {simulacion.Dispositivos.Count}");
            Console.WriteLine($"Paquetes: {simulacion.Paquetes.Count}");
        }

        // Método nuevo: estadísticas por dispositivo (usa el SP)
        public void MostrarEstadisticasDispositivos()
        {
            var resultados = _estadisticaRepositorio.ObtenerEstadisticasDispositivos();

            Console.WriteLine("===== Estadísticas por Dispositivo =====");

            foreach (var item in resultados)
            {
                Console.WriteLine($"Dispositivo ID: {item.dispositivo_id}");
                Console.WriteLine($"Cantidad de recorridos: {item.cantidad_recorridos}");
                Console.WriteLine($"Latencia promedio: {item.latencia_promedio}");
                Console.WriteLine($"Latencia máxima: {item.latencia_maxima}");
                Console.WriteLine("--------------------------------------");
            }
        }

        // Método nuevo: estadísticas generales de simulaciones (usa el SP)
        public void MostrarEstadisticasSimulaciones()
        {
            var resultados = _estadisticaRepositorio.ObtenerEstadisticasSimulaciones();

            Console.WriteLine("===== Estadísticas Generales de Simulaciones =====");

            foreach (var item in resultados)
            {
                Console.WriteLine($"Total simulaciones: {item.total_simulaciones}");
                Console.WriteLine($"Latencia promedio: {item.latencia_promedio}");
                Console.WriteLine($"Latencia máxima: {item.latencia_maxima}");
            }
        }
    }
}