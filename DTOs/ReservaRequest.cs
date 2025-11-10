namespace DTOs
{
    public class ReservaRequest
    {
        
        public string Fecha { get; set; } = "";
        public string HoraDesde { get; set; } = "";
        public string HoraHasta { get; set; } = "";
        public string? MailUsuario { get; set; }
    }
}
