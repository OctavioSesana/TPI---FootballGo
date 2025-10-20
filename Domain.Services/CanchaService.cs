using System;
using System.Collections.Generic;
using Data;
using Domain.Model;

namespace Domain.Services
{
    public class CanchaService
    {
        private readonly ICanchaRepository _repo;

        public CanchaService() : this(new CanchaRepository()) { }
        public CanchaService(ICanchaRepository repo) => _repo = repo;

        public int Crear(int nroCancha, EstadoCancha estado, int tipo, decimal precio)
        {
            if (nroCancha <= 0) throw new ArgumentException("El número de cancha debe ser > 0.");
            if (tipo != 5 && tipo != 7) throw new ArgumentException("El tipo de cancha debe ser 5 o 7.");
            if (precio <= 0) throw new ArgumentException("El precio por hora debe ser > 0.");

            var existenteConMismoNro = _repo.GetByNro(nroCancha);
            if (existenteConMismoNro != null)
                throw new InvalidOperationException("Ya existe una cancha con ese número.");

            var c = new Cancha
            {
                NroCancha = nroCancha,
                EstadoCancha = estado,
                TipoCancha = tipo,
                PrecioPorHora = precio
            };
            return _repo.Insert(c);
        }

        public void Actualizar(int nroCancha, EstadoCancha estado, int tipo, decimal precio)
        {
            if (nroCancha <= 0) throw new ArgumentException("El número de cancha debe ser > 0.");
            if (tipo != 5 && tipo != 7) throw new ArgumentException("El tipo de cancha debe ser 5 o 7.");
            if (precio <= 0) throw new ArgumentException("El precio por hora debe ser > 0.");

            var existente = _repo.GetByNro(nroCancha)
                ?? throw new InvalidOperationException("Cancha no encontrada.");

            var otraConEseNro = _repo.GetByNro(nroCancha);

            existente.NroCancha = nroCancha;
            existente.EstadoCancha = estado;
            existente.TipoCancha = tipo;
            existente.PrecioPorHora = precio;

            _repo.Update(existente);
        }

        public void Eliminar(int nroCancha)
        {
            var existente = _repo.GetByNro(nroCancha)
                ?? throw new InvalidOperationException("La cancha no existe.");

            _repo.Delete(nroCancha);
        }


        public List<Cancha> Listar()
        {
            return _repo.GetAll();
        }
    }
}