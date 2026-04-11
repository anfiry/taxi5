using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class AdminOrderForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewOrders;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonApplyFilter;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxStatusFilter;
        private System.Windows.Forms.Label labelStatusFilter;
        private System.Windows.Forms.Panel panelTop;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonApplyFilter = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxStatusFilter = new System.Windows.Forms.ComboBox();
            this.labelStatusFilter = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.AllowUserToAddRows = false;
            this.dataGridViewOrders.AllowUserToDeleteRows = false;
            this.dataGridViewOrders.AllowUserToResizeRows = false;
            this.dataGridViewOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOrders.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewOrders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(12, 95);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.ReadOnly = true;
            this.dataGridViewOrders.RowHeadersVisible = false;
            this.dataGridViewOrders.RowHeadersWidth = 51;
            this.dataGridViewOrders.Size = new System.Drawing.Size(1471, 738);
            this.dataGridViewOrders.TabIndex = 0;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonRefresh.Location = new System.Drawing.Point(1232, 22);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(119, 40);
            this.buttonRefresh.TabIndex = 0;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonSearch.Location = new System.Drawing.Point(930, 22);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(90, 40);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "Найти";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonBack.Location = new System.Drawing.Point(1369, 22);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(106, 40);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonApplyFilter
            // 
            this.buttonApplyFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.buttonApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApplyFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonApplyFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonApplyFilter.Location = new System.Drawing.Point(386, 22);
            this.buttonApplyFilter.Name = "buttonApplyFilter";
            this.buttonApplyFilter.Size = new System.Drawing.Size(134, 40);
            this.buttonApplyFilter.TabIndex = 7;
            this.buttonApplyFilter.Text = "Применить";
            this.buttonApplyFilter.UseVisualStyleBackColor = false;
            this.buttonApplyFilter.Click += new System.EventHandler(this.buttonApplyFilter_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxSearch.Location = new System.Drawing.Point(715, 31);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(200, 24);
            this.textBoxSearch.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(647, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 2;
            this.label8.Text = "Поиск:";
            // 
            // comboBoxStatusFilter
            // 
            this.comboBoxStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatusFilter.FormattingEnabled = true;
            this.comboBoxStatusFilter.Location = new System.Drawing.Point(181, 31);
            this.comboBoxStatusFilter.Name = "comboBoxStatusFilter";
            this.comboBoxStatusFilter.Size = new System.Drawing.Size(180, 24);
            this.comboBoxStatusFilter.TabIndex = 6;
            // 
            // labelStatusFilter
            // 
            this.labelStatusFilter.AutoSize = true;
            this.labelStatusFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.labelStatusFilter.Location = new System.Drawing.Point(30, 35);
            this.labelStatusFilter.Name = "labelStatusFilter";
            this.labelStatusFilter.Size = new System.Drawing.Size(126, 18);
            this.labelStatusFilter.TabIndex = 5;
            this.labelStatusFilter.Text = "Статус заказа:";
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.buttonApplyFilter);
            this.panelTop.Controls.Add(this.comboBoxStatusFilter);
            this.panelTop.Controls.Add(this.labelStatusFilter);
            this.panelTop.Controls.Add(this.buttonSearch);
            this.panelTop.Controls.Add(this.textBoxSearch);
            this.panelTop.Controls.Add(this.label8);
            this.panelTop.Controls.Add(this.buttonBack);
            this.panelTop.Controls.Add(this.buttonRefresh);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1495, 80);
            this.panelTop.TabIndex = 8;
            // 
            // AdminOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1495, 845);
            this.Controls.Add(this.dataGridViewOrders);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(1200, 664);
            this.Name = "AdminOrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Просмотр заказов";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}