using Domain.Model;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FootballGo.UI
{
    public partial class dgvClientes : Form
    {
        private readonly EmpleadoService _empleadoService;
        private readonly MenuForm _menuForm;

        public dgvClientes(EmpleadoService empleadoService, MenuForm menuForm)
        {
            InitializeComponent();
            _empleadoService = empleadoService ?? throw new ArgumentNullException(nameof(empleadoService));
            _menuForm = menuForm;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
        }

        private void CargarEmpleados()
        {
            try
            {
                IEnumerable<Empleado> empleados = _empleadoService.GetAll();
                dgvCliente.DataSource = null; // Corrected to use 'dgvCliente' instead of 'dgvClientes'
                dgvCliente.DataSource = empleados.ToList();
                dgvCliente.ReadOnly = true;
                dgvCliente.AllowUserToAddRows = false;
                dgvCliente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCliente.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los empleados: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Alta de empleado → mostramos el form dentro del panel
            var frm = new EmpleadoDetailsForm(_menuForm);
            _menuForm.MostrarEnPanel(frm);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCliente.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un empleado para editar.");
                return;
            }

            var seleccionado = dgvCliente.SelectedRows[0].DataBoundItem as Empleado;
            if (seleccionado == null)
            {
                MessageBox.Show("No se pudo obtener el empleado.");
                return;
            }

            // Edición → mostramos el form dentro del panel
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCliente.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un empleado para eliminar.");
                return;
            }

            var seleccionado = dgvCliente.SelectedRows[0].DataBoundItem as Empleado;
            if (seleccionado == null)
            {
                MessageBox.Show("No se pudo obtener el empleado.");
                return;
            }

            if (MessageBox.Show(
                    $"¿Está seguro de eliminar a {seleccionado.Nombre} {seleccionado.Apellido}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _empleadoService.Delete(seleccionado.Id);
                CargarEmpleados();
            }
        }
    }
}
