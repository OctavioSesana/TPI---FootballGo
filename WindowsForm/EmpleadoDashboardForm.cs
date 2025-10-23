using API.Clients;
using Domain.Model;
using Domain.Services;
using System.Windows.Forms;
using EmpleadoDTO = DTOs.Empleado;
using ReservaDTO = DTOs.Reserva;
using System;

namespace FootballGo.UI
{
    public partial class EmpleadoDashboardForm : Form
    {
        private readonly Empleado _empleado;
        private readonly MenuForm _menuForm;
        private List<ReservaDTO> _reservasCache = new List<ReservaDTO>();
        private Form? _child; 

        public EmpleadoDashboardForm(Empleado empleado, MenuForm menuForm)
        {
            InitializeComponent();
            _empleado = empleado;
            _menuForm = menuForm;

            CrearMenu();
        }

        private void EmpleadoDashboardForm_Load(object? sender, System.EventArgs e)
        {
            //lblSesion.Text = $"Bienvenido! Usuario : {_empleado.Nombre} {_empleado.Apellido}";
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

            // Perfil
            var mPerfil = new ToolStripMenuItem("Perfil");
            var itPerfil = new ToolStripMenuItem("Gestionar Perfil", null, btnGestionarPerfil_Click);
            var itCerrar = new ToolStripMenuItem("Cerrar Sesión", null, btnCerrarSesion_Click);
            var itBaja = new ToolStripMenuItem("Baja de Cuenta", null, btnBajaCuenta_Click);
            mPerfil.DropDownItems.AddRange(new[] { itPerfil, itCerrar, itBaja });

            // Gestión
            var mGestion = new ToolStripMenuItem("Gestión");
            var itAltaCanchas = new ToolStripMenuItem("Alta de Canchas", null, btnAlta_Click_1);
            var itListado = new ToolStripMenuItem("Listado de Canchas", null, btnListadoCanchas_Click);
            var itAltaArt = new ToolStripMenuItem("Listado de clientes", null, btnListadoClientes_Click);
            var itReservas = new ToolStripMenuItem("Gestión de Reservas", null, btnGestion_Click);
            var itArticulos = new ToolStripMenuItem("Cargar Artículos", null, btnCargarArticulos_Click);
            var verArticulos = new ToolStripMenuItem("Lista de Artículos", null, btnVerArticulos_Click);
            mGestion.DropDownItems.AddRange(new[] { itAltaCanchas, itListado, itAltaArt, itReservas, itArticulos, verArticulos });

            menuStrip.Items.Add(mPerfil);
            menuStrip.Items.Add(mGestion);
        }

        private void btnVerArticulos_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad en desarrollo.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCargarArticulos_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad en desarrollo.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCerrarSesion_Click(object? sender, System.EventArgs e) => _menuForm.CerrarSesion();

        private EmpleadoDTO MapToDto(Domain.Model.Empleado e)
        {
            return new EmpleadoDTO
            {
                IdEmpleado = e.Id,
                Nombre = e.Nombre,
                Apellido = e.Apellido,
                Dni = e.Dni,
                Contrasenia = e.contrasenia,
                SueldoSemanal = e.SueldoSemanal,
                EstaActivo = e.EstaActivo,
                FechaIngreso = e.FechaIngreso
            };
        }


        private void btnGestionarPerfil_Click(object? sender, System.EventArgs e)
        {
            var perfilForm = new EmpleadoDetailsForm(MapToDto(_empleado), _menuForm);
            _menuForm.MostrarEnPanel(perfilForm);
        }

        private async void btnListadoClientes_Click(object? sender, EventArgs e)
        {
            try
            {
                var clientes = await API.Clients.ClienteApiClient.GetAllAsync();
                CargarEnPanel(new Form1(clientes, _menuForm));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}");
            }
        }


        private async void btnBajaCuenta_Click(object? sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¿Está seguro de que desea eliminar su cuenta de empleado? Esta acción no se puede deshacer.",
                "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                await API.Clients.EmpleadoApiClient.DeleteAsync(_empleado.Id);

                MessageBox.Show("Cuenta eliminada correctamente.", "OK");
                DialogResult = DialogResult.Abort;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo eliminar la cuenta. Detalle: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGestion_Click(object? sender, System.EventArgs e)
        {
            this.dgvReservas.Visible = false;
            this.btnOrdenarAsc.Visible = false;
            this.btnOrdenarDesc.Visible = false;

            this.dgvReservas.DataSource = null;
            this.dgvReservas.Rows.Clear();
            this.dgvReservas.Columns.Clear();

            try
            {
                _reservasCache = (await ReservaApiClient.GetAllAsync()).ToList();

                if (_reservasCache.Any())
                {
                    this.dgvReservas.Visible = true;
                    this.btnOrdenarAsc.Visible = true;
                    this.btnOrdenarDesc.Visible = true;

                    MostrarReservas(_reservasCache.OrderBy(r => r.FechaReserva.Date.Add(r.HoraInicio)));
                }
                else
                {
                    MessageBox.Show("No hay reservas activas para gestionar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las reservas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarReservas(IEnumerable<ReservaDTO> reservas)
        {
            var listaParaGrid = reservas.Select(r => new
            {
                ID = r.IdReserva,
                Cancha = r.NroCancha,
                Cliente = r.mailUsuario,
                Fecha = r.FechaReserva.ToShortDateString(),
                Hora = r.HoraInicio.ToString(@"hh\:mm"),
                Total = r.PrecioTotal.ToString("C")
            }).ToList();

            this.dgvReservas.DataSource = listaParaGrid;
            this.dgvReservas.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnOrdenarAsc_Click(object? sender, EventArgs e)
        {
            if (_reservasCache.Any())
            {
                var reservasAsc = _reservasCache.OrderBy(r => r.FechaReserva.Date.Add(r.HoraInicio));
                MostrarReservas(reservasAsc);
            }
        }

        private void btnOrdenarDesc_Click(object? sender, EventArgs e)
        {
            if (_reservasCache.Any())
            {
                var reservasDesc = _reservasCache.OrderByDescending(r => r.FechaReserva.Date.Add(r.HoraInicio));
                MostrarReservas(reservasDesc);
            }
        }

        private void btnAlta_Click_1(object? sender, System.EventArgs e)
        {
            CargarEnPanel(new FrmCanchaEdicion());
        }

        private async void btnListadoCanchas_Click(object? sender, EventArgs e)
        {
            try
            {
                var canchas = await API.Clients.CanchaApiClient.GetAllAsync();
                CargarEnPanel(new FrmCanchas());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar canchas: {ex.Message}");
            }
        }

        private void contentPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
