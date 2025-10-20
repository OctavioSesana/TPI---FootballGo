namespace FootballGo.UI
{
    public partial class EmpleadoDetailsForm:Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            lblNombre = new Label();
            lblApellido = new Label();
            lblDNI = new Label();
            lblSueldo = new Label();
            lblEstado = new Label();
            lblFecha = new Label();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtDNI = new TextBox();
            txtSueldo = new TextBox();
            txtEstado = new TextBox();
            dtpFechaIngreso = new DateTimePicker();
            btnSave = new Button();
            btnCancel = new Button();
            txtContrasenia = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(14, 27);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(67, 20);
            lblNombre.TabIndex = 13;
            lblNombre.Text = "Nombre:";
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(14, 73);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(69, 20);
            lblApellido.TabIndex = 11;
            lblApellido.Text = "Apellido:";
            // 
            // lblDNI
            // 
            lblDNI.AutoSize = true;
            lblDNI.Location = new Point(14, 120);
            lblDNI.Name = "lblDNI";
            lblDNI.Size = new Size(38, 20);
            lblDNI.TabIndex = 9;
            lblDNI.Text = "DNI:";
            // 
            // lblSueldo
            // 
            lblSueldo.AutoSize = true;
            lblSueldo.Location = new Point(13, 190);
            lblSueldo.Name = "lblSueldo";
            lblSueldo.Size = new Size(117, 20);
            lblSueldo.TabIndex = 7;
            lblSueldo.Text = "Sueldo semanal:";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(13, 236);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(57, 20);
            lblEstado.TabIndex = 5;
            lblEstado.Text = "Estado:";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(13, 283);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(50, 20);
            lblFecha.TabIndex = 3;
            lblFecha.Text = "Fecha:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(137, 23);
            txtNombre.Margin = new Padding(3, 4, 3, 4);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(228, 27);
            txtNombre.TabIndex = 12;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(137, 69);
            txtApellido.Margin = new Padding(3, 4, 3, 4);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(228, 27);
            txtApellido.TabIndex = 10;
            // 
            // txtDNI
            // 
            txtDNI.Location = new Point(137, 116);
            txtDNI.Margin = new Padding(3, 4, 3, 4);
            txtDNI.Name = "txtDNI";
            txtDNI.Size = new Size(228, 27);
            txtDNI.TabIndex = 8;
            // 
            // txtSueldo
            // 
            txtSueldo.Location = new Point(136, 186);
            txtSueldo.Margin = new Padding(3, 4, 3, 4);
            txtSueldo.Name = "txtSueldo";
            txtSueldo.Size = new Size(228, 27);
            txtSueldo.TabIndex = 6;
            // 
            // txtEstado
            // 
            txtEstado.Location = new Point(136, 232);
            txtEstado.Margin = new Padding(3, 4, 3, 4);
            txtEstado.Name = "txtEstado";
            txtEstado.Size = new Size(228, 27);
            txtEstado.TabIndex = 4;
            // 
            // dtpFechaIngreso
            // 
            dtpFechaIngreso.Location = new Point(136, 279);
            dtpFechaIngreso.Margin = new Padding(3, 4, 3, 4);
            dtpFechaIngreso.Name = "dtpFechaIngreso";
            dtpFechaIngreso.Size = new Size(228, 27);
            dtpFechaIngreso.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(136, 330);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(103, 40);
            btnSave.TabIndex = 1;
            btnSave.Text = "Guardar";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnGuardar_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(262, 330);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(103, 40);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancelar_Click;
            // 
            // txtContrasenia
            // 
            txtContrasenia.Location = new Point(137, 151);
            txtContrasenia.Margin = new Padding(3, 4, 3, 4);
            txtContrasenia.Name = "txtContrasenia";
            txtContrasenia.Size = new Size(228, 27);
            txtContrasenia.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 155);
            label1.Name = "label1";
            label1.Size = new Size(86, 20);
            label1.TabIndex = 15;
            label1.Text = "Contraseña:";
            // 
            // EmpleadoDetailsForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(1191, 455);
            Controls.Add(txtContrasenia);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(dtpFechaIngreso);
            Controls.Add(lblFecha);
            Controls.Add(txtEstado);
            Controls.Add(lblEstado);
            Controls.Add(txtSueldo);
            Controls.Add(lblSueldo);
            Controls.Add(txtDNI);
            Controls.Add(lblDNI);
            Controls.Add(txtApellido);
            Controls.Add(lblApellido);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Margin = new Padding(3, 4, 3, 4);
            Name = "EmpleadoDetailsForm";
            Text = "Nuevo empleado";
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Label lblNombre, lblApellido, lblDNI, lblSueldo, lblEstado, lblFecha;
        private TextBox txtNombre, txtApellido, txtDNI, txtSueldo, txtEstado;
        private DateTimePicker dtpFechaIngreso;
        private Button btnSave, btnCancel;
        private TextBox txtContrasenia;
        private Label label1;
    }
}
