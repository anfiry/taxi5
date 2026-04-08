using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class DriverShiftsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Настройка формы
            this.Text = "📋 История смен";
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.MinimumSize = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Верхняя панель
            this.topPanel = new Panel();
            this.topPanel.Dock = DockStyle.Top;
            this.topPanel.Height = 60;
            this.topPanel.BackColor = Color.FromArgb(52, 73, 94);

            // Заголовок
            this.lblTitle = new Label();
            this.lblTitle.Text = "📋 История смен";
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(20, 12);
            this.lblTitle.Size = new Size(300, 35);
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            // Кнопка "Назад"
            this.btnBack = new Button();
            this.btnBack.Text = "◀ Назад";
            this.btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBack.Size = new Size(100, 35);
            this.btnBack.BackColor = Color.FromArgb(231, 76, 60);
            this.btnBack.ForeColor = Color.White;
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.Cursor = Cursors.Hand;
            this.btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Добавляем элементы на верхнюю панель
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Controls.Add(this.btnBack);

            // Настройка DataGridView
            this.dgvHistory = new DataGridView();
            this.dgvHistory.Dock = DockStyle.Fill;
            this.dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.BackgroundColor = Color.White;
            this.dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            this.dgvHistory.BorderStyle = BorderStyle.None;
            this.dgvHistory.Font = new Font("Segoe UI", 10F);

            // Колонки
            this.dgvHistory.Columns.Add("StartDateTime", "🕐 Начало смены");
            this.dgvHistory.Columns.Add("EndDateTime", "🏁 Конец смены");
            this.dgvHistory.Columns.Add("Duration", "⏱ Длительность");
            this.dgvHistory.Columns.Add("Status", "📌 Статус");

            // Настройка выравнивания
            if (this.dgvHistory.Columns["Duration"] != null)
                this.dgvHistory.Columns["Duration"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (this.dgvHistory.Columns["Status"] != null)
                this.dgvHistory.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Добавляем элементы на форму
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.topPanel);
        }
    }
}