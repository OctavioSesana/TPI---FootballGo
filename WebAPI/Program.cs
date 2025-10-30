using Data;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using FootballGo.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FootballGoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<CanchaService>();
builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<ArticuloService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});
builder.Services.AddCors(p => p.AddPolicy("AllowAll", b =>
    b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapClienteEndpoints();
app.MapEmpleadoEndpoints();
app.MapCanchaEndpoints();
app.MapReservaEndpoints();
app.MapArticuloEndpoints();

app.Run();
