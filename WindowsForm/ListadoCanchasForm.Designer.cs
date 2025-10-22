namespace MisCanchasApp
{
    partial class ListadoCanchasForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvCanchas;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBoxFiltros;
        private System.Windows.Forms.RadioButton rbFiltroTodos;
        private System.Windows.Forms.RadioButton rbFiltro5;
        private System.Windows.Forms.RadioButton rbFiltro7;

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
            this.groupBoxFiltros = new System.Windows.Forms.GroupBox();
            this.rbFiltro7 = new System.Windows.Forms.RadioButton();
            this.rbFiltro5 = new System.Windows.Forms.RadioButton();
            this.rbFiltroTodos = new System.Windows.Forms.RadioButton();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCanchas)).BeginInit();
            // Iniciar GroupBox para que podamos agregarle los RadioButtons
            this.groupBoxFiltros.SuspendLayout();
            this.SuspendLayout();

            // 
            // dgvCanchas
            // 
            this.dgvCanchas.AllowUserToAddRows = false;
            this.dgvCanchas.AllowUserToDeleteRows = false;
            this.dgvCanchas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCanchas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // *** CAMBIO: Movido de 70 a 110 para dejar espacio al filtro ***
            this.dgvCanchas.Location = new System.Drawing.Point(25, 110);
            this.dgvCanchas.Name = "dgvCanchas";
            this.dgvCanchas.ReadOnly = true;
            this.dgvCanchas.RowHeadersVisible = false;

            // *** CAMBIO: Reducido de 350 a 310 para compensar el movimiento ***
            this.dgvCanchas.Size = new System.Drawing.Size(750, 310);
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
            // groupBoxFiltros (Añadiendo propiedades de posición y contenido)
            // 
            this.groupBoxFiltros.Controls.Add(this.rbFiltro7);
            this.groupBoxFiltros.Controls.Add(this.rbFiltro5);
            this.groupBoxFiltros.Controls.Add(this.rbFiltroTodos);
            this.groupBoxFiltros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxFiltros.Location = new System.Drawing.Point(25, 60);
            this.groupBoxFiltros.Name = "groupBoxFiltros";
            this.groupBoxFiltros.Size = new System.Drawing.Size(350, 45);
            this.groupBoxFiltros.TabIndex = 2;
            this.groupBoxFiltros.TabStop = false;
            this.groupBoxFiltros.Text = "Filtrar por Tipo de Cancha";

            // 
            // rbFiltro7 (Añadiendo propiedades)
            // 
            this.rbFiltro7.AutoSize = true;
            this.rbFiltro7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbFiltro7.Location = new System.Drawing.Point(200, 18); // Posición dentro del GroupBox
            this.rbFiltro7.Name = "rbFiltro7";
            this.rbFiltro7.Size = new System.Drawing.Size(95, 20);
            this.rbFiltro7.TabIndex = 2;
            this.rbFiltro7.Text = "Fútbol 7";
            this.rbFiltro7.UseVisualStyleBackColor = true;

            // 
            // rbFiltro5 (Añadiendo propiedades)
            // 
            this.rbFiltro5.AutoSize = true;
            this.rbFiltro5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbFiltro5.Location = new System.Drawing.Point(100, 18); // Posición dentro del GroupBox
            this.rbFiltro5.Name = "rbFiltro5";
            this.rbFiltro5.Size = new System.Drawing.Size(95, 20);
            this.rbFiltro5.TabIndex = 1;
            this.rbFiltro5.Text = "Fútbol 5";
            this.rbFiltro5.UseVisualStyleBackColor = true;

            // 
            // rbFiltroTodos (Añadiendo propiedades)
            // 
            this.rbFiltroTodos.AutoSize = true;
            this.rbFiltroTodos.Checked = true; // Valor por defecto
            this.rbFiltroTodos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbFiltroTodos.Location = new System.Drawing.Point(20, 18); // Posición dentro del GroupBox
            this.rbFiltroTodos.Name = "rbFiltroTodos";
            this.rbFiltroTodos.Size = new System.Drawing.Size(65, 20);
            this.rbFiltroTodos.TabIndex = 0;
            this.rbFiltroTodos.TabStop = true;
            this.rbFiltroTodos.Text = "Todas";
            this.rbFiltroTodos.UseVisualStyleBackColor = true;

            // 
            // ListadoCanchasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.groupBoxFiltros); // Esto ya estaba bien
            this.Controls.Add(this.dgvCanchas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ListadoCanchasForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listado de Canchas";
            this.Load += new System.EventHandler(this.ListadoCanchasForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvCanchas)).EndInit();

            // Finalizar GroupBox
            this.groupBoxFiltros.ResumeLayout(false);
            this.groupBoxFiltros.PerformLayout();

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
