
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class RouterEndpoints
    {
        public static void MapRouterEndpoints(this IEndpointRouteBuilder app)
        {
            // GET: Listar todos los routers.
            app.MapGet("/api/routers", ([FromServices] IRouterServicio servicio) =>
            {
                return Results.Ok(servicio.Listar());
            })
            .WithTags("Routers");

            // GET: Buscar un router por su ID.
            app.MapGet("/api/routers/{id:int}", (int id, [FromServices] IRouterServicio servicio) =>
            {
                var router = servicio.ObtenerPorId(id);

                if (router is null)
                    return Results.NotFound();

                return Results.Ok(router);
            })
            .WithTags("Routers");

            // POST: Crear un nuevo router.
            app.MapPost("/api/routers",
                ([FromBody] RouterRequestDto solicitud,
                [FromServices] IRouterServicio servicio) =>
            {
                var router = servicio.Crear(solicitud);

                // Devuelve 201 e indica la ubicación del router creado.
                return Results.Created(
                    $"/api/routers/{router.Id}",
                    router
                );
            })
            .WithTags("Routers");

            // PUT: Actualizar los datos de un router.
            app.MapDelete("/api/routers/{id:int}", (int id, [FromServices] IRouterServicio servicio) =>
            {
                var router = servicio.ObtenerPorId(id);

                if (router is null)
                    return Results.NotFound();

                servicio.Eliminar(id);

                return Results.NoContent();
            })
            .WithTags("Routers");

            app.MapPut("/api/routers/{id:int}", (
                int id,
                [FromBody] ActualizarRouterRequestDto solicitud,
                [FromServices] IRouterServicio servicio) =>
            {
                var router = servicio.ObtenerPorId(id);

                if (router is null)
                    return Results.NotFound();

                servicio.Actualizar(id, solicitud);

                return Results.NoContent();
            })
            .WithTags("Routers");
        }
    }
}
