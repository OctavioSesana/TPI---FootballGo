namespace FootballGo.UI
{
    partial class FrmCanchaEdicion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            nudNro = new NumericUpDown();
            cboEstado = new ComboBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            cboTipoCancha = new ComboBox();
            nudPrecio = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)nudNro).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).BeginInit();
            SuspendLayout();
            // 
            // nudNro
            // 
            nudNro.Location = new Point(273, 76);
            nudNro.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNro.Name = "nudNro";
            nudNro.Size = new Size(150, 27);
            nudNro.TabIndex = 0;
            nudNro.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudNro.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // cboEstado
            // 
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.FormattingEnabled = true;
            cboEstado.Location = new Point(272, 122);
            cboEstado.Name = "cboEstado";
            cboEstado.Size = new Size(151, 28);
            cboEstado.TabIndex = 1;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(360, 283);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(216, 283);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // cboTipoCancha
            // 
            cboTipoCancha.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoCancha.FormattingEnabled = true;
            cboTipoCancha.Location = new Point(272, 166);
            cboTipoCancha.Name = "cboTipoCancha";
            cboTipoCancha.Size = new Size(151, 28);
            cboTipoCancha.TabIndex = 4;
            // 
            // nudPrecio
            // 
            nudPrecio.Location = new Point(272, 215);
            nudPrecio.Maximum = new decimal(new int[] { 1215752191, 23, 0, 0 });
            nudPrecio.Name = "nudPrecio";
            nudPrecio.Size = new Size(151, 27);
            nudPrecio.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(158, 174);
            label1.Name = "label1";
            label1.Size = new Size(89, 20);
            label1.TabIndex = 6;
            label1.Text = "Tipo cancha";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(136, 222);
            label2.Name = "label2";
            label2.Size = new Size(111, 20);
            label2.TabIndex = 7;
            label2.Text = "Precio por hora";
            //label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(184, 78);
            label3.Name = "label3";
            label3.Size = new Size(63, 20);
            label3.TabIndex = 8;
            label3.Text = "Numero";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(193, 122);
            label4.Name = "label4";
            label4.Size = new Size(54, 20);
            label4.TabIndex = 9;
            label4.Text = "Estado";
            // 
            // FrmCanchaEdicion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(nudPrecio);
            Controls.Add(cboTipoCancha);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(cboEstado);
            Controls.Add(nudNro);
            Name = "FrmCanchaEdicion";
            Text = "FrmCanchaEdicion";
            Load += FrmCanchaEdicion_Load;
            ((System.ComponentModel.ISupportInitialize)nudNro).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        // Add this method to handle the ValueChanged event for nudNro
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // Implement the logic for handling the ValueChanged event here
            // For example:
            MessageBox.Show($"Nuevo valor: {nudNro.Value}");
        }

        #endregion

        private NumericUpDown nudNro;
        private ComboBox cboEstado;
        private Button btnGuardar;
        private Button btnCancelar;
        private ComboBox cboTipoCancha;
        private NumericUpDown nudPrecio;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}