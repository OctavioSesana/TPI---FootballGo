namespace FootballGo.UI
{
    partial class ClienteDetailsForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.lblName = new Label();
            this.lblLastName = new Label();
            this.lblDNI = new Label();
            this.lblMail = new Label();
            this.labelContrasenia = new Label();
            this.lblTel = new Label();
            this.lblFecha = new Label();

            this.txtNombre = new TextBox();
            this.txtApellido = new TextBox();
            this.txtDNI = new TextBox();
            this.txtEmail = new TextBox();
            this.txtContrasenia = new TextBox();
            this.txtTel = new TextBox();
            this.dtpFechaAlta = new DateTimePicker();

            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(12, 26);
            this.lblName.Text = "Nombre:";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new Point(12, 63);
            this.lblLastName.Text = "Apellido:";
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Location = new Point(12, 102);
            this.lblDNI.Text = "DNI:";
            // 
            // lblMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.Location = new Point(12, 141);
            this.lblMail.Text = "Email:";
            // 
            // labelContrasenia
            // 
            this.labelContrasenia.AutoSize = true;
            this.labelContrasenia.Location = new Point(12, 180);
            this.labelContrasenia.Text = "Contraseña:";
            // 
            // lblTel
            // 
            this.lblTel.AutoSize = true;
            this.lblTel.Location = new Point(12, 219);
            this.lblTel.Text = "Tel.:";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new Point(12, 258);
            this.lblFecha.Text = "Fecha:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new Point(110, 23);
            this.txtNombre.Size = new Size(196, 27);
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new Point(110, 60);
            this.txtApellido.Size = new Size(196, 27);
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new Point(110, 99);
            this.txtDNI.Size = new Size(196, 27);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new Point(110, 138);
            this.txtEmail.Size = new Size(196, 27);
            // 
            // txtContrasenia
            // 
            this.txtContrasenia.Location = new Point(110, 177);
            this.txtContrasenia.Size = new Size(196, 27);
            this.txtContrasenia.PasswordChar = '*';
            // 
            // txtTel
            // 
            this.txtTel.Location = new Point(110, 216);
            this.txtTel.Size = new Size(196, 27);
            // 
            // dtpFechaAlta
            // 
            this.dtpFechaAlta.Location = new Point(110, 255);
            this.dtpFechaAlta.Size = new Size(196, 27);
            // 
            // btnSave
            // 
            this.btnSave.Location = new Point(110, 300);
            this.btnSave.Size = new Size(94, 29);
            this.btnSave.Text = "Guardar";
            this.btnSave.Click += new EventHandler(this.btnGuardar_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(212, 300);
            this.btnCancel.Size = new Size(94, 29);
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Click += new EventHandler(this.btnCancelar_Click);
            // 
            // ClienteDetailsForm
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(360, 360);
            this.Controls.AddRange(new Control[] {
                lblName, txtNombre,
                lblLastName, txtApellido,
                lblDNI, txtDNI,
                lblMail, txtEmail,
                labelContrasenia, txtContrasenia,
                lblTel, txtTel,
                lblFecha, dtpFechaAlta,
                btnSave, btnCancel
            });
            this.Name = "ClienteDetailsForm";
            this.Text = "Detalles Cliente";
            //this.Load += new EventHandler(this.ClienteDetailsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private Label lblName;
        private Label lblLastName;
        private Label lblDNI;
        private Label lblMail;
        private Label labelContrasenia;
        private Label lblTel;
        private Label lblFecha;

        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtDNI;
        private TextBox txtEmail;
        private TextBox txtContrasenia;
        private TextBox txtTel;

        private DateTimePicker dtpFechaAlta;
        private Button btnSave;
        private Button btnCancel;
    }
}
