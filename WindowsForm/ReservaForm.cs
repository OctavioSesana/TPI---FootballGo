using Domain.Model;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FootballGo.UI
{
    public partial class ReservaForm : Form
    {
        private readonly Cancha _cancha;
        private readonly string _mailUsuario;
        private readonly ReservaService _reservaService = new ReservaService();
        private readonly List<TimeSpan> _horarios = new();

        public ReservaForm(Cancha cancha, string mailUsuario)
        {
            InitializeComponent();
            _cancha = cancha;
            _mailUsuario = mailUsuario;

            if (_cancha == null || _cancha.NroCancha <= 0)
            {
                MessageBox.Show("La cancha seleccionada no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Load += ReservaForm_Load;
            dgvTurnos.CellClick += DgvTurnos_CellClick;
        }

        private void ReservaForm_Load(object sender, EventArgs e)
        {

            lblCancha.Text = $"Cancha N° {_cancha.NroCancha} - Futbol {_cancha.TipoCancha}";
            lblPrecio.Text = $"Precio: {_cancha.PrecioPorHora:C2}";

            dtpFecha.MinDate = DateTime.Today;
            dtpFecha.ValueChanged += (_, _) => CargarHorarios();

            ConfigurarGrilla();
            CargarHorarios();
        }

        private void ConfigurarGrilla()
        {
            dgvTurnos.AutoGenerateColumns = false;
            dgvTurnos.Columns.Clear();

            dgvTurnos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Horario",
                Name = "Horario",
                HeaderText = "Horario",
                Width = 150
            });

            dgvTurnos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Estado",
                Name = "Estado",
                HeaderText = "Estado",
                Width = 120
            });

            var btn = new DataGridViewButtonColumn
            {
                HeaderText = "",
                Text = "Reservar",
                Name = "Reservar",
                UseColumnTextForButtonValue = true,
                Width = 100
            };
            dgvTurnos.Columns.Add(btn);
        }


        private void CargarHorarios()
        {
            dgvTurnos.DataSource = null;
            _horarios.Clear();

            for (int hora = 9; hora < 20; hora++)
                _horarios.Add(new TimeSpan(hora, 0, 0));

            var reservasExistentes = _reservaService.Listar()
                .Where(r => r.NroCancha == _cancha.NroCancha && r.FechaReserva.Date == dtpFecha.Value.Date)
                .ToList();

            var lista = _horarios.Select(h => new
            {
                Horario = $"{h:hh\\:mm} - {(h + TimeSpan.FromHours(1)):hh\\:mm}",
                Estado = reservasExistentes.Any(r => r.HoraInicio == h)
                            ? "Ocupado"
                            : "Disponible",
                Hora = h
            }).ToList();

            dgvTurnos.DataSource = lista;
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void DgvTurnos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTurnos.Columns[e.ColumnIndex].Name == "Reservar")
            {
                var fila = dgvTurnos.Rows[e.RowIndex];
                string estado = fila.Cells["Estado"].Value.ToString()!;
                if (estado == "Ocupado")
                {
                    MessageBox.Show("Este horario ya está reservado.", "Turno ocupado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TimeSpan horaSeleccionada = _horarios[e.RowIndex];
                DateTime fecha = dtpFecha.Value.Date;

                var confirmar = MessageBox.Show(
                    $"¿Confirmar reserva?\n\n" +
                    $"Cancha N°: {_cancha.NroCancha}\n" +
                    $"Mail Usuario: {_mailUsuario}\n" +
                    $"Fecha: {fecha:dd/MM/yyyy}\n" +
                    $"Hora: {horaSeleccionada:hh\\:mm}\n" +
                    $"Precio: {_cancha.PrecioPorHora:C2}",
                    "Confirmar reserva",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmar == DialogResult.Yes)
                {
                    try
                    {
                        _reservaService.Crear(
                            _cancha.NroCancha,
                            _mailUsuario,
                            fecha,
                            horaSeleccionada,
                            _cancha.PrecioPorHora
                        );

                        MessageBox.Show("✅ Reserva creada correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarHorarios();
                    }
                    catch (Exception ex)
                    {
                        var inner = ex.InnerException?.Message ?? "Sin detalles internos";
                        MessageBox.Show($"Error al crear la reserva:\n{ex.Message}\n\nDetalles: {inner}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
