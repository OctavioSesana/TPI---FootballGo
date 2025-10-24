using Domain.Model;
using Domain.Services;
using System.Windows.Forms;

namespace FootballGo.Desktop
{
    public partial class ArticuloEdicionForm : Form
    {
        private readonly ArticuloService _articuloService;
        private Articulo _articuloOriginal;

        public ArticuloEdicionForm(Articulo articulo, ArticuloService servicio)
        {
            InitializeComponent();
            _articuloService = servicio;
            _articuloOriginal = articulo;

            this.Text = $"Editar Estado: {articulo.Tipo} - {articulo.Marca}";

            cmbEstado.Items.AddRange(new object[] { "Disponible", "En uso", "Mantenimiento", "Perdido" });
            cmbEstado.SelectedItem = articulo.Estado;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevoEstado = cmbEstado.SelectedItem?.ToString() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(nuevoEstado))
                {
                    MessageBox.Show("Debe seleccionar un estado válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _articuloOriginal.SetEstado(nuevoEstado);

                if (_articuloService.Update(_articuloOriginal))
                {
                    MessageBox.Show("Estado actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al intentar actualizar el artículo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}