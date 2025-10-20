using Domain.Model;
using Domain.Services;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaDTO = DTOs.Reserva;

namespace FootballGo.WebAPI
{
    public static class ReservaEndpoints
    {
        public static void MapReservaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/reservas");

            // ✅ GET /reservas -> lista todas las reservas (uso general)
            group.MapGet("/", (ReservaService service) =>
            {
                var lista = service.Listar();

                var dtos = lista.Select(ToDtoReserva);

                return Results.Ok(dtos);
            })
            .WithName("GetAllReservas")
            .Produces<IEnumerable<ReservaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // 🏆 SOLUCIÓN AL ERROR 404: Mapeo para la búsqueda por criterio (GET /reservas/criteria?texto=...)
            group.MapGet("/criteria", ([FromQuery] string texto, ReservaService service) =>
            {
                var lista = service.BuscarPorCriterio(texto ?? string.Empty);

                var dtos = lista.Select(ToDtoReserva);

                return Results.Ok(dtos);
            })
            .WithName("GetReservasByCriteria")
            .Produces<IEnumerable<ReservaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // ✅ GET /reservas/{id}
            group.MapGet("/{id:int}", (int id, ReservaService service) =>
            {
                var reserva = service.Listar().FirstOrDefault(r => r.IdReserva == id);

                if (reserva == null)
                    return Results.NotFound($"No existe la reserva con Id {id}.");

                return Results.Ok(ToDtoReserva(reserva));
            })
            .WithName("GetReservaById")
            .Produces<ReservaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();


            // ✅ POST /reservas -> crear nueva reserva
            group.MapPost("/", (ReservaDTO dto, ReservaService service) =>
            {
                try
                {
                    // Asume que Crear en ReservaService devuelve el Id generado
                    var id = service.Crear(
                        dto.NroCancha,
                        dto.mailUsuario,
                        dto.FechaReserva,
                        dto.HoraInicio,
                        dto.PrecioTotal
                    );

                    return Results.Created($"/reservas/{id}", new { Id = id });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (InvalidOperationException ex)
                {
                    // Generalmente, se usa 409 Conflict o 400 Bad Request
                    return Results.Conflict(new { error = ex.Message });
                }
            })
            .WithName("CreateReserva")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            // ✅ DELETE /reservas/{id}
            group.MapDelete("/{id:int}", (int id, ReservaService service) =>
            {
                try
                {
                    service.Eliminar(id);
                    return Results.NoContent();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
            })
            .WithName("DeleteReserva")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // PUT /reservas no es común si la Reserva es inmutable, pero se incluye por completitud
            // Si necesitas actualizarla, seguirías el patrón de CanchaEndpoint.cs
        }

        // Helper para mapear Domain.Model.Reserva a DTOs.Reserva
        private static ReservaDTO ToDtoReserva(Domain.Model.Reserva r) => new()
        {
            IdReserva = r.IdReserva,
            NroCancha = r.NroCancha,
            mailUsuario = r.mailUsuario,
            FechaReserva = r.FechaReserva,
            HoraInicio = r.HoraInicio,
            PrecioTotal = r.PrecioTotal
        };
    }
}