using Domain.Model;
using Domain.Services;
using System;
using System.Windows.Forms;

namespace FootballGo.Desktop
{
    public partial class ArticuloCargaForm : Form
    {
        private readonly ArticuloService _articuloService;

        public ArticuloCargaForm(ArticuloService articuloService)
        {
            InitializeComponent();
            _articuloService = articuloService;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string tipo = txtTipo.Text.Trim();
            string marca = txtMarca.Text.Trim();
            string color = txtColor.Text.Trim();
            string? estado = cmbEstado.SelectedItem?.ToString();

            try
            {
                var nuevoArticulo = new Articulo(
                    id: 0,
                    tipo: tipo,
                    marca: marca,
                    color: color,
                    estado: estado ?? string.Empty 
                );

                _articuloService.Add(nuevoArticulo);

                MessageBox.Show(
                    $"Artículo '{nuevoArticulo.Tipo} - {nuevoArticulo.Marca}' cargado con éxito.",
                    "Carga Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
            }
            catch (ArgumentException ex)
            {
                // Captura errores de validación (valores nulos/vacíos) lanzados desde Articulo.cs
                MessageBox.Show(
                    $"Error de validación: {ex.Message}",
                    "Datos Inválidos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                // Maneja errores de persistencia (ej. conexión, violación de longitud de campo)
                string innerExMessage = ex.InnerException != null
                                        ? ex.InnerException.Message
                                        : ex.Message;

                MessageBox.Show(
                    $"Ocurrió un error al intentar guardar el artículo. Detalle de DB: {innerExMessage}",
                    "Error del Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}