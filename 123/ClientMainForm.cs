using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Npgsql;

namespace TaxiClientApp
{
    public partial class ClientMainForm : Form
    {
        private int currentClientId;
        private string connectionString = "Server=localhost;Port=5432;Database=taxi5;User Id=postgres;Password=135246";
        private Label lblWelcome;
        private Panel panelActions;

        public ClientMainForm(int clientId)
        {
            currentClientId = clientId;
            InitializeComponent();
            LoadClientData();
        }

        private void InitializeComponent()
        {
            this.Text = "Такси — Главная";
            this.Size = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Заголовок
            var lblTitle = new Label
            {
                Text = "🚖 Такси Сервис",
                Location = new Point(30, 20),
                Size = new Size(400, 50),
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            // Приветствие
            lblWelcome = new Label
            {
                Location = new Point(30, 80),
                Size = new Size(500, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // Панель действий (плитки)
            panelActions = new RoundedPanel
            {
                Location = new Point(30, 130),
                Size = new Size(770, 300),
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Кнопка "Новый заказ"
            var btnNewOrder = CreateTileButton("🚕 НОВЫЙ ЗАКАЗ", Color.FromArgb(46, 204, 113));
            btnNewOrder.Location = new Point(20, 20);
            btnNewOrder.Click += BtnNewOrder_Click;

            // Кнопка "Мои заказы"
            var btnMyOrders = CreateTileButton("📋 МОИ ЗАКАЗЫ", Color.FromArgb(52, 152, 219));
            btnMyOrders.Location = new Point(260, 20);
            btnMyOrders.Click += BtnMyOrders_Click;

            // Кнопка "Профиль"
            var btnProfile = CreateTileButton("👤 ПРОФИЛЬ", Color.FromArgb(155, 89, 182));
            btnProfile.Location = new Point(500, 20);
            btnProfile.Click += BtnProfile_Click;

            // Новая кнопка "Мои акции"
            var btnPromotions = CreateTileButton("🎁 МОИ АКЦИИ", Color.FromArgb(241, 196, 15));
            btnPromotions.Location = new Point(20, 120);
            btnPromotions.Click += BtnPromotions_Click;

            // Кнопка "Выйти"
            var btnLogout = CreateTileButton("🚪 ВЫЙТИ", Color.FromArgb(231, 76, 60));
            btnLogout.Location = new Point(260, 120);
            btnLogout.Click += BtnLogout_Click;

            panelActions.Controls.AddRange(new Control[] { btnNewOrder, btnMyOrders, btnProfile, btnPromotions, btnLogout });

            // Блок статистики
            var lblStatsHeader = new Label
            {
                Text = "📊 Ваша статистика",
                Location = new Point(20, 210),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            var lblStatsContent = new Label
            {
                Text = "Загрузка...",
                Location = new Point(40, 240),
                Size = new Size(400, 60),
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(127, 140, 141)
            };
            lblStatsContent.Name = "lblStatsContent";

            panelActions.Controls.Add(lblStatsHeader);
            panelActions.Controls.Add(lblStatsContent);

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblWelcome);
            this.Controls.Add(panelActions);
        }

        private Button CreateTileButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                Size = new Size(220, 80),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
        }
        // Обработчик для новой кнопки
        private void BtnPromotions_Click(object sender, EventArgs e)
        {
            using (var form = new PromotionsForm(currentClientId, connectionString))
                form.ShowDialog();
        }
        private async void LoadClientData()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    // Имя клиента
                    string nameQuery = "SELECT first_name, last_name FROM client WHERE client_id = @id";
                    using (var cmd = new NpgsqlCommand(nameQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentClientId);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string firstName = reader["first_name"].ToString();
                                string lastName = reader["last_name"].ToString();
                                lblWelcome.Text = $"👋 Добро пожаловать, {firstName} {lastName}!";
                            }
                        }
                    }

                    // Статистика
                    string statsQuery = @"
                        SELECT 
                            COUNT(*) AS total_rides,
                            COALESCE(SUM(final_cost), 0) AS total_spent,
                            COALESCE(AVG(r.rating), 0) AS avg_rating
                        FROM ""Order"" o
                        LEFT JOIN review r ON o.order_id = r.order_id
                        WHERE o.client_id = @id";
                    using (var cmd = new NpgsqlCommand(statsQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentClientId);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int rides = Convert.ToInt32(reader["total_rides"]);
                                decimal spent = Convert.ToDecimal(reader["total_spent"]);
                                double rating = Convert.ToDouble(reader["avg_rating"]);

                                var lblStats = panelActions.Controls["lblStatsContent"] as Label;
                                if (lblStats != null)
                                {
                                    lblStats.Text = $"✅ Поездок: {rides}\n" +
                                                    $"💰 Потрачено: {spent:F0} ₽\n" +
                                                    $"⭐ Рейтинг: {rating:F1} / 5.0";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNewOrder_Click(object sender, EventArgs e)
        {
            using (var form = new NewOrderForm(currentClientId, connectionString))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadClientData(); // обновим статистику
            }
        }

        private void BtnMyOrders_Click(object sender, EventArgs e)
        {
            using (var form = new MyOrdersForm(currentClientId, connectionString))
                form.ShowDialog();
        }

        private void BtnProfile_Click(object sender, EventArgs e)
        {
            using (var form = new ProfileForm(currentClientId, connectionString))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadClientData(); // обновим имя, если изменили
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Выход",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Abort;
                Close();
            }
        }
    }

    // Кастомная панель с закруглёнными углами
    public class RoundedPanel : Panel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var path = new GraphicsPath())
            {
                int radius = 15;
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(Width - radius, Height - radius, radius, radius, 0, 90);
                path.AddArc(0, Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                this.Region = new Region(path);
            }
            base.OnPaint(e);
        }
    }
}