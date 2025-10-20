using API.Clients;
using Domain.Model;
using DTOs;
using FootballGo.UI;
using System.Drawing;
using System.Windows.Forms;

namespace MisCanchasApp
{
    public partial class ListadoReservasUsuarioForm : Form
    {
        private readonly string _mailUsuario;

        public ListadoReservasUsuarioForm(string mailUsuario)
        {
            InitializeComponent();
            _mailUsuario = mailUsuario;
            this.Text = $"Mis Reservas - {_mailUsuario}";
            Load += ListadoReservasUsuarioForm_Load;
        }

        private async void ListadoReservasUsuarioForm_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            await CargarReservasAsync();
        }

        private void ConfigurarGrilla()
        {
            dgvReservas.AutoGenerateColumns = false;
            dgvReservas.Columns.Clear();

            // Nro Cancha
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(DTOs.Reserva.NroCancha),
                HeaderText = "Cancha N°",
                Width = 80
            });

            // Fecha
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(DTOs.Reserva.FechaReserva),
                HeaderText = "Fecha",
                Width = 100,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            // Hora Inicio
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(DTOs.Reserva.HoraInicio),
                HeaderText = "Hora",
                Width = 80,
                DefaultCellStyle = { Format = @"hh\:mm" }
            });

            // Precio Total
            dgvReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(DTOs.Reserva.PrecioTotal),
                HeaderText = "Total",
                Width = 100,
                DefaultCellStyle = { Format = "C2" }
            });

            // Botón Cancelar
            var btnCancelar = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Name = "Cancelar",
                Text = "Cancelar",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            dgvReservas.Columns.Add(btnCancelar);

            // Estilo del botón Cancelar
            dgvReservas.CellFormatting += (s, e) =>
            {
                if (dgvReservas.Columns[e.ColumnIndex].Name == "Cancelar" && e.RowIndex >= 0)
                {
                    var reserva = dgvReservas.Rows[e.RowIndex].DataBoundItem as Domain.Model.Reserva;
                    var cell = dgvReservas.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;

                    if (reserva != null && cell != null)
                    {
                        if (reserva.FechaReserva.Date < DateTime.Today)
                        {
                            cell.Style.BackColor = Color.Red;
                            cell.Style.ForeColor = Color.DarkRed; // Cambiado a rojo oscuro
                            cell.Style.Font = new Font(dgvReservas.Font, FontStyle.Regular);
                            cell.Value = "No Cancelable";
                        }
                        else
                        {
                            cell.Style.BackColor = Color.Red;
                            cell.Style.ForeColor = Color.DarkRed; // Cambiado a rojo oscuro
                            cell.Style.Font = new Font(dgvReservas.Font, FontStyle.Bold);
                            cell.Value = "Cancelar";
                        }
                    }
                }
            };
        }

        private async Task CargarReservasAsync()
        {
            try
            {
                var reservas = await ReservaApiClient.GetByCriteriaAsync(_mailUsuario);

                dgvReservas.DataSource = reservas.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tus reservas: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void dgvReservas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvReservas.Columns[e.ColumnIndex].Name != "Cancelar") return;

            try
            {
                var reservaSeleccionadaDTO = (DTOs.Reserva)dgvReservas.Rows[e.RowIndex].DataBoundItem;

                if (reservaSeleccionadaDTO == null)
                {
                    throw new InvalidCastException("El objeto DataBoundItem es nulo después del casting.");
                }

                if (reservaSeleccionadaDTO.FechaReserva.Date < DateTime.Today)
                {
                    MessageBox.Show("No puedes cancelar reservas que ya han pasado.", "Reserva No Cancelable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show(
                    $"¿Está seguro de que desea cancelar la reserva de la Cancha N° {reservaSeleccionadaDTO.NroCancha} para el {reservaSeleccionadaDTO.FechaReserva:dd/MM} a las {reservaSeleccionadaDTO.HoraInicio:hh\\:mm}?",
                    "Confirmar Cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        
                        await API.Clients.ReservaApiClient.DeleteAsync(reservaSeleccionadaDTO.IdReserva);

                        MessageBox.Show("Reserva cancelada con éxito.", "Cancelación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await CargarReservasAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cancelar la reserva: {ex.Message}", "Error de Cancelación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la selección: {ex.Message}", "Error Crítico de Datos (Reintento)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}