
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class SwitchEndpoints
    {
        public static void MapSwitchEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/switches", ([FromServices] ISwitchServicio servicio) =>
            {
                return Results.Ok(servicio.Listar());
            })
            .WithTags("Switches");

            app.MapGet("/api/switches/{id:int}", (int id, [FromServices] ISwitchServicio servicio) =>
            {
                var switchRed = servicio.BuscarPorId(id);

                if (switchRed is null)
                    return Results.NotFound();

                return Results.Ok(switchRed);
            })
            .WithTags("Switches");

            app.MapPost("/api/switches", ([FromBody] SwitchRequestDto solicitud,
                [FromServices] ISwitchServicio servicio) =>
            {
                var switchRed = servicio.Crear(solicitud);

                return Results.Created($"/api/switches/{switchRed.Id}", switchRed);
            })
            .WithTags("Switches");

            app.MapDelete("/api/switches/{id:int}", (int id, [FromServices] ISwitchServicio servicio) =>
            {
                var switchRed = servicio.BuscarPorId(id);

                if (switchRed is null)
                    return Results.NotFound();

                servicio.Eliminar(id);

                return Results.NoContent();
            })
            .WithTags("Switches");

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
