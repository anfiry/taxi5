using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Npgsql;

namespace taxi4
{
    public partial class DriverStatisticsRatingForm : Form
    {
        private int driverId;
        private string connectionString;
        private int totalOrdersForPeriod = 0;
        private bool isLoading = false;

        public DriverStatisticsRatingForm(int driverId, string connectionString)
        {
            this.driverId = driverId;
            this.connectionString = connectionString;
            InitializeComponent();

            // Настройка колонок DataGridView (переносим сюда из InitializeComponent)
            ConfigureDataGridViewColumns();

            // Настройка отрисовки ListBox
            this.lstComments.DrawItem += LstComments_DrawItem;

            // Создание и настройка кнопки "Назад"
            InitializeBackButton();

            cmbPeriod.SelectedIndexChanged += CmbPeriod_SelectedIndexChanged;
            cmbPeriod.SelectedIndex = 4;

            this.Load += DriverStatisticsRatingForm_Load;
            this.Resize += DriverStatisticsRatingForm_Resize;
        }

        private void ConfigureDataGridViewColumns()
        {
            // Настройка выравнивания для колонки Earnings
            if (dgvDailyStats.Columns["Earnings"] != null)
            {
                dgvDailyStats.Columns["Earnings"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Настройка выравнивания для колонки Rating
            if (dgvDailyStats.Columns["Rating"] != null)
            {
                dgvDailyStats.Columns["Rating"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Настройка выравнивания для колонки Passenger
            if (dgvDailyStats.Columns["Passenger"] != null)
            {
                dgvDailyStats.Columns["Passenger"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void InitializeBackButton()
        {
            this.btnBack = new Button();
            this.btnBack.Text = "◀ Назад";
            this.btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBack.Size = new Size(100, 35);
            this.btnBack.BackColor = Color.FromArgb(52, 73, 94);
            this.btnBack.ForeColor = Color.White;
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.Cursor = Cursors.Hand;
            this.btnBack.Click += BtnBack_Click;

            this.Controls.Add(this.btnBack);
            UpdateBackButtonPosition();
        }

        private void UpdateBackButtonPosition()
        {
            if (btnBack != null)
            {
                btnBack.Location = new Point(this.ClientSize.Width - 120, 12);
                btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
        }

        private void DriverStatisticsRatingForm_Load(object sender, EventArgs e)
        {
            LoadAllData();
            AdjustLayout();
            UpdateBackButtonPosition();
        }

        private void DriverStatisticsRatingForm_Resize(object sender, EventArgs e)
        {
            AdjustLayout();
            UpdateBackButtonPosition();
        }

        private void AdjustLayout()
        {
            int width = this.ClientSize.Width - 40;
            if (width <= 0) return;

            if (indicatorsPanel != null)
            {
                indicatorsPanel.Width = width;
                int cardWidth = (width - 40) / 3;
                if (cardWidth > 0)
                {
                    if (cardEarnings != null) cardEarnings.Width = cardWidth;
                    if (cardOrders != null) cardOrders.Width = cardWidth;
                    if (cardAvgBill != null) cardAvgBill.Width = cardWidth;
                    if (cardOrders != null) cardOrders.Left = cardWidth + 10;
                    if (cardAvgBill != null) cardAvgBill.Left = (cardWidth + 10) * 2;
                }
            }

            if (detailGroup != null && dgvDailyStats != null)
            {
                detailGroup.Width = width;
                dgvDailyStats.Width = width - 20;
            }
        }

        private void LstComments_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            string text = ((ListBox)sender).Items[e.Index].ToString();

            using (var font = new System.Drawing.Font("Segoe UI", 9F))
            {
                e.Graphics.DrawString(text, font, System.Drawing.Brushes.Black, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void CmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoading && this.IsHandleCreated)
            {
                LoadAllData();
            }
        }

        private string GetPeriodFilter(string dateField = "o.start_trip_time")
        {
            switch (cmbPeriod.SelectedIndex)
            {
                case 0: return $"AND DATE({dateField}) = CURRENT_DATE";
                case 1: return $"AND DATE({dateField}) = CURRENT_DATE - INTERVAL '1 day'";
                case 2: return $"AND DATE({dateField}) >= DATE_TRUNC('week', CURRENT_DATE)";
                case 3: return $"AND DATE({dateField}) >= DATE_TRUNC('month', CURRENT_DATE)";
                default: return "";
            }
        }

        private void LoadAllData()
        {
            if (isLoading) return;
            isLoading = true;

            try
            {
                LoadStatistics();
                LoadRatingData();
            }
            finally
            {
                isLoading = false;
            }
        }

        private void SafeInvoke(Action action)
        {
            if (this.IsDisposed || this.Disposing) return;

            if (this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                    this.Invoke(action);
                else
                    action();
            }
            else
            {
                EventHandler handler = null;
                handler = (s, e) =>
                {
                    this.HandleCreated -= handler;
                    if (!this.IsDisposed && this.IsHandleCreated)
                    {
                        this.Invoke(action);
                    }
                };
                this.HandleCreated += handler;
            }
        }

        private void LoadStatistics()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string periodFilter = GetPeriodFilter();

                    string earningsQuery = $@"
                        SELECT 
                            COALESCE(SUM(o.final_cost), 0) AS total_earnings,
                            COUNT(o.order_id) AS total_orders,
                            COALESCE(AVG(o.final_cost), 0) AS avg_bill
                        FROM ""Order"" o
                        WHERE o.driver_id = @driver_id
                          AND o.order_status = 3
                          {periodFilter}";

                    using (var cmd = new NpgsqlCommand(earningsQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@driver_id", driverId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal totalEarnings = reader.GetDecimal(0);
                                totalOrdersForPeriod = reader.GetInt32(1);
                                decimal avgBill = reader.GetDecimal(2);
                                SafeInvoke(() =>
                                {
                                    lblTotalEarnings.Text = $"{totalEarnings:N0} ₽";
                                    lblOrdersCount.Text = totalOrdersForPeriod.ToString();
                                    lblAvgBill.Text = $"{avgBill:N0} ₽";
                                });
                            }
                        }
                    }

                    string dailyStatsQuery = $@"
                        SELECT 
                            o.start_trip_time AS start_datetime,
                            o.end_trip_time AS end_datetime,
                            o.final_cost AS earnings,
                            COALESCE(r.rating, 0) AS rating,
                            COALESCE(c.first_name || ' ' || c.last_name, 'Неизвестный') AS passenger_name
                        FROM ""Order"" o
                        LEFT JOIN review r ON o.order_id = r.orber_id AND r.driver_id = o.driver_id
                        LEFT JOIN client c ON o.client_id = c.client_id
                        WHERE o.driver_id = @driver_id
                          AND o.order_status = 3
                          AND o.start_trip_time IS NOT NULL
                          {periodFilter}
                        ORDER BY o.start_trip_time DESC
                        LIMIT 100";

                    using (var cmd = new NpgsqlCommand(dailyStatsQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@driver_id", driverId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            SafeInvoke(() => dgvDailyStats.Rows.Clear());

                            while (reader.Read())
                            {
                                DateTime startDateTime = reader.GetDateTime(0);
                                DateTime? endDateTime = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1);
                                decimal earnings = reader.GetDecimal(2);
                                double rating = reader.GetDouble(3);
                                string passengerName = reader.GetString(4);

                                SafeInvoke(() =>
                                {
                                    dgvDailyStats.Rows.Add(
                                        startDateTime.ToString("dd.MM.yyyy HH:mm"),
                                        endDateTime.HasValue ? endDateTime.Value.ToString("dd.MM.yyyy HH:mm") : "—",
                                        passengerName,
                                        $"{earnings:N0} ₽",
                                        rating > 0 ? rating.ToString("0.0") : "—"
                                    );
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SafeInvoke(() =>
                {
                    MessageBox.Show($"Ошибка загрузки статистики: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }

        private void LoadRatingData()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

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

                                SafeInvoke(() =>
                                {
                                    lblRatingValue.Text = avgRating.ToString("0.00");
                                    lblRatingCount.Text = $"На основе {ratingCount} оценок";

                                    int fullStars = (int)Math.Floor(avgRating);
                                    bool halfStar = (avgRating - fullStars) >= 0.5;
                                    string stars = new string('★', fullStars);
                                    if (halfStar) stars += "½";
                                    stars += new string('☆', 5 - stars.Length);
                                    lblStars.Text = stars;
                                });
                            }
                        }
                    }

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
                                counts[0] = reader.GetInt32(0);
                                counts[1] = reader.GetInt32(1);
                                counts[2] = reader.GetInt32(2);
                                counts[3] = reader.GetInt32(3);
                                counts[4] = reader.GetInt32(4);
                                int total = counts.Sum();

                                SafeInvoke(() =>
                                {
                                    for (int i = 0; i < 5; i++)
                                    {
                                        int percent = total > 0 ? counts[i] * 100 / total : 0;
                                        if (i < ratingProgressBars.Length && ratingProgressBars[i] != null)
                                            ratingProgressBars[i].Value = Math.Min(percent, 100);
                                        if (i < ratingCountLabels.Length && ratingCountLabels[i] != null)
                                            ratingCountLabels[i].Text = $"{counts[i]} ({percent}%)";
                                    }
                                });
                            }
                        }
                    }

                    string commentsQuery = @"
                        SELECT 
                            r.rating,
                            r.comment,
                            c.first_name,
                            c.last_name,
                            o.start_trip_time AS trip_date
                        FROM review r
                        JOIN client c ON r.clent_id = c.client_id
                        JOIN ""Order"" o ON r.orber_id = o.order_id
                        WHERE r.driver_id = @driver_id
                          AND r.comment IS NOT NULL AND r.comment != ''
                        ORDER BY o.start_trip_time DESC
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
                                DateTime tripDate = reader.GetDateTime(4);

                                string stars = new string('★', (int)Math.Round(rating));
                                stars += new string('☆', 5 - stars.Length);

                                string dateStr = tripDate.ToString("dd.MM.yyyy");
                                comments.Add($"[{dateStr}] {stars} {firstName} {lastName}: {comment}");
                            }
                            SafeInvoke(() =>
                            {
                                lstComments.Items.Clear();
                                if (comments.Count > 0)
                                    lstComments.Items.AddRange(comments.ToArray());
                                else
                                    lstComments.Items.Add("Пока нет отзывов с комментариями.");
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SafeInvoke(() =>
                {
                    MessageBox.Show($"Ошибка загрузки рейтинга: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}