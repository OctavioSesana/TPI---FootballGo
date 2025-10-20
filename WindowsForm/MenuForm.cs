using System;
using System.Windows.Forms;
using Domain.Model;
using Domain.Services;

namespace FootballGo.UI
{
    public partial class MenuForm : Form
    {
        private readonly IServiceProvider _provider;

        public MenuForm(IServiceProvider provider)
        {
            InitializeComponent();
            _provider = provider;
        }

        public void MostrarBienvenida()
        {
            MostrarEnPanel(new Label
            {
                Text = "Bienvenido a FootballGo",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            });
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            MostrarBienvenida();
        }

        // --------- Utilidad para cargar Forms dentro del panel ---------
        public void MostrarEnPanel(Control control)
        {
            panelContainer.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(control);
        }

        public void MostrarEnPanel(Form form)
        {
            panelContainer.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(form);
            form.Show();
        }

        // --------- Método de Bienvenida tras login ---------
        public void MostrarBienvenidaUsuario(string nombre, string apellido, string tipoUsuario)
        {
            btnClientes.Visible = false;
            btnEmpleados.Visible = false;

            lblBienvenida.Text = $"Bienvenido! Usuario : {nombre} {apellido}";
            lblBienvenida.Visible = true;
        }

        public void CerrarSesion()
        {
            // Restaurar menú superior
            btnClientes.Visible = true;
            btnEmpleados.Visible = true;

            // Ocultar label de bienvenida
            lblBienvenida.Visible = false;

            // Mostrar pantalla de bienvenida inicial
            MostrarBienvenida();
        }


        // --------- Clientes ---------
        private void btnClientes_Click(object sender, EventArgs e)
        {
            var loginForm = (LoginForm)_provider.GetService(typeof(LoginForm));
            loginForm.FormClosed += (s, args) =>
            {
                if (loginForm.ClienteLogueado != null)
                {
                    var dashboard = new ClienteDashboardForm(loginForm.ClienteLogueado, this, loginForm.ClienteLogueado.Email);
                    MostrarEnPanel(dashboard);

                    // Mostrar bienvenida
                    MostrarBienvenidaUsuario(
                        loginForm.ClienteLogueado.Nombre,
                        loginForm.ClienteLogueado.Apellido,
                        "Cliente");
                }
            };
            MostrarEnPanel(loginForm);
        }

        // --------- Empleados ---------
        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            var loginForm = (EmpleadoLoginForm)_provider.GetService(typeof(EmpleadoLoginForm));
            loginForm.FormClosed += (s, args) =>
            {
                if (loginForm.EmpleadoLogueado != null)
                {
                    var dashboard = new EmpleadoDashboardForm(loginForm.EmpleadoLogueado, this);
                    MostrarEnPanel(dashboard);

                    // Mostrar bienvenida
                    MostrarBienvenidaUsuario(
                        loginForm.EmpleadoLogueado.Nombre,
                        loginForm.EmpleadoLogueado.Apellido,
                        "Empleado");
                }
            };
            MostrarEnPanel(loginForm);
        }
    }
}
