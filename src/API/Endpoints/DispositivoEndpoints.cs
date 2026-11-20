
using Aplicacion.Interfaces;
using Aplicacion.DTOs;
using Persistencia.Entidades;
namespace API.Endpoints
{
    public static class DispositivoEndpoints
    {
        public static void MapDispositivoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/dispositivos", (
                IRouterServicio routerServicio,
                ISwitchServicio switchServicio,
                IFirewallServicio firewallServicio,
                IAccessPointServicio accessPointServicio) =>
            {
                var dispositivos = new List<DispositivoDTO>();

                foreach (var d in routerServicio.Listar())
                {
                    dispositivos.Add(new DispositivoDTO
                    {
                        Id = d.Id,
                        Nombre = d.Nombre,
                        Tipo = "Router",
                        LatenciaMs = d.Latencia
                    });
                }

                foreach (var d in switchServicio.Listar())
                {
                    dispositivos.Add(new DispositivoDTO
                    {
                        Id = d.Id,
                        Nombre = d.Nombre,
                        Tipo = "Switch",
                        LatenciaMs = d.Latencia
                    });
                }

                foreach (var d in firewallServicio.Listar())
                {
                    dispositivos.Add(new DispositivoDTO
                    {
                        Id = d.Id,
                        Nombre = d.Nombre,
                        Tipo = "Firewall",
                        LatenciaMs = d.Latencia
                    });
                }

                foreach (var d in accessPointServicio.Listar())
                {
                    dispositivos.Add(new DispositivoDTO
                    {
                        Id = d.Id,
                        Nombre = d.Nombre,
                        Tipo = "AccessPoint",
                        LatenciaMs = d.Latencia
                    });
                }

                return Results.Ok(dispositivos);
            });
        
            
            app.MapGet("/api/dispositivos/{id:int}", (
                int id,
                IRouterServicio routerServicio,
                ISwitchServicio switchServicio,
                IFirewallServicio firewallServicio,
                IAccessPointServicio accessPointServicio) =>
            {
                var router = routerServicio.ObtenerPorId(id);

                if (router != null)
                    return Results.Ok(new DispositivoDTO
                    {
                        Id = router.Id,
                        Nombre = router.Nombre,
                        Tipo = "Router",
                        LatenciaMs = router.Latencia
                    });

                var sw = switchServicio.BuscarPorId(id);

                if (sw != null)
                    return Results.Ok(new DispositivoDTO
                    {
                        Id = sw.Id,
                        Nombre = sw.Nombre,
                        Tipo = "Switch",
                        LatenciaMs = sw.Latencia
                    });

                var firewall = firewallServicio.BuscarPorId(id);

                if (firewall != null)
                    return Results.Ok(new DispositivoDTO
                    {
                        Id = firewall.Id,
                        Nombre = firewall.Nombre,
                        Tipo = "Firewall",
                        LatenciaMs = firewall.Latencia
                    });

                var accessPoint = accessPointServicio.BuscarPorId(id);

                if (accessPoint != null)
                    return Results.Ok(new DispositivoDTO
                    {
                        Id = accessPoint.Id,
                        Nombre = accessPoint.Nombre,
                        Tipo = "AccessPoint",
                        LatenciaMs = accessPoint.Latencia
                    });

                return Results.NotFound();
            });


            app.MapDelete("/api/dispositivos/{id:int}", (
                int id,
                IRouterServicio routerServicio,
                ISwitchServicio switchServicio,
                IFirewallServicio firewallServicio,
                IAccessPointServicio accessPointServicio) =>
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
            });

            app.MapPut("/api/dispositivos/{id:int}", (
                int id,
                ActualizarDispositivoRequestDto solicitud,
                IRouterServicio routerServicio,
                ISwitchServicio switchServicio,
                IFirewallServicio firewallServicio,
                IAccessPointServicio accessPointServicio) =>
            {
                if (solicitud.TipoDispositivo.ToLower() == "router")
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
                else if (solicitud.TipoDispositivo.ToLower() == "switch")
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
                else if (solicitud.TipoDispositivo.ToLower() == "firewall")
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
                else if (solicitud.TipoDispositivo.ToLower() == "accesspoint")
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

                return Results.Ok(new DispositivoDTO
                {
                    Id = id,
                    Nombre = solicitud.Nombre,
                    Tipo = solicitud.TipoDispositivo,
                    LatenciaMs = solicitud.Latencia
                });
            });

        }
    }
}
