using System.Drawing;
using System.Windows.Forms;

namespace FootballGo.UI
{
    partial class EmpleadoDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip;
        private Panel contentPanel;

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
        }
    }
}
