using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class DriverOrdersForm
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flowLayoutPanel;
        private Button btnBack;
        private Label lblActiveOrderInfo;
        private Panel panelTop;
        private Label labelTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblActiveOrderInfo = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(20, 130);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel.Size = new System.Drawing.Size(1163, 516);
            this.flowLayoutPanel.TabIndex = 3;
            this.flowLayoutPanel.WrapContents = false;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(1070, 15);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 40);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "◀ Назад";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // lblActiveOrderInfo
            // 
            this.lblActiveOrderInfo.BackColor = System.Drawing.Color.FromArgb(255, 200, 200);
            this.lblActiveOrderInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblActiveOrderInfo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblActiveOrderInfo.ForeColor = System.Drawing.Color.DarkRed;
            this.lblActiveOrderInfo.Location = new System.Drawing.Point(0, 70);
            this.lblActiveOrderInfo.Name = "lblActiveOrderInfo";
            this.lblActiveOrderInfo.Size = new System.Drawing.Size(1203, 40);
            this.lblActiveOrderInfo.TabIndex = 2;
            this.lblActiveOrderInfo.Text = "⚠️ У вас уже есть активный заказ. Завершите его, чтобы видеть новые заказы.";
            this.lblActiveOrderInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblActiveOrderInfo.Visible = false;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Controls.Add(this.btnBack);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1203, 70);
            this.panelTop.TabIndex = 4;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(440, 15);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(280, 32);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Доступные заказы";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DriverOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1203, 680);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.lblActiveOrderInfo);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "DriverOrdersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Доступные заказы";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}