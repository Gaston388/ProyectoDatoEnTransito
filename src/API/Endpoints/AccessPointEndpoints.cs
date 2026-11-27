
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class AccessPointEndpoints
    {
        public static void MapAccessPointEndpoints(this IEndpointRouteBuilder app)
        {
            // GET: Obtener todos los Access Points.
            app.MapGet("/api/access-points", ([FromServices] IAccessPointServicio servicio) =>
            {
                return Results.Ok(servicio.Listar());
            })
            .WithTags("Access Points");

            // GET: Buscar un Access Point por su ID.
            app.MapGet("/api/access-points/{id:int}", (int id, [FromServices] IAccessPointServicio servicio) =>
            {
                var accessPoint = servicio.BuscarPorId(id);

                if (accessPoint is null)
                    return Results.NotFound();

                return Results.Ok(accessPoint);
            })
            .WithTags("Access Points");

            // POST: Crear un nuevo Access Point.
            app.MapPost("/api/access-points", ([FromBody] AccessPointRequestDto solicitud,
                [FromServices] IAccessPointServicio servicio) =>
            {
                var accessPoint = servicio.Crear(solicitud);
                // Devuelve 201 e indica la ubicación del recurso creado.
                return Results.Created($"/api/access-points/{accessPoint.Id}", accessPoint);
            })
            .WithTags("Access Points");

            // DELETE: Eliminar un Access Point.
            app.MapDelete("/api/access-points/{id:int}", (int id, [FromServices] IAccessPointServicio servicio) =>
            {
                var accessPoint = servicio.BuscarPorId(id);

                if (accessPoint is null)
                    return Results.NotFound();

                servicio.Eliminar(id);

                return Results.NoContent();
            })
            .WithTags("Access Points");

            // PUT: Actualizar los datos de un Access Point.
            app.MapPut("/api/access-points/{id:int}", (
                int id,
                [FromBody] ActualizarAccessPointRequestDto solicitud,
                [FromServices] IAccessPointServicio servicio) =>
            {
                var accessPoint = servicio.BuscarPorId(id);

                if (accessPoint is null)
                    return Results.NotFound();

                servicio.Actualizar(id, solicitud);

                return Results.NoContent();
            })
            .WithTags("Access Points");
        }
    }
}
