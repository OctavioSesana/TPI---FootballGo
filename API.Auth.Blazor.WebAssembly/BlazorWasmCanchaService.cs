using API.Clients;
using DTOs;
using System.Net.Http.Json;

public class CanchaClient : ICanchaClient
{
    private readonly HttpClient _http;


    public CanchaClient(HttpClient http) => _http = http;


    public async Task<IReadOnlyList<Cancha>> GetAllAsync(CancellationToken ct = default)
    {
        var result = await _http.GetFromJsonAsync<List<Cancha>>("canchas", ct);
        return result ?? new List<Cancha>();
    }


    public async Task<IReadOnlyList<TurnoSlotDto>> GetDisponibilidadAsync(int canchaId, DateOnly fecha, CancellationToken ct = default)
    {
        // Ejemplo: GET api/canchas/{id}/disponibilidad?fecha=2025-11-09
        var url = $"canchas/{canchaId}/disponibilidad?fecha={fecha:yyyy-MM-dd}";
        var result = await _http.GetFromJsonAsync<List<TurnoSlotDto>>(url, ct);
        return result ?? new List<TurnoSlotDto>();
    }


    public async Task<bool> ReservarAsync(int canchaId, DateOnly fecha, string horaDesde, string horaHasta, CancellationToken ct = default)
    {
        var payload = new { fecha = fecha.ToString("yyyy-MM-dd"), horaDesde, horaHasta };
        var resp = await _http.PostAsJsonAsync($"canchas/{canchaId}/reservas", payload, ct);
        return resp.IsSuccessStatusCode;
    }
}