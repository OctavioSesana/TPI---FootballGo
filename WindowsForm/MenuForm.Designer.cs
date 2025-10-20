namespace FootballGo.UI
{
    partial class MenuForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnEmpleados;
        private System.Windows.Forms.Label lblBienvenida;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            panelTop = new Panel();
            btnClientes = new Button();
            btnEmpleados = new Button();
            lblBienvenida = new Label();
            panelContainer = new Panel();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.LightGray;
            panelTop.Controls.Add(btnClientes);
            panelTop.Controls.Add(btnEmpleados);
            panelTop.Controls.Add(lblBienvenida);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1251, 50);
            panelTop.TabIndex = 1;
            // 
            // btnClientes
            // 
            btnClientes.Location = new Point(10, 10);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(90, 30);
            btnClientes.TabIndex = 1;
            btnClientes.Text = "Clientes";
            btnClientes.UseVisualStyleBackColor = true;
            btnClientes.Click += btnClientes_Click;
            // 
            // btnEmpleados
            // 
            btnEmpleados.Location = new Point(110, 10);
            btnEmpleados.Name = "btnEmpleados";
            btnEmpleados.Size = new Size(119, 30);
            btnEmpleados.TabIndex = 2;
            btnEmpleados.Text = "Empleados";
            btnEmpleados.UseVisualStyleBackColor = true;
            btnEmpleados.Click += btnEmpleados_Click;
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblBienvenida.Location = new Point(527, 8);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(118, 28);
            lblBienvenida.TabIndex = 3;
            lblBienvenida.Text = "Bienvenido";
            lblBienvenida.Visible = false;
            // 
            // panelContainer
            // 
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Location = new Point(0, 50);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(1251, 558);
            panelContainer.TabIndex = 0;
            // 
            // MenuForm
            // 
            ClientSize = new Size(1251, 608);
            Controls.Add(panelContainer);
            Controls.Add(panelTop);
            Name = "MenuForm";
            Text = "FootballGo - Unificado";
            Load += MenuForm_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ResumeLayout(false);
        }
        #endregion
    }
}
