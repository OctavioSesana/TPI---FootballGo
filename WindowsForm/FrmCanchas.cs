using System;
using System.Linq;
using System.Windows.Forms;
using Domain.Model;
using Domain.Services;

namespace FootballGo.UI
{
    public partial class FrmCanchas : Form
    {
        private readonly CanchaService _service = new CanchaService();

        public FrmCanchas()
        {
            InitializeComponent();
            Load += FrmCanchas_Load;
        }

        private void FrmCanchas_Load(object? sender, EventArgs e)
        {
            ConfigurarGrilla();
            CargarDatos();
            dgvCanchas.CellDoubleClick += (s, ev) => { if (ev.RowIndex >= 0) AbrirEdicion(); };
        }

        private void ConfigurarGrilla()
        {
            dgvCanchas.AutoGenerateColumns = false;
            dgvCanchas.Columns.Clear();

            dgvCanchas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cancha.NroCancha),
                HeaderText = "N°",
                Width = 70
            });
            dgvCanchas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cancha.EstadoCancha),
                HeaderText = "Estado",
                Width = 140
            });
            dgvCanchas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cancha.TipoCancha),
                HeaderText = "Tipo",
                Width = 80
            });
            dgvCanchas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cancha.PrecioPorHora),
                HeaderText = "Precio/Hora",
                Width = 120,
                DefaultCellStyle = { Format = "C2" }
            });
        }

        private void CargarDatos() =>
            dgvCanchas.DataSource = _service.Listar().OrderBy(x => x.NroCancha).ToList();

        private Cancha? Seleccionada() => dgvCanchas.CurrentRow?.DataBoundItem as Cancha;

        private EmpleadoDashboardForm? GetDashboard() => this.FindForm() as EmpleadoDashboardForm;

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            GetDashboard()?.CargarEnPanel(new FrmCanchaEdicion());
        }

        private void btnEditar_Click(object sender, EventArgs e) => AbrirEdicion();

        private void AbrirEdicion()
        {
            //var sel = Seleccionada();
            //if (sel == null) { MessageBox.Show("Seleccioná una cancha."); return; }
            //GetDashboard()?.CargarEnPanel(new FrmCanchaEdicion(sel));


            var sel = Seleccionada();
            if (sel == null)
            {
                MessageBox.Show("Seleccioná una cancha.");
                return;
            }

            using var frm = new FrmCanchaEdicion(sel);
            if (frm.ShowDialog(this) == DialogResult.OK)
                CargarDatos(); // refresca la grilla al volver


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var sel = Seleccionada();
            if (sel == null) { MessageBox.Show("Seleccioná una cancha."); return; }

            var ok = MessageBox.Show($"¿Eliminar la cancha #{sel.NroCancha}?",
                                     "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (ok != DialogResult.Yes) return;

            try
            {
                _service.Eliminar(sel.NroCancha);
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarDatos();
        private void btnCerrar_Click(object sender, EventArgs e) => GetDashboard()?.CargarEnPanel(new FrmCanchas());
    }
}
