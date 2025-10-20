using API.Clients;
using Domain.Model;
using Domain.Services;
using System;
using System.Windows.Forms;
using EmpleadoDomain = Domain.Model.Empleado;
using EmpleadoDTO = DTOs.Empleado;

namespace FootballGo.UI
{
    public partial class EmpleadoLoginForm : Form
    {
        private readonly EmpleadoService _empleadoService;
        private readonly MenuForm _menuForm;
        public Empleado? EmpleadoLogueado { get; private set; }

        public EmpleadoLoginForm(MenuForm menuForm)
        {
            InitializeComponent();
            _empleadoService = new EmpleadoService();
            _menuForm = menuForm;

            this.Load += EmpleadoLoginForm_Load;
        }

        private async void EmpleadoLoginForm_Load(object? sender, EventArgs e)
        {
            try
            {
                var _ = await EmpleadoApiClient.GetAllAsync();
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
            var pass = txtContrasenia.Text.Trim();

            if (!int.TryParse(txtDni.Text, out int dni))
            {
                MessageBox.Show("Debe ingresar un DNI válido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var btn = sender as Button;
            if (btn != null) btn.Enabled = false;

            try
            {
                EmpleadoDTO? dto = null;
                if (dto == null)
                {
                    var lista = await EmpleadoApiClient.GetByCriteriaAsync(dni.ToString());
                    dto = lista.FirstOrDefault(e =>
                        e.Dni == dni &&
                        string.Equals(e.Contrasenia, pass));
                }

                if (dto != null)
                {
                    var domain = MapToDomain(dto);

                    EmpleadoLogueado = domain;

                    _menuForm.MostrarBienvenidaUsuario(domain.Nombre, domain.Apellido, "Empleado");

                    var dashboard = new EmpleadoDashboardForm(domain, _menuForm);
                    _menuForm.MostrarEnPanel(dashboard);
                }
                else
                {
                    MessageBox.Show("DNI o contraseña incorrectos", "Error de acceso",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (btn != null) btn.Enabled = true;
            }
        }


        private EmpleadoDomain? MapToDomain(EmpleadoDTO dto)
        {
            return new EmpleadoDomain(
                dto.IdEmpleado,
                dto.Nombre,
                dto.Apellido,
                dto.Dni,
                dto.SueldoSemanal,
                dto.EstaActivo,
                dto.FechaIngreso,
                dto.Contrasenia
            );
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            // Mostrar el formulario de registro dentro del mismo panel
            var registroForm = new EmpleadoDetailsForm(_menuForm, esRegistro: true); 
            _menuForm.MostrarEnPanel(registroForm);
        }
    }
}
