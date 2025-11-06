using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DTOs;
using static System.Net.WebRequestMethods;

namespace API.Clients
{
    public class BlazorWasmAuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        // Estado interno del usuario
        private bool _isAuthenticated;
        private string? _token;
        private string? _email;

        // Evento de cambio de autenticación
        public event Action<bool>? AuthenticationStateChanged;

        public BlazorWasmAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // LOGIN
        public async Task<bool> LoginAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", new
            {
                Email = email,
                Password = password
            });

            _isAuthenticated = response.IsSuccessStatusCode;

            if (_isAuthenticated)
            {
                // Ejemplo: el token podría venir en el cuerpo de la respuesta
                _token = await response.Content.ReadAsStringAsync();
                _email = email;
            }

            // Notificar a los suscriptores (componentes Blazor, etc.)
            AuthenticationStateChanged?.Invoke(_isAuthenticated);

            return _isAuthenticated;
        }

        // LOGOUT
        public Task LogoutAsync()
        {
            _isAuthenticated = false;
            _token = null;
            _email = null;

            AuthenticationStateChanged?.Invoke(false);
            return Task.CompletedTask;
        }

        // Devuelve si el usuario está autenticado
        public Task<bool> IsAuthenticatedAsync()
        {
            return Task.FromResult(_isAuthenticated);
        }

        // Devuelve el token actual
        public Task<string?> GetTokenAsync()
        {
            return Task.FromResult(_token);
        }

        // Devuelve el nombre de usuario actual
        public Task<string?> GetUsernameAsync()
        {
            return Task.FromResult(_email);
        }

        // Simula verificación de expiración del token
        public Task CheckTokenExpirationAsync()
        {
            // Podés implementar lógica real más adelante
            return Task.CompletedTask;
        }

        // Simula verificación de permisos
        public Task<bool> HasPermissionAsync(string permission)
        {
            // Podés implementar roles y claims más adelante
            return Task.FromResult(true);
        }

        public async Task<RegisterResponse?> RegisterAsync(RegisterRequest request)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync("auth/register", request);

            if (httpResponse.IsSuccessStatusCode)
                return await httpResponse.Content.ReadFromJsonAsync<RegisterResponse>();

            return null;


        }
    }
}
