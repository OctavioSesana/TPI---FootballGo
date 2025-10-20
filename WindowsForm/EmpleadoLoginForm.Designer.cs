namespace FootballGo.UI
{
    partial class EmpleadoLoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblDni;
        private Label lblContrasenia;
        private TextBox txtDni;
        private TextBox txtContrasenia;
        private Button btnIngresar;
        private Button btnRegistro;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblDni = new Label();
            lblContrasenia = new Label();
            txtDni = new TextBox();
            txtContrasenia = new TextBox();
            btnIngresar = new Button();
            btnRegistro = new Button();
            SuspendLayout();
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(22, 48);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(38, 20);
            lblDni.TabIndex = 2;
            lblDni.Text = "DNI:";
            // 
            // lblContrasenia
            // 
            lblContrasenia.AutoSize = true;
            lblContrasenia.Location = new Point(22, 88);
            lblContrasenia.Name = "lblContrasenia";
            lblContrasenia.Size = new Size(86, 20);
            lblContrasenia.TabIndex = 3;
            lblContrasenia.Text = "Contraseña:";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(122, 45);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(200, 27);
            txtDni.TabIndex = 6;
            // 
            // txtContrasenia
            // 
            txtContrasenia.Location = new Point(122, 85);
            txtContrasenia.Name = "txtContrasenia";
            txtContrasenia.PasswordChar = '*';
            txtContrasenia.Size = new Size(200, 27);
            txtContrasenia.TabIndex = 7;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(122, 133);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(90, 30);
            btnIngresar.TabIndex = 8;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnRegistro
            // 
            btnRegistro.Location = new Point(232, 133);
            btnRegistro.Name = "btnRegistro";
            btnRegistro.Size = new Size(90, 30);
            btnRegistro.TabIndex = 9;
            btnRegistro.Text = "Registrarse";
            btnRegistro.UseVisualStyleBackColor = true;
            btnRegistro.Click += btnRegistro_Click;
            // 
            // EmpleadoLoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(370, 250);
            Controls.Add(btnRegistro);
            Controls.Add(btnIngresar);
            Controls.Add(txtContrasenia);
            Controls.Add(txtDni);
            Controls.Add(lblContrasenia);
            Controls.Add(lblDni);
            Name = "EmpleadoLoginForm";
            Text = "Inicio de sesión - Empleado";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
