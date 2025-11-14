using System;
using System.Collections.Generic;
using Data;
using Domain.Model;

namespace Domain.Services
{
    public class ReservaService
    {
        private readonly ReservaRepository _repo;

        public ReservaService() : this(new ReservaRepository()) { }
        public ReservaService(ReservaRepository repo) => _repo = repo;

        public int Crear(int nroCancha, string mailUsuario, DateTime fechaReserva, TimeSpan horaInicio, decimal precioTotal)
        {
            if (nroCancha <= 0) throw new ArgumentException("El Id de cancha debe ser válido.");
            if (string.IsNullOrWhiteSpace(mailUsuario)) throw new ArgumentException("El mail del usuario es obligatorio.");
            if (fechaReserva.Date < DateTime.Today) throw new ArgumentException("La fecha de reserva no puede ser anterior a hoy.");
            if (precioTotal <= 0) throw new ArgumentException("El precio total debe ser mayor a cero.");

            var reservasExistentes = _repo.GetAll();
            foreach (var r in reservasExistentes)
            {
                if (r.NroCancha == nroCancha && r.FechaReserva.Date == fechaReserva.Date && r.HoraInicio == horaInicio)
                    throw new InvalidOperationException("Ya existe una reserva para esa cancha en ese horario.");
            }

            var reserva = new Reserva(
                Id: 0,
                NroCancha: nroCancha,
                mailUsuario: mailUsuario,
                FechaReserva: fechaReserva,
                HoraInicio: horaInicio,
                PrecioTotal: precioTotal
            );

            _repo.Add(reserva);
            return reserva.IdReserva;
        }

        public void Actualizar(int idReserva, int idCancha, string mailUsuario, DateTime fechaReserva, TimeSpan horaInicio, decimal precioTotal, int NroCancha)
        {
            if (idCancha <= 0) throw new ArgumentException("El Id de cancha debe ser válido.");
            if (string.IsNullOrWhiteSpace(mailUsuario)) throw new ArgumentException("El mail del usuario es obligatorio.");
            if (fechaReserva.Date < DateTime.Today) throw new ArgumentException("La fecha de reserva no puede ser anterior a hoy.");
            if (precioTotal <= 0) throw new ArgumentException("El precio total debe ser mayor a cero.");

            var existente = _repo.Get(idReserva)
                ?? throw new InvalidOperationException("Reserva no encontrada.");

            existente.SetNroCancha(NroCancha);
            existente.SetMailUsuario(mailUsuario);
            existente.SetFechaReserva(fechaReserva);
            existente.SetHoraInicio(horaInicio);
            existente.SetPrecioTotal(precioTotal);
            _repo.Update(existente);
        }

        public void Eliminar(int idReserva)
        {
            var existente = _repo.Get(idReserva)
                ?? throw new InvalidOperationException("La reserva no existe.");

            _repo.Delete(idReserva);
        }

        public List<Reserva> Listar()
        {
            return new List<Reserva>(_repo.GetAll());
        }

        public List<Reserva> ListarPorUsuario(string mailUsuario)
        {
            if (string.IsNullOrWhiteSpace(mailUsuario))
                throw new ArgumentException("El mail del usuario es obligatorio.");

            return new List<Reserva>(_repo.GetByEmail(mailUsuario));
        }

        public List<Reserva> BuscarPorCriterio(string texto)
        {
            return new List<Reserva>(_repo.GetByCriteria(texto));
        }

        public Task<IEnumerable<Reserva>> GetByClienteEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El mail del usuario es obligatorio.", nameof(email));

            var result = _repo.GetByEmail(email) ?? Enumerable.Empty<Reserva>();
            return Task.FromResult<IEnumerable<Reserva>>(result);
        }

        public Task<decimal> GetTotalGastadoPorEmailAsync(string email)
            => _repo.GetTotalGastadoPorEmailAsync(email);
    }
}
