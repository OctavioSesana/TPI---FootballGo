namespace FootballGo.UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvCliente = new DataGridView();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCliente).BeginInit();
            SuspendLayout();
            // 
            // dgvCliente
            // 
            dgvCliente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCliente.Location = new Point(12, 12);
            dgvCliente.Name = "dgvCliente";
            dgvCliente.RowHeadersWidth = 51;
            dgvCliente.Size = new Size(1188, 218);
            dgvCliente.TabIndex = 0;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1106, 236);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Eliminar";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1212, 450);
            Controls.Add(btnDelete);
            Controls.Add(dgvCliente);
            Name = "Form1";
            Text = "Clientes";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCliente).EndInit();
            ResumeLayout(false);
        }
        // Add the missing event handler method for button1_Click
        #endregion

        private DataGridView dgvCliente;
        private Button btnDelete;
    }
}
