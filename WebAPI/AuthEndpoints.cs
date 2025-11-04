
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
        }
    }
}