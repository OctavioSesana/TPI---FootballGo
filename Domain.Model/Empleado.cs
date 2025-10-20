namespace Domain.Model
{
    public class Empleado
    {
        public int IdEmpleado { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public int Dni { get; private set; }
        public decimal SueldoSemanal { get; private set; }
        public bool EstaActivo { get; private set; }
        public DateTime FechaIngreso { get; private set; }
        public string contrasenia { get; private set; }

        // 🔹 Alias para que la UI pueda usar .Id como en Cliente
        public int Id => IdEmpleado;

        // 🔹 Constructor vacío (requerido para crear objetos sin parámetros)
        public Empleado() { }

        // 🔹 Constructor completo (como el que ya tenías)
        public Empleado(int idEmpleado, string nombre, string apellido, int dni, decimal sueldoSemanal, bool estaActivo, DateTime fechaIngreso, string contra)
        {
            IdEmpleado = idEmpleado;
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            SueldoSemanal = sueldoSemanal;
            EstaActivo = estaActivo;
            FechaIngreso = fechaIngreso;
            contrasenia = contra;
        }

        // 🔹 Setters compatibles con Cliente
        public void SetId(int id) => IdEmpleado = id;  // Nombre igual al usado en Cliente
        public void SetIdEmpleado(int id) => IdEmpleado = id;
        public void SetNombre(string nombre) => Nombre = nombre;
        public void SetApellido(string apellido) => Apellido = apellido;
        public void SetDni(int dni) => Dni = dni;
        public void SetSueldoSemanal(decimal sueldo) => SueldoSemanal = sueldo;
        public void SetEstaActivo(bool activo) => EstaActivo = activo;
        public void SetFechaIngreso(DateTime fecha) => FechaIngreso = fecha;
        public void SetContrasenia(string contra) => contrasenia = contra;
    }
}
