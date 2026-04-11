using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class DriverMenu
    {
        private System.ComponentModel.IContainer components = null;
        private Button Startingshifts;
        private Button EndShif;
        private Button Orders;
        private Button Statistics;
        private Button Shifts;
        private Button buttonBack;
        private Panel panelTop;
        private Label labelTitle;
        private Label labelWelcome;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Startingshifts = new System.Windows.Forms.Button();
            this.EndShif = new System.Windows.Forms.Button();
            this.Orders = new System.Windows.Forms.Button();
            this.Statistics = new System.Windows.Forms.Button();
            this.Shifts = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // Startingshifts
            // 
            this.Startingshifts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Startingshifts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.Startingshifts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Startingshifts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.Startingshifts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.Startingshifts.Location = new System.Drawing.Point(166, 278);
            this.Startingshifts.Name = "Startingshifts";
            this.Startingshifts.Size = new System.Drawing.Size(246, 60);
            this.Startingshifts.TabIndex = 2;
            this.Startingshifts.Text = "Начать смену";
            this.Startingshifts.UseVisualStyleBackColor = false;
            this.Startingshifts.Click += new System.EventHandler(this.Startingshifts_Click);
            // 
            // EndShif
            // 
            this.EndShif.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EndShif.BackColor = System.Drawing.SystemColors.Control;
            this.EndShif.Enabled = false;
            this.EndShif.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EndShif.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.EndShif.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.EndShif.Location = new System.Drawing.Point(478, 278);
            this.EndShif.Name = "EndShif";
            this.EndShif.Size = new System.Drawing.Size(292, 60);
            this.EndShif.TabIndex = 3;
            this.EndShif.Text = "Завершить смену";
            this.EndShif.UseVisualStyleBackColor = false;
            this.EndShif.Click += new System.EventHandler(this.EndShif_Click);
            // 
            // Orders
            // 
            this.Orders.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Orders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.Orders.Enabled = false;
            this.Orders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Orders.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.Orders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.Orders.Location = new System.Drawing.Point(166, 390);
            this.Orders.Name = "Orders";
            this.Orders.Size = new System.Drawing.Size(246, 60);
            this.Orders.TabIndex = 4;
            this.Orders.Text = "Заказы";
            this.Orders.UseVisualStyleBackColor = false;
            this.Orders.Click += new System.EventHandler(this.Orders_Click);
            // 
            // Statistics
            // 
            this.Statistics.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Statistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.Statistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Statistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.Statistics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.Statistics.Location = new System.Drawing.Point(478, 390);
            this.Statistics.Name = "Statistics";
            this.Statistics.Size = new System.Drawing.Size(292, 60);
            this.Statistics.TabIndex = 5;
            this.Statistics.Text = "Статистика";
            this.Statistics.UseVisualStyleBackColor = false;
            this.Statistics.Click += new System.EventHandler(this.Statistics_Click);
            // 
            // Shifts
            // 
            this.Shifts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Shifts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.Shifts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Shifts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.Shifts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.Shifts.Location = new System.Drawing.Point(835, 390);
            this.Shifts.Name = "Shifts";
            this.Shifts.Size = new System.Drawing.Size(242, 60);
            this.Shifts.TabIndex = 6;
            this.Shifts.Text = "История смен";
            this.Shifts.UseVisualStyleBackColor = false;
            this.Shifts.Click += new System.EventHandler(this.Shifts_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.buttonBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonBack.Location = new System.Drawing.Point(835, 278);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(242, 60);
            this.buttonBack.TabIndex = 7;
            this.buttonBack.Text = "Выход";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.button4_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(52)))), ((int)(((byte)(247)))));
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1209, 80);
            this.panelTop.TabIndex = 8;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.labelTitle.Location = new System.Drawing.Point(498, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(253, 36);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Меню водителя";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.labelWelcome.Location = new System.Drawing.Point(30, 110);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(0, 25);
            this.labelWelcome.TabIndex = 1;
            // 
            // DriverMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1209, 671);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.Shifts);
            this.Controls.Add(this.Statistics);
            this.Controls.Add(this.Orders);
            this.Controls.Add(this.EndShif);
            this.Controls.Add(this.Startingshifts);
            this.Controls.Add(this.labelWelcome);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "DriverMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню водителя";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}