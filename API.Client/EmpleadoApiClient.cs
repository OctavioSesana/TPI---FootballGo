using DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using EmpleadoDTO = DTOs.Empleado;

namespace API.Clients
{
    public static class EmpleadoApiClient
    {
        private static readonly HttpClient client = new();

        static EmpleadoApiClient()
        {
            client.BaseAddress = new Uri("http://localhost:5292/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);
        }

        public static async Task<EmpleadoDTO> GetAsync(int id)
        {
            try
            {
                var response = await client.GetAsync($"empleados/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var dto = await response.Content.ReadFromJsonAsync<EmpleadoDTO>();
                    if (dto == null) throw new Exception("Respuesta vacía al obtener empleado.");
                    return dto;
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener empleado con Id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener empleado con Id {id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener empleado con Id {id}: {ex.Message}", ex);
            }
        }

        public static async Task<IEnumerable<EmpleadoDTO>> GetAllAsync()
        {
            try
            {
                var response = await client.GetAsync("empleados");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<EmpleadoDTO>>()
                           ?? Enumerable.Empty<EmpleadoDTO>();
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener lista de empleados. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener lista de empleados: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener lista de empleados: {ex.Message}", ex);
            }
        }

        public static async Task<EmpleadoDTO> AddAsync(EmpleadoDTO empleado)
        {
            var response = await client.PostAsJsonAsync("empleados", empleado);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al crear empleado. Status: {response.StatusCode}. {error}");
            }

            var created = await response.Content.ReadFromJsonAsync<EmpleadoDTO>();
            if (created == null) throw new Exception("La API no devolvió el empleado creado.");
            return created;
        }

        public static async Task UpdateAsync(EmpleadoDTO empleado)
        {
            try
            {
                var response = await client.PutAsJsonAsync("empleados", empleado); // PUT sin id en ruta
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar empleado con Id {empleado.IdEmpleado}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al actualizar empleado con Id {empleado.IdEmpleado}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al actualizar empleado con Id {empleado.IdEmpleado}: {ex.Message}", ex);
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                var response = await client.DeleteAsync($"empleados/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al eliminar empleado con Id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al eliminar empleado con Id {id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al eliminar empleado con Id {id}: {ex.Message}", ex);
            }
        }

        public static async Task<IEnumerable<EmpleadoDTO>> GetByCriteriaAsync(string texto)
        {
            try
            {
                var response = await client.GetAsync($"empleados/criteria?texto={Uri.EscapeDataString(texto)}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<EmpleadoDTO>>()
                           ?? Enumerable.Empty<EmpleadoDTO>();
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al buscar empleados. Status: {response.StatusCode}, Detalle: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al buscar empleados: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al buscar empleados: {ex.Message}", ex);
            }
        }
    }
}
