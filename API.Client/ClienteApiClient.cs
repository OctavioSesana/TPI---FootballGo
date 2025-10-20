using DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ClienteDTO = DTOs.Cliente;

namespace API.Clients
{
    public static class ClienteApiClient
    {
        private static readonly HttpClient client = new();

        static ClienteApiClient()
        {
            client.BaseAddress = new Uri("http://localhost:5292/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);
        }

        public static async Task<ClienteDTO> GetAsync(int id)
        {
            try
            {
                var response = await client.GetAsync($"clientes/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var dto = await response.Content.ReadFromJsonAsync<ClienteDTO>();
                    if (dto == null) throw new Exception("Respuesta vacía al obtener cliente.");
                    return dto;
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener cliente con Id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener cliente con Id {id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener cliente con Id {id}: {ex.Message}", ex);
            }
        }

        public static async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            try
            {
                var response = await client.GetAsync("clientes");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ClienteDTO>>()
                           ?? Enumerable.Empty<ClienteDTO>();
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener lista de clientes. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener lista de clientes: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener lista de clientes: {ex.Message}", ex);
            }
        }

        public static async Task<ClienteDTO> AddAsync(ClienteDTO cliente)
        {
            try
            {
                var response = await client.PostAsJsonAsync("clientes", cliente);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al crear cliente. Status: {response.StatusCode}. {error}");
                }

                var created = await response.Content.ReadFromJsonAsync<ClienteDTO>();
                if (created == null) throw new Exception("La API no devolvió el cliente creado.");
                return created;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al crear cliente: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al crear cliente: {ex.Message}", ex);
            }
        }

        public static async Task UpdateAsync(ClienteDTO cliente)
        {
            try
            {
                var response = await client.PutAsJsonAsync("clientes", cliente); // PUT sin id en ruta
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar cliente con Id {cliente.Id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Uso cliente.Id en el mensaje para ser más específico.
                throw new Exception($"Error de conexión al actualizar cliente con Id {cliente.Id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                // Uso cliente.Id en el mensaje para ser más específico.
                throw new Exception($"Timeout al actualizar cliente con Id {cliente.Id}: {ex.Message}", ex);
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                var response = await client.DeleteAsync($"clientes/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al eliminar cliente con Id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al eliminar cliente con Id {id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al eliminar cliente con Id {id}: {ex.Message}", ex);
            }
        }

        public static async Task<IEnumerable<ClienteDTO>> GetByCriteriaAsync(string texto)
        {
            try
            {
                var response = await client.GetAsync($"clientes/criteria?texto={Uri.EscapeDataString(texto)}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ClienteDTO>>()
                           ?? Enumerable.Empty<ClienteDTO>();
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al buscar clientes. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al buscar clientes: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al buscar clientes: {ex.Message}", ex);
            }
        }
    }
}