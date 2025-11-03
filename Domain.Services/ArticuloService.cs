using Data;
using Domain.Model;
using FootballGo.DTOs;

namespace Domain.Services
{
    public class ArticuloService
    {
        private readonly ArticuloRepository _repo;

        public ArticuloService()
        {
            _repo = new ArticuloRepository();
        }

        public void Add(Articulo articulo) => _repo.Add(articulo);

        public bool Delete(int id) => _repo.Delete(id);

        public Articulo? Get(int id) => _repo.Get(id);

        public IEnumerable<Articulo> GetAll() => _repo.GetAll();

        public bool Update(Articulo articulo) => _repo.Update(articulo);

        public IEnumerable<Articulo> GetByCriteria(FootballGo.DTOs.ArticuloCriteria criteria) => _repo.GetByCriteria(criteria);
    }
}
