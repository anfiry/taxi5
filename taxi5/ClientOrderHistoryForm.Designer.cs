using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class ClientOrderHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridViewOrders;
        private Button buttonBack;
        private Button buttonRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewOrders = new DataGridView();
            this.buttonBack = new Button();
            this.buttonRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)this.dataGridViewOrders).BeginInit();
            this.SuspendLayout();

            // dataGridViewOrders
            this.dataGridViewOrders.AllowUserToAddRows = false;
            this.dataGridViewOrders.AllowUserToDeleteRows = false;
            this.dataGridViewOrders.AllowUserToResizeRows = false;
            this.dataGridViewOrders.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.dataGridViewOrders.BackgroundColor = Color.White;
            this.dataGridViewOrders.BorderStyle = BorderStyle.Fixed3D;
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new Point(12, 70);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.ReadOnly = true;
            this.dataGridViewOrders.RowHeadersVisible = false;
            this.dataGridViewOrders.RowHeadersWidth = 51;
            this.dataGridViewOrders.Size = new Size(1060, 500);
            this.dataGridViewOrders.TabIndex = 0;
            this.dataGridViewOrders.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridViewOrders_CellDoubleClick);

            // buttonBack
            this.buttonBack.BackColor = Color.FromArgb(255, 227, 227);
            this.buttonBack.FlatStyle = FlatStyle.Flat;
            this.buttonBack.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.buttonBack.ForeColor = Color.FromArgb(4, 0, 66);
            this.buttonBack.Location = new Point(980, 20);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new Size(100, 40);
            this.buttonBack.TabIndex = 2;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new EventHandler(this.buttonBack_Click);

            // buttonRefresh
            this.buttonRefresh.BackColor = Color.FromArgb(251, 228, 255);
            this.buttonRefresh.FlatStyle = FlatStyle.Flat;
            this.buttonRefresh.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            this.buttonRefresh.ForeColor = Color.FromArgb(4, 0, 66);
            this.buttonRefresh.Location = new Point(860, 20);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new Size(100, 40);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new EventHandler(this.buttonRefresh_Click);

            // ClientOrderHistoryForm
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(1084, 600);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.dataGridViewOrders);
            this.MinimumSize = new Size(1100, 640);
            this.Name = "ClientOrderHistoryForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "История поездок";
            ((System.ComponentModel.ISupportInitialize)this.dataGridViewOrders).EndInit();
            this.ResumeLayout(false);
        }
    }
}