namespace API.Clients
{
    public interface IAuthService
    {
        //event Action<bool>? AuthenticationStateChanged;
        public event Action<bool>? AuthenticationStateChanged { add { } remove { } }


        Task<bool> IsAuthenticatedAsync();
        Task<string?> GetTokenAsync();
        Task<string?> GetUsernameAsync();
        Task<bool> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task CheckTokenExpirationAsync();
        Task<bool> HasPermissionAsync(string permission);
    }
}