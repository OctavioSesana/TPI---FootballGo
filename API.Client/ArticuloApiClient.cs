using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Model;

namespace API.Client
{
    public class ArticuloApiClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5292/";

        public ArticuloApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Articulo?> GetArticuloByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Articulo>();
            }
            return null;
        }

        public async Task<IEnumerable<Articulo>?> GetAllArticulosAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Articulo>>();
            }
            return null;
        }

        public async Task<bool> CreateArticuloAsync(Articulo articulo)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, articulo);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateArticuloAsync(int id, Articulo articulo)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", articulo);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteArticuloAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}