using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class DriverOrderDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private WebBrowser webBrowser1;
        private Button btnAction1;
        private Button btnAction2;
        private Button btnDecline;
        private Panel actionPanel;
        private Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.webBrowser1 = new WebBrowser();
            this.btnAction1 = new Button();
            this.btnAction2 = new Button();
            this.btnDecline = new Button();
            this.actionPanel = new Panel();
            this.lblStatus = new Label();
            this.actionPanel.SuspendLayout();
            this.SuspendLayout();

            // webBrowser1
            this.webBrowser1.Dock = DockStyle.Fill;
            this.webBrowser1.Location = new Point(0, 0);
            this.webBrowser1.MinimumSize = new Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new Size(800, 400);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = null;

            // btnAction1
            this.btnAction1.FlatStyle = FlatStyle.Flat;
            this.btnAction1.Font = new Font("Arial", 11F, FontStyle.Bold);
            this.btnAction1.ForeColor = Color.White;
            this.btnAction1.Location = new Point(20, 12);
            this.btnAction1.Name = "btnAction1";
            this.btnAction1.Size = new Size(180, 45);
            this.btnAction1.TabIndex = 0;
            this.btnAction1.Text = "Кнопка действия 1";
            this.btnAction1.UseVisualStyleBackColor = true;
            this.btnAction1.Visible = false;
            this.btnAction1.Click += new EventHandler(this.BtnAction1_Click);

            // btnAction2
            this.btnAction2.FlatStyle = FlatStyle.Flat;
            this.btnAction2.Font = new Font("Arial", 11F, FontStyle.Bold);
            this.btnAction2.ForeColor = Color.White;
            this.btnAction2.Location = new Point(210, 12);
            this.btnAction2.Name = "btnAction2";
            this.btnAction2.Size = new Size(180, 45);
            this.btnAction2.TabIndex = 1;
            this.btnAction2.Text = "Кнопка действия 2";
            this.btnAction2.UseVisualStyleBackColor = true;
            this.btnAction2.Visible = false;
            this.btnAction2.Click += new EventHandler(this.BtnAction2_Click);

            // btnDecline
            this.btnDecline.FlatStyle = FlatStyle.Flat;
            this.btnDecline.Font = new Font("Arial", 11F, FontStyle.Bold);
            this.btnDecline.ForeColor = Color.White;
            this.btnDecline.Location = new Point(400, 12);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new Size(180, 45);
            this.btnDecline.TabIndex = 2;
            this.btnDecline.Text = "❌ Отказаться";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Visible = false;
            this.btnDecline.Click += new EventHandler(this.BtnDecline_Click);

            // actionPanel
            this.actionPanel.BackColor = Color.FromArgb(245, 245, 245);
            this.actionPanel.BorderStyle = BorderStyle.FixedSingle;
            this.actionPanel.Controls.Add(this.btnAction1);
            this.actionPanel.Controls.Add(this.btnAction2);
            this.actionPanel.Controls.Add(this.btnDecline);
            this.actionPanel.Dock = DockStyle.Bottom;
            this.actionPanel.Location = new Point(0, 550);
            this.actionPanel.Name = "actionPanel";
            this.actionPanel.Size = new Size(984, 70);
            this.actionPanel.TabIndex = 1;

            // lblStatus
            this.lblStatus.Font = new Font("Arial", 10F, FontStyle.Bold);
            this.lblStatus.ForeColor = Color.White;
            this.lblStatus.Location = new Point(15, 115);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(300, 25);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Статус: загрузка...";

            // DriverOrderDetailsForm
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(241, 59, 198);
            this.ClientSize = new Size(984, 620);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.actionPanel);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DriverOrderDetailsForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Детали заказа";
            this.actionPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}