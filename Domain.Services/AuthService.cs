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
            if (cliente.Contrasenia != request.Password)
            {
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

        // ===== NUEVO: Registro =====
        // Nota: como el repo es síncrono, devolvemos Task usando Task.FromResult
        public Task<RegisterResult> RegisterAsync(RegisterRequest request)
        {
            // 1) Validaciones básicas (tu RegisterRequest NO tiene Password, usa Contrasenia)
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Contrasenia))
                return Task.FromResult(RegisterResult.Bad("Email y contraseña son obligatorios."));

            // 2) Unicidad de email (ajusta el nombre del método si es distinto)
            var existe = clienteRepository.EmailExists(request.Email);
            if (existe)
                return Task.FromResult(RegisterResult.Conflict("EmailExists", "Email ya registrado."));

            // 3) (Opcional) Hashear contraseña — por ahora, guardamos tal cual
            var contrasenia = request.Contrasenia;

            // 4) Crear entidad (usar el Cliente de Domain.Model para evitar ambigüedad)
            var nuevo = new Domain.Model.Cliente(
                id: 0,
                nombre: request.Nombre,
                apellido: request.Apellido,
                email: request.Email,
                dni: int.TryParse(request.Dni, out var dniVal) ? dniVal : 0,
                telefono: int.TryParse(request.Telefono, out var telVal) ? telVal : 0,
                fechaAlta: DateTime.UtcNow,
                contrasenia: request.Contrasenia
            );

            // 5) Persistir (ajusta si tus métodos se llaman distinto)
            clienteRepository.Add(nuevo);
            

            // 6) Devolver OK
            return Task.FromResult(RegisterResult.Ok(nuevo.Id, nuevo.Email));
        }
    }
}
