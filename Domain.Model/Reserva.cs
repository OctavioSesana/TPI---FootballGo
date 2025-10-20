using System;
namespace Domain.Model;

public class Reserva
{
    public int IdReserva { get; private set; }
    public int NroCancha { get; private set; }
    public string mailUsuario { get; private set; }
    public DateTime FechaReserva { get; private set; }
    public TimeSpan HoraInicio { get; set; }
    public decimal PrecioTotal { get; set; }

    public Reserva() { }

    public Reserva(int Id, int NroCancha, string mailUsuario, DateTime FechaReserva, TimeSpan HoraInicio, decimal PrecioTotal)
    {
        SetId(Id);
        SetNroCancha(NroCancha);
        SetMailUsuario(mailUsuario);
        SetFechaReserva(FechaReserva);
        SetHoraInicio(HoraInicio);
        SetPrecioTotal(PrecioTotal);
    }


    public void SetId(int Id) => IdReserva = Id;
    public void SetNroCancha(int NroCancha) => this.NroCancha = NroCancha;

    public void SetMailUsuario(string mail) => mailUsuario = mail ?? throw new ArgumentNullException(nameof(mail));
    public void SetFechaReserva(DateTime FechaReserva) => this.FechaReserva = FechaReserva;
    public void SetHoraInicio(TimeSpan HoraInicio) => this.HoraInicio = HoraInicio;
    public void SetPrecioTotal(decimal PrecioTotal) => this.PrecioTotal = PrecioTotal;
}

