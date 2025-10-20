namespace FootballGo.UI
{
    partial class ClienteDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip;
        private Panel contentPanel;
        private Label lblSesion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.lblSesion = new System.Windows.Forms.Label();
            //this.IsMdiContainer = true;
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 28);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(800, 422);
            this.contentPanel.TabIndex = 1;
            // 
            // lblSesion
            // 
            this.lblSesion.AutoSize = true;
            this.lblSesion.Location = new System.Drawing.Point(12, 40);
            this.lblSesion.Name = "lblSesion";
            this.lblSesion.Size = new System.Drawing.Size(160, 20);
            this.lblSesion.TabIndex = 2;
            // 
            // ClienteDashboardForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblSesion);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ClienteDashboardForm";
            this.Text = "Dashboard Cliente";
            this.Load += new System.EventHandler(this.ClienteDashboardForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
