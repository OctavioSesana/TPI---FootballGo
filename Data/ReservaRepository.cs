using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Data
{
    public class ReservaRepository
    {
        private FootballGoDbContext CreateContext()
        {
            return new FootballGoDbContext();
        }

        public void Add(Reserva reserva)
        {
            using var context = CreateContext();
            context.Reservas.Add(reserva);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            using var context = CreateContext();
            var reserva = context.Reservas.Find(id);
            if (reserva != null)
            {
                context.Reservas.Remove(reserva);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Reserva? Get(int id)
        {
            using var context = CreateContext();
            return context.Reservas.Find(id);
        }

        public IEnumerable<Reserva> GetAll()
        {
            using var context = CreateContext();
            return context.Reservas.AsNoTracking().ToList();
        }

        public bool Update(Reserva reserva)
        {
            using var context = CreateContext();
            var existingReserva = context.Reservas.Find(reserva.IdReserva);
            if (existingReserva != null)
            {
                existingReserva.SetNroCancha(reserva.NroCancha);
                existingReserva.SetMailUsuario(reserva.mailUsuario);
                existingReserva.SetFechaReserva(reserva.FechaReserva);
                existingReserva.SetHoraInicio(reserva.HoraInicio);
                existingReserva.SetPrecioTotal(reserva.PrecioTotal);

                context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Reserva> GetByEmail(string email)
        {
            using var context = CreateContext();
            return context.Reservas
                .AsNoTracking()
                .Where(r => r.mailUsuario.ToLower() == email.ToLower())
                .ToList();
        }

        public IEnumerable<Reserva> GetByCriteria(string texto)
        {
            const string sql = @"
                SELECT IdReserva, NroCancha, mailUsuario, FechaReserva, HoraInicio, PrecioTotal
                FROM Reservas
                WHERE mailUsuario LIKE @SearchTerm
                   OR CONVERT(VARCHAR, FechaReserva, 103) LIKE @SearchTerm
                   OR CAST(NroCancha AS VARCHAR) LIKE @SearchTerm
                ORDER BY FechaReserva DESC";

            var reservas = new List<Reserva>();
            string connectionString = new FootballGoDbContext().Database.GetConnectionString();
            string searchPattern = $"%{texto}%";

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@SearchTerm", searchPattern);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var reserva = new Reserva(
                    reader.GetInt32(0),           // IdReserva
                    reader.GetInt32(1),           // NroCancha
                    reader.GetString(2),          // mailUsuario
                    reader.GetDateTime(3),        // FechaReserva
                    reader.GetTimeSpan(4),        // HoraInicio
                    reader.GetDecimal(5)          // PrecioTotal
                );

                reservas.Add(reserva);
            }

            return reservas;
        }

        public async Task<decimal> GetTotalGastadoPorEmailAsync(string email)
        {
            using var context = CreateContext();

            return await context.Reservas
                .Where(r => r.mailUsuario.ToLower() == email.ToLower())
                .SumAsync(r => r.PrecioTotal);
        }
    }
}
