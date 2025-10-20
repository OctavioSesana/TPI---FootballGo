using System.Text.Json.Serialization;
using Domain.Model;

namespace DTOs
{
    public class Cancha
    {
        public int NroCancha { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EstadoCancha Estado { get; set; }

        public int TipoCancha { get; set; }
        public decimal PrecioPorHora { get; set; }
    }
}