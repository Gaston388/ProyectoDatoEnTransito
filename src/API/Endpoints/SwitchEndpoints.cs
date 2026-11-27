
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class SwitchEndpoints
    {
        public static void MapSwitchEndpoints(this IEndpointRouteBuilder app)
        {
            // GET: Listar todos los switches.
            app.MapGet("/api/switches", ([FromServices] ISwitchServicio servicio) =>
            {
                return Results.Ok(servicio.Listar());
            })
            .WithTags("Switches");

            // GET: Buscar un switch por su ID.
            app.MapGet("/api/switches/{id:int}", (int id, [FromServices] ISwitchServicio servicio) =>
            {
                var switchRed = servicio.BuscarPorId(id);

                if (switchRed is null)
                    return Results.NotFound();

                return Results.Ok(switchRed);
            })
            .WithTags("Switches");

            // POST: Crear un nuevo switch.
            app.MapPost("/api/switches", ([FromBody] SwitchRequestDto solicitud,
                [FromServices] ISwitchServicio servicio) =>
            {
                var switchRed = servicio.Crear(solicitud);

                // Devuelve 201 e indica la ubicación del switch creado.
                return Results.Created($"/api/switches/{switchRed.Id}", switchRed);
            })
            .WithTags("Switches");

            // DELETE: Eliminar un switch.
            app.MapDelete("/api/switches/{id:int}", (int id, [FromServices] ISwitchServicio servicio) =>
            {
                var switchRed = servicio.BuscarPorId(id);

                if (switchRed is null)
                    return Results.NotFound();

                servicio.Eliminar(id);

                return Results.NoContent();
            })
            .WithTags("Switches");

            // PUT: Actualizar los datos de un switch.
            app.MapPut("/api/switches/{id:int}", (
                int id,
                [FromBody] ActualizarSwitchRequestDto solicitud,
                [FromServices] ISwitchServicio servicio) =>
            {
                var switchRed = servicio.BuscarPorId(id);

                if (switchRed is null)
                    return Results.NotFound();

                servicio.Actualizar(id, solicitud);

                return Results.NoContent();
            })
            .WithTags("Switches");
        }
    }
}
