
using DTOs;
using Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/auth/login", async (LoginRequest request, IConfiguration configuration) =>
            {
                try
                {
                    var authService = new AuthService();

                    var response = await authService.LoginAsync(request);

                    if (response == null)
                    {
                        return Results.Unauthorized();
                    }

                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error durante el login: {ex.Message}");
                }
            })
            .WithName("Login")
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi()
            .AllowAnonymous(); // Este endpoint NO requiere autenticación


            app.MapPost("/auth/register", async (RegisterRequest request) =>
            {
                try
                {
                    var authService = new AuthService(); // tu servicio de dominio

                    var result = await authService.RegisterAsync(request);

                    if (!result.Success) // ajustá a tu contrato
                    {
                        // email duplicado
                        if (result.Code == "EmailExists")
                            return Results.Conflict("Email ya registrado");

                        return Results.BadRequest(result.Message ?? "No se pudo registrar");
                    }

                    return Results.Ok(new RegisterResponse
                    {
                        // lo que devuelvas: Id, Email, Mensaje, etc.
                        // Id = result.UserId, ...
                    });
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error durante el registro: {ex.Message}");
                }
            })
.WithName("Register")
.Produces<RegisterResponse>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status409Conflict)
.Produces(StatusCodes.Status500InternalServerError)
.WithOpenApi()
.AllowAnonymous();

        }
    }
}