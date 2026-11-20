using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.DTOs;
namespace API.Endpoints
{
    public static class SimulacionEndpoints
    {
        public static void MapSimulacionEndpoints(this IEndpointRouteBuilder app)
        {
            // GET /api/simulaciones
            app.MapGet("/api/simulaciones", ([FromServices] ISimulacionServicio servicio) =>
            {
                var simulaciones = servicio.Listar();
                return Results.Ok(simulaciones);
            });
            // GET /api/simulaciones/{id}
            app.MapGet("/api/simulaciones/{id:int}", (int id, [FromServices] ISimulacionServicio servicio) =>
            {
                var simulacion = servicio.BuscarPorId(id);

                if (simulacion is null)
                    return Results.NotFound();

                return Results.Ok(simulacion);
            });
            // POST /api/simulaciones
           app.MapPost("/api/simulaciones",
                ([FromBody] SimulacionRequestDto solicitud,
                [FromServices] ISimulacionServicio servicio) =>
            {
                var resultado = servicio.CrearSimulacion(solicitud);

                return Results.Created(
                    $"/api/simulaciones/{resultado.Id}",
                    resultado
                );
            });
            // POST /api/simulaciones/{id}/finalizar
            app.MapPost("/api/simulaciones/{id:int}/finalizar",
                (int id, bool exitoso, [FromServices] ISimulacionServicio servicio) =>
            {
                servicio.Finalizar(id, exitoso);

                return Results.Ok(new
                {
                    mensaje = "Simulación finalizada correctamente.",
                    id = id,
                    exitoso = exitoso
                });
            });
        }
    }
}