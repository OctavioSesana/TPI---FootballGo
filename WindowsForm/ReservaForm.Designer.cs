
namespace FootballGo.UI
{
    partial class ReservaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCancha;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.DataGridView dgvTurnos;
        private System.Windows.Forms.Button btnCerrar;

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
            this.lblCancha = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.dgvTurnos = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCancha
            // 
            this.lblCancha.AutoSize = true;
            this.lblCancha.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCancha.Location = new System.Drawing.Point(20, 20);
            this.lblCancha.Name = "lblCancha";
            this.lblCancha.Size = new System.Drawing.Size(124, 25);
            this.lblCancha.TabIndex = 0;
            this.lblCancha.Text = "Cancha N° X";
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPrecio.Location = new System.Drawing.Point(20, 50);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(84, 23);
            this.lblPrecio.TabIndex = 1;
            this.lblPrecio.Text = "Precio: $0";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFecha.Location = new System.Drawing.Point(20, 85);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(60, 23);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFecha.Location = new System.Drawing.Point(90, 80);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(250, 30);
            this.dtpFecha.TabIndex = 3;
            // 
            // dgvTurnos
            // 
            this.dgvTurnos.AllowUserToAddRows = false;
            this.dgvTurnos.AllowUserToDeleteRows = false;
            this.dgvTurnos.AllowUserToResizeRows = false;
            this.dgvTurnos.BackgroundColor = System.Drawing.Color.White;
            this.dgvTurnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTurnos.Location = new System.Drawing.Point(20, 130);
            this.dgvTurnos.MultiSelect = false;
            this.dgvTurnos.Name = "dgvTurnos";
            this.dgvTurnos.ReadOnly = true;
            this.dgvTurnos.RowHeadersVisible = false;
            this.dgvTurnos.RowTemplate.Height = 28;
            this.dgvTurnos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTurnos.Size = new System.Drawing.Size(450, 400);
            this.dgvTurnos.TabIndex = 4;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.LightGray;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.Location = new System.Drawing.Point(370, 550);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(100, 35);
            this.btnCerrar.TabIndex = 5;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // ReservaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 600);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvTurnos);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.lblCancha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReservaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reserva de Turno";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
