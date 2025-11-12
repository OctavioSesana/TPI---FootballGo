using Domain.Model;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Cliente = Domain.Model.Cliente;
using ClienteDTO = DTOs.Cliente;

namespace FootballGo.WebAPI
{
    public static class ClienteEndpoints
    {
        public static void MapClienteEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/clientes");

            group.MapGet("/", (ClienteService service) =>
            {
                var list = service.GetAll();
                return Results.Ok(list);
            });

            group.MapGet("/{id:int}", (int id, ClienteService service) =>
            {
                var cliente = service.Get(id);
                return cliente is null ? Results.NotFound() : Results.Ok(cliente);
            });

            // OBTENER CLIENTE POR EMAIL.
            group.MapGet("/by-email", async ([FromQuery] string email, ClienteService svc) =>
            {
                var cli = await svc.GetByEmailAsync(email);   
                return cli is null ? Results.NotFound() : Results.Ok(cli);
            })
            .WithName("GetClienteByEmail")
            .Produces<Cliente>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/", (ClienteDTO dto, ClienteService service) =>
            {
                var nuevo = new Cliente(0, dto.Nombre, dto.Apellido, dto.Email, dto.dni, dto.telefono, dto.FechaAlta, dto.Contrasenia);
                service.Add(nuevo);
                return Results.Created($"/clientes/{nuevo.Id}", nuevo);
            });

            group.MapPut("/", (ClienteDTO dto, ClienteService service) =>
            {
                var c = service.Get(dto.Id);
                if (c is null) return Results.NotFound();
                c.SetNombre(dto.Nombre);
                c.SetApellido(dto.Apellido);
                c.SetEmail(dto.Email);
                c.SetDNI(dto.dni);
                c.SetTelefono(dto.telefono);
                c.SetFechaAlta(dto.FechaAlta);
                c.SetContrasenia(dto.Contrasenia);
                service.Update(c);
                return Results.NoContent();
            });


            // PUT PARA BLAZOR
            group.MapPut("/{id:int}", (int id, ClienteDTO dto, ClienteService service) =>
            {
                var c = service.Get(id);
                if (c is null) return Results.NotFound();
                c.SetNombre(dto.Nombre);
                c.SetApellido(dto.Apellido);
                c.SetEmail(dto.Email);
                c.SetDNI(dto.dni);
                c.SetTelefono(dto.telefono);
                c.SetFechaAlta(dto.FechaAlta);
                c.SetContrasenia(dto.Contrasenia);
                service.Update(c);
                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", (int id, ClienteService service) =>
            {
                bool ok = service.Delete(id);
                return ok ? Results.NoContent() : Results.NotFound();
            });

            group.MapGet("/criteria", (string texto, ClienteService service) =>
            {
                texto = texto?.Trim().ToLower() ?? "";

                var lista = service.GetAll()
                    .Where(c =>
                        c.Nombre.ToLower().Contains(texto) ||
                        c.Apellido.ToLower().Contains(texto) ||
                        c.Email.ToLower().Contains(texto))
                    .ToList();

                return Results.Ok(lista);
            })
            .WithName("GetClientesByCriteria")
            .Produces<IEnumerable<Cliente>>(StatusCodes.Status200OK)
            .WithOpenApi();
        }
    }
}
