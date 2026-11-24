
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.DTOs;

namespace API.Endpoints
{
    public static class SimulacionEndpoints
    {
        public static void MapSimulacionEndpoints(this IEndpointRouteBuilder app)
        {
            // GET: Listar todas las simulaciones
            app.MapGet("/api/simulaciones", ([FromServices] ISimulacionServicio servicio) =>
            {
                var simulaciones = servicio.Listar();
                return Results.Ok(simulaciones);
            })
            .WithTags("Simulaciones");

            // GET: Buscar una simulación por ID
            app.MapGet("/api/simulaciones/{id:int}", (int id, [FromServices] ISimulacionServicio servicio) =>
            {
                var simulacion = servicio.BuscarPorId(id);

                if (simulacion is null)
                    return Results.NotFound();

                return Results.Ok(simulacion);
            })
            .WithTags("Simulaciones");

            // POST: Crear una simulación
            app.MapPost("/api/simulaciones",
                ([FromBody] SimulacionRequestDto solicitud,
                [FromServices] ISimulacionServicio servicio) =>
            {
                var resultado = servicio.CrearSimulacion(solicitud);

                return Results.Created(
                    $"/api/simulaciones/{resultado.Id}",
                    resultado
                );
            })
            .WithTags("Simulaciones");

            // DELETE: Eliminar una simulación
            app.MapDelete("/api/simulaciones/{id:int}",
                (int id, [FromServices] ISimulacionServicio servicio) =>
            {
                if (servicio.BuscarPorId(id) == null)
                    return Results.NotFound();

                servicio.Eliminar(id);

                return Results.NoContent();
            })
            .WithTags("Simulaciones");

            // PUT: Actualizar una simulación
            app.MapPut("/api/simulaciones/{id:int}",
                (int id,
                [FromBody] SimulacionRequestDto solicitud,
                [FromServices] ISimulacionServicio servicio) =>
            {
                var resultado = servicio.ActualizarSimulacion(id, solicitud);

                return Results.Ok(resultado);
            })
            .WithTags("Simulaciones");

            // PUT: Actualizar un paso del recorrido
            app.MapPut("/api/simulaciones/{id:int}/pasos/{orden:int}",
                (int id,
                int orden,
                [FromBody] ActualizarPasoRequestDto solicitud,
                [FromServices] ISimulacionServicio servicio) =>
            {
                var resultado = servicio.ActualizarPaso(id, orden, solicitud);

                return Results.Ok(resultado);
            })
            .WithTags("Simulaciones");
        }
    }
}
