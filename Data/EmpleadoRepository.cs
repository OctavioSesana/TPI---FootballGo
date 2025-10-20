using Domain.Model;
using FootballGo.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Data
{
    public class EmpleadoRepository
    {
        private FootballGoDbContext CreateContext()
        {
            // Acá usás tu propio DbContext en lugar de TPIContext
            return new FootballGoDbContext();
        }

        public void Add(Empleado empleado)
        {
            using var context = CreateContext();
            context.Empleados.Add(empleado);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            using var context = CreateContext();
            var empleado = context.Empleados.Find(id);
            if (empleado != null)
            {
                context.Empleados.Remove(empleado);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Empleado? Get(int IdEmpleado)
        {
            using var context = CreateContext();
            return context.Empleados.Find(IdEmpleado);
        }

        public Empleado? GetByDni(int dni)
        {
            using var context = CreateContext();
            return context.Empleados.FirstOrDefault(e => e.Dni == dni);
        }

        public IEnumerable<Empleado> GetAll()
        {
            using var context = CreateContext();
            return context.Empleados.AsNoTracking().ToList();
        }

        public bool Update(Empleado empleado)
        {
            using var context = CreateContext();
            var existingEmpleado = context.Empleados.Find(empleado.Id);
            if (existingEmpleado != null)
            {
                existingEmpleado.SetNombre(empleado.Nombre);
                existingEmpleado.SetApellido(empleado.Apellido);
                existingEmpleado.SetDni(empleado.Dni);
                existingEmpleado.SetSueldoSemanal(empleado.SueldoSemanal);
                existingEmpleado.SetEstaActivo(empleado.EstaActivo);
                existingEmpleado.SetFechaIngreso(empleado.FechaIngreso);
                existingEmpleado.SetContrasenia(empleado.contrasenia);

                context.SaveChanges();
                return true;
            }
            return false;
        }


        public IEnumerable<Empleado> GetByCriteria(FootballGo.DTOs.EmpleadoCriteria criteria) // Fully qualify the type to resolve ambiguity
        {
            const string sql = @"
                    SELECT IdEmpleado, Nombre, Apellido, Dni, EstaActivo, FechaIngreso, SueldoSemanal, contrasenia
                    FROM Empleados 
                    WHERE Nombre LIKE @SearchTerm 
                       OR Apellido LIKE @SearchTerm 
                       OR Dni LIKE @SearchTerm
                    ORDER BY Nombre, Apellido";

            var empleados = new List<Empleado>();
            string connectionString = new FootballGoDbContext().Database.GetConnectionString();
            string searchPattern = $"%{criteria.Texto}%";

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@SearchTerm", searchPattern);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var empleado = new Empleado(
                    reader.GetInt32(0),    // IdEmpleado
                    reader.GetString(1),   // Nombre
                    reader.GetString(2),   // Apellido
                    reader.GetInt32(8),   // Dni
                    reader.GetDecimal(4),  // SueldoSemanal
                    reader.GetBoolean(5),  // EstaActivo
                    reader.GetDateTime(6),  // FechaIngreso
                    reader.GetString(10)    // contrasenia
                );
            }

            return empleados;
        }
    }
}
