using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;          // 👈 Importante
using DTOs;

namespace API.Clients
{
    public class BlazorWasmAuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        // Estado interno del usuario (cache en memoria)
        private bool _isAuthenticated;
        private string? _token;
        private string? _email;

        // Control de carga perezosa desde LocalStorage
        private bool _loadedFromStorage;

        // Claves de LocalStorage
        private const string TokenKey = "authToken";
        private const string EmailKey = "clienteEmail";

        // Evento de cambio de autenticación
        public event Action<bool>? AuthenticationStateChanged;

        public BlazorWasmAuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        /// <summary>
        /// Carga en memoria el estado guardado en LocalStorage (una sola vez).
        /// </summary>
        private async Task EnsureLoadedAsync()
        {
            if (_loadedFromStorage) return;

            _token = await _localStorage.GetItemAsync<string>(TokenKey);
            _email = await _localStorage.GetItemAsync<string>(EmailKey);
            _isAuthenticated = !string.IsNullOrWhiteSpace(_token);

            _loadedFromStorage = true;
        }

        // LOGIN
        public async Task<bool> LoginAsync(string email, string password)
        {
            Console.WriteLine($"✅ Login exitoso. Token guardado: {_token}");
            Console.WriteLine($"✅ Email guardado: {_email}");

            var response = await _httpClient.PostAsJsonAsync("auth/login", new
            {
                Email = email,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
            {
                _isAuthenticated = false;
                _token = null;
                _email = null;
                AuthenticationStateChanged?.Invoke(false);
                return false;
            }

            // Si tu API devuelve un JSON tipo { token: "...", email: "..." }
            // usá el DTO adecuado. Si no, mantené el ReadAsStringAsync().
            string? token;
            try
            {
                var loginDto = await response.Content.ReadFromJsonAsync<LoginResponse>();
                token = loginDto?.Token ?? await response.Content.ReadAsStringAsync();
            }
            catch
            {
                token = await response.Content.ReadAsStringAsync();
            }

            _token = string.IsNullOrWhiteSpace(token) ? null : token;
            _email = email;
            _isAuthenticated = !string.IsNullOrWhiteSpace(_token);

            // Persistir sesión
            if (_isAuthenticated)
            {
                await _localStorage.SetItemAsync(TokenKey, _token);
                await _localStorage.SetItemAsync(EmailKey, _email);
            }
            else
            {
                await _localStorage.RemoveItemAsync(TokenKey);
                await _localStorage.RemoveItemAsync(EmailKey);
            }

            _loadedFromStorage = true;
            AuthenticationStateChanged?.Invoke(_isAuthenticated);
            return _isAuthenticated;
        }

        // LOGOUT
        public async Task LogoutAsync()
        {
            _isAuthenticated = false;
            _token = null;
            _email = null;

            await _localStorage.RemoveItemAsync(TokenKey);
            await _localStorage.RemoveItemAsync(EmailKey);

            AuthenticationStateChanged?.Invoke(false);
        }

        // Devuelve si el usuario está autenticado
        public async Task<bool> IsAuthenticatedAsync()
        {
            await EnsureLoadedAsync();
            return _isAuthenticated;
        }

        // Devuelve el token actual
        public async Task<string?> GetTokenAsync()
        {
            await EnsureLoadedAsync();
            return _token;
        }

        // Devuelve el nombre de usuario (email) actual
        public async Task<string?> GetUsernameAsync()
        {
            await EnsureLoadedAsync();
            return _email;
        }

        // Simula verificación de expiración del token
        public Task CheckTokenExpirationAsync()
        {
            // Podés implementar lógica real más adelante (validar JWT, expiración, refresh, etc.)
            return Task.CompletedTask;
        }

        // Simula verificación de permisos
        public Task<bool> HasPermissionAsync(string permission)
        {
            // Implementar roles/claims cuando tengas backend con autorización fina
            return Task.FromResult(true);
        }

        // Registro
        public async Task<RegisterResponse?> RegisterAsync(RegisterRequest request)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync("auth/register", request);

            if (httpResponse.IsSuccessStatusCode)
                return await httpResponse.Content.ReadFromJsonAsync<RegisterResponse>();

            return null;
        }
    }
}
