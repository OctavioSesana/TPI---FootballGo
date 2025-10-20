using Domain.Model;
using Domain.Services;
using System.DirectoryServices.ActiveDirectory;
using ClienteDTO = DTOs.Cliente;

namespace FootballGo.UI
{
    public partial class Form1 : Form
    {
        private readonly ClienteService _clienteService;
        private readonly MenuForm _menuForm;

        public Form1(IEnumerable<ClienteDTO> clientes, MenuForm menuForm)
        {
            InitializeComponent();
            _menuForm = menuForm;
            _clienteService = _clienteService ?? new ClienteService();
            dgvCliente.DataSource = clientes.ToList();
            dgvCliente.ReadOnly = true;
            dgvCliente.AllowUserToAddRows = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                IEnumerable<Cliente> clientes = _clienteService.GetAll();
                dgvCliente.DataSource = clientes.ToList();
                dgvCliente.ReadOnly = true;
                dgvCliente.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los clientes: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCliente.SelectedRows.Count > 0)
            {
                var clienteSeleccionado = (Cliente)dgvCliente.SelectedRows[0].DataBoundItem;

                var confirm = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar a {clienteSeleccionado.Nombre} {clienteSeleccionado.Apellido}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    bool eliminado = _clienteService.Delete(clienteSeleccionado.Id);

                    if (eliminado)
                    {
                        MessageBox.Show("Cliente eliminado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarClientes();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el cliente. El ID podría no existir.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente para eliminar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCliente.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un cliente para editar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var clienteSeleccionado = (Domain.Model.Cliente)dgvCliente.SelectedRows[0].DataBoundItem;

            var formEditar = new ClienteDetailsForm(clienteSeleccionado, _menuForm);
            DialogResult resultado = formEditar.ShowDialog();

            if (resultado == DialogResult.OK && formEditar.ClienteResult != null)
            {
                bool actualizado = _clienteService.Update((Cliente)formEditar.ClienteResult);

                if (actualizado)
                {
                    MessageBox.Show("Cliente actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarClientes();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el cliente.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var clienteDetailsForm = new ClienteDetailsForm(_menuForm);
            _menuForm.MostrarEnPanel(clienteDetailsForm);
        }


    }
}
