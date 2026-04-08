using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class DriverStatisticsRatingForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabStatistics;
        private System.Windows.Forms.TabPage tabRating;
        private System.Windows.Forms.Button btnBack;

        // Элементы вкладки Статистика
        private System.Windows.Forms.GroupBox periodGroup;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.ComboBox cmbPeriod;
        private System.Windows.Forms.Panel indicatorsPanel;
        private System.Windows.Forms.Panel cardEarnings;
        private System.Windows.Forms.Panel cardOrders;
        private System.Windows.Forms.Panel cardAvgBill;
        private System.Windows.Forms.Label lblTotalEarnings;
        private System.Windows.Forms.Label lblOrdersCount;
        private System.Windows.Forms.Label lblAvgBill;
        private System.Windows.Forms.GroupBox detailGroup;
        private System.Windows.Forms.DataGridView dgvDailyStats;

        // Элементы вкладки Рейтинг
        private System.Windows.Forms.GroupBox ratingGroup;
        private System.Windows.Forms.Label lblRatingValue;
        private System.Windows.Forms.Label lblStars;
        private System.Windows.Forms.Label lblRatingCount;
        private System.Windows.Forms.GroupBox distributionGroup;
        private System.Windows.Forms.GroupBox commentsGroup;
        private System.Windows.Forms.ListBox lstComments;

        // Массивы для распределения оценок
        private System.Windows.Forms.ProgressBar[] ratingProgressBars;
        private System.Windows.Forms.Label[] ratingCountLabels;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Настройка формы
            this.Text = "📊 Статистика и рейтинг водителя";
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // TabControl
            this.tabControl = new TabControl();
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);

            // Вкладка Статистика
            this.tabStatistics = new TabPage();
            this.tabStatistics.Text = "📈 Статистика";
            this.tabStatistics.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.tabStatistics.AutoScroll = true;

            // Вкладка Рейтинг
            this.tabRating = new TabPage();
            this.tabRating.Text = "⭐ Рейтинг и отзывы";
            this.tabRating.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.tabRating.AutoScroll = true;

            this.tabControl.TabPages.Add(this.tabStatistics);
            this.tabControl.TabPages.Add(this.tabRating);

            // ========== НАСТРОЙКА ВКЛАДКИ СТАТИСТИКА ==========

            // Группа выбора периода
            this.periodGroup = new GroupBox();
            this.periodGroup.Text = "📅 Период";
            this.periodGroup.Location = new System.Drawing.Point(20, 20);
            this.periodGroup.Size = new System.Drawing.Size(400, 65);
            this.periodGroup.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);

            this.lblPeriod = new Label();
            this.lblPeriod.Text = "Выберите период:";
            this.lblPeriod.Location = new System.Drawing.Point(15, 28);
            this.lblPeriod.Size = new System.Drawing.Size(120, 25);
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.cmbPeriod = new ComboBox();
            this.cmbPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPeriod.Items.AddRange(new object[] { "Сегодня", "Вчера", "Эта неделя", "Этот месяц", "Всё время" });
            this.cmbPeriod.Location = new System.Drawing.Point(145, 25);
            this.cmbPeriod.Size = new System.Drawing.Size(230, 28);
            this.cmbPeriod.SelectedIndex = 4;

            this.periodGroup.Controls.Add(this.lblPeriod);
            this.periodGroup.Controls.Add(this.cmbPeriod);

            // Панель карточек (просто пустая панель, карточки создадим в коде)
            this.indicatorsPanel = new Panel();
            this.indicatorsPanel.Location = new System.Drawing.Point(20, 100);
            this.indicatorsPanel.Size = new System.Drawing.Size(1140, 140);

            // Временно создаем простые панели для карточек (будут заменены в конструкторе)
            this.cardEarnings = new Panel();
            this.cardOrders = new Panel();
            this.cardAvgBill = new Panel();

            this.indicatorsPanel.Controls.Add(this.cardEarnings);
            this.indicatorsPanel.Controls.Add(this.cardOrders);
            this.indicatorsPanel.Controls.Add(this.cardAvgBill);

            // Группа детальной статистики
            this.detailGroup = new GroupBox();
            this.detailGroup.Text = "📋 Детальная статистика по заказам";
            this.detailGroup.Location = new System.Drawing.Point(20, 260);
            this.detailGroup.Size = new System.Drawing.Size(1140, 350);
            this.detailGroup.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);

            this.dgvDailyStats = new DataGridView();
            this.dgvDailyStats.Location = new System.Drawing.Point(10, 25);
            this.dgvDailyStats.Size = new System.Drawing.Size(1120, 310);
            this.dgvDailyStats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.dgvDailyStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDailyStats.ReadOnly = true;
            this.dgvDailyStats.RowHeadersVisible = false;
            this.dgvDailyStats.AllowUserToAddRows = false;
            this.dgvDailyStats.BackgroundColor = System.Drawing.Color.White;
            this.dgvDailyStats.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.dgvDailyStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Создаем колонки
            this.dgvDailyStats.Columns.Add("StartDateTime", "📅 Дата и время начала");
            this.dgvDailyStats.Columns.Add("EndDateTime", "🏁 Дата и время завершения");
            this.dgvDailyStats.Columns.Add("Passenger", "👤 Пассажир");
            this.dgvDailyStats.Columns.Add("Earnings", "💰 Заработок");
            this.dgvDailyStats.Columns.Add("Rating", "⭐ Оценка");

            this.detailGroup.Controls.Add(this.dgvDailyStats);

            // Добавление элементов на вкладку статистики
            this.tabStatistics.Controls.Add(this.periodGroup);
            this.tabStatistics.Controls.Add(this.indicatorsPanel);
            this.tabStatistics.Controls.Add(this.detailGroup);

            // ========== НАСТРОЙКА ВКЛАДКИ РЕЙТИНГ ==========

            // Группа основного рейтинга
            this.ratingGroup = new GroupBox();
            this.ratingGroup.Text = "⭐ Ваш рейтинг";
            this.ratingGroup.Location = new System.Drawing.Point(20, 20);
            this.ratingGroup.Size = new System.Drawing.Size(600, 220);
            this.ratingGroup.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);

            this.lblRatingValue = new Label();
            this.lblRatingValue.Text = "0.0";
            this.lblRatingValue.Font = new System.Drawing.Font("Segoe UI", 52, FontStyle.Bold);
            this.lblRatingValue.Location = new System.Drawing.Point(20, 20);
            this.lblRatingValue.Size = new System.Drawing.Size(220, 100);
            this.lblRatingValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRatingValue.ForeColor = System.Drawing.Color.Black;

            this.lblStars = new Label();
            this.lblStars.Text = "☆☆☆☆☆";
            this.lblStars.Font = new System.Drawing.Font("Segoe UI", 28);
            this.lblStars.Location = new System.Drawing.Point(20, 130);
            this.lblStars.Size = new System.Drawing.Size(250, 50);
            this.lblStars.ForeColor = System.Drawing.Color.Gold;

            this.lblRatingCount = new Label();
            this.lblRatingCount.Text = "На основе 0 оценок";
            this.lblRatingCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRatingCount.Location = new System.Drawing.Point(20, 180);
            this.lblRatingCount.Size = new System.Drawing.Size(300, 25);

            this.ratingGroup.Controls.Add(this.lblRatingValue);
            this.ratingGroup.Controls.Add(this.lblStars);
            this.ratingGroup.Controls.Add(this.lblRatingCount);

            // Группа распределения оценок (только контейнер)
            this.distributionGroup = new GroupBox();
            this.distributionGroup.Text = "📊 Распределение оценок";
            this.distributionGroup.Location = new System.Drawing.Point(640, 20);
            this.distributionGroup.Size = new System.Drawing.Size(700, 220);
            this.distributionGroup.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);

            // Группа комментариев
            this.commentsGroup = new GroupBox();
            this.commentsGroup.Text = "💬 Последние отзывы с комментариями";
            this.commentsGroup.Location = new System.Drawing.Point(20, 240);
            this.commentsGroup.Size = new System.Drawing.Size(1140, 300);
            this.commentsGroup.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold);

            this.lstComments = new ListBox();
            this.lstComments.Location = new System.Drawing.Point(10, 25);
            this.lstComments.Size = new System.Drawing.Size(1120, 260);
            this.lstComments.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstComments.BackColor = System.Drawing.Color.White;

            this.commentsGroup.Controls.Add(this.lstComments);

            // Добавление элементов на вкладку рейтинга
            this.tabRating.Controls.Add(this.ratingGroup);
            this.tabRating.Controls.Add(this.distributionGroup);
            this.tabRating.Controls.Add(this.commentsGroup);

            // Добавление элементов на форму
            this.Controls.Add(this.tabControl);
        }
    }
}