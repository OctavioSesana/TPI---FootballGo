using System;
using System.Windows.Forms;
using Domain.Model;
using Domain.Services;

namespace FootballGo.UI
{
    public partial class FrmCanchaEdicion : Form
    {
        private readonly CanchaService _service = new CanchaService();
        private readonly Cancha? _cancha;  // null = alta, con valor = edición

        public FrmCanchaEdicion()
        {
            InitializeComponent();
            Load += FrmCanchaEdicion_Load;
        }

        public FrmCanchaEdicion(Cancha cancha) : this()
        {
            _cancha = cancha;
        }

        // Busca el Dashboard de forma robusta
        private EmpleadoDashboardForm? GetDashboard()
        {
            Control? c = this.Parent;
            while (c != null && c is not EmpleadoDashboardForm) c = c.Parent;
            return c as EmpleadoDashboardForm
                   ?? this.FindForm() as EmpleadoDashboardForm
                   ?? Application.OpenForms["EmpleadoDashboardForm"] as EmpleadoDashboardForm;
        }

        private void FrmCanchaEdicion_Load(object? sender, EventArgs e)
        {
            // Enter/Esc
            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;

            // Estado (enum)
            cboEstado.DataSource = Enum.GetValues(typeof(EstadoCancha));

            // Tipo 5/7
            cboTipoCancha.Items.Clear();
            cboTipoCancha.Items.Add(5);
            cboTipoCancha.Items.Add(7);

            // Nro y Precio
            nudNro.Minimum = 1;
            nudNro.Maximum = 10000;
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Minimum = 0;
            nudPrecio.Maximum = 100000;

            if (_cancha == null)
            {
                // Alta
                cboTipoCancha.SelectedIndex = 0;
                Text = "Alta de cancha";
                nudNro.ReadOnly = false; // editable en alta
            }
            else
            {
                // Edición: precargar datos
                nudNro.Value = _cancha.NroCancha;

                // Seleccionar enum de forma segura
                if (Enum.IsDefined(typeof(EstadoCancha), _cancha.EstadoCancha))
                    cboEstado.SelectedItem = _cancha.EstadoCancha;

                // Seleccionar tipo (5/7) de forma segura
                int idx = cboTipoCancha.Items.IndexOf(_cancha.TipoCancha);
                if (idx >= 0) cboTipoCancha.SelectedIndex = idx;

                nudPrecio.Value = _cancha.PrecioPorHora;

                Text = $"Editar cancha #{_cancha.NroCancha}";

                // Opcional: evitar cambiar el número en edición
                nudNro.ReadOnly = true;     // quita teclado
                nudNro.Enabled = false;    // grisado total
            }

            // Asegurar eventos si no están conectados en el Designer
            btnGuardar.Click -= btnGuardar_Click;
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Click -= btnCancelar_Click;
            btnCancelar.Click += btnCancelar_Click;
            

        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                int nro = (int)nudNro.Value;
                var estado = (EstadoCancha)cboEstado.SelectedItem!;
                int tipo = (int)cboTipoCancha.SelectedItem!;
                decimal precio = nudPrecio.Value;

                if (_cancha == null)
                {
                    // Alta
                    _service.Crear(nro, estado, tipo, precio);
                }
                else
                {
                    // Edición
                    _service.Actualizar(nro, estado, tipo, precio);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();


                // Volver al listado dentro del dashboard
                GetDashboard()?.CargarEnPanel(new FrmCanchas());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            GetDashboard()?.CargarEnPanel(new FrmCanchas());
        }
    }
}
