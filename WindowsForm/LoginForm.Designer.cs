namespace FootballGo.UI
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblEmail;
        private Label lblPassword;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnIngresar;
        private Button btnRegistrarse;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblEmail = new Label();
            lblPassword = new Label();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            btnIngresar = new Button();
            btnRegistrarse = new Button();
            SuspendLayout();
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(30, 30);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 20);
            lblEmail.TabIndex = 0;
            lblEmail.Text = "Email:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(30, 75);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(86, 20);
            lblPassword.TabIndex = 1;
            lblPassword.Text = "Contraseña:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(130, 27);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 27);
            txtEmail.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(130, 72);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(200, 27);
            txtPassword.TabIndex = 3;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(130, 120);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(94, 29);
            btnIngresar.TabIndex = 4;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnRegistrarse
            // 
            btnRegistrarse.Location = new Point(236, 120);
            btnRegistrarse.Name = "btnRegistrarse";
            btnRegistrarse.Size = new Size(94, 29);
            btnRegistrarse.TabIndex = 5;
            btnRegistrarse.Text = "Registrarse";
            btnRegistrarse.UseVisualStyleBackColor = true;
            btnRegistrarse.Click += btnRegistrarse_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(465, 242);
            Controls.Add(btnRegistrarse);
            Controls.Add(btnIngresar);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Controls.Add(lblPassword);
            Controls.Add(lblEmail);
            Name = "LoginForm";
            Text = "Iniciar Sesión";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
