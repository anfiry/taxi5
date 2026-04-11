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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.periodGroup = new System.Windows.Forms.GroupBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.cmbPeriod = new System.Windows.Forms.ComboBox();
            this.indicatorsPanel = new System.Windows.Forms.Panel();
            this.cardEarnings = new System.Windows.Forms.Panel();
            this.cardOrders = new System.Windows.Forms.Panel();
            this.cardAvgBill = new System.Windows.Forms.Panel();
            this.detailGroup = new System.Windows.Forms.GroupBox();
            this.dgvDailyStats = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabRating = new System.Windows.Forms.TabPage();
            this.ratingGroup = new System.Windows.Forms.GroupBox();
            this.lblRatingValue = new System.Windows.Forms.Label();
            this.lblStars = new System.Windows.Forms.Label();
            this.lblRatingCount = new System.Windows.Forms.Label();
            this.distributionGroup = new System.Windows.Forms.GroupBox();
            this.commentsGroup = new System.Windows.Forms.GroupBox();
            this.lstComments = new System.Windows.Forms.ListBox();
            this.tabControl.SuspendLayout();
            this.tabStatistics.SuspendLayout();
            this.periodGroup.SuspendLayout();
            this.indicatorsPanel.SuspendLayout();
            this.detailGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDailyStats)).BeginInit();
            this.tabRating.SuspendLayout();
            this.ratingGroup.SuspendLayout();
            this.commentsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabStatistics);
            this.tabControl.Controls.Add(this.tabRating);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(982, 653);
            this.tabControl.TabIndex = 0;
            // 
            // tabStatistics
            // 
            this.tabStatistics.AutoScroll = true;
            this.tabStatistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tabStatistics.Controls.Add(this.button1);
            this.tabStatistics.Controls.Add(this.periodGroup);
            this.tabStatistics.Controls.Add(this.indicatorsPanel);
            this.tabStatistics.Controls.Add(this.detailGroup);
            this.tabStatistics.Location = new System.Drawing.Point(4, 32);
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Size = new System.Drawing.Size(974, 617);
            this.tabStatistics.TabIndex = 0;
            this.tabStatistics.Text = "📈 Статистика";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(850, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // periodGroup
            // 
            this.periodGroup.Controls.Add(this.lblPeriod);
            this.periodGroup.Controls.Add(this.cmbPeriod);
            this.periodGroup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.periodGroup.Location = new System.Drawing.Point(20, 20);
            this.periodGroup.Name = "periodGroup";
            this.periodGroup.Size = new System.Drawing.Size(400, 65);
            this.periodGroup.TabIndex = 0;
            this.periodGroup.TabStop = false;
            this.periodGroup.Text = "📅 Период";
            // 
            // lblPeriod
            // 
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPeriod.Location = new System.Drawing.Point(15, 28);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(120, 25);
            this.lblPeriod.TabIndex = 0;
            this.lblPeriod.Text = "Выберите период:";
            // 
            // cmbPeriod
            // 
            this.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriod.Items.AddRange(new object[] {
            "Сегодня",
            "Вчера",
            "Эта неделя",
            "Этот месяц",
            "Всё время"});
            this.cmbPeriod.Location = new System.Drawing.Point(145, 25);
            this.cmbPeriod.Name = "cmbPeriod";
            this.cmbPeriod.Size = new System.Drawing.Size(230, 28);
            this.cmbPeriod.TabIndex = 1;
            // 
            // indicatorsPanel
            // 
            this.indicatorsPanel.Controls.Add(this.cardEarnings);
            this.indicatorsPanel.Controls.Add(this.cardOrders);
            this.indicatorsPanel.Controls.Add(this.cardAvgBill);
            this.indicatorsPanel.Location = new System.Drawing.Point(20, 100);
            this.indicatorsPanel.Name = "indicatorsPanel";
            this.indicatorsPanel.Size = new System.Drawing.Size(1140, 140);
            this.indicatorsPanel.TabIndex = 1;
            // 
            // cardEarnings
            // 
            this.cardEarnings.Location = new System.Drawing.Point(0, 0);
            this.cardEarnings.Name = "cardEarnings";
            this.cardEarnings.Size = new System.Drawing.Size(200, 100);
            this.cardEarnings.TabIndex = 0;
            // 
            // cardOrders
            // 
            this.cardOrders.Location = new System.Drawing.Point(0, 0);
            this.cardOrders.Name = "cardOrders";
            this.cardOrders.Size = new System.Drawing.Size(200, 100);
            this.cardOrders.TabIndex = 1;
            // 
            // cardAvgBill
            // 
            this.cardAvgBill.Location = new System.Drawing.Point(0, 0);
            this.cardAvgBill.Name = "cardAvgBill";
            this.cardAvgBill.Size = new System.Drawing.Size(200, 100);
            this.cardAvgBill.TabIndex = 2;
            // 
            // detailGroup
            // 
            this.detailGroup.Controls.Add(this.dgvDailyStats);
            this.detailGroup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.detailGroup.Location = new System.Drawing.Point(20, 260);
            this.detailGroup.Name = "detailGroup";
            this.detailGroup.Size = new System.Drawing.Size(1140, 350);
            this.detailGroup.TabIndex = 2;
            this.detailGroup.TabStop = false;
            this.detailGroup.Text = "📋 Детальная статистика по заказам";
            // 
            // dgvDailyStats
            // 
            this.dgvDailyStats.AllowUserToAddRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.dgvDailyStats.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDailyStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDailyStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDailyStats.BackgroundColor = System.Drawing.Color.White;
            this.dgvDailyStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDailyStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dgvDailyStats.Location = new System.Drawing.Point(10, 25);
            this.dgvDailyStats.Name = "dgvDailyStats";
            this.dgvDailyStats.ReadOnly = true;
            this.dgvDailyStats.RowHeadersVisible = false;
            this.dgvDailyStats.RowHeadersWidth = 51;
            this.dgvDailyStats.Size = new System.Drawing.Size(1120, 310);
            this.dgvDailyStats.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "📅 Дата и время начала";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "🏁 Дата и время завершения";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "👤 Пассажир";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "💰 Заработок";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "⭐ Оценка";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // tabRating
            // 
            this.tabRating.AutoScroll = true;
            this.tabRating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tabRating.Controls.Add(this.ratingGroup);
            this.tabRating.Controls.Add(this.distributionGroup);
            this.tabRating.Controls.Add(this.commentsGroup);
            this.tabRating.Location = new System.Drawing.Point(4, 32);
            this.tabRating.Name = "tabRating";
            this.tabRating.Size = new System.Drawing.Size(974, 617);
            this.tabRating.TabIndex = 1;
            this.tabRating.Text = "⭐ Рейтинг и отзывы";
            // 
            // ratingGroup
            // 
            this.ratingGroup.Controls.Add(this.lblRatingValue);
            this.ratingGroup.Controls.Add(this.lblStars);
            this.ratingGroup.Controls.Add(this.lblRatingCount);
            this.ratingGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ratingGroup.Location = new System.Drawing.Point(20, 20);
            this.ratingGroup.Name = "ratingGroup";
            this.ratingGroup.Size = new System.Drawing.Size(600, 220);
            this.ratingGroup.TabIndex = 0;
            this.ratingGroup.TabStop = false;
            this.ratingGroup.Text = "⭐ Ваш рейтинг";
            // 
            // lblRatingValue
            // 
            this.lblRatingValue.Font = new System.Drawing.Font("Segoe UI", 52F, System.Drawing.FontStyle.Bold);
            this.lblRatingValue.ForeColor = System.Drawing.Color.Black;
            this.lblRatingValue.Location = new System.Drawing.Point(20, 20);
            this.lblRatingValue.Name = "lblRatingValue";
            this.lblRatingValue.Size = new System.Drawing.Size(220, 100);
            this.lblRatingValue.TabIndex = 0;
            this.lblRatingValue.Text = "0.0";
            this.lblRatingValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStars
            // 
            this.lblStars.Font = new System.Drawing.Font("Segoe UI", 28F);
            this.lblStars.ForeColor = System.Drawing.Color.Gold;
            this.lblStars.Location = new System.Drawing.Point(20, 130);
            this.lblStars.Name = "lblStars";
            this.lblStars.Size = new System.Drawing.Size(250, 50);
            this.lblStars.TabIndex = 1;
            this.lblStars.Text = "☆☆☆☆☆";
            // 
            // lblRatingCount
            // 
            this.lblRatingCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRatingCount.Location = new System.Drawing.Point(20, 180);
            this.lblRatingCount.Name = "lblRatingCount";
            this.lblRatingCount.Size = new System.Drawing.Size(300, 25);
            this.lblRatingCount.TabIndex = 2;
            this.lblRatingCount.Text = "На основе 0 оценок";
            // 
            // distributionGroup
            // 
            this.distributionGroup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.distributionGroup.Location = new System.Drawing.Point(640, 20);
            this.distributionGroup.Name = "distributionGroup";
            this.distributionGroup.Size = new System.Drawing.Size(700, 220);
            this.distributionGroup.TabIndex = 1;
            this.distributionGroup.TabStop = false;
            this.distributionGroup.Text = "📊 Распределение оценок";
            // 
            // commentsGroup
            // 
            this.commentsGroup.Controls.Add(this.lstComments);
            this.commentsGroup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.commentsGroup.Location = new System.Drawing.Point(20, 240);
            this.commentsGroup.Name = "commentsGroup";
            this.commentsGroup.Size = new System.Drawing.Size(1140, 300);
            this.commentsGroup.TabIndex = 2;
            this.commentsGroup.TabStop = false;
            this.commentsGroup.Text = "💬 Последние отзывы с комментариями";
            // 
            // lstComments
            // 
            this.lstComments.BackColor = System.Drawing.Color.White;
            this.lstComments.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstComments.ItemHeight = 23;
            this.lstComments.Location = new System.Drawing.Point(10, 25);
            this.lstComments.Name = "lstComments";
            this.lstComments.Size = new System.Drawing.Size(1120, 257);
            this.lstComments.TabIndex = 0;
            // 
            // DriverStatisticsRatingForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "DriverStatisticsRatingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "📊 Статистика и рейтинг водителя";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl.ResumeLayout(false);
            this.tabStatistics.ResumeLayout(false);
            this.periodGroup.ResumeLayout(false);
            this.indicatorsPanel.ResumeLayout(false);
            this.detailGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDailyStats)).EndInit();
            this.tabRating.ResumeLayout(false);
            this.ratingGroup.ResumeLayout(false);
            this.commentsGroup.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Button button1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}