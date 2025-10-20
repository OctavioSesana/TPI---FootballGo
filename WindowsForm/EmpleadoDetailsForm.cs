using API.Clients;
using Domain.Model;
using EmpleadoDTO = DTOs.Empleado;
using DomainEmpleado = Domain.Model.Empleado;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace FootballGo.UI
{
    public partial class EmpleadoDetailsForm : Form
    {
        private EmpleadoDTO _empleado;
        private readonly MenuForm _menuForm;
        private readonly bool _esRegistro;

        public EmpleadoDetailsForm(MenuForm menuForm, bool esRegistro = true, EmpleadoDTO? empleado = null)
        {
            InitializeComponent();
            _menuForm = menuForm;
            _esRegistro = esRegistro;
            _empleado = empleado ?? new EmpleadoDTO();

            Text = _esRegistro ? "Registrar Empleado" : "Editar Perfil";
            if (!_esRegistro) CargarDatosEnFormulario(_empleado);
        }

        public EmpleadoDetailsForm(EmpleadoDTO empleadoAEditar, MenuForm menuForm)
        {
            InitializeComponent();
            Text = "Editar Empleado";
            _menuForm = menuForm;
            _empleado = empleadoAEditar ?? new EmpleadoDTO();
            CargarDatosEnFormulario(_empleado);
        }

        private void CargarDatosEnFormulario(EmpleadoDTO empleado)
        {
            txtNombre.Text = empleado.Nombre;
            txtApellido.Text = empleado.Apellido;
            txtDNI.Text = empleado.Dni.ToString();
            txtContrasenia.Text = empleado.Contrasenia;
            txtSueldo.Text = empleado.SueldoSemanal.ToString("N2", CultureInfo.CurrentCulture);
            dtpFechaIngreso.Value = empleado.FechaIngreso == default ? DateTime.Now : empleado.FechaIngreso;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidateEmpleado()) return;

            var btn = sender as Button;
            if (btn != null) btn.Enabled = false;

            try
            {
                _empleado.Nombre = txtNombre.Text.Trim();
                _empleado.Apellido = txtApellido.Text.Trim();
                _empleado.Dni = int.Parse(txtDNI.Text);
                _empleado.EstaActivo = true;
                _empleado.FechaIngreso = dtpFechaIngreso.Value;
                _empleado.Contrasenia = txtContrasenia.Text.Trim();

                if (decimal.TryParse(txtSueldo.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var sueldo))
                {
                    _empleado.SueldoSemanal = sueldo;
                }

                _empleado.FechaIngreso = dtpFechaIngreso.Value;

                if (!_esRegistro && _empleado.IdEmpleado > 0)
                {
                    await EmpleadoApiClient.UpdateAsync(_empleado);
                }
                else
                {
                    var creado = await EmpleadoApiClient.AddAsync(_empleado);
                    if (creado != null) _empleado.IdEmpleado = creado.IdEmpleado;
                }

                var dashboard = new EmpleadoDashboardForm(MapToDomain(_empleado), _menuForm);
                _menuForm.MostrarEnPanel(dashboard);
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

        private DomainEmpleado MapToDomain(EmpleadoDTO d)
        {
            var contrasenia = string.IsNullOrWhiteSpace(d.Contrasenia) ? "" : d.Contrasenia;

            return new DomainEmpleado(
                d.IdEmpleado,
                d.Nombre,
                d.Apellido,
                d.Dni,
                d.SueldoSemanal,
                d.EstaActivo,
                d.FechaIngreso,
                contrasenia
            );
        }

        private async void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_esRegistro)
            {
                _menuForm.MostrarEnPanel(new EmpleadoLoginForm(_menuForm));
                return;
            }

            try
            {
                if (_empleado != null && _empleado.IdEmpleado > 0)
                {
                    var dto = await EmpleadoApiClient.GetAsync(_empleado.IdEmpleado);
                    var domain = MapToDomain(dto);
                    var dashboard = new EmpleadoDashboardForm(domain, _menuForm);
                    _menuForm.MostrarEnPanel(dashboard);
                }
                else
                {
                    var domain = MapToDomain(_empleado ?? new EmpleadoDTO());
                    var dashboard = new EmpleadoDashboardForm(domain, _menuForm);
                    _menuForm.MostrarEnPanel(dashboard);
                }
            }
            catch (Exception)
            {
                var domain = MapToDomain(_empleado ?? new EmpleadoDTO());
                var dashboard = new EmpleadoDashboardForm(domain, _menuForm);
                _menuForm.MostrarEnPanel(dashboard);
            }
        }

        private bool ValidateEmpleado()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Ingrese el apellido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }
            if (!int.TryParse(txtDNI.Text, out _))
            {
                MessageBox.Show("DNI inválido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDNI.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtContrasenia.Text) || txtContrasenia.Text.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContrasenia.Focus();
                return false;
            }
            if (!decimal.TryParse(txtSueldo.Text, out _))
            {
                MessageBox.Show("Sueldo inválido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSueldo.Focus();
                return false;
            }
            return true;
        }
    }
}
