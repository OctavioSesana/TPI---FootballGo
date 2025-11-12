using DTOs;

namespace API.Clients
{
    public interface IClienteClient
    {
        Task<Cliente?> GetByEmailAsync(string email);
        Task<Cliente?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, object body);
        Task<bool> DeleteAsync(int id);
    }
}
