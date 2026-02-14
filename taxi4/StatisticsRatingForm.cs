using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Npgsql;

namespace taxi4
{
    public partial class StatisticsRatingForm : Form
    {
        // Параметры подключения и водителя
        private int driverId;
        private string connectionString;

        // ---- Элементы вкладки "Статистика" ----
        private Label lblTotalEarnings;   // карточка "Общий заработок"
        private Label lblOrdersCount;     // карточка "Выполнено заказов"
        private Label lblAvgBill;         // карточка "Средний чек"
        private Label lblOnlineTime;      // карточка "Время онлайн"
        private DataGridView dgvDailyStats; // таблица детальной статистики
        private ComboBox cmbPeriod;       // выбор периода

        // ---- Элементы вкладки "Рейтинг" ----
        private Label lblRatingValue;     // большая цифра рейтинга
        private Label lblStars;           // звёзды (текст)
        private Label lblRatingCount;     // количество оценок
        private ProgressBar[] ratingProgressBars; // прогресс-бары распределения
        private Label[] ratingCountLabels;        // подписи количества
        private ListBox lstComments;      // последние отзывы
        private Label lblVeal;            // вежливость
        private Label lblDriving;         // аккуратность
        private Label lblClean;           // чистота
        private Label lblPunctual;        // пунктуальность

        public StatisticsRatingForm()
        {
            InitializeComponent();
            throw new Exception("Используйте конструктор с параметрами driverId и connectionString");
        }

        // Основной конструктор – вызывается из меню водителя
        public StatisticsRatingForm(int driverId, string connectionString)
        {
            this.driverId = driverId;
            this.connectionString = connectionString;

            InitializeComponent();
            this.Text = "Статистика и рейтинг";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;

            // Создаём вкладки
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            TabPage tabStatistics = new TabPage("📊 Статистика");
            TabPage tabRating = new TabPage("⭐ Рейтинг");
            TabPage tabAchievements = new TabPage("🏆 Достижения");

            SetupStatisticsTab(tabStatistics);
            SetupRatingTab(tabRating);
            SetupAchievementsTab(tabAchievements); // оставлено без изменений (демо)

            tabControl.TabPages.Add(tabStatistics);
            tabControl.TabPages.Add(tabRating);
            tabControl.TabPages.Add(tabAchievements);

            this.Controls.Add(tabControl);

            // Загружаем данные при открытии формы (период = "Сегодня")
            LoadStatistics(0);
        }

        // ---------- ВКЛАДКА СТАТИСТИКА ----------
        private void SetupStatisticsTab(TabPage tab)
        {
            tab.BackColor = Color.White;

            // Группа выбора периода
            GroupBox periodGroup = new GroupBox();
            periodGroup.Text = "Период";
            periodGroup.Location = new Point(10, 10);
            periodGroup.Size = new Size(300, 60);

            cmbPeriod = new ComboBox();
            cmbPeriod.Items.AddRange(new string[] {
                "Сегодня", "Вчера", "Эта неделя", "Этот месяц", "Все время"
            });
            cmbPeriod.SelectedIndex = 0;
            cmbPeriod.Location = new Point(10, 20);
            cmbPeriod.Size = new Size(280, 25);
            cmbPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPeriod.SelectedIndexChanged += (s, e) => LoadStatistics(cmbPeriod.SelectedIndex);
            periodGroup.Controls.Add(cmbPeriod);

            // Панель с карточками
            Panel indicatorsPanel = new Panel();
            indicatorsPanel.Location = new Point(10, 80);
            indicatorsPanel.Size = new Size(960, 150);

            // Создаём карточки (значения будут заменены из БД)
            Panel cardEarnings = CreateStatCard("💰 Общий заработок", "0 ₽", "загрузка...", Color.LightGreen);
            cardEarnings.Location = new Point(0, 0);
            Panel cardOrders = CreateStatCard("📦 Выполнено заказов", "0", "загрузка...", Color.LightBlue);
            cardOrders.Location = new Point(240, 0);
            Panel cardAvgBill = CreateStatCard("📈 Средний чек", "0 ₽", "загрузка...", Color.LightYellow);
            cardAvgBill.Location = new Point(480, 0);
            Panel cardOnlineTime = CreateStatCard("⏱️ Время онлайн", "0 ч", "загрузка...", Color.LightCoral);
            cardOnlineTime.Location = new Point(720, 0);

            // Сохраняем ссылки на значения карточек (это Labels, которые находятся на позиции [1] в Controls)
            lblTotalEarnings = (Label)cardEarnings.Controls[1];
            lblOrdersCount = (Label)cardOrders.Controls[1];
            lblAvgBill = (Label)cardAvgBill.Controls[1];
            lblOnlineTime = (Label)cardOnlineTime.Controls[1];

            indicatorsPanel.Controls.AddRange(new Control[] { cardEarnings, cardOrders, cardAvgBill, cardOnlineTime });

            // Детальная статистика – таблица
            GroupBox detailGroup = new GroupBox();
            detailGroup.Text = "Детальная статистика";
            detailGroup.Location = new Point(10, 240);
            detailGroup.Size = new Size(960, 380);

            dgvDailyStats = new DataGridView();
            dgvDailyStats.Location = new Point(10, 20);
            dgvDailyStats.Size = new Size(940, 350);
            dgvDailyStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDailyStats.ReadOnly = true;
            dgvDailyStats.RowHeadersVisible = false;
            dgvDailyStats.AllowUserToAddRows = false;
            dgvDailyStats.BackgroundColor = Color.White;
            dgvDailyStats.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Колонки
            dgvDailyStats.Columns.Add("Date", "Дата");
            dgvDailyStats.Columns.Add("Orders", "Заказы");
            dgvDailyStats.Columns.Add("Earnings", "Заработок");
            dgvDailyStats.Columns.Add("Hours", "Часы онлайн");
            dgvDailyStats.Columns.Add("Rating", "Ср. оценка");
            dgvDailyStats.Columns.Add("Tips", "Чаевые");

            dgvDailyStats.Columns["Earnings"].DefaultCellStyle.Format = "C0";
            dgvDailyStats.Columns["Earnings"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDailyStats.Columns["Rating"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDailyStats.Columns["Orders"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDailyStats.Columns["Hours"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDailyStats.Columns["Tips"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            detailGroup.Controls.Add(dgvDailyStats);

            // Кнопка экспорта (оставлена без изменений)
            Button btnExport = new Button();
            btnExport.Text = "📊 Экспорт в Excel";
            btnExport.Location = new Point(820, 625);
            btnExport.Size = new Size(150, 35);
            btnExport.BackColor = Color.LightGreen;
            btnExport.Click += BtnExport_Click;

            tab.Controls.AddRange(new Control[] { periodGroup, indicatorsPanel, detailGroup, btnExport });
        }

        // Вспомогательный метод для создания карточки
        private Panel CreateStatCard(string title, string value, string change, Color color)
        {
            Panel card = new Panel();
            card.Size = new Size(230, 140);
            card.BackColor = color;
            card.BorderStyle = BorderStyle.FixedSingle;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            lblTitle.Location = new Point(10, 10);
            lblTitle.Size = new Size(210, 20);

            Label lblValue = new Label();
            lblValue.Text = value;
            lblValue.Font = new Font("Arial", 24, FontStyle.Bold);
            lblValue.Location = new Point(10, 40);
            lblValue.Size = new Size(210, 40);

            Label lblChange = new Label();
            lblChange.Text = change;
            lblChange.Font = new Font("Arial", 9);
            lblChange.Location = new Point(10, 90);
            lblChange.Size = new Size(210, 20);

            if (change.Contains("+"))
                lblChange.ForeColor = Color.Green;
            else if (change.Contains("-"))
                lblChange.ForeColor = Color.Red;

            card.Controls.AddRange(new Control[] { lblTitle, lblValue, lblChange });
            return card;
        }

        // ---------- ВКЛАДКА РЕЙТИНГ ----------
        private void SetupRatingTab(TabPage tab)
        {
            tab.BackColor = Color.White;

            // Основной рейтинг
            GroupBox ratingGroup = new GroupBox();
            ratingGroup.Text = "Ваш рейтинг";
            ratingGroup.Location = new Point(10, 10);
            ratingGroup.Size = new Size(480, 200);

            lblRatingValue = new Label();
            lblRatingValue.Text = "0.0";
            lblRatingValue.Font = new Font("Arial", 48, FontStyle.Bold);
            lblRatingValue.Location = new Point(20, 30);
            lblRatingValue.Size = new Size(150, 80);
            lblRatingValue.TextAlign = ContentAlignment.MiddleCenter;

            lblStars = new Label();
            lblStars.Text = "☆☆☆☆☆";
            lblStars.Font = new Font("Arial", 24);
            lblStars.Location = new Point(20, 120);
            lblStars.Size = new Size(150, 40);
            lblStars.ForeColor = Color.Gold;

            lblRatingCount = new Label();
            lblRatingCount.Text = "На основе 0 оценок";
            lblRatingCount.Font = new Font("Arial", 12);
            lblRatingCount.Location = new Point(20, 170);
            lblRatingCount.Size = new Size(200, 20);

            ratingGroup.Controls.AddRange(new Control[] { lblRatingValue, lblStars, lblRatingCount });

            // Распределение оценок
            GroupBox distributionGroup = new GroupBox();
            distributionGroup.Text = "Распределение оценок";
            distributionGroup.Location = new Point(500, 10);
            distributionGroup.Size = new Size(470, 200);

            ratingProgressBars = new ProgressBar[5];
            ratingCountLabels = new Label[5];
            string[] ratingLabels = { "⭐⭐⭐⭐⭐", "⭐⭐⭐⭐", "⭐⭐⭐", "⭐⭐", "⭐" };

            for (int i = 0; i < 5; i++)
            {
                int y = 30 + i * 35;

                Label lblStarLabel = new Label();
                lblStarLabel.Text = ratingLabels[i];
                lblStarLabel.Location = new Point(10, y);
                lblStarLabel.Size = new Size(80, 30);
                lblStarLabel.Font = new Font("Arial", 10);
                lblStarLabel.ForeColor = Color.Gold;

                ProgressBar progressBar = new ProgressBar();
                progressBar.Location = new Point(100, y);
                progressBar.Size = new Size(200, 25);
                progressBar.Maximum = 100;
                progressBar.Value = 0;
                ratingProgressBars[i] = progressBar;

                Label lblCount = new Label();
                lblCount.Text = "0 (0%)";
                lblCount.Location = new Point(310, y);
                lblCount.Size = new Size(150, 30);
                lblCount.Font = new Font("Arial", 9);
                ratingCountLabels[i] = lblCount;

                distributionGroup.Controls.AddRange(new Control[] { lblStarLabel, progressBar, lblCount });
            }

            // Комментарии пассажиров
            GroupBox commentsGroup = new GroupBox();
            commentsGroup.Text = "Последние отзывы";
            commentsGroup.Location = new Point(10, 220);
            commentsGroup.Size = new Size(960, 250);

            lstComments = new ListBox();
            lstComments.Location = new Point(10, 20);
            lstComments.Size = new Size(940, 220);
            lstComments.Font = new Font("Arial", 10);
            lstComments.BackColor = Color.White;
            commentsGroup.Controls.Add(lstComments);

            // Средние показатели
            GroupBox metricsGroup = new GroupBox();
            metricsGroup.Text = "Ваши показатели";
            metricsGroup.Location = new Point(10, 480);
            metricsGroup.Size = new Size(960, 80);

            string[] metrics = { "Вежливость", "Аккуратность", "Чистота", "Пунктуальность" };

            for (int i = 0; i < metrics.Length; i++)
            {
                Label lblMetric = new Label();
                lblMetric.Text = $"{metrics[i]}: 0%";
                lblMetric.Location = new Point(20 + i * 230, 30);
                lblMetric.Size = new Size(220, 30);
                lblMetric.Font = new Font("Arial", 10, FontStyle.Bold);
                lblMetric.TextAlign = ContentAlignment.MiddleCenter;
                lblMetric.BackColor = Color.LightBlue;
                lblMetric.BorderStyle = BorderStyle.FixedSingle;

                switch (i)
                {
                    case 0: lblVeal = lblMetric; break;
                    case 1: lblDriving = lblMetric; break;
                    case 2: lblClean = lblMetric; break;
                    case 3: lblPunctual = lblMetric; break;
                }
                metricsGroup.Controls.Add(lblMetric);
            }

            tab.Controls.AddRange(new Control[] { ratingGroup, distributionGroup, commentsGroup, metricsGroup });
        }

        // ---------- ВКЛАДКА ДОСТИЖЕНИЯ (без изменений, демо) ----------
        private void SetupAchievementsTab(TabPage tab)
        {
            tab.BackColor = Color.White;

            Label lblTitle = new Label();
            lblTitle.Text = "🏆 Ваши достижения";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.Location = new Point(10, 10);
            lblTitle.Size = new Size(300, 30);

            GroupBox levelGroup = new GroupBox();
            levelGroup.Text = "Уровень водителя: ЭКСПЕРТ";
            levelGroup.Location = new Point(10, 50);
            levelGroup.Size = new Size(960, 80);

            Label lblLevel = new Label();
            lblLevel.Text = "Уровень 7";
            lblLevel.Font = new Font("Arial", 14, FontStyle.Bold);
            lblLevel.Location = new Point(20, 25);
            lblLevel.Size = new Size(100, 30);

            ProgressBar levelProgress = new ProgressBar();
            levelProgress.Location = new Point(130, 30);
            levelProgress.Size = new Size(700, 25);
            levelProgress.Maximum = 1000;
            levelProgress.Value = 750;

            Label lblProgress = new Label();
            lblProgress.Text = "750/1000 XP";
            lblProgress.Location = new Point(840, 30);
            lblProgress.Size = new Size(100, 25);

            levelGroup.Controls.AddRange(new Control[] { lblLevel, levelProgress, lblProgress });

            GroupBox achievementsGroup = new GroupBox();
            achievementsGroup.Text = "Полученные достижения";
            achievementsGroup.Location = new Point(10, 140);
            achievementsGroup.Size = new Size(960, 400);

            FlowLayoutPanel achievementsPanel = new FlowLayoutPanel();
            achievementsPanel.Location = new Point(10, 20);
            achievementsPanel.Size = new Size(940, 370);
            achievementsPanel.AutoScroll = true;

            var achievements = new[]
            {
                new { Icon = "🚗", Title = "Первый заказ", Desc = "Выполните первый заказ", Earned = true },
                new { Icon = "💯", Title = "Сотый заказ", Desc = "Выполните 100 заказов", Earned = true },
                new { Icon = "⭐", Title = "Пятизвёздочный", Desc = "Получите 50 оценок '5 звезд'", Earned = true },
                new { Icon = "⏰", Title = "Ночной волк", Desc = "Работайте ночью 50 часов", Earned = false },
                new { Icon = "💰", Title = "Золотые руки", Desc = "Заработайте 100 000 ₽", Earned = true },
                new { Icon = "👑", Title = "Король дорог", Desc = "Будьте в топ-10 неделю", Earned = false },
                new { Icon = "🚀", Title = "Скоростной", Desc = "Выполните 10 заказов за час", Earned = true },
                new { Icon = "❤️", Title = "Любимчик", Desc = "Получите 20 повторных заказов", Earned = false },
                new { Icon = "🧹", Title = "Чистюля", Desc = "Посетители довольны чистотой", Earned = false },
                new { Icon = "🧠", Title = "Повелитель пиков", Desc = "Проработать 12 часов в час пик без нервного срыва", Earned = true }
            };

            foreach (var a in achievements)
            {
                Panel p = CreateAchievementCard(a.Icon, a.Title, a.Desc, a.Earned);
                achievementsPanel.Controls.Add(p);
            }

            achievementsGroup.Controls.Add(achievementsPanel);
            tab.Controls.AddRange(new Control[] { lblTitle, levelGroup, achievementsGroup });
        }

        private Panel CreateAchievementCard(string icon, string title, string description, bool earned)
        {
            Panel panel = new Panel();
            panel.Size = new Size(220, 150);
            panel.BackColor = earned ? Color.LightGreen : Color.LightGray;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Margin = new Padding(10);

            Label lblIcon = new Label();
            lblIcon.Text = icon;
            lblIcon.Font = new Font("Arial", 24);
            lblIcon.Location = new Point(10, 10);
            lblIcon.Size = new Size(200, 40);
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTitle.Location = new Point(10, 60);
            lblTitle.Size = new Size(200, 25);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            Label lblStatus = new Label();
            lblStatus.Text = earned ? "✅ Получено" : "❌ Не получено";
            lblStatus.ForeColor = earned ? Color.DarkGreen : Color.DarkRed;
            lblStatus.Font = new Font("Arial", 9);
            lblStatus.Location = new Point(10, 90);
            lblStatus.Size = new Size(200, 20);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(panel, description);

            panel.Controls.AddRange(new Control[] { lblIcon, lblTitle, lblStatus });
            return panel;
        }

        // ---------- ЗАГРУЗКА ДАННЫХ ИЗ БАЗЫ ДАННЫХ ----------
        private void LoadStatistics(int periodIndex)
        {
            // Выполняем запросы в фоновом потоке, чтобы не блокировать UI
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();

                        // ---- 1. Общий заработок, количество заказов, средний чек ----
                        string earningsQuery = @"
                            SELECT 
                                COALESCE(SUM(o.final_cost), 0) AS total_earnings,
                                COUNT(o.order_id) AS total_orders,
                                COALESCE(AVG(o.final_cost), 0) AS avg_bill
                            FROM ""Order"" o
                            WHERE o.driver_id = @driver_id
                              AND o.order_status = (SELECT order_status_id FROM order_status WHERE name = 'Завершён' LIMIT 1)";

                        using (var cmd = new NpgsqlCommand(earningsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", driverId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    decimal totalEarnings = reader.GetDecimal(0);
                                    int totalOrders = reader.GetInt32(1);
                                    decimal avgBill = reader.GetDecimal(2);
                                    this.Invoke((Action)(() =>
                                    {
                                        lblTotalEarnings.Text = $"{totalEarnings:N0} ₽";
                                        lblOrdersCount.Text = totalOrders.ToString();
                                        lblAvgBill.Text = $"{avgBill:N0} ₽";
                                    }));
                                }
                            }
                        }

                        // ---- 2. Чаевые ----
                        string tipsQuery = @"
                            SELECT COALESCE(SUM(op.promotion_amount), 0)
                            FROM order_promotion op
                            JOIN ""Order"" o ON op.order_id = o.order_id
                            WHERE o.driver_id = @driver_id
                              AND o.order_status = (SELECT order_status_id FROM order_status WHERE name = 'Завершён' LIMIT 1)";

                        decimal totalTips = 0;
                        using (var cmd = new NpgsqlCommand(tipsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", driverId);
                            totalTips = Convert.ToDecimal(cmd.ExecuteScalar());
                        }

                        // ---- 3. Время онлайн ----
                        string onlineTimeQuery = @"
                            SELECT COALESCE(SUM(EXTRACT(EPOCH FROM (end_time - start_time)) / 3600), 0)
                            FROM work_schedule
                            WHERE driver_id = @driver_id
                              AND end_time IS NOT NULL";

                        using (var cmd = new NpgsqlCommand(onlineTimeQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", driverId);
                            double hours = Convert.ToDouble(cmd.ExecuteScalar());
                            this.Invoke((Action)(() => lblOnlineTime.Text = $"{hours:F1} ч"));
                        }

                        // ---- 4. Рейтинг и количество оценок ----
                        string ratingQuery = @"
                            SELECT 
                                COALESCE(AVG(r.rating), 0),
                                COUNT(r.review_id)
                            FROM review r
                            WHERE r.driver_id = @driver_id";

                        using (var cmd = new NpgsqlCommand(ratingQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", driverId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    double avgRating = reader.GetDouble(0);
                                    int ratingCount = reader.GetInt32(1);

                                    this.Invoke((Action)(() =>
                                    {
                                        lblRatingValue.Text = avgRating.ToString("0.00");
                                        lblRatingCount.Text = $"На основе {ratingCount} оценок";

                                        // Генерация звёзд
                                        int fullStars = (int)Math.Floor(avgRating);
                                        bool halfStar = (avgRating - fullStars) >= 0.5;
                                        string stars = new string('★', fullStars);
                                        if (halfStar) stars += "½";
                                        stars += new string('☆', 5 - stars.Length);
                                        lblStars.Text = stars;
                                    }));
                                }
                            }
                        }

                        // ---- 5. Распределение оценок ----
                        string ratingDistQuery = @"
                            SELECT 
                                COALESCE(SUM(CASE WHEN rating >= 4.5 THEN 1 ELSE 0 END), 0) AS five_star,
                                COALESCE(SUM(CASE WHEN rating >= 3.5 AND rating < 4.5 THEN 1 ELSE 0 END), 0) AS four_star,
                                COALESCE(SUM(CASE WHEN rating >= 2.5 AND rating < 3.5 THEN 1 ELSE 0 END), 0) AS three_star,
                                COALESCE(SUM(CASE WHEN rating >= 1.5 AND rating < 2.5 THEN 1 ELSE 0 END), 0) AS two_star,
                                COALESCE(SUM(CASE WHEN rating < 1.5 THEN 1 ELSE 0 END), 0) AS one_star
                            FROM review
                            WHERE driver_id = @driver_id";

                        using (var cmd = new NpgsqlCommand(ratingDistQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", driverId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int[] counts = new int[5];
                                    counts[0] = reader.GetInt32(0); // 5
                                    counts[1] = reader.GetInt32(1); // 4
                                    counts[2] = reader.GetInt32(2); // 3
                                    counts[3] = reader.GetInt32(3); // 2
                                    counts[4] = reader.GetInt32(4); // 1
                                    int total = counts.Sum();

                                    this.Invoke((Action)(() =>
                                    {
                                        for (int i = 0; i < 5; i++)
                                        {
                                            int percent = total > 0 ? counts[i] * 100 / total : 0;
                                            ratingProgressBars[i].Value = percent;
                                            ratingCountLabels[i].Text = $"{counts[i]} ({percent}%)";
                                        }
                                    }));
                                }
                            }
                        }

                        // ---- 6. Последние отзывы ----
                        string commentsQuery = @"
                            SELECT 
                                r.rating,
                                r.comment,
                                c.first_name,
                                c.last_name
                            FROM review r
                            JOIN client c ON r.clent_id = c.client_id
                            WHERE r.driver_id = @driver_id
                              AND r.comment IS NOT NULL AND r.comment != ''
                            ORDER BY r.review_id DESC
                            LIMIT 10";

                        using (var cmd = new NpgsqlCommand(commentsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", driverId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                var comments = new List<string>();
                                while (reader.Read())
                                {
                                    double rating = reader.GetDouble(0);
                                    string comment = reader.GetString(1);
                                    string firstName = reader.GetString(2);
                                    string lastName = reader.GetString(3);
                                    string stars = new string('★', (int)Math.Round(rating));
                                    stars += new string('☆', 5 - stars.Length);
                                    comments.Add($"{stars} {firstName} {lastName}: {comment}");
                                }
                                this.Invoke((Action)(() =>
                                {
                                    lstComments.Items.Clear();
                                    if (comments.Count > 0)
                                        lstComments.Items.AddRange(comments.ToArray());
                                    else
                                        lstComments.Items.Add("Пока нет отзывов с комментариями.");
                                }));
                            }
                        }

                        // ---- 7. Детальная статистика по дням (последние 7 дней) ----
                        string dailyStatsQuery = @"
                            SELECT 
                                DATE(o.order_datetime) AS order_date,
                                COUNT(o.order_id) AS orders_count,
                                SUM(o.final_cost) AS earnings,
                                COALESCE(SUM(op.promotion_amount), 0) AS tips,
                                AVG(r.rating) AS avg_rating
                            FROM ""Order"" o
                            LEFT JOIN order_promotion op ON o.order_id = op.order_id
                            LEFT JOIN review r ON o.order_id = r.orber_id
                            WHERE o.driver_id = @driver_id
                              AND o.order_status = (SELECT order_status_id FROM order_status WHERE name = 'Завершён' LIMIT 1)
                              AND o.order_datetime >= CURRENT_DATE - INTERVAL '7 days'
                            GROUP BY DATE(o.order_datetime)
                            ORDER BY order_date DESC";

                        using (var cmd = new NpgsqlCommand(dailyStatsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", driverId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                this.Invoke((Action)(() => dgvDailyStats.Rows.Clear()));

                                while (reader.Read())
                                {
                                    DateTime date = reader.GetDateTime(0);
                                    int orders = reader.GetInt32(1);
                                    decimal earnings = reader.GetDecimal(2);
                                    decimal tips = reader.GetDecimal(3);
                                    double? rating = reader.IsDBNull(4) ? (double?)null : reader.GetDouble(4);

                                    this.Invoke((Action)(() =>
                                    {
                                        dgvDailyStats.Rows.Add(
                                            date.ToString("dd.MM.yyyy"),
                                            orders,
                                            earnings,
                                            "", // часы онлайн – пока не считаем
                                            rating.HasValue ? rating.Value.ToString("0.0") : "—",
                                            tips
                                        );
                                    }));
                                }
                            }
                        }

                        // ---- 8. Показатели (на основе среднего рейтинга, можно улучшить) ----
                        using (var cmd = new NpgsqlCommand(ratingQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", driverId);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    double avgRating = reader.GetDouble(0);
                                    this.Invoke((Action)(() =>
                                    {
                                        int percent = (int)(avgRating * 20); // 5 → 100%
                                        if (lblVeal != null) lblVeal.Text = $"Вежливость: {percent}%";
                                        if (lblDriving != null) lblDriving.Text = $"Аккуратность: {percent}%";
                                        if (lblClean != null) lblClean.Text = $"Чистота: {percent}%";
                                        if (lblPunctual != null) lblPunctual.Text = $"Пунктуальность: {percent}%";
                                    }));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((Action)(() =>
                    {
                        MessageBox.Show($"Ошибка загрузки статистики: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            });
        }

        // Обработчик кнопки экспорта
        private void BtnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Экспорт в Excel выполнен!", "Экспорт",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}