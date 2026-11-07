using Aplicacion.Interfaces;
using Persistencia.Entidades;

namespace Aplicacion.Servicios
{
    public class DispositivoServicio : IDispositivoServicio
    {
        // Administra los dispositivos que pertenecen a una simulación en memoria.
        // No recibe repositorios porque esta clase no consulta ni guarda en la base de datos.

        public void RegistrarDispositivo(Simulacion simulacion, DispositivoRed dispositivo)
        {
            ArgumentNullException.ThrowIfNull(simulacion);
            ArgumentNullException.ThrowIfNull(dispositivo);

            simulacion.AgregarDispositivo(dispositivo);
        }

        public DispositivoRed ObtenerDispositivoPorId(Simulacion simulacion, int id)
        {
            ArgumentNullException.ThrowIfNull(simulacion);

            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "El ID debe ser mayor que 0.");
            }

            DispositivoRed? dispositivo = simulacion.Dispositivos
                .FirstOrDefault(d => d.Id == id);

            return dispositivo ?? throw new KeyNotFoundException(
                $"No se encontró un dispositivo con el ID {id} en la simulación.");
        }

        public void EliminarDispositivo(Simulacion simulacion, DispositivoRed dispositivo)
        {
            ArgumentNullException.ThrowIfNull(simulacion);
            ArgumentNullException.ThrowIfNull(dispositivo);

            if (!simulacion.Dispositivos.Remove(dispositivo))
            {
                throw new KeyNotFoundException(
                    $"El dispositivo con ID {dispositivo.Id} no pertenece a esta simulación.");
            }
        }
    }
}