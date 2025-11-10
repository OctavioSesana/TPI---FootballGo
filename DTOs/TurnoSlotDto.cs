namespace DTOs;


public class TurnoSlotDto
{
    public string HoraDesde { get; set; } = string.Empty; // "09:00"
    public string HoraHasta { get; set; } = string.Empty; // "10:00"
    public bool Disponible { get; set; }
}