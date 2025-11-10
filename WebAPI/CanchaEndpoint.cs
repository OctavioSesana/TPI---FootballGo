using Domain.Model;
using Domain.Services;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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


            // ======================================================
            // 🆕 NUEVOS ENDPOINTS: DISPONIBILIDAD Y RESERVA DE CANCHA
            // ======================================================

            // ✅ GET /canchas/{nro:int}/disponibilidad?fecha=yyyy-MM-dd
            group.MapGet("/{nro:int}/disponibilidad", (
                int nro,
                [FromQuery] DateOnly fecha,          // viene como DateOnly en el query
                ReservaService reservaSrv,
                CanchaService canchaSrv) =>
            {
                // Convertimos DateOnly -> DateTime (00:00)
                var fechaDt = new DateTime(fecha.Year, fecha.Month, fecha.Day);

                var apertura = 9;    // 09:00
                var cierre = 23;   // 23:00

                // Traemos reservas del día para esa cancha
                // Suponiendo que Reserva.FechaReserva es DateTime y HoraInicio es TimeSpan
                var reservasHoras = reservaSrv.Listar()
                    .Where(r => r.NroCancha == nro && r.FechaReserva.Date == fechaDt.Date)
                    .Select(r => r.HoraInicio) // TimeSpan
                    .ToHashSet();

                var slots = new List<TurnoSlotDto>();
                for (var h = apertura; h < cierre; h++)
                {
                    var iniTs = TimeSpan.FromHours(h);         // 18:00 -> TimeSpan
                    var finTs = TimeSpan.FromHours(h + 1);     // 19:00 -> TimeSpan

                    slots.Add(new TurnoSlotDto
                    {
                        HoraDesde = iniTs.ToString(@"hh\:mm", CultureInfo.InvariantCulture),
                        HoraHasta = finTs.ToString(@"hh\:mm", CultureInfo.InvariantCulture),
                        Disponible = !reservasHoras.Contains(iniTs)
                    });
                }

                return Results.Ok(slots);
            })
            .WithName("GetDisponibilidadCancha")
            .Produces<IEnumerable<TurnoSlotDto>>(StatusCodes.Status200OK)
            .WithOpenApi();

            // ✅ POST /canchas/{nro:int}/reservas
            group.MapPost("/{nro:int}/reservas", (
                int nro,
                [FromBody] ReservaRequest req, // Fecha/HoraDesde/HoraHasta vienen como string
                ReservaService reservaSrv,
                CanchaService canchaSrv) =>
            {
                try
                {
                    var cancha = canchaSrv.Listar().FirstOrDefault(c => c.NroCancha == nro);
                    if (cancha is null)
                        return Results.NotFound(new { error = $"No existe la cancha {nro}" });

                    // ---- PARSEOS ----
                    // Fecha: "yyyy-MM-dd" -> DateTime
                    var fechaDt = DateTime.ParseExact(req.Fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    // Horas: "HH:mm" -> TimeSpan
                    var horaIniTs = TimeSpan.ParseExact(req.HoraDesde, @"hh\:mm", CultureInfo.InvariantCulture);
                    // var horaFinTs = TimeSpan.ParseExact(req.HoraHasta, @"hh\:mm", CultureInfo.InvariantCulture); // si lo necesitás

                    // Validación de solape
                    var yaTomado = reservaSrv.Listar().Any(r =>
                        r.NroCancha == nro &&
                        r.FechaReserva.Date == fechaDt.Date &&
                        r.HoraInicio == horaIniTs);

                    if (yaTomado)
                        return Results.Conflict(new { error = "El horario ya fue reservado." });

                    var precio = cancha.PrecioPorHora;

                    // Si más adelante tenés auth, tomalo del token.
                    var mailUsuario = string.IsNullOrWhiteSpace(req.MailUsuario)
                        ? "invitado@local"
                        : req.MailUsuario;

                    var id = reservaSrv.Crear(
                        nro,
                        mailUsuario,
                        fechaDt,
                        horaIniTs,
                        precio
                    );

                    return Results.Created($"/reservas/{id}", new { Id = id });
                }
                catch (FormatException ex)
                {
                    return Results.BadRequest(new { error = $"Formato inválido (usa yyyy-MM-dd y HH:mm): {ex.Message}" });
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
            .WithName("CreateReservaEnCancha")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .WithOpenApi();
        }
    }
}
