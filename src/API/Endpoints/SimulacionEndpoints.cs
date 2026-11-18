using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        }
    }
}