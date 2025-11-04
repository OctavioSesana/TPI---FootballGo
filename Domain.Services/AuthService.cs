using DTOs;
using Data;
using Domain.Model;


namespace Domain.Services
{
    public class AuthService
    {
        private readonly ClienteRepository clienteRepository;

        public AuthService()
        {
            clienteRepository = new ClienteRepository();
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            // Validación mínima
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return null;

            Console.WriteLine($"[DEBUG] Intentando login con: {request.Email} / {request.Password}");
            // Buscar usuario por email
            var cliente = clienteRepository.GetByEmail(request.Email);
            if (cliente == null)
            {
                Console.WriteLine("[DEBUG] Usuario no encontrado");
                return null;
            }
            // Validar contraseña literal (sin hash por ahora)
            if (cliente.Contrasenia != request.Password) {
                Console.WriteLine($"[DEBUG] Contraseña incorrecta para {cliente.Email}");

                return null;
            }

            Console.WriteLine("[DEBUG] Login exitoso");

            // Simular token y expiración
            string fakeToken = Guid.NewGuid().ToString();
            DateTime expiresAt = DateTime.UtcNow.AddHours(1);

            return new LoginResponse
            {
                Token = fakeToken,
                ExpiresAt = expiresAt,
                Email = cliente.Email
            };
        }
    }
}
