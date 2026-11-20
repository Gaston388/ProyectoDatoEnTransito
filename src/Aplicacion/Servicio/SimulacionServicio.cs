using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;
using Aplicacion.DTOs;
namespace Aplicacion.Servicios
{
    public class SimulacionServicio : ISimulacionServicio
    {
        private readonly ISimulacionRepositorio? _simulacionRepositorio;
        private readonly IRouterRepositorio? _routerRepositorio;
        private readonly ISwitchRepositorio? _switchRepositorio;
        private readonly IFirewallRepositorio? _firewallRepositorio;
        private readonly IAccessPointRepositorio? _accessPointRepositorio;
        private readonly IPaqueteServicio? _paqueteServicio;
        public SimulacionServicio(
            ISimulacionRepositorio simulacionRepositorio,
            IRouterRepositorio routerRepositorio,
            ISwitchRepositorio switchRepositorio,
            IFirewallRepositorio firewallRepositorio,
            IAccessPointRepositorio accessPointRepositorio,
            IPaqueteServicio paqueteServicio)
        {
            _simulacionRepositorio = simulacionRepositorio;
            _routerRepositorio = routerRepositorio;
            _switchRepositorio = switchRepositorio;
            _firewallRepositorio = firewallRepositorio;
            _accessPointRepositorio = accessPointRepositorio;
            _paqueteServicio = paqueteServicio;
        }
        //este constructor vacio se conserva para no impedir las operaciones en memoria
        public SimulacionServicio()
        {
            _simulacionRepositorio = null;
            _routerRepositorio = null;
            _switchRepositorio = null;
            _firewallRepositorio = null;
            _accessPointRepositorio = null;
            _paqueteServicio = null;
        }
        public void AgregarDispositivo(Simulacion simulacion, DispositivoRed dispositivo)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            if (dispositivo == null)
            {
                throw new ArgumentNullException(nameof(dispositivo));
            }

            simulacion.AgregarDispositivo(dispositivo);
        }

        public void AgregarPaquete(Simulacion simulacion, PaqueteRed paquete)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            if (paquete == null)
            {
                throw new ArgumentNullException(nameof(paquete));
            }

            simulacion.AgregarPaquete(paquete);
        }

        public bool ProcesarPaquete(Simulacion simulacion, PaqueteRed paquete)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            if (paquete == null)
            {
                throw new ArgumentNullException(nameof(paquete));
            }

            return simulacion.ProcesarPaquete(paquete);
        }

        public Simulacion? BuscarPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "El ID debe ser mayor que 0.");
            }
            if(_simulacionRepositorio == null)
            {
                throw new InvalidOperationException("El repositorio de simulación no está inicializado.");
            }
            return _simulacionRepositorio.BuscarPorId(id);
        }

        public List<Simulacion> Listar()
        {
            if(_simulacionRepositorio == null)
            {
                throw new InvalidOperationException("El repositorio de simulación no está inicializado.");
            }

            return _simulacionRepositorio.Listar();
        }
        public int Agregar()
        {
            if(_simulacionRepositorio == null)
            {
                throw new InvalidOperationException("El repositorio de simulación no está inicializado.");
            }
            Simulacion simulacion = new Simulacion
            {
                FechaInicio = DateTime.Now,
                Finalizado = false  
            };
            return _simulacionRepositorio.Agregar(simulacion);
        }
        public void Finalizar(int id, bool exitoso)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(id), "El ID debe ser mayor que 0.");
            }

            if (_simulacionRepositorio == null)
            {
                throw new InvalidOperationException(
                    "El repositorio de simulación no está inicializado.");
            }

            Simulacion? simulacion = _simulacionRepositorio.BuscarPorId(id);

            if (simulacion == null)
            {
                throw new KeyNotFoundException(
                    "No existe una simulación con ese ID.");
            }

            if (simulacion.Finalizado)
            {
                throw new InvalidOperationException(
                    "La simulación ya está finalizada.");
            }

            _simulacionRepositorio.Finalizar(id, exitoso);
        }
       private DispositivoRed ObtenerDispositivo(DispositivoSimulacionDto solicitud)
        {
            if (solicitud.Id <= 0)
                throw new ArgumentException("El ID del dispositivo debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(solicitud.Tipo))
                throw new ArgumentException("El tipo de dispositivo es obligatorio.");

            DispositivoRed? dispositivo = solicitud.Tipo.Trim().ToLowerInvariant() switch
            {
                "router" => _routerRepositorio?.ObtenerPorId(solicitud.Id),

                "switch" => _switchRepositorio?.ObtenerPorId(solicitud.Id),

                "firewall" => _firewallRepositorio?.ObtenerPorId(solicitud.Id),

                "accesspoint" or "access point" => _accessPointRepositorio?.ObtenerPorId(solicitud.Id),

                _ => throw new ArgumentException(
                    $"El tipo de dispositivo '{solicitud.Tipo}' no es válido.")
            };

            if (dispositivo is null)
                throw new ArgumentException(
                    $"No se encontró un dispositivo de tipo '{solicitud.Tipo}' con ID {solicitud.Id}.");

            return dispositivo;
        }
        private void ValidarSolicitud(SimulacionRequestDto solicitud)
        {
            if (solicitud is null)
                throw new ArgumentNullException(nameof(solicitud));

            if (string.IsNullOrWhiteSpace(solicitud.Origen))
                throw new ArgumentException("La IP de origen es obligatoria.");

            if (string.IsNullOrWhiteSpace(solicitud.Destino))
                throw new ArgumentException("La IP de destino es obligatoria.");

            if (solicitud.TamanioBytes <= 0)
                throw new ArgumentException("El tamaño del paquete debe ser mayor a cero.");

            if (solicitud.Ttl <= 0)
                throw new ArgumentException("El TTL debe ser mayor a cero.");

            if (solicitud.Dispositivos is null || solicitud.Dispositivos.Count == 0)
                throw new ArgumentException("Debe indicar al menos un dispositivo.");

            foreach (var dispositivo in solicitud.Dispositivos)
            {
                if (dispositivo is null)
                    throw new ArgumentException("La lista contiene un dispositivo inválido.");

                if (dispositivo.Id <= 0)
                    throw new ArgumentException("El ID de cada dispositivo debe ser mayor a cero.");

                if (string.IsNullOrWhiteSpace(dispositivo.Tipo))
                    throw new ArgumentException("Todos los dispositivos deben tener un tipo.");
            }
        }
        private List<DispositivoRed> ObtenerDispositivos(SimulacionRequestDto solicitud)
        {
            var dispositivos = new List<DispositivoRed>();
            foreach (var dispositivoSolicitud in solicitud.Dispositivos)
            {
                var dispositivo = ObtenerDispositivo(dispositivoSolicitud);
                dispositivos.Add(dispositivo);
            }
            return dispositivos;
        }
        public SimulacionResponseDto CrearSimulacion(SimulacionRequestDto solicitud)
        {
            ValidarSolicitud(solicitud);

            if (_simulacionRepositorio is null ||
                _paqueteServicio is null)
            {
                throw new InvalidOperationException(
                    "Los servicios necesarios no están configurados.");
            }

            // 1. Recuperamos los dispositivos solicitados.
            List<DispositivoRed> dispositivos =
                ObtenerDispositivos(solicitud);

            // 2. Creamos la simulación en la base de datos.
            int simulacionId = Agregar();

            // 3. Recuperamos la simulación recién creada.
            Simulacion? simulacion =
                _simulacionRepositorio.BuscarPorId(simulacionId);

            if (simulacion is null)
            {
                throw new InvalidOperationException(
                    "No se pudo recuperar la simulación creada.");
            }

            // 4. Agregamos los dispositivos a la simulación.
            foreach (var dispositivo in dispositivos)
            {
                AgregarDispositivo(simulacion, dispositivo);
            }

            // 5. Creamos el paquete.
            PaqueteRed paquete = new PaqueteRed(
                solicitud.Ttl,
                solicitud.Origen,
                solicitud.Destino,
                "00:00:00:00:00:01",
                "00:00:00:00:00:02",
                solicitud.TamanioBytes,
                "TCP",
                "Datos de prueba",
                DateTime.Now
            );

            // 6. Guardamos el paquete y lo asociamos.
            _paqueteServicio.CrearPaquete(simulacion, paquete);
            
            bool exitoso = ProcesarYGuardarRecorrido(simulacion, paquete);

            _paqueteServicio.ActualizarResultado(paquete);

            Finalizar(simulacion.Id, exitoso);
            // Por ahora devolvemos el estado inicial.
            return new SimulacionResponseDto
            {
                Id = simulacion.Id,
                Estado = exitoso ? "Exitoso" : "Fallido",
                TtlFinal = paquete.TtlFinal ?? paquete.Ttl,
                LatenciaTotalMs = paquete.LatenciaAcumulada
            };
        }
        public void Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (_simulacionRepositorio == null)
                throw new InvalidOperationException(
                    "El repositorio de simulación no está inicializado.");

            Simulacion? simulacion = _simulacionRepositorio.BuscarPorId(id);

            if (simulacion == null)
                throw new KeyNotFoundException(
                    "No existe una simulación con ese ID.");

            _simulacionRepositorio.Eliminar(id);
        }

        private bool ProcesarYGuardarRecorrido(Simulacion simulacion, PaqueteRed paquete)
        {
            int orden = 1;

            foreach (DispositivoRed dispositivo in simulacion.Dispositivos)
            {
                if (paquete.Ttl <= 0)
                {
                    paquete.Procesado = false;
                    simulacion.Finalizar(false);
                    return false;
                }

                bool permitido = dispositivo.ProcesarPaquete(paquete);

                paquete.LatenciaAcumulada += dispositivo.Latencia;

                _simulacionRepositorio!.AgregarPaso(
                    simulacion.Id,
                    dispositivo.Id,
                    orden,
                    dispositivo.Latencia,
                    paquete.Ttl
                );

                orden++;

                if (!permitido)
                {
                    paquete.Procesado = false;
                    simulacion.Finalizar(false);
                    return false;
                }
            }

            paquete.Procesado = true;
            simulacion.Finalizar(true);

            return true;
        }
        public SimulacionResponseDto ActualizarSimulacion(
            int id, SimulacionRequestDto solicitud)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            ValidarSolicitud(solicitud);

            if (_simulacionRepositorio == null || _paqueteServicio == null)
                throw new InvalidOperationException("Los servicios no están configurados.");

            // Buscamos la simulación.
            Simulacion? simulacion = _simulacionRepositorio.BuscarPorId(id);

            if (simulacion == null)
                throw new KeyNotFoundException("No existe esa simulación.");

            // Buscamos el paquete asociado.
            PaqueteRed? paquete = _paqueteServicio.ObtenerPorSimulacionId(id);

            if (paquete == null)
                throw new KeyNotFoundException("La simulación no tiene un paquete asociado.");

            // Buscamos los nuevos dispositivos.
            List<DispositivoRed> dispositivos = ObtenerDispositivos(solicitud);

            // Actualizamos los datos del paquete.
            paquete.IpOrigen = solicitud.Origen;
            paquete.IpDestino = solicitud.Destino;
            paquete.Tamaño = solicitud.TamanioBytes;
            paquete.Ttl = solicitud.Ttl;
            paquete.TtlFinal = null;
            paquete.Procesado = false;
            paquete.LatenciaAcumulada = 0;

            _paqueteServicio.ActualizarDatos(paquete);

            // Eliminamos el recorrido anterior y reiniciamos la simulación.
            _simulacionRepositorio.EliminarPasos(id);
            _simulacionRepositorio.Reiniciar(id);

            // Agregamos los nuevos dispositivos.
            foreach (var dispositivo in dispositivos)
                AgregarDispositivo(simulacion, dispositivo);

            // Procesamos nuevamente el paquete.
            bool exitoso = ProcesarYGuardarRecorrido(simulacion, paquete);

            _paqueteServicio.ActualizarResultado(paquete);
            Finalizar(id, exitoso);

            return new SimulacionResponseDto
            {
                Id = id,
                Estado = exitoso ? "Exitoso" : "Fallido",
                TtlFinal = paquete.TtlFinal ?? paquete.Ttl,
                LatenciaTotalMs = paquete.LatenciaAcumulada
            };
        }
    }
}