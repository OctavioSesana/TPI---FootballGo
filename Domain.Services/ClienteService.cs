using Data;
using Domain.Model;
using FootballGo.DTOs;

namespace Domain.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _repo;

        public ClienteService()
        {
            _repo = new ClienteRepository();
        }

        public Cliente? Login(string email, string contrasenia)
        {
            var cliente = _repo.GetByEmail(email);

            if (cliente == null)
                return null;

            return cliente.Contrasenia == contrasenia ? cliente : null;
        }


        public void Add(Cliente cliente) => _repo.Add(cliente);

        public bool Delete(int id) => _repo.Delete(id);

        public Cliente? Get(int id) => _repo.Get(id);

        public IEnumerable<Cliente> GetAll() => _repo.GetAll();

        public bool Update(Cliente cliente) => _repo.Update(cliente);

        public bool EmailExists(string email, int? excludeId = null) => _repo.EmailExists(email, excludeId);

        public IEnumerable<Cliente> GetByCriteria(FootballGo.DTOs.ClienteCriteria criteria) => _repo.GetByCriteria(criteria);
    }
}
