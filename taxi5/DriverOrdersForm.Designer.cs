using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class DriverOrdersForm
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnBack;
        private Label lblActiveOrderInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.btnBack = new Button();
            this.lblActiveOrderInfo = new Label();
            this.SuspendLayout();

            // flowLayoutPanel1
            this.flowLayoutPanel1.Dock = DockStyle.Fill;
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = Color.White;
            this.flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Padding = new Padding(10);
            this.flowLayoutPanel1.Margin = new Padding(0);
            this.flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutPanel1.WrapContents = false;
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";

            // btnBack
            this.btnBack.Text = "← Назад";
            this.btnBack.Font = new Font("Arial", 11, FontStyle.Bold);
            this.btnBack.Size = new Size(100, 40);
            this.btnBack.BackColor = Color.FromArgb(192, 176, 212);
            this.btnBack.ForeColor = Color.White;
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.Cursor = Cursors.Hand;
            this.btnBack.Dock = DockStyle.Bottom;
            this.btnBack.Height = 50;
            this.btnBack.Name = "btnBack";
            this.btnBack.Click += new EventHandler(this.BtnBack_Click);

            // lblActiveOrderInfo
            this.lblActiveOrderInfo.Dock = DockStyle.Top;
            this.lblActiveOrderInfo.Height = 40;
            this.lblActiveOrderInfo.BackColor = Color.FromArgb(255, 200, 200);
            this.lblActiveOrderInfo.ForeColor = Color.DarkRed;
            this.lblActiveOrderInfo.Font = new Font("Arial", 11, FontStyle.Bold);
            this.lblActiveOrderInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblActiveOrderInfo.Visible = false;
            this.lblActiveOrderInfo.Name = "lblActiveOrderInfo";

            // DriverOrdersForm
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 600);
            this.MinimumSize = new Size(900, 600);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblActiveOrderInfo);
            this.Text = "Доступные заказы";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(241, 59, 198);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DriverOrdersForm";
            this.ResumeLayout(false);
        }
    }
}