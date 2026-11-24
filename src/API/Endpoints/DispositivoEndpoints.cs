
using Aplicacion.Interfaces;
using Aplicacion.DTOs;
using Persistencia.Entidades;
using MySqlConnector;

namespace API.Endpoints
{
    public static class DispositivoEndpoints
    {
        public static void MapDispositivoEndpoints(this IEndpointRouteBuilder app)
        {
            // GET: Listar todos los dispositivos
            app.MapGet("/api/dispositivos", (
                IRouterServicio routerServicio,
                ISwitchServicio switchServicio,
                IFirewallServicio firewallServicio,
                IAccessPointServicio accessPointServicio) =>
            {
                var dispositivos = new List<DispositivoDTO>();

                foreach (var d in routerServicio.Listar())
                    dispositivos.Add(CrearDTO(d.Id, d.Nombre, "Router", d.Latencia));

                foreach (var d in switchServicio.Listar())
                    dispositivos.Add(CrearDTO(d.Id, d.Nombre, "Switch", d.Latencia));

                foreach (var d in firewallServicio.Listar())
                    dispositivos.Add(CrearDTO(d.Id, d.Nombre, "Firewall", d.Latencia));

                foreach (var d in accessPointServicio.Listar())
                    dispositivos.Add(CrearDTO(d.Id, d.Nombre, "AccessPoint", d.Latencia));

                return Results.Ok(dispositivos);
            })
            .WithTags("Dispositivos");

            // GET: Buscar un dispositivo por ID
            app.MapGet("/api/dispositivos/{id:int}", (
                int id,
                IRouterServicio routerServicio,
                ISwitchServicio switchServicio,
                IFirewallServicio firewallServicio,
                IAccessPointServicio accessPointServicio) =>
            {
                var router = routerServicio.ObtenerPorId(id);

                if (router != null)
                    return Results.Ok(CrearDTO(router.Id, router.Nombre, "Router", router.Latencia));

                var sw = switchServicio.BuscarPorId(id);

                if (sw != null)
                    return Results.Ok(CrearDTO(sw.Id, sw.Nombre, "Switch", sw.Latencia));

                var firewall = firewallServicio.BuscarPorId(id);

                if (firewall != null)
                    return Results.Ok(CrearDTO(firewall.Id, firewall.Nombre, "Firewall", firewall.Latencia));

                var accessPoint = accessPointServicio.BuscarPorId(id);

                if (accessPoint != null)
                    return Results.Ok(CrearDTO(accessPoint.Id, accessPoint.Nombre, "AccessPoint", accessPoint.Latencia));

                return Results.NotFound();
            })
            .WithTags("Dispositivos");

            // DELETE: Eliminar un dispositivo
            app.MapDelete("/api/dispositivos/{id:int}", (
                int id,
                IRouterServicio routerServicio,
                ISwitchServicio switchServicio,
                IFirewallServicio firewallServicio,
                IAccessPointServicio accessPointServicio) =>
            {
                try
                {
                    if (routerServicio.ObtenerPorId(id) != null)
                    {
                        routerServicio.Eliminar(id);
                        return Results.NoContent();
                    }

                    if (switchServicio.BuscarPorId(id) != null)
                    {
                        switchServicio.Eliminar(id);
                        return Results.NoContent();
                    }

                    if (firewallServicio.BuscarPorId(id) != null)
                    {
                        firewallServicio.Eliminar(id);
                        return Results.NoContent();
                    }

                    if (accessPointServicio.BuscarPorId(id) != null)
                    {
                        accessPointServicio.Eliminar(id);
                        return Results.NoContent();
                    }

                    return Results.NotFound();
                }
                catch (MySqlException ex) when (ex.Number == 1217)
                {
                    return Results.Conflict(new
                    {
                        error = "No se puede eliminar el dispositivo porque está siendo utilizado en un recorrido guardado."
                    });
                }
            })
            .WithTags("Dispositivos");

            // PUT: Actualizar un dispositivo
            app.MapPut("/api/dispositivos/{id:int}", (
                int id,
                ActualizarDispositivoRequestDto solicitud,
                IRouterServicio routerServicio,
                ISwitchServicio switchServicio,
                IFirewallServicio firewallServicio,
                IAccessPointServicio accessPointServicio) =>
            {
                string tipo = solicitud.TipoDispositivo?.ToLower() ?? "";

                if (tipo == "router")
                {
                    if (routerServicio.ObtenerPorId(id) == null)
                        return Results.NotFound();

                    routerServicio.Actualizar(id, new ActualizarRouterRequestDto
                    {
                        Nombre = solicitud.Nombre,
                        DireccionIp = solicitud.DireccionIp,
                        DireccionMAC = solicitud.DireccionMAC,
                        Encendido = solicitud.Encendido,
                        PaquetesBloqueados = solicitud.PaquetesBloqueados,
                        PaquetesPermitidos = solicitud.PaquetesPermitidos,
                        Latencia = solicitud.Latencia
                    });
                }
                else if (tipo == "switch")
                {
                    if (switchServicio.BuscarPorId(id) == null)
                        return Results.NotFound();

                    switchServicio.Actualizar(id, new ActualizarSwitchRequestDto
                    {
                        Nombre = solicitud.Nombre,
                        DireccionIp = solicitud.DireccionIp,
                        DireccionMAC = solicitud.DireccionMAC,
                        Encendido = solicitud.Encendido,
                        CantidadPuertos = solicitud.CantidadPuertos,
                        PuertosOcupados = solicitud.PuertosOcupados,
                        VlanActiva = solicitud.VlanActiva,
                        CantidadVlan = solicitud.CantidadVlan,
                        Latencia = solicitud.Latencia
                    });
                }
                else if (tipo == "firewall")
                {
                    if (firewallServicio.BuscarPorId(id) == null)
                        return Results.NotFound();

                    firewallServicio.Actualizar(id, new ActualizarFirewallRequestDto
                    {
                        Nombre = solicitud.Nombre,
                        DireccionIp = solicitud.DireccionIp,
                        DireccionMAC = solicitud.DireccionMAC,
                        Encendido = solicitud.Encendido,
                        FiltradoActivo = solicitud.FiltradoActivo,
                        PoliticaPredeterminada = solicitud.PoliticaPredeterminada,
                        CantidadReglas = solicitud.CantidadReglas,
                        BloqueaTraficoEntrante = solicitud.BloqueaTraficoEntrante,
                        BloqueaTraficoSaliente = solicitud.BloqueaTraficoSaliente,
                        Tipo = solicitud.Tipo,
                        Latencia = solicitud.Latencia
                    });
                }
                else if (tipo == "accesspoint")
                {
                    if (accessPointServicio.BuscarPorId(id) == null)
                        return Results.NotFound();

                    accessPointServicio.Actualizar(id, new ActualizarAccessPointRequestDto
                    {
                        Nombre = solicitud.Nombre,
                        DireccionIp = solicitud.DireccionIp,
                        DireccionMAC = solicitud.DireccionMAC,
                        Encendido = solicitud.Encendido,
                        Ssid = solicitud.Ssid,
                        Seguridad = solicitud.Seguridad,
                        Canal = solicitud.Canal,
                        MaximoDispositivos = solicitud.MaximoDispositivos,
                        Latencia = solicitud.Latencia
                    });
                }
                else
                {
                    return Results.BadRequest("Tipo de dispositivo no válido.");
                }

                return Results.Ok(CrearDTO(
                    id,
                    solicitud.Nombre,
                    solicitud.TipoDispositivo,
                    solicitud.Latencia
                ));
            })
            .WithTags("Dispositivos");

            // POST: Crear un dispositivo
            app.MapPost("/api/dispositivos", (
                CrearDispositivoRequestDto solicitud,
                IRouterServicio routerServicio,
                ISwitchServicio switchServicio,
                IFirewallServicio firewallServicio,
                IAccessPointServicio accessPointServicio) =>
            {
                if (string.IsNullOrWhiteSpace(solicitud.Nombre))
                    return Results.BadRequest(new { error = "El nombre es obligatorio." });

                if (solicitud.LatenciaMs < 0)
                    return Results.BadRequest(new { error = "La latencia no puede ser negativa." });

                string direccionIp = $"192.168.1.{Random.Shared.Next(2, 255)}";
                string direccionMAC = "02:" + Guid.NewGuid().ToString("N").Substring(0, 10);

                int id;
                string tipo;

                switch (solicitud.Tipo?.ToLower())
                {
                    case "router":
                        var router = routerServicio.Crear(new RouterRequestDto
                        {
                            Nombre = solicitud.Nombre,
                            DireccionIp = direccionIp,
                            DireccionMAC = direccionMAC,
                            Encendido = true,
                            PaquetesBloqueados = 0,
                            PaquetesPermitidos = 0,
                            Latencia = solicitud.LatenciaMs
                        });

                        id = router.Id;
                        tipo = "Router";
                        break;

                    case "switch":
                        var sw = switchServicio.Crear(new SwitchRequestDto
                        {
                            Nombre = solicitud.Nombre,
                            DireccionIp = direccionIp,
                            DireccionMAC = direccionMAC,
                            Encendido = true,
                            CantidadPuertos = 8,
                            PuertosOcupados = 0,
                            VlanActiva = false,
                            CantidadVlan = 0,
                            Latencia = solicitud.LatenciaMs
                        });

                        id = sw.Id;
                        tipo = "Switch";
                        break;

                    case "firewall":
                        var firewall = firewallServicio.Crear(new FirewallRequestDto
                        {
                            Nombre = solicitud.Nombre,
                            DireccionIp = direccionIp,
                            DireccionMAC = direccionMAC,
                            Encendido = true,
                            FiltradoActivo = false,
                            PoliticaPredeterminada = "permitir",
                            CantidadReglas = 0,
                            BloqueaTraficoEntrante = false,
                            BloqueaTraficoSaliente = false,
                            Tipo = "Basico",
                            Latencia = solicitud.LatenciaMs
                        });

                        id = firewall.Id;
                        tipo = "Firewall";
                        break;

                    case "accesspoint":
                        var accessPoint = accessPointServicio.Crear(new AccessPointRequestDto
                        {
                            Nombre = solicitud.Nombre,
                            DireccionIp = direccionIp,
                            DireccionMAC = direccionMAC,
                            Encendido = true,
                            Ssid = solicitud.Nombre,
                            Seguridad = "WPA2",
                            Canal = 6,
                            MaximoDispositivos = 10,
                            Latencia = solicitud.LatenciaMs
                        });

                        id = accessPoint.Id;
                        tipo = "AccessPoint";
                        break;

                    default:
                        return Results.BadRequest(new { error = "Tipo de dispositivo no válido." });
                }

                var respuesta = CrearDTO(id, solicitud.Nombre, tipo, solicitud.LatenciaMs);

                return Results.Created($"/api/dispositivos/{id}", respuesta);
            })
            .WithTags("Dispositivos");
        }

        private static DispositivoDTO CrearDTO(int id, string nombre, string tipo, int latencia)
        {
            return new DispositivoDTO
            {
                Id = id,
                Nombre = nombre,
                Tipo = tipo,
                LatenciaMs = latencia
            };
        }
    }
}
