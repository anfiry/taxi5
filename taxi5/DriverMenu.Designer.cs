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
        private Button button4; // выход
        private Panel panel1;    // для перетаскивания
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
            this.button4 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Startingshifts
            // 
            this.Startingshifts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.Startingshifts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Startingshifts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.Startingshifts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.Startingshifts.Location = new System.Drawing.Point(50, 150);
            this.Startingshifts.Name = "Startingshifts";
            this.Startingshifts.Size = new System.Drawing.Size(180, 50);
            this.Startingshifts.TabIndex = 0;
            this.Startingshifts.Text = "Начать смену";
            this.Startingshifts.UseVisualStyleBackColor = false;
            this.Startingshifts.Click += new System.EventHandler(this.Startingshifts_Click);
            // 
            // EndShif
            // 
            this.EndShif.BackColor = System.Drawing.SystemColors.Control;
            this.EndShif.Enabled = false;
            this.EndShif.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EndShif.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.EndShif.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.EndShif.Location = new System.Drawing.Point(260, 150);
            this.EndShif.Name = "EndShif";
            this.EndShif.Size = new System.Drawing.Size(180, 50);
            this.EndShif.TabIndex = 1;
            this.EndShif.Text = "Завершить смену";
            this.EndShif.UseVisualStyleBackColor = false;
            this.EndShif.Click += new System.EventHandler(this.EndShif_Click);
            // 
            // Orders
            // 
            this.Orders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.Orders.Enabled = false;
            this.Orders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Orders.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.Orders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.Orders.Location = new System.Drawing.Point(50, 230);
            this.Orders.Name = "Orders";
            this.Orders.Size = new System.Drawing.Size(180, 50);
            this.Orders.TabIndex = 2;
            this.Orders.Text = "Заказы";
            this.Orders.UseVisualStyleBackColor = false;
            this.Orders.Click += new System.EventHandler(this.Orders_Click);
            // 
            // Statistics
            // 
            this.Statistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.Statistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Statistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.Statistics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.Statistics.Location = new System.Drawing.Point(260, 230);
            this.Statistics.Name = "Statistics";
            this.Statistics.Size = new System.Drawing.Size(180, 50);
            this.Statistics.TabIndex = 3;
            this.Statistics.Text = "Статистика";
            this.Statistics.UseVisualStyleBackColor = false;
            this.Statistics.Click += new System.EventHandler(this.Statistics_Click);
            // 
            // Shifts
            // 
            this.Shifts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.Shifts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Shifts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.Shifts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.Shifts.Location = new System.Drawing.Point(470, 230);
            this.Shifts.Name = "Shifts";
            this.Shifts.Size = new System.Drawing.Size(180, 50);
            this.Shifts.TabIndex = 4;
            this.Shifts.Text = "История смен";
            this.Shifts.UseVisualStyleBackColor = false;
            this.Shifts.Click += new System.EventHandler(this.Shifts_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.button4.Location = new System.Drawing.Point(470, 150);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(180, 50);
            this.button4.TabIndex = 5;
            this.button4.Text = "Выход";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 30);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.labelTitle.Location = new System.Drawing.Point(50, 60);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(223, 31);
            this.labelTitle.TabIndex = 7;
            this.labelTitle.Text = "Меню водителя";
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.labelWelcome.Location = new System.Drawing.Point(50, 100);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(0, 20);
            this.labelWelcome.TabIndex = 8;
            // 
            // DriverMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.labelWelcome);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.Shifts);
            this.Controls.Add(this.Statistics);
            this.Controls.Add(this.Orders);
            this.Controls.Add(this.EndShif);
            this.Controls.Add(this.Startingshifts);
            this.Name = "DriverMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню водителя";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DriverMenu_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DriverMenu_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}