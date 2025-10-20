
using Data;
using Domain.Model;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FootballGoDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "API FootballGo funcionando 🚀");

var clientes = app.MapGroup("/clientes");

// GET /clientes  -> lista
clientes.MapGet("/", async (FootballGoDbContext db) =>
{
    var list = await db.Clientes.AsNoTracking().ToListAsync();
    return Results.Ok(list.Select(ToDto));
});

// GET /clientes/{id}
clientes.MapGet("/{id:int}", async (int id, FootballGoDbContext db) =>
{
    var e = await db.Clientes.FindAsync(id);
    return e is null ? Results.NotFound() : Results.Ok(ToDto(e));
});

// GET /clientes/criteria?texto=...
clientes.MapGet("/criteria", async (string texto, FootballGoDbContext db) =>
{
    texto = (texto ?? string.Empty).ToLower();
    var list = await db.Clientes
        .Where(c =>
            c.Nombre.ToLower().Contains(texto) ||
            c.Apellido.ToLower().Contains(texto) ||
            c.Email.ToLower().Contains(texto))
        .AsNoTracking()
        .ToListAsync();

    return Results.Ok(list.Select(ToDto));
});

// POST /clientes
clientes.MapPost("/", async (DTOs.Cliente dto, FootballGoDbContext db) =>
{

    var e = new Domain.Model.Cliente(
        0,
        dto.Nombre,
        dto.Apellido,
        dto.Email,
        dto.dni,
        dto.telefono,
        dto.FechaAlta,
        dto.Contrasenia
    );

    db.Clientes.Add(e);
    await db.SaveChangesAsync();

    return Results.Created($"/clientes/{e.Id}", ToDto(e));
});

// PUT /clientes
clientes.MapPut("/", async (DTOs.Cliente dto, FootballGoDbContext db) =>
{
    if (dto.Id <= 0) return Results.BadRequest("Id inválido.");

    var e = await db.Clientes.FindAsync(dto.Id);
    if (e is null) return Results.NotFound($"No existe cliente Id {dto.Id}");

    Apply(e, dto);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// DELETE /clientes/{id}
clientes.MapDelete("/{id:int}", async (int id, FootballGoDbContext db) =>
{
    var e = await db.Clientes.FindAsync(id);
    if (e is null) return Results.NotFound();
    db.Clientes.Remove(e);
    await db.SaveChangesAsync();
    return Results.NoContent();
});


static DTOs.Cliente ToDto(Domain.Model.Cliente c) => new()
{
    Id = c.Id,
    Nombre = c.Nombre,
    Apellido = c.Apellido,
    Email = c.Email,
    dni = c.dni,
    telefono = c.telefono,
    FechaAlta = c.FechaAlta,
    Contrasenia = c.Contrasenia
};

static void Apply(Domain.Model.Cliente e, DTOs.Cliente d)
{
    e.SetNombre(d.Nombre);
    e.SetApellido(d.Apellido);
    e.SetEmail(d.Email);
    e.SetDNI(d.dni);
    e.SetTelefono(d.telefono);
    e.SetFechaAlta(d.FechaAlta);
    e.SetContrasenia(d.Contrasenia);
}

var empleados = app.MapGroup("/empleados");

// GET /empleados -> lista
empleados.MapGet("/", async (FootballGoDbContext db) =>
{
    var list = await db.Empleados.AsNoTracking().ToListAsync();
    return Results.Ok(list.Select(ToDtoEmpleado));
});

// GET /empleados/{id}
empleados.MapGet("/{id:int}", async (int id, FootballGoDbContext db) =>
{
    var e = await db.Empleados.FindAsync(id);
    return e is null ? Results.NotFound() : Results.Ok(ToDtoEmpleado(e));
});

// GET /empleados/criteria?texto=...
empleados.MapGet("/criteria", async (string texto, FootballGoDbContext db) =>
{
    texto = (texto ?? string.Empty).ToLower();
    var list = await db.Empleados
        .Where(e =>
            e.Nombre.ToLower().Contains(texto) ||
            e.Apellido.ToLower().Contains(texto) ||
            e.Dni.ToString().Contains(texto))
        .AsNoTracking()
        .ToListAsync();

    return Results.Ok(list.Select(ToDtoEmpleado));
});

// POST /empleados
empleados.MapPost("/", async (DTOs.Empleado dto, FootballGoDbContext db) =>
{
    var e = new Domain.Model.Empleado(
        0,
        dto.Nombre,
        dto.Apellido,
        dto.Dni,
        dto.SueldoSemanal,
        dto.EstaActivo,
        dto.FechaIngreso,
        dto.Contrasenia
    );

    db.Empleados.Add(e);
    await db.SaveChangesAsync();

    return Results.Created($"/empleados/{e.Id}", ToDtoEmpleado(e));
});

// PUT /empleados
empleados.MapPut("/", async (DTOs.Empleado dto, FootballGoDbContext db) =>
{
    if (dto.IdEmpleado <= 0) return Results.BadRequest("Id inválido.");

    var e = await db.Empleados.FindAsync(dto.IdEmpleado);
    if (e is null) return Results.NotFound($"No existe empleado Id {dto.IdEmpleado}");

    ApplyEmpleado(e, dto);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// DELETE /empleados/{id}
empleados.MapDelete("/{id:int}", async (int id, FootballGoDbContext db) =>
{
    var e = await db.Empleados.FindAsync(id);
    if (e is null) return Results.NotFound();
    db.Empleados.Remove(e);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

static DTOs.Empleado ToDtoEmpleado(Domain.Model.Empleado e) => new()
{
    IdEmpleado = e.Id,
    Nombre = e.Nombre,
    Apellido = e.Apellido,
    Dni = e.Dni,
    SueldoSemanal = e.SueldoSemanal,
    EstaActivo = e.EstaActivo,
    FechaIngreso = e.FechaIngreso,
    Contrasenia = e.contrasenia,
};

static void ApplyEmpleado(Domain.Model.Empleado e, DTOs.Empleado d)
{
    e.SetNombre(d.Nombre);
    e.SetApellido(d.Apellido);
    e.SetDni(d.Dni);
    e.SetSueldoSemanal(d.SueldoSemanal);
    e.SetEstaActivo(d.EstaActivo);
    e.SetFechaIngreso(d.FechaIngreso);
    e.SetContrasenia(d.Contrasenia);


}

// ===================== CANCHAS =====================
var canchas = app.MapGroup("/canchas");

// GET /api/canchas  -> lista
canchas.MapGet("", async (FootballGoDbContext db) =>
{
    var list = await db.Canchas.AsNoTracking().ToListAsync();
    return Results.Ok(list.Select(ToDtoCancha));
});

// GET /api/canchas/{id}
canchas.MapGet("/{id:int}", async (int id, FootballGoDbContext db) =>
{
    var e = await db.Canchas.FindAsync(id);
    return e is null ? Results.NotFound() : Results.Ok(ToDtoCancha(e));
});

// GET /api/canchas/nro/{nro}
canchas.MapGet("/nro/{nro:int}", async (int nro, FootballGoDbContext db) =>
{
    var e = await db.Canchas.AsNoTracking().FirstOrDefaultAsync(x => x.NroCancha == nro);
    return e is null ? Results.NotFound() : Results.Ok(ToDtoCancha(e));
});

// POST /api/canchas
canchas.MapPost("", async (DTOs.Cancha dto, FootballGoDbContext db) =>
{
    if (dto.NroCancha <= 0) return Results.BadRequest("Número de cancha inválido.");
    if (dto.TipoCancha != 5 && dto.TipoCancha != 7) return Results.BadRequest("El tipo de cancha debe ser 5 o 7.");
    if (dto.PrecioPorHora <= 0) return Results.BadRequest("Precio por hora inválido.");

    var existeNro = await db.Canchas.AnyAsync(c => c.NroCancha == dto.NroCancha);
    if (existeNro) return Results.Conflict("Ya existe una cancha con ese número.");

    var e = new Domain.Model.Cancha
    {
        NroCancha = dto.NroCancha,
        EstadoCancha = dto.Estado,
        TipoCancha = dto.TipoCancha,
        PrecioPorHora = dto.PrecioPorHora
    };

    db.Canchas.Add(e);
    await db.SaveChangesAsync();

    return Results.Created($"/api/canchas/{e.NroCancha}", ToDtoCancha(e));
});

// PUT /api/canchas
canchas.MapPut("", async (DTOs.Cancha dto, FootballGoDbContext db) =>
{
    if (dto.NroCancha <= 0) return Results.BadRequest("Id inválido.");

    var e = await db.Canchas.FindAsync(dto.NroCancha);
    if (e is null) return Results.NotFound($"No existe cancha Id {dto.NroCancha}");

    var otraConEseNro = await db.Canchas
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.NroCancha == dto.NroCancha && c.NroCancha != dto.NroCancha);
    if (otraConEseNro != null) return Results.Conflict("Ya existe otra cancha con ese número.");

    e.NroCancha = dto.NroCancha;
    e.EstadoCancha = dto.Estado;
    e.TipoCancha = dto.TipoCancha;
    e.PrecioPorHora = dto.PrecioPorHora;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// DELETE /api/canchas/{id}
canchas.MapDelete("/{id:int}", async (int id, FootballGoDbContext db) =>
{
    var e = await db.Canchas.FindAsync(id);
    if (e is null) return Results.NotFound();
    db.Canchas.Remove(e);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

static DTOs.Cancha ToDtoCancha(Domain.Model.Cancha c) => new()
{
    NroCancha = c.NroCancha,
    Estado = c.EstadoCancha,
    TipoCancha = c.TipoCancha,
    PrecioPorHora = c.PrecioPorHora
};

// ===================== RESERVAS =====================
var reservas = app.MapGroup("/reservas");

reservas.MapGet("/criteria", async (string texto, FootballGoDbContext db) =>
{
    texto = (texto ?? string.Empty).ToLower();

    var list = await db.Reservas
        .Where(r => r.mailUsuario.ToLower().Contains(texto))
        .AsNoTracking()
        .ToListAsync();

    return Results.Ok(list.Select(ToDtoReserva));
});



reservas.MapDelete("/{id:int}", async (int id, FootballGoDbContext db) =>
{
    var e = await db.Reservas.FindAsync(id);
    if (e is null) return Results.NotFound();


    db.Reservas.Remove(e);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

reservas.MapGet("/{id:int}", async (int id, FootballGoDbContext db) =>
{
    var e = await db.Reservas.AsNoTracking().FirstOrDefaultAsync(r => r.IdReserva == id);
    return e is null ? Results.NotFound() : Results.Ok(ToDtoReserva(e));
});

static DTOs.Reserva ToDtoReserva(Domain.Model.Reserva r) => new()
{
    IdReserva = r.IdReserva,
    NroCancha = r.NroCancha,
    mailUsuario = r.mailUsuario,
    FechaReserva = r.FechaReserva,
    HoraInicio = r.HoraInicio,
    PrecioTotal = r.PrecioTotal
};

app.Run();
