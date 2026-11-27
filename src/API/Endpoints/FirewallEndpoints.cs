
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class FirewallEndpoints
    {
        public static void MapFirewallEndpoints(this IEndpointRouteBuilder app)
        {
            // GET: Listar todos los firewalls.
            app.MapGet("/api/firewalls", ([FromServices] IFirewallServicio servicio) =>
            {
                return Results.Ok(servicio.Listar());
            })
            .WithTags("Firewalls");

            // GET: Buscar un firewall por su ID.
            app.MapGet("/api/firewalls/{id:int}", (int id, [FromServices] IFirewallServicio servicio) =>
            {
                var firewall = servicio.BuscarPorId(id);

                if (firewall is null)
                    return Results.NotFound();

                return Results.Ok(firewall);
            })
            .WithTags("Firewalls");

            // POST: Crear un nuevo firewall.
            app.MapPost("/api/firewalls", ([FromBody] FirewallRequestDto solicitud,
                [FromServices] IFirewallServicio servicio) =>
            {
                var firewall = servicio.Crear(solicitud);
                // Devuelve 201 e indica la ubicación del firewall creado.
                return Results.Created($"/api/firewalls/{firewall.Id}", firewall);
            })
            .WithTags("Firewalls");

            // DELETE: Eliminar un firewall.
            app.MapDelete("/api/firewalls/{id:int}", (int id, [FromServices] IFirewallServicio servicio) =>
            {
                var firewall = servicio.BuscarPorId(id);

                if (firewall is null)
                    return Results.NotFound();

                servicio.Eliminar(id);

                return Results.NoContent();
            })
            .WithTags("Firewalls");

            // PUT: Actualizar los datos de un firewall.
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
            })
            .WithTags("Firewalls");
        }
    }
}
