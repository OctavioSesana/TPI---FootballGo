using System;
using System.Net.Http.Json;
using DTOs;
using Microsoft.JSInterop;

namespace API.Clients
{
    public class ClienteClient : IClienteClient
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public ClienteClient(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        private async Task AddAuthAsync(HttpRequestMessage req)
        {
            // si usás JWT:
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
            token = token?.Replace("\"", "");
            if (!string.IsNullOrWhiteSpace(token))
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<Cliente?> GetByEmailAsync(string email)
        {
            var url = $"/clientes/by-email?email={Uri.EscapeDataString(email)}";
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            await AddAuthAsync(req);
            var res = await _http.SendAsync(req);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<Cliente>();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, $"/clientes/{id}");
            await AddAuthAsync(req);
            var res = await _http.SendAsync(req);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<Cliente>();
        }

        public async Task<bool> UpdateAsync(int id, object body)
        {
            
            var req = new HttpRequestMessage(HttpMethod.Put, $"/clientes/{id}")
            {
                Content = JsonContent.Create(body)
            };
            await AddAuthAsync(req);
            var res = await _http.SendAsync(req);

           
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, $"/clientes/{id}");
            await AddAuthAsync(req);
            var res = await _http.SendAsync(req);
            return res.IsSuccessStatusCode;
        }
    }
}
