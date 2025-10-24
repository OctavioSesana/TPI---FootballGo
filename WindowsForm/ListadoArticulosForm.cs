using Domain.Model;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FootballGo.Desktop
{
    public partial class ListadoArticulosForm : Form
    {
        private readonly ArticuloService _articuloService;
        private List<Articulo> _articulosCache;

        public ListadoArticulosForm(ArticuloService articuloService)
        {
            InitializeComponent();
            _articuloService = articuloService;
            this.Load += ListadoArticulosForm_Load;
        }

        private void ListadoArticulosForm_Load(object sender, EventArgs e)
        {
            CargarArticulos();
        }

        private void CargarArticulos()
        {
            dgvArticulos.SuspendLayout();

            try
            {
                _articulosCache = _articuloService.GetAll().ToList();

                dgvArticulos.DataSource = null;
                dgvArticulos.Columns.Clear();

                var listaParaGrid = _articulosCache.Select(a => new
                {
                    ID = a.Id,
                    Tipo = a.Tipo,
                    Marca = a.Marca,
                    Color = a.Color,
                    Estado = a.Estado
                }).ToList();

                dgvArticulos.DataSource = listaParaGrid;

                int accionIndex = dgvArticulos.Columns.Count;

                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "Acción";
                btn.Name = "btnEditar";

                btn.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                btn.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.SeaGreen;
                btn.FlatStyle = FlatStyle.Popup;

                dgvArticulos.Columns.Insert(accionIndex, btn);

                if (dgvArticulos.Columns.Count > 0)
                {
                    dgvArticulos.Columns["ID"].Visible = false;
                    dgvArticulos.Columns["Tipo"].HeaderText = "Tipo de Artículo";
                    dgvArticulos.Columns["Marca"].HeaderText = "Marca";
                    dgvArticulos.Columns["Color"].HeaderText = "Color";
                    dgvArticulos.Columns["Estado"].HeaderText = "Estado Actual";
                }

                btn.UseColumnTextForButtonValue = true;
                btn.Text = "Editar";

                dgvArticulos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                lblTitulo.Text = $"Listado y Gestión de Artículos ({_articulosCache.Count} en total)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el listado de artículos: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgvArticulos.ResumeLayout();
            }
        }
        private void dgvArticulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvArticulos.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                if (dgvArticulos.Rows[e.RowIndex].Cells["ID"].Value is int articuloId)
                {
                    var articuloAEditar = _articulosCache.FirstOrDefault(a => a.Id == articuloId);

                    if (articuloAEditar != null)
                    {
                        var edicionForm = new ArticuloEdicionForm(articuloAEditar, _articuloService);

                        DialogResult result = edicionForm.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            CargarArticulos();
                        }
                    }
                }
            }
        }
    }
}