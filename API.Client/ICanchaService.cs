using DTOs;


namespace API.Clients;


public interface ICanchaClient
{
    Task<IReadOnlyList<Cancha>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<TurnoSlotDto>> GetDisponibilidadAsync(int canchaId, DateOnly fecha, CancellationToken ct = default);
    Task<bool> ReservarAsync(int canchaId, DateOnly fecha, string horaDesde, string horaHasta, CancellationToken ct = default);
}
