namespace DTOs
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public int NroCancha { get; set; }
        public string mailUsuario { get; set; } = string.Empty;
        public DateTime FechaReserva { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}