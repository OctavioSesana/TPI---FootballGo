using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class BlazorWasmAuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public BlazorWasmAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", new
            {
                Username = username,
                Password = password
            });

            return response.IsSuccessStatusCode;
        }
    }
}
