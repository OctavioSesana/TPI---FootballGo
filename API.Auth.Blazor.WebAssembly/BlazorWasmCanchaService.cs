using API.Clients;
using DTOs;
using System.Net.Http.Json;
using System.Net.Http.Headers;

public class CanchaClient : ICanchaClient
{
    private readonly HttpClient _http;
    private readonly IAuthService _authService;

    public CanchaClient(HttpClient http, IAuthService authService)
    {
        _http = http;
        _authService = authService;
    }

    // Método auxiliar para agregar el token en cada request
    private async Task AgregarTokenAsync()
    {
        var token = await _authService.GetTokenAsync();

        // Evitar duplicar encabezado si ya está presente
        if (!string.IsNullOrWhiteSpace(token))
        {
            if (!_http.DefaultRequestHeaders.Contains("Authorization"))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                Console.WriteLine($"🔐 Token agregado al header: {token.Substring(0, Math.Min(token.Length, 25))}...");
            }
        }
        else
        {
            Console.WriteLine("⚠️ No se encontró token en LocalStorage. Se enviará como invitado.");
        }
    }

    public async Task<IReadOnlyList<Cancha>> GetAllAsync(CancellationToken ct = default)
    {
        await AgregarTokenAsync();

        var result = await _http.GetFromJsonAsync<List<Cancha>>("canchas", ct);
        return result ?? new List<Cancha>();
    }

    public async Task<IReadOnlyList<TurnoSlotDto>> GetDisponibilidadAsync(int canchaId, DateOnly fecha, CancellationToken ct = default)
    {
        await AgregarTokenAsync();

        var url = $"canchas/{canchaId}/disponibilidad?fecha={fecha:yyyy-MM-dd}";
        var result = await _http.GetFromJsonAsync<List<TurnoSlotDto>>(url, ct);
        return result ?? new List<TurnoSlotDto>();
    }

    public async Task<bool> ReservarAsync(int canchaId, DateOnly fecha, string horaDesde, string horaHasta, CancellationToken ct = default)
    {
        await AgregarTokenAsync();

        var payload = new
        {
            fecha = fecha.ToString("yyyy-MM-dd"),
            horaDesde,
            horaHasta
        };

        var resp = await _http.PostAsJsonAsync($"canchas/{canchaId}/reservas", payload, ct);

        if (resp.IsSuccessStatusCode)
        {
            Console.WriteLine("✅ Reserva enviada correctamente con token de cliente.");
            return true;
        }

        Console.WriteLine($"❌ Error al reservar. StatusCode: {resp.StatusCode}");
        return false;
    }

    // REPORTE: total gastado en todo el historial del cliente (por Email)
    public async Task<decimal> GetTotalGastadoPorEmailAsync(string email)
    {
        await AgregarTokenAsync();

        var url = $"clientes/total-gastado?email={Uri.EscapeDataString(email)}";

        var resp = await _http.GetFromJsonAsync<TotalGastadoResponse>(url);

        return resp?.TotalGastado ?? 0m;
    }

}
