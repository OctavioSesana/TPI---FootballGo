using System.Drawing;
using System.Windows.Forms;

namespace FootballGo.UI
{
    partial class EmpleadoDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip;
        private Panel contentPanel;
        private System.Windows.Forms.DataGridView dgvReservas;
        private System.Windows.Forms.Button btnOrdenarAsc;
        private System.Windows.Forms.Button btnOrdenarDesc;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            contentPanel = new Panel();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1000, 28);
            menuStrip.TabIndex = 2;
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.White;
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(0, 28);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(1000, 622);
            contentPanel.TabIndex = 0;
            contentPanel.Paint += contentPanel_Paint;
            // 
            // EmpleadoDashboardForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Controls.Add(contentPanel);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "EmpleadoDashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FootballGo - Unificado";
            Load += EmpleadoDashboardForm_Load;
            ResumeLayout(false);
            PerformLayout();
            // btnOrdenarAsc
            // 
            this.btnOrdenarAsc = new System.Windows.Forms.Button();
            this.btnOrdenarAsc.Text = "Fecha Asc.";
            this.btnOrdenarAsc.Location = new System.Drawing.Point(20, 10); // Ejemplo: En la esquina superior izquierda del panel
            this.btnOrdenarAsc.Name = "btnOrdenarAsc";
            this.btnOrdenarAsc.Size = new System.Drawing.Size(200, 30);
            this.btnOrdenarAsc.UseVisualStyleBackColor = true;
            this.btnOrdenarAsc.Click += new System.EventHandler(this.btnOrdenarAsc_Click); // ASOCIAR EVENTO
            this.contentPanel.Controls.Add(this.btnOrdenarAsc);
            this.btnOrdenarAsc.Visible = false;
            // 
            // btnOrdenarDesc
            // 
            this.btnOrdenarDesc = new System.Windows.Forms.Button();
            this.btnOrdenarDesc.Text = "Fecha Desc.";
            this.btnOrdenarDesc.Location = new System.Drawing.Point(230, 10); // Ubicado a la derecha del botón Ascendente
            this.btnOrdenarDesc.Name = "btnOrdenarDesc";
            this.btnOrdenarDesc.Size = new System.Drawing.Size(200, 30);
            this.btnOrdenarDesc.UseVisualStyleBackColor = true;
            this.btnOrdenarDesc.Click += new System.EventHandler(this.btnOrdenarDesc_Click); // ASOCIAR EVENTO
            this.contentPanel.Controls.Add(this.btnOrdenarDesc);
            this.btnOrdenarDesc.Visible = false;
            // 
            // dgvReservas
            // 
            this.dgvReservas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservas)).BeginInit();
            this.dgvReservas.AllowUserToOrderColumns = true;

            this.dgvReservas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReservas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReservas.Location = new System.Drawing.Point(20, 50);
            this.dgvReservas.Name = "dgvReservas";
            this.dgvReservas.ReadOnly = true;
            this.dgvReservas.Size = new System.Drawing.Size(960, 600);
            this.dgvReservas.TabIndex = 5;
            this.dgvReservas.Visible = false;

            this.contentPanel.Controls.Add(this.dgvReservas);

            ((System.ComponentModel.ISupportInitialize)(this.dgvReservas)).EndInit();

        }
    }
}
