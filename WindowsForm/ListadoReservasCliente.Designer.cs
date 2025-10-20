namespace MisCanchasApp
{
    partial class ListadoReservasUsuarioForm
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
            dgvReservas = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvReservas).BeginInit();
            SuspendLayout();
            // 
            // dgvReservas
            // 
            dgvReservas.AllowUserToAddRows = false;
            dgvReservas.AllowUserToDeleteRows = false;
            dgvReservas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReservas.Dock = DockStyle.Fill;
            dgvReservas.Location = new Point(0, 0);
            dgvReservas.Margin = new Padding(3, 4, 3, 4);
            dgvReservas.MultiSelect = false;
            dgvReservas.Name = "dgvReservas";
            dgvReservas.ReadOnly = true;
            dgvReservas.RowHeadersVisible = false;
            dgvReservas.RowHeadersWidth = 51;
            dgvReservas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReservas.Size = new Size(587, 281);
            dgvReservas.TabIndex = 0;
            dgvReservas.CellContentClick += dgvReservas_CellContentClick;
            // 
            // ListadoReservasUsuarioForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(587, 281);
            Controls.Add(dgvReservas);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ListadoReservasUsuarioForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mis Reservas";
            ((System.ComponentModel.ISupportInitialize)dgvReservas).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReservas;
    }
}