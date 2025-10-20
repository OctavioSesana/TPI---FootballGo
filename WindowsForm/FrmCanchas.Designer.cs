namespace FootballGo.UI
{
    partial class FrmCanchas
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
            dgvCanchas = new DataGridView();
            btnEditar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCanchas).BeginInit();
            SuspendLayout();
            // 
            // dgvCanchas
            // 
            dgvCanchas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCanchas.Location = new Point(74, 61);
            dgvCanchas.Name = "dgvCanchas";
            dgvCanchas.ReadOnly = true;
            dgvCanchas.RowHeadersWidth = 51;
            dgvCanchas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCanchas.Size = new Size(669, 242);
            dgvCanchas.TabIndex = 0;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(649, 348);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 1;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(493, 348);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // FrmCanchas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(dgvCanchas);
            Name = "FrmCanchas";
            Text = "FrmCanchas";
            Load += FrmCanchas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCanchas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCanchas;
        private Button btnEditar;
        private Button btnEliminar;
    }
}