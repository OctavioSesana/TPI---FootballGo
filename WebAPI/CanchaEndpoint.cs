using Domain.Model;
using Domain.Services;
using DTOs;
using Microsoft.AspNetCore.Http;
using CanchaDTO = DTOs.Cancha;

namespace FootballGo.WebAPI
{
    public static class CanchaEndpoints
    {
        public static void MapCanchaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/canchas");

            // ✅ GET /canchas -> lista todas las canchas
            group.MapGet("/", (CanchaService service) =>
            {
                var lista = service.Listar();

                var dtos = lista.Select(c => new CanchaDTO
                {
                    NroCancha = c.NroCancha,
                    Estado = c.EstadoCancha,
                    TipoCancha = c.TipoCancha,
                    PrecioPorHora = c.PrecioPorHora
                });

                return Results.Ok(dtos);
            })
            .WithName("GetCanchas")
            .Produces<IEnumerable<CanchaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // ✅ GET /canchas/{id}
            group.MapGet("/{id:int}", (int id, CanchaService service) =>
            {
                var cancha = service.Listar().FirstOrDefault(c => c.NroCancha == id);

                if (cancha == null)
                    return Results.NotFound($"No existe la cancha con Id {id}.");

                var dto = new CanchaDTO
                {
                    NroCancha = cancha.NroCancha,
                    Estado = cancha.EstadoCancha,
                    TipoCancha = cancha.TipoCancha,
                    PrecioPorHora = cancha.PrecioPorHora
                };

                return Results.Ok(dto);
            })
            .WithName("GetCanchaById")
            .Produces<CanchaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            // ✅ POST /canchas -> crear nueva cancha
            group.MapPost("/", (CanchaDTO dto, CanchaService service) =>
            {
                try
                {
                    var id = service.Crear(
                        dto.NroCancha,
                        dto.Estado,
                        dto.TipoCancha,
                        dto.PrecioPorHora
                    );

                    return Results.Created($"/canchas/{id}", new { Id = id });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(new { error = ex.Message });
                }
            })
            .WithName("CreateCancha")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            // ✅ PUT /canchas -> actualizar cancha existente
            group.MapPut("/", (CanchaDTO dto, CanchaService service) =>
            {
                try
                {
                    service.Actualizar(
                        dto.NroCancha,
                        dto.Estado,
                        dto.TipoCancha,
                        dto.PrecioPorHora
                    );

                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.Conflict(new { error = ex.Message });
                }
            })
            .WithName("UpdateCancha")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();

            // ✅ DELETE /canchas/{id}
            group.MapDelete("/{id:int}", (int id, CanchaService service) =>
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
            .WithName("DeleteCancha")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
