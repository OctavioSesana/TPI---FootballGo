using API.Clients;
using Domain.Model;
using ClienteDTO = DTOs.Cliente;
using DomainCliente = Domain.Model.Cliente;
using Domain.Services;
using System;
using System.Windows.Forms;

namespace FootballGo.UI
{
    public partial class ClienteDetailsForm : Form
    {
        private ClienteDTO _cliente;
        private DomainCliente clienteSeleccionado;
        private readonly MenuForm _menuForm;
        private readonly bool _esRegistro;
        public object ClienteResult { get; internal set; }

        public ClienteDetailsForm(MenuForm menuForm, bool esRegistro = true, ClienteDTO? cliente = null)
        {
            InitializeComponent();
            _menuForm = menuForm;
            _esRegistro = esRegistro;
            _cliente = cliente ?? new ClienteDTO();

            Text = _esRegistro ? "Registrar Cliente" : "Editar Perfil";
            if (!_esRegistro) CargarDatosEnFormulario(_cliente);
        }

        public ClienteDetailsForm(ClienteDTO clienteAEditar, MenuForm menuForm)
        {
            InitializeComponent();
            Text = "Editar Cliente";
            _menuForm = menuForm;
            _cliente = clienteAEditar ?? new ClienteDTO();
            CargarDatosEnFormulario(_cliente);
        }

        public ClienteDetailsForm(DomainCliente clienteSeleccionado, MenuForm menuForm)
        {
            this.clienteSeleccionado = clienteSeleccionado;
            _menuForm = menuForm;
        }

        public ClienteDetailsForm(DomainCliente clienteSeleccionado)
        {
            this.clienteSeleccionado = clienteSeleccionado;
        }

        private void CargarDatosEnFormulario(ClienteDTO cliente)
        {
            txtNombre.Text = cliente.Nombre;
            txtApellido.Text = cliente.Apellido;
            txtEmail.Text = cliente.Email;
            txtContrasenia.Text = cliente.Contrasenia;
            txtDNI.Text = cliente.dni.ToString();
            txtTel.Text = cliente.telefono.ToString();
            dtpFechaAlta.Value = cliente.FechaAlta == default ? DateTime.Now : cliente.FechaAlta;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidateCliente()) return;

            var btn = sender as Button;
            if (btn != null) btn.Enabled = false;

            try
            {
                _cliente.Nombre = txtNombre.Text.Trim();
                _cliente.Apellido = txtApellido.Text.Trim();
                _cliente.Email = txtEmail.Text.Trim();
                _cliente.dni = int.Parse(txtDNI.Text);
                _cliente.telefono = int.Parse(txtTel.Text);
                _cliente.FechaAlta = dtpFechaAlta.Value;
                _cliente.Contrasenia = txtContrasenia.Text.Trim();

                if (!_esRegistro && _cliente.Id > 0)
                {
                    await ClienteApiClient.UpdateAsync(_cliente);  
                }
                else
                {
                    var creado = await ClienteApiClient.AddAsync(_cliente);
                    if (creado != null) _cliente.Id = creado.Id;
                }

                var dashboard = new ClienteDashboardForm(MapToDomain(_cliente), _menuForm, _cliente.Email);
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
        private Domain.Model.Cliente MapToDomain(ClienteDTO d)
        {
            var contrasenia = string.IsNullOrWhiteSpace(d.Contrasenia) ? "" : d.Contrasenia;

            var c = new Domain.Model.Cliente(
                d.Id,
                d.Nombre,
                d.Apellido,
                d.Email,
                d.dni,
                d.telefono,
                d.FechaAlta,
                contrasenia              
            );

            return c;
        }


        private async void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_esRegistro)
            {
                _menuForm.MostrarEnPanel(new LoginForm(_menuForm));
                return;
            }

            try
            {
                if (_cliente != null && _cliente.Id > 0)
                {
                    var dto = await ClienteApiClient.GetAsync(_cliente.Id);
                    var domain = MapToDomain(dto);
                    var dashboard = new ClienteDashboardForm(domain, _menuForm, _cliente.Email);
                    _menuForm.MostrarEnPanel(dashboard);
                }
                else
                {
                    var domain = MapToDomain(_cliente ?? new ClienteDTO());
                    var dashboard = new ClienteDashboardForm(domain, _menuForm, _cliente.Email);
                    _menuForm.MostrarEnPanel(dashboard);
                }
            }
            catch (Exception)
            {
                var domain = MapToDomain(_cliente ?? new ClienteDTO());
                var dashboard = new ClienteDashboardForm(domain, _menuForm, _cliente.Email);
                _menuForm.MostrarEnPanel(dashboard);
            }
        }

        private bool ValidateCliente()
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
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Ingrese el email.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            if (!int.TryParse(txtDNI.Text, out _))
            {
                MessageBox.Show("DNI inválido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDNI.Focus();
                return false;
            }
            if (!int.TryParse(txtTel.Text, out _))
            {
                MessageBox.Show("Teléfono inválido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTel.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtContrasenia.Text) || txtContrasenia.Text.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContrasenia.Focus();
                return false;
            }
            return true;
        }
    }
}
