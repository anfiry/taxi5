namespace TaxiClientApp
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.welcomePanel = new System.Windows.Forms.Panel();
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.quickAccessPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.myOrdersButton = new System.Windows.Forms.Button();
            this.profileButton = new System.Windows.Forms.Button();
            this.newOrderButton = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.welcomePanel.SuspendLayout();
            this.quickAccessPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(823, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
            // 
            // welcomePanel
            // 
            this.welcomePanel.Controls.Add(this.welcomeLabel);
            this.welcomePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.welcomePanel.Location = new System.Drawing.Point(0, 24);
            this.welcomePanel.Name = "welcomePanel";
            this.welcomePanel.Size = new System.Drawing.Size(823, 87);
            this.welcomePanel.TabIndex = 1;
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.welcomeLabel.Location = new System.Drawing.Point(0, 0);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(823, 87);
            this.welcomeLabel.TabIndex = 0;
            this.welcomeLabel.Text = "welcomeLabel";
            this.welcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.welcomeLabel.Click += new System.EventHandler(this.welcomeLabel_Click);
            // 
            // quickAccessPanel
            // 
            this.quickAccessPanel.Controls.Add(this.myOrdersButton);
            this.quickAccessPanel.Controls.Add(this.profileButton);
            this.quickAccessPanel.Controls.Add(this.newOrderButton);
            this.quickAccessPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.quickAccessPanel.Location = new System.Drawing.Point(0, 111);
            this.quickAccessPanel.Name = "quickAccessPanel";
            this.quickAccessPanel.Padding = new System.Windows.Forms.Padding(17);
            this.quickAccessPanel.Size = new System.Drawing.Size(823, 69);
            this.quickAccessPanel.TabIndex = 2;
            this.quickAccessPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.quickAccessPanel_Paint);
            // 
            // myOrdersButton
            // 
            this.myOrdersButton.Location = new System.Drawing.Point(20, 20);
            this.myOrdersButton.Name = "myOrdersButton";
            this.myOrdersButton.Size = new System.Drawing.Size(129, 43);
            this.myOrdersButton.TabIndex = 1;
            this.myOrdersButton.UseVisualStyleBackColor = true;
            // 
            // profileButton
            // 
            this.profileButton.Location = new System.Drawing.Point(155, 20);
            this.profileButton.Name = "profileButton";
            this.profileButton.Size = new System.Drawing.Size(129, 43);
            this.profileButton.TabIndex = 2;
            this.profileButton.UseVisualStyleBackColor = true;
            this.profileButton.Click += new System.EventHandler(this.profileButton_Click);
            // 
            // newOrderButton
            // 
            this.newOrderButton.Location = new System.Drawing.Point(290, 20);
            this.newOrderButton.Name = "newOrderButton";
            this.newOrderButton.Size = new System.Drawing.Size(129, 43);
            this.newOrderButton.TabIndex = 0;
            this.newOrderButton.UseVisualStyleBackColor = true;
            this.newOrderButton.Click += new System.EventHandler(this.newOrderButton_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 180);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(17);
            this.mainPanel.Size = new System.Drawing.Size(823, 340);
            this.mainPanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 520);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.quickAccessPanel);
            this.Controls.Add(this.welcomePanel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.welcomePanel.ResumeLayout(false);
            this.quickAccessPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Panel welcomePanel;
        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.FlowLayoutPanel quickAccessPanel;
        private System.Windows.Forms.Button newOrderButton;
        private System.Windows.Forms.Button myOrdersButton;
        private System.Windows.Forms.Button profileButton;
        private System.Windows.Forms.Panel mainPanel;
    }
}