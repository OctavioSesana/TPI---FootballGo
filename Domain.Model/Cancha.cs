namespace Domain.Model
{
    public enum EstadoCancha { Disponible, Mantenimiento, Ocupada }

    public class Cancha
    {
        public int NroCancha { get; set; }
        public EstadoCancha EstadoCancha { get; set; }
        public int TipoCancha { get; set; }       // 5 o 7
        public decimal PrecioPorHora { get; set; }
    }
}
