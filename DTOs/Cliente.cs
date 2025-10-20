using System.Text.RegularExpressions;

namespace DTOs
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int dni { get; set; }
        public int telefono { get; set; }

        public DateTime FechaAlta { get; set; }
        public string Contrasenia { get; set; }

    }
}