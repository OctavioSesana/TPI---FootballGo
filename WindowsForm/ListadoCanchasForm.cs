using Domain.Model;
using Domain.Services;
using FootballGo.UI;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Linq;

namespace MisCanchasApp
{
    public partial class ListadoCanchasForm : Form
    {
        private readonly CanchaService _service = new CanchaService();
        private readonly string _mailUsuario;

        public ListadoCanchasForm(string mailUsuario)
        {
            InitializeComponent();
            Load += ListadoCanchasForm_Load;
            dgvCanchas.CellClick += dgvCanchas_CellClick;

            //Filtros
            rbFiltroTodos.CheckedChanged += Filtro_CheckedChanged;
            rbFiltro5.CheckedChanged += Filtro_CheckedChanged;
            rbFiltro7.CheckedChanged += Filtro_CheckedChanged;

            _mailUsuario = mailUsuario;
        }


        private async void ListadoCanchasForm_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            await CargarCanchasAsync();
        }

        private async void Filtro_CheckedChanged(object? sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                await CargarCanchasAsync();
            }
        }

        private void ConfigurarGrilla()
        {
            dgvCanchas.AutoGenerateColumns = false;
            dgvCanchas.Columns.Clear();

            // N°
            dgvCanchas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cancha.NroCancha),
                HeaderText = "N°",
                Width = 60
            });

            // Estado
            dgvCanchas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cancha.EstadoCancha),
                HeaderText = "Estado",
                Width = 120
            });

            // Tipo
            dgvCanchas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cancha.TipoCancha),
                HeaderText = "Tipo",
                Width = 80
            });

            // Precio/Hora
            dgvCanchas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cancha.PrecioPorHora),
                HeaderText = "Precio/Hora",
                Width = 120,
                DefaultCellStyle = { Format = "C2" }
            });

            // Botón Reservar
            var btnReservar = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Name = "Reservar",
                Text = "Reservar",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            dgvCanchas.Columns.Add(btnReservar);

            dgvCanchas.CellFormatting += (s, e) =>
            {
                if (dgvCanchas.Columns[e.ColumnIndex].Name == "Reservar" && e.RowIndex >= 0)
                {
                    var cancha = dgvCanchas.Rows[e.RowIndex].DataBoundItem as Cancha;
                    var cell = dgvCanchas.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;

                    if (cancha != null && cell != null)
                    {
                       
                            cell.Style.BackColor = Color.LightGreen;
                            cell.Style.ForeColor = Color.DarkGreen;
                            cell.Style.Font = new Font(dgvCanchas.Font, FontStyle.Bold);
                            cell.Value = "Reservar";
                        
                    }
                }
            };
        }

        private async Task CargarCanchasAsync()
        {
            IEnumerable<Cancha> canchas = _service.Listar();

            string tipoCanchaFiltro = "Todos";

            if (rbFiltro5.Checked)
            {
                tipoCanchaFiltro = "5";
            }
            else if (rbFiltro7.Checked)
            {
                tipoCanchaFiltro = "7";
            }

            if (tipoCanchaFiltro != "Todos")
            {
                canchas = canchas.Where(c => c.TipoCancha.ToString() == tipoCanchaFiltro);
            }

            dgvCanchas.DataSource = canchas.OrderBy(c => c.NroCancha).ToList();
        }

        private void dgvCanchas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCanchas.Columns[e.ColumnIndex].Name == "Reservar")
            {
                var canchaSeleccionada = (Cancha)dgvCanchas.Rows[e.RowIndex].DataBoundItem;
                if (canchaSeleccionada.EstadoCancha == EstadoCancha.Mantenimiento)
                {
                    MessageBox.Show(
                    $"La Cancha N° {canchaSeleccionada.NroCancha} se encuentra en mantenimiento y no puede ser reservada.",
                    "Cancha No Disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    return;
                }

                var reservaForm = new ReservaForm(canchaSeleccionada, _mailUsuario);
                reservaForm.TopLevel = false;
                reservaForm.FormBorderStyle = FormBorderStyle.None;
                reservaForm.Dock = DockStyle.Fill;

                this.Controls.Add(reservaForm);

                reservaForm.BringToFront();

                reservaForm.Show();
            }
        }
    }
}

