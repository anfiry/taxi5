using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class DriverOrderDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnAction;
        private Button btnCancel;
        private Button btnOpenInYandex;
        private Button btnBack;
        private Label lblOrderId;
        private Label lblFrom;
        private Label lblTo;
        private Label lblPrice;
        private Label lblTime;
        private Label lblStatus;
        private Label lblClientName;
        private Label lblClientPhone;
        private Label lblAcceptTime;
        private Panel topPanel;
        private Panel contentPanel;
        private Panel buttonPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.topPanel = new System.Windows.Forms.Panel();
            this.lblOrderId = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.btnOpenInYandex = new System.Windows.Forms.Button();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblClientName = new System.Windows.Forms.Label();
            this.lblClientPhone = new System.Windows.Forms.Label();
            this.lblAcceptTime = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnAction = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.topPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.topPanel.Controls.Add(this.lblOrderId);
            this.topPanel.Controls.Add(this.lblFrom);
            this.topPanel.Controls.Add(this.btnOpenInYandex);
            this.topPanel.Controls.Add(this.lblTo);
            this.topPanel.Controls.Add(this.lblClientName);
            this.topPanel.Controls.Add(this.lblClientPhone);
            this.topPanel.Controls.Add(this.lblAcceptTime);
            this.topPanel.Controls.Add(this.lblTime);
            this.topPanel.Controls.Add(this.lblStatus);
            this.topPanel.Controls.Add(this.lblPrice);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Padding = new System.Windows.Forms.Padding(40, 20, 40, 20);
            this.topPanel.Size = new System.Drawing.Size(1214, 419);
            this.topPanel.TabIndex = 0;
            // 
            // lblOrderId
            // 
            this.lblOrderId.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblOrderId.ForeColor = System.Drawing.Color.White;
            this.lblOrderId.Location = new System.Drawing.Point(40, 20);
            this.lblOrderId.Name = "lblOrderId";
            this.lblOrderId.Size = new System.Drawing.Size(1120, 50);
            this.lblOrderId.TabIndex = 0;
            this.lblOrderId.Text = "Заказ #0";
            this.lblOrderId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFrom
            // 
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblFrom.ForeColor = System.Drawing.Color.White;
            this.lblFrom.Location = new System.Drawing.Point(40, 89);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(1120, 25);
            this.lblFrom.TabIndex = 1;
            this.lblFrom.Text = "Откуда: ";
            // 
            // btnOpenInYandex
            // 
            this.btnOpenInYandex.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOpenInYandex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnOpenInYandex.FlatAppearance.BorderSize = 0;
            this.btnOpenInYandex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenInYandex.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnOpenInYandex.ForeColor = System.Drawing.Color.White;
            this.btnOpenInYandex.Location = new System.Drawing.Point(940, 332);
            this.btnOpenInYandex.Name = "btnOpenInYandex";
            this.btnOpenInYandex.Size = new System.Drawing.Size(220, 55);
            this.btnOpenInYandex.TabIndex = 2;
            this.btnOpenInYandex.Text = "НА КАРТЕ";
            this.btnOpenInYandex.UseVisualStyleBackColor = false;
            this.btnOpenInYandex.Click += new System.EventHandler(this.btnOpenInYandex_Click);
            // 
            // lblTo
            // 
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTo.ForeColor = System.Drawing.Color.White;
            this.lblTo.Location = new System.Drawing.Point(40, 123);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(1120, 25);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "Куда: ";
            // 
            // lblClientName
            // 
            this.lblClientName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblClientName.ForeColor = System.Drawing.Color.White;
            this.lblClientName.Location = new System.Drawing.Point(40, 171);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(550, 25);
            this.lblClientName.TabIndex = 3;
            this.lblClientName.Text = "Клиент: ";
            // 
            // lblClientPhone
            // 
            this.lblClientPhone.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblClientPhone.ForeColor = System.Drawing.Color.White;
            this.lblClientPhone.Location = new System.Drawing.Point(41, 211);
            this.lblClientPhone.Name = "lblClientPhone";
            this.lblClientPhone.Size = new System.Drawing.Size(550, 25);
            this.lblClientPhone.TabIndex = 4;
            this.lblClientPhone.Text = "Телефон: ";
            // 
            // lblAcceptTime
            // 
            this.lblAcceptTime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblAcceptTime.ForeColor = System.Drawing.Color.White;
            this.lblAcceptTime.Location = new System.Drawing.Point(642, 171);
            this.lblAcceptTime.Name = "lblAcceptTime";
            this.lblAcceptTime.Size = new System.Drawing.Size(550, 25);
            this.lblAcceptTime.TabIndex = 5;
            this.lblAcceptTime.Text = "Принят: ";
            this.lblAcceptTime.Visible = false;
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(642, 201);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(550, 25);
            this.lblTime.TabIndex = 6;
            this.lblTime.Text = "🕐 Создан: ";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(43, 294);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(400, 30);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Статус: ";
            // 
            // lblPrice
            // 
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblPrice.Location = new System.Drawing.Point(428, 309);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(360, 90);
            this.lblPrice.TabIndex = 8;
            this.lblPrice.Text = "0 ₽";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.contentPanel.Controls.Add(this.buttonPanel);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 419);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1214, 312);
            this.contentPanel.TabIndex = 1;
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonPanel.Controls.Add(this.panel1);
            this.buttonPanel.Controls.Add(this.btnBack);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.Location = new System.Drawing.Point(0, 0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(40, 50, 40, 50);
            this.buttonPanel.Size = new System.Drawing.Size(1214, 312);
            this.buttonPanel.TabIndex = 0;
            // 
            // btnAction
            // 
            this.btnAction.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAction.FlatAppearance.BorderSize = 0;
            this.btnAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAction.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAction.ForeColor = System.Drawing.Color.White;
            this.btnAction.Location = new System.Drawing.Point(25, 30);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(220, 55);
            this.btnAction.TabIndex = 0;
            this.btnAction.Text = "ПРИНЯТЬ";
            this.btnAction.UseVisualStyleBackColor = false;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(338, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(220, 55);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "ОТМЕНИТЬ";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(883, 223);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(220, 55);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "НАЗАД";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAction);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Location = new System.Drawing.Point(49, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(608, 119);
            this.panel1.TabIndex = 4;
            // 
            // DriverOrderDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1214, 731);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.topPanel);
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "DriverOrderDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Детали заказа";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.topPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Panel panel1;
    }
}