namespace API.Clients
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
    }
}
