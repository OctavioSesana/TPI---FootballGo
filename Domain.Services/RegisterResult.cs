// Domain.Services/Auth/RegisterResult.cs (o dentro de Domain.Services)
namespace Domain.Services
{
    public class RegisterResult
    {
        public bool Success { get; init; }
        public string? Code { get; init; }      // p.ej. "EmailExists"
        public string? Message { get; init; }
        public int? UserId { get; init; }
        public string? Email { get; init; }

        public static RegisterResult Ok(int userId, string email) =>
            new RegisterResult { Success = true, UserId = userId, Email = email };

        public static RegisterResult Conflict(string code, string message) =>
            new RegisterResult { Success = false, Code = code, Message = message };

        public static RegisterResult Bad(string message) =>
            new RegisterResult { Success = false, Message = message };
    }
}
