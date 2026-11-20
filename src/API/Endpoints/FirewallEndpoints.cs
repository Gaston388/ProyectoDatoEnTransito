using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class FirewallEndpoints
    {
        public static void MapFirewallEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/firewalls", ([FromServices] IFirewallServicio servicio) =>
            {
                return Results.Ok(servicio.Listar());
            });

            app.MapGet("/api/firewalls/{id:int}", (int id, [FromServices] IFirewallServicio servicio) =>
            {
                var firewall = servicio.BuscarPorId(id);

                if (firewall is null)
                    return Results.NotFound();

                return Results.Ok(firewall);
            });

            app.MapPost("/api/firewalls", ([FromBody] FirewallRequestDto solicitud,
                [FromServices] IFirewallServicio servicio) =>
            {
                var firewall = servicio.Crear(solicitud);

                return Results.Created($"/api/firewalls/{firewall.Id}", firewall);
            });

            app.MapDelete("/api/firewalls/{id:int}", (int id, [FromServices] IFirewallServicio servicio) =>
            {
                var firewall = servicio.BuscarPorId(id);

                if (firewall is null)
                    return Results.NotFound();

                servicio.Eliminar(id);

                return Results.NoContent();
            });

            app.MapPut("/api/firewalls/{id:int}", (
                int id,
                [FromBody] ActualizarFirewallRequestDto solicitud,
                [FromServices] IFirewallServicio servicio) =>
            {
                var firewall = servicio.BuscarPorId(id);

                if (firewall is null)
                    return Results.NotFound();

                servicio.Actualizar(id, solicitud);

                return Results.NoContent();
            });
        }
    }
}