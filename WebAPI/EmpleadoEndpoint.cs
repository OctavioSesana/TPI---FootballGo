using Domain.Model;
using Domain.Services;
using DTOs;
using Empleado = Domain.Model.Empleado;
using EmpleadoDTO = DTOs.Empleado;

namespace FootballGo.WebAPI
{
    public static class EmpleadoEndpoints
    {
        public static void MapEmpleadoEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/empleados");

            // 🔹 GET /empleados -> lista completa
            group.MapGet("/", (EmpleadoService service) =>
            {
                var lista = service.GetAll();

                // Mapeo a DTO
                var dtos = lista.Select(e => new EmpleadoDTO
                {
                    IdEmpleado = e.Id,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Dni = e.Dni,
                    SueldoSemanal = e.SueldoSemanal,
                    EstaActivo = e.EstaActivo,
                    FechaIngreso = e.FechaIngreso,
                    Contrasenia = e.contrasenia
                });

                return Results.Ok(dtos);
            });

            // 🔹 GET /empleados/{id}
            group.MapGet("/{id:int}", (int id, EmpleadoService service) =>
            {
                var e = service.Get(id);
                if (e is null) return Results.NotFound();

                var dto = new EmpleadoDTO
                {
                    IdEmpleado = e.Id,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Dni = e.Dni,
                    SueldoSemanal = e.SueldoSemanal,
                    EstaActivo = e.EstaActivo,
                    FechaIngreso = e.FechaIngreso,
                    Contrasenia = e.contrasenia
                };

                return Results.Ok(dto);
            });

            // 🔹 POST /empleados
            group.MapPost("/", (EmpleadoDTO dto, EmpleadoService service) =>
            {
                var nuevo = new Empleado(
                    0,
                    dto.Nombre,
                    dto.Apellido,
                    dto.Dni,
                    dto.SueldoSemanal,
                    dto.EstaActivo,
                    dto.FechaIngreso,
                    dto.Contrasenia
                );

                service.Add(nuevo);
                return Results.Created($"/empleados/{nuevo.Id}", nuevo);
            });

            // 🔹 PUT /empleados
            group.MapPut("/", (EmpleadoDTO dto, EmpleadoService service) =>
            {
                var e = service.Get(dto.IdEmpleado);
                if (e is null) return Results.NotFound();

                e.SetNombre(dto.Nombre);
                e.SetApellido(dto.Apellido);
                e.SetDni(dto.Dni);
                e.SetSueldoSemanal(dto.SueldoSemanal);
                e.SetEstaActivo(dto.EstaActivo);
                e.SetFechaIngreso(dto.FechaIngreso);
                e.SetContrasenia(dto.Contrasenia);

                service.Update(e);
                return Results.NoContent();
            });

            // 🔹 DELETE /empleados/{id}
            group.MapDelete("/{id:int}", (int id, EmpleadoService service) =>
            {
                bool ok = service.Delete(id);
                return ok ? Results.NoContent() : Results.NotFound();
            });

            // 🔹 GET /empleados/criteria?texto=...
            group.MapGet("/criteria", (string texto, EmpleadoService service) =>
            {
                texto = texto?.Trim().ToLower() ?? "";

                var lista = service.GetAll()
                    .Where(e =>
                        e.Nombre.ToLower().Contains(texto) ||
                        e.Apellido.ToLower().Contains(texto) ||
                        e.Dni.ToString().Contains(texto))
                    .ToList();

                var dtos = lista.Select(e => new EmpleadoDTO
                {
                    IdEmpleado = e.Id,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Dni = e.Dni,
                    SueldoSemanal = e.SueldoSemanal,
                    EstaActivo = e.EstaActivo,
                    FechaIngreso = e.FechaIngreso,
                    Contrasenia = e.contrasenia
                });

                return Results.Ok(dtos);
            })
            .WithName("GetEmpleadosByCriteria")
            .Produces<IEnumerable<EmpleadoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();
        }
    }
}
