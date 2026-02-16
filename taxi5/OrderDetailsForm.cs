using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace taxi4
{
    public partial class OrderDetailsForm : Form
    {
        private WebBrowser webBrowser1;
        private int orderId;
        private string startAddress;
        private string endAddress;
        private string price;
        private string passengers;
        private int driverId;
        private string connectionString;

        // Элементы управления
        private Button btnAction1;      // Принять / Начать поездку
        private Button btnAction2;      // Завершить поездку
        private Button btnDecline;      // ❌ Отказаться (только для свободных заказов)
        private Panel actionPanel;
        private Label lblStatus;

        private string currentStatus;

        public OrderDetailsForm()
        {
            InitializeComponent();
        }

        public OrderDetailsForm(int orderId, string from, string to, string price, string passengers, int driverId, string connectionString)
        {
            this.orderId = orderId;
            this.startAddress = from;
            this.endAddress = to;
            this.price = price;
            this.passengers = passengers;
            this.driverId = driverId;
            this.connectionString = connectionString;

            InitializeComponent();
            InitializeOrderDetailsForm();

            // Загружаем статус и настраиваем интерфейс
            LoadOrderStatus();
        }

        private void LoadOrderStatus()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT os.name, o.driver_id, o.start_trip_time, o.end_trip_time
                    FROM ""Order"" o
                    JOIN order_status os ON o.order_status = os.order_status_id
                    WHERE o.order_id = @order_id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@order_id", orderId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentStatus = reader.GetString(0);
                            int? assignedDriver = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1);
                            bool isStarted = !reader.IsDBNull(2);
                            bool isEnded = !reader.IsDBNull(3);

                            ConfigureUIForStatus(currentStatus, assignedDriver, isStarted, isEnded);
                        }
                    }
                }
            }
        }

        private void ConfigureUIForStatus(string status, int? assignedDriver, bool isStarted, bool isEnded)
        {
            // Скрываем все кнопки, потом покажем нужные
            btnAction1.Visible = false;
            btnAction2.Visible = false;
            btnDecline.Visible = false;

            if (assignedDriver == driverId)
            {
                // Заказ назначен текущему водителю
                if (status == "Завершён")
                {
                    MessageBox.Show("Этот заказ уже завершён.", "Информация");
                    this.Close();
                    return;
                }
                else if (status == "В процессе")
                {
                    if (!isStarted)
                    {
                        // Принят, но не начат – показываем "Начать поездку"
                        btnAction1.Visible = true;
                        btnAction1.Text = "🚀 Начать поездку";
                        btnAction1.BackColor = Color.FromArgb(52, 152, 219);
                        btnAction1.Click -= BtnAccept_Click;
                        btnAction1.Click += BtnStartTrip_Click;
                        lblStatus.Text = "Статус: принят, ожидает начала";
                    }
                    else if (!isEnded)
                    {
                        // Поездка начата – показываем "Завершить поездку"
                        btnAction2.Visible = true;
                        btnAction2.Text = "🏁 Завершить поездку";
                        btnAction2.BackColor = Color.FromArgb(46, 204, 113);
                        btnAction2.Click += BtnEndTrip_Click;
                        lblStatus.Text = "Статус: поездка начата";
                    }
                }
            }
            else if (assignedDriver == null)
            {
                // Заказ свободен – показываем "Принять" и "Отказаться"
                btnAction1.Visible = true;
                btnAction1.Text = "✅ ПРИНЯТЬ ЗАКАЗ";
                btnAction1.BackColor = Color.FromArgb(46, 204, 113);
                btnAction1.Click += BtnAccept_Click;

                btnDecline.Visible = true;
                btnDecline.Text = "❌ ОТКАЗАТЬСЯ";
                btnDecline.BackColor = Color.FromArgb(231, 76, 60);
                btnDecline.Click += BtnDecline_Click;

                lblStatus.Text = "Статус: доступен";
            }
            else
            {
                // Заказ занят другим водителем
                MessageBox.Show("Этот заказ уже принят другим водителем.", "Информация");
                this.Close();
            }
        }

        private void InitializeOrderDetailsForm()
        {
            // Настройка формы – убираем лишние скроллы
            this.Text = $"Детали заказа #{orderId}";
            this.Size = new Size(1000, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(241, 59, 198);
            this.AutoScroll = false;               // отключаем скролл формы
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // запрещаем изменение размера
            this.MaximizeBox = false;

            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(10);
            mainPanel.AutoScroll = false;          // отключаем скролл панели

            // Информационная панель
            Panel infoPanel = new Panel();
            infoPanel.Dock = DockStyle.Top;
            infoPanel.Height = 150;
            infoPanel.BackColor = Color.FromArgb(192, 176, 212);
            infoPanel.BorderStyle = BorderStyle.FixedSingle;

            Label lblTitle = new Label();
            lblTitle.Text = $"Заказ #{orderId}";
            lblTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTitle.Location = new Point(15, 15);
            lblTitle.Size = new Size(300, 25);

            Label lblRoute = new Label();
            lblRoute.Text = $"📍 {startAddress}\n➔\n📍 {endAddress}";
            lblRoute.Font = new Font("Arial", 11);
            lblRoute.Location = new Point(15, 45);
            lblRoute.Size = new Size(500, 60);

            Panel detailsPanel = new Panel();
            detailsPanel.Location = new Point(530, 15);
            detailsPanel.Size = new Size(300, 90);

            Label lblPrice = new Label();
            lblPrice.Text = $"💰 Стоимость: {price} руб";
            lblPrice.Font = new Font("Arial", 11, FontStyle.Bold);
            lblPrice.Location = new Point(0, 0);
            lblPrice.Size = new Size(200, 25);

            Label lblPassengers = new Label();
            lblPassengers.Text = $"👥 Пассажиров: {passengers}";
            lblPassengers.Font = new Font("Arial", 11);
            lblPassengers.Location = new Point(0, 30);
            lblPassengers.Size = new Size(200, 25);

            Label lblTime = new Label();
            lblTime.Text = $"🕐 Создан: {DateTime.Now:HH:mm}";
            lblTime.Font = new Font("Arial", 10);
            lblTime.Location = new Point(0, 60);
            lblTime.Size = new Size(200, 25);

            detailsPanel.Controls.AddRange(new Control[] { lblPrice, lblPassengers, lblTime });

            lblStatus = new Label();
            lblStatus.Text = "Статус: загрузка...";
            lblStatus.Font = new Font("Arial", 10, FontStyle.Bold);
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(15, 115);
            lblStatus.Size = new Size(300, 25);

            infoPanel.Controls.AddRange(new Control[] { lblTitle, lblRoute, detailsPanel, lblStatus });

            // Панель карты
            Panel mapPanel = new Panel();
            mapPanel.Dock = DockStyle.Fill;
            mapPanel.Padding = new Padding(0, 10, 0, 0);
            mapPanel.AutoScroll = false;           // отключаем скролл панели карты

            webBrowser1 = new WebBrowser();
            webBrowser1.Dock = DockStyle.Fill;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.ScrollBarsEnabled = true;  // скролл внутри браузера (один, нормальный)

            // Панель действий (кнопки)
            actionPanel = new Panel();
            actionPanel.Dock = DockStyle.Bottom;
            actionPanel.Height = 70;
            actionPanel.BackColor = Color.FromArgb(245, 245, 245);
            actionPanel.BorderStyle = BorderStyle.FixedSingle;

            // Кнопка 1 (Принять / Начать поездку)
            btnAction1 = new Button();
            btnAction1.Size = new Size(180, 45);
            btnAction1.Location = new Point(20, 12);
            btnAction1.ForeColor = Color.White;
            btnAction1.Font = new Font("Arial", 11, FontStyle.Bold);
            btnAction1.FlatStyle = FlatStyle.Flat;
            btnAction1.FlatAppearance.BorderSize = 0;
            btnAction1.Cursor = Cursors.Hand;

            // Кнопка 2 (Завершить поездку) – изначально скрыта
            btnAction2 = new Button();
            btnAction2.Size = new Size(180, 45);
            btnAction2.Location = new Point(210, 12);
            btnAction2.ForeColor = Color.White;
            btnAction2.Font = new Font("Arial", 11, FontStyle.Bold);
            btnAction2.FlatStyle = FlatStyle.Flat;
            btnAction2.FlatAppearance.BorderSize = 0;
            btnAction2.Cursor = Cursors.Hand;
            btnAction2.Visible = false;

            // Кнопка 3 (Отказаться) – для свободных заказов
            btnDecline = new Button();
            btnDecline.Size = new Size(180, 45);
            btnDecline.Location = new Point(400, 12); // смещаем, чтобы не пересекалось
            btnDecline.ForeColor = Color.White;
            btnDecline.Font = new Font("Arial", 11, FontStyle.Bold);
            btnDecline.FlatStyle = FlatStyle.Flat;
            btnDecline.FlatAppearance.BorderSize = 0;
            btnDecline.Cursor = Cursors.Hand;
            btnDecline.Visible = false;

            // Кнопка "Открыть в Яндекс.Картах"
            Button btnOpenInYandex = new Button();
            btnOpenInYandex.Text = "🗺️ ОТКРЫТЬ В ЯНДЕКС.КАРТАХ";
            btnOpenInYandex.Size = new Size(220, 45);
            btnOpenInYandex.Location = new Point(600, 12);
            btnOpenInYandex.BackColor = Color.FromArgb(52, 152, 219);
            btnOpenInYandex.ForeColor = Color.White;
            btnOpenInYandex.Font = new Font("Arial", 10, FontStyle.Bold);
            btnOpenInYandex.FlatStyle = FlatStyle.Flat;
            btnOpenInYandex.FlatAppearance.BorderSize = 0;
            btnOpenInYandex.Cursor = Cursors.Hand;
            btnOpenInYandex.Click += BtnOpenInYandex_Click;

            actionPanel.Controls.AddRange(new Control[] { btnAction1, btnAction2, btnDecline, btnOpenInYandex });

            mapPanel.Controls.Add(webBrowser1);
            mainPanel.Controls.AddRange(new Control[] { infoPanel, mapPanel, actionPanel });
            this.Controls.Add(mainPanel);

            LoadMapWithRoute();
        }

        private void LoadMapWithRoute()
        {
            try
            {
                string encodedStart = Uri.EscapeDataString(startAddress);
                string encodedEnd = Uri.EscapeDataString(endAddress);
                string yandexMapsUrl = $"https://yandex.ru/maps/?rtext={encodedStart}~{encodedEnd}&rtt=auto&z=13";
                webBrowser1.Navigate(yandexMapsUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки карты: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Принять заказ
        private void BtnAccept_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите принять заказ #{orderId}?",
                "Подтверждение заказа",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        string getStatusIdQuery = "SELECT order_status_id FROM order_status WHERE name = 'В процессе' LIMIT 1";
                        int inProgressStatusId = Convert.ToInt32(new NpgsqlCommand(getStatusIdQuery, conn).ExecuteScalar());

                        string updateQuery = @"
                            UPDATE ""Order"" 
                            SET driver_id = @driver_id, 
                                order_status = @status_id
                            WHERE order_id = @order_id";
                        using (var cmd = new NpgsqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", driverId);
                            cmd.Parameters.AddWithValue("@status_id", inProgressStatusId);
                            cmd.Parameters.AddWithValue("@order_id", orderId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"Заказ #{orderId} принят!", "Заказ принят",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadOrderStatus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при принятии заказа: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Отказаться от заказа (только для свободных)
        private void BtnDecline_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Отказаться от заказа? Он останется доступным для других водителей.",
                "Отказ от заказа",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Ничего не меняем в БД, просто закрываем форму
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // Начать поездку
        private void BtnStartTrip_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = @"
                        UPDATE ""Order""
                        SET start_trip_time = @start_time
                        WHERE order_id = @order_id";
                    using (var cmd = new NpgsqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@start_time", DateTime.Now);
                        cmd.Parameters.AddWithValue("@order_id", orderId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Поездка начата! Счастливого пути!", "Старт",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadOrderStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при начале поездки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Завершить поездку
        private void BtnEndTrip_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Завершить поездку?",
                "Завершение заказа",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        string getStatusIdQuery = "SELECT order_status_id FROM order_status WHERE name = 'Завершён' LIMIT 1";
                        int completedStatusId = Convert.ToInt32(new NpgsqlCommand(getStatusIdQuery, conn).ExecuteScalar());

                        string updateQuery = @"
                            UPDATE ""Order""
                            SET end_trip_time = @end_time,
                                order_status = @status_id
                            WHERE order_id = @order_id";
                        using (var cmd = new NpgsqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@end_time", DateTime.Now);
                            cmd.Parameters.AddWithValue("@status_id", completedStatusId);
                            cmd.Parameters.AddWithValue("@order_id", orderId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"Заказ #{orderId} завершён! Спасибо за работу!",
                        "Заказ завершён", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при завершении заказа: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnOpenInYandex_Click(object sender, EventArgs e)
        {
            try
            {
                string encodedStart = Uri.EscapeDataString(startAddress);
                string encodedEnd = Uri.EscapeDataString(endAddress);
                string url = $"https://yandex.ru/maps/?rtext={encodedStart}~{encodedEnd}&rtt=auto";
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть Яндекс.Карты: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}