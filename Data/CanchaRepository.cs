using System.Collections.Generic;
using System.Linq;
using Domain.Model;

namespace Data
{
    public interface ICanchaRepository
    {
        int Insert(Cancha c);
        void Update(Cancha c);
        List<Cancha> GetAll();
        Cancha? GetByNro(int nro);
        void Delete(int id);
    }

    public class CanchaRepository : ICanchaRepository
    {
        public int Insert(Cancha c)
        {
            using var db = new FootballGoDbContext();
            db.Canchas.Add(c);
            db.SaveChanges();
            return c.NroCancha;
        }

        public void Update(Cancha c)
        {
            using var db = new FootballGoDbContext();
            db.Canchas.Update(c);
            db.SaveChanges();
        }

        public List<Cancha> GetAll()
        {
            using var db = new FootballGoDbContext();
            return db.Canchas
                     .OrderBy(x => x.NroCancha)
                     .ToList();
        }

        public Cancha? GetById(int nroCancha)
        {
            using var db = new FootballGoDbContext();
            return db.Canchas.FirstOrDefault(x => x.NroCancha == nroCancha);
        }

        public Cancha? GetByNro(int nro)
        {
            using var db = new FootballGoDbContext();
            return db.Canchas.FirstOrDefault(x => x.NroCancha == nro);
        }

        public void Delete(Cancha c)
        {
            using var db = new FootballGoDbContext();
            db.Canchas.Remove(c);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            using var db = new FootballGoDbContext();
            var cancha = db.Canchas.FirstOrDefault(c => c.NroCancha == id);
            if (cancha == null) return;

            db.Canchas.Remove(cancha);
            db.SaveChanges();
        }


    }
}
