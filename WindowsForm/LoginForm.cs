using System;
using System.Linq;
using System.Windows.Forms;
using API.Clients;
using ClienteDTO = DTOs.Cliente;       
using ClienteDomain = Domain.Model.Cliente; 

namespace FootballGo.UI
{
    public partial class LoginForm : Form
    {
        private readonly MenuForm _menuForm;

        public ClienteDomain? ClienteLogueado { get; private set; }

        public LoginForm(MenuForm menuForm)
        {
            InitializeComponent();
            _menuForm = menuForm;

            this.Load += LoginForm_Load;
        }

        private async void LoginForm_Load(object? sender, EventArgs e)
        {
            try
            {
                var _ = await ClienteApiClient.GetAllAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No puedo conectar con la API. Revisá que el backend esté ejecutándose y que el BaseAddress coincida.\n\nDetalle: "
                    + ex.Message,
                    "Conexión con API",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Debe ingresar Email y Contraseña", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var btn = sender as Button;
            if (btn != null) btn.Enabled = false;

            try
            {
                ClienteDTO? dto = null;
                if (dto == null)
                {
                    var lista = await ClienteApiClient.GetByCriteriaAsync(email);
                    dto = lista.FirstOrDefault(c =>
                        c.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(c.Contrasenia, pass));
                }

                if (dto != null)
                {
                    var domain = MapToDomain(dto);

                    ClienteLogueado = domain;

                    _menuForm.MostrarBienvenidaUsuario(domain.Nombre, domain.Apellido, "Cliente");

                    var dashboard = new ClienteDashboardForm(domain, _menuForm, email);
                    _menuForm.MostrarEnPanel(dashboard);
                }
                else
                {
                    MessageBox.Show("Email o contraseña incorrectos", "Error de acceso",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (btn != null) btn.Enabled = true;
            }
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            var registroForm = new ClienteDetailsForm(_menuForm, esRegistro: true);
            _menuForm.MostrarEnPanel(registroForm);
        }

        private static ClienteDomain MapToDomain(ClienteDTO d)
        {
            return new ClienteDomain(
                d.Id,
                d.Nombre ?? "",
                d.Apellido ?? "",
                d.Email ?? "",
                d.dni,
                d.telefono,
                d.FechaAlta == default ? DateTime.Now : d.FechaAlta,
                d.Contrasenia ?? ""
            );
        }
    }
}
