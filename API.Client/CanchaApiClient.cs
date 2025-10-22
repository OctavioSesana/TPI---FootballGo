using System.Net.Http.Headers;
using System.Net.Http.Json;
using DTOs;
using CanchaDTO = DTOs.Cancha;

namespace API.Clients
{
    public static class CanchaApiClient
    {
        private static readonly HttpClient client = new();

        static CanchaApiClient()
        {
            client.BaseAddress = new Uri("http://localhost:5292/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);
        }

        public static async Task<IEnumerable<CanchaDTO>> GetAllAsync()
        {
            var response = await client.GetAsync("canchas");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<CanchaDTO>>()
                   ?? Enumerable.Empty<CanchaDTO>();
        }

        public static async Task<CanchaDTO?> GetAsync(int id)
        {
            var response = await client.GetAsync($"canchas/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CanchaDTO>();
        }

        public static async Task<CanchaDTO?> GetByNumberAsync(int nro)
        {
            var response = await client.GetAsync($"canchas/nro/{nro}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CanchaDTO>();
        }

        public static async Task<CanchaDTO> AddAsync(CanchaDTO cancha)
        {
            var response = await client.PostAsJsonAsync("canchas", cancha);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<CanchaDTO>();
            return created ?? throw new Exception("La API no devolvió la cancha creada.");
        }

        public static async Task UpdateAsync(CanchaDTO cancha)
        {
            var response = await client.PutAsJsonAsync("canchas", cancha); // ✅ sin id en ruta

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(
                    $"Error al actualizar la cancha N°{cancha.NroCancha}. " +
                    $"Status: {response.StatusCode}. {error}"
                );
            }
        }


        public static async Task DeleteAsync(int id)
        {
            try
            {
                var res = await client.DeleteAsync($"canchas/{id}");
                if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var detail = await res.Content.ReadAsStringAsync();
                    throw new Exception($"La cancha {id} no existe. Detalle: {detail}");
                }

                res.EnsureSuccessStatusCode(); // 2xx = OK
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"No pude conectar con la API al eliminar la cancha {id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al eliminar la cancha {id}: {ex.Message}", ex);
            }
        }

    }
}