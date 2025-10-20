namespace MisCanchasApp
{
    partial class ListadoCanchasForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvCanchas;
        private System.Windows.Forms.Label lblTitulo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgvCanchas = new System.Windows.Forms.DataGridView();
            this.lblTitulo = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCanchas)).BeginInit();
            this.SuspendLayout();

            // 
            // dgvCanchas
            // 
            this.dgvCanchas.AllowUserToAddRows = false;
            this.dgvCanchas.AllowUserToDeleteRows = false;
            this.dgvCanchas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCanchas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCanchas.Location = new System.Drawing.Point(25, 70);
            this.dgvCanchas.Name = "dgvCanchas";
            this.dgvCanchas.ReadOnly = true;
            this.dgvCanchas.RowHeadersVisible = false;
            this.dgvCanchas.Size = new System.Drawing.Size(750, 350);
            this.dgvCanchas.TabIndex = 0;
            this.dgvCanchas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCanchas_CellClick);

            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(25, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(400, 30);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Listado de Canchas Disponibles";

            // 
            // ListadoCanchasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dgvCanchas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ListadoCanchasForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listado de Canchas";
            this.Load += new System.EventHandler(this.ListadoCanchasForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvCanchas)).EndInit();
            this.ResumeLayout(false);

            // Agregamos el botón “Reservar”
            var reservarButton = new System.Windows.Forms.DataGridViewButtonColumn();
            reservarButton.HeaderText = "Acción";
            reservarButton.Name = "Reservar";
            reservarButton.Text = "Reservar";
            reservarButton.UseColumnTextForButtonValue = true;
            dgvCanchas.Columns.Add(reservarButton);
        }
        #endregion
    }
}
