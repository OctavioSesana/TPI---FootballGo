using API.Clients;
using Domain.Model;
using DTOs;
using MisCanchasApp;
using System;
using System.Windows.Forms;
using ClienteDTO = DTOs.Cliente;

namespace FootballGo.UI
{
    public partial class ClienteDashboardForm : Form
    {
        private Domain.Model.Cliente _cliente;
        private readonly MenuForm _menuForm;
        private Form? _child;
        private readonly string _mailUsuario;  

        public ClienteDashboardForm(Domain.Model.Cliente cliente, MenuForm menuForm, string mailUsuario)
        {
            InitializeComponent();
            _cliente = cliente;
            _menuForm = menuForm;
            CrearMenu();
            _mailUsuario = mailUsuario;
        }

        private void ClienteDashboardForm_Load(object sender, EventArgs e)
        {
            //lblSesion.Text = $"Sesión iniciada como: {_cliente.Email} ({_cliente.Nombre} {_cliente.Apellido})";
        }

        public void CargarEnPanel(Form child)
        {
            if (_child != null)
            {
                _child.Close();
                _child.Dispose();
                _child = null;
            }

            _child = child;
            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;

            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(child);
            child.Show();
        }

        private void CrearMenu()
        {
            menuStrip.Items.Clear();

            // PERFIL
            var mPerfil = new ToolStripMenuItem("Perfil");
            var itPerfil = new ToolStripMenuItem("Gestionar Perfil", null, btnGestionarPerfil_Click);
            var itCerrar = new ToolStripMenuItem("Cerrar Sesión", null, btnCerrarSesion_Click);
            var itBaja = new ToolStripMenuItem("Eliminar Cuenta", null, btnDelete_Click);
            mPerfil.DropDownItems.AddRange(new[] { itPerfil, itCerrar, itBaja });

            // RESERVAS
            var mReservas = new ToolStripMenuItem("Reservas");
            var itReservar = new ToolStripMenuItem("Reservar Cancha", null, btnReservarCancha_Click);
            var verReservas = new ToolStripMenuItem("Mis reservas", null, btnReservas);
            mReservas.DropDownItems.AddRange(new[] { itReservar, verReservas });

            // AGREGAR AL MENU
            menuStrip.Items.AddRange(new[] { mPerfil, mReservas });
        }

        private void btnCerrarSesion_Click(object? sender, EventArgs e)
        {
            _menuForm.CerrarSesion();
        }

        private void btnReservas(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_mailUsuario))
            {
                MessageBox.Show("No se ha detectado una sesión de usuario válida.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CargarEnPanel(new ListadoReservasUsuarioForm(_mailUsuario)
);

            
        }

        private void btnGestionarPerfil_Click(object? sender, EventArgs e)
        {
            var dto = MapToDto(_cliente);
            var perfilForm = new ClienteDetailsForm(dto, _menuForm);
            _menuForm.MostrarEnPanel(perfilForm);
        }

        private ClienteDTO MapToDto(Domain.Model.Cliente c)
        {
            return new ClienteDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Email = c.Email,
                dni = c.dni,
                telefono = c.telefono,
                FechaAlta = c.FechaAlta
            };
        }

        private async void btnReservarCancha_Click(object? sender, EventArgs e)
        {
            try
            {
                var canchas = await CanchaApiClient.GetAllAsync();
                CargarEnPanel(new ListadoCanchasForm(_mailUsuario));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar canchas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¿Está seguro de que desea eliminar su cuenta? Esta acción no se puede deshacer.",
                "Confirmar baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await ClienteApiClient.DeleteAsync(_cliente.Id);

                    MessageBox.Show("Su cuenta ha sido eliminada correctamente.", "Cuenta eliminada",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _menuForm.MostrarEnPanel(new LoginForm(_menuForm));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo eliminar la cuenta. Detalle: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
