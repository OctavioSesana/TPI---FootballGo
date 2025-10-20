using System.Text.RegularExpressions;

namespace DTOs
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public decimal SueldoSemanal { get; set; }
        public bool EstaActivo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Contrasenia { get; set; } // Agregado para manejar la contraseña
    }
}
