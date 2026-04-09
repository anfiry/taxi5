using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace taxi4
{
    public partial class DriverMenu : Form
    {
        // Статические переменные для хранения статистики текущей смены
        private static decimal shiftTotalEarnings = 0;
        private static int shiftTotalOrders = 0;

        public string Role { get; set; }
        public int AccountId { get; private set; }
        public string UserLogin { get; set; }

        private string connectionString = "Server=localhost;Port=5432;Database=taxi4;User Id=postgres;Password=123";
        private int currentDriverId = 0;
        private string driverName = "";
        private int? currentWorkScheduleId = null;
        private DateTime shiftStartTime;
        private bool back = false;

        // Статический метод для добавления суммы заказа
        public static void AddToShiftTotal(decimal amount)
        {
            shiftTotalEarnings += amount;
            shiftTotalOrders++;
        }

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        // Статический метод для сброса статистики
        private static void ResetShiftStatistics()
        {
            shiftTotalEarnings = 0;
            shiftTotalOrders = 0;
        }

        public DriverMenu(int accountId)
        {
            InitializeComponent();
            AccountId = accountId;

            // Настройка полноэкранного режима
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(800, 600);

            LoadDriverData();
            this.Load += DriverMenu_Load;
        }

        public DriverMenu()
        {
            InitializeComponent();

            // Настройка полноэкранного режима для дизайнера
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        

        private void LoadDriverData()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT driver_id, first_name, last_name FROM driver WHERE account_id = @accountId";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@accountId", AccountId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentDriverId = reader.GetInt32(0);
                                string firstName = reader.GetString(1);
                                string lastName = reader.GetString(2);
                                driverName = $"{firstName} {lastName}".Trim();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных водителя: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DriverMenu_Load(object sender, EventArgs e)
        {
            if (currentDriverId == 0)
            {
                MessageBox.Show("Не удалось определить водителя для данного аккаунта.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            CheckActiveShift();
            UpdateUI();
            labelWelcome.Text = $"Добро пожаловать, {driverName}!";
        }

        // ---------- Проверка активной смены ----------
        private bool IsShiftActive()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT COUNT(*) FROM work_schedule
                    WHERE driver_id = @driver_id
                      AND end_datetime IS NULL;";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@driver_id", currentDriverId);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void CheckActiveShift()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT work_schedule, start_datetime
                        FROM work_schedule
                        WHERE driver_id = @driver_id
                          AND end_datetime IS NULL
                        ORDER BY work_schedule DESC
                        LIMIT 1;";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@driver_id", currentDriverId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentWorkScheduleId = reader.GetInt32(0);
                                shiftStartTime = reader.GetDateTime(1);

                                // Если есть активная смена, загружаем статистику из БД
                                LoadShiftStatistics();
                            }
                            else
                            {
                                currentWorkScheduleId = null;
                                ResetShiftStatistics();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке активной смены: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadShiftStatistics()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT COUNT(*) as orders_count, COALESCE(SUM(final_cost), 0) as total_earnings
                        FROM ""Order""
                        WHERE driver_id = @driverId 
                        AND order_status = 3
                        AND end_trip_time >= @shiftStartTime";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@driverId", currentDriverId);
                        cmd.Parameters.AddWithValue("@shiftStartTime", shiftStartTime);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                shiftTotalOrders = reader.GetInt32(0);
                                shiftTotalEarnings = reader.GetDecimal(1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки статистики: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUI()
        {
            bool isActive = IsShiftActive();

            if (isActive && currentWorkScheduleId != null)
            {
                Startingshifts.Enabled = false;
                Startingshifts.Text = "Смена активна";
                Startingshifts.BackColor = Color.LightGreen;
                Startingshifts.ForeColor = Color.DarkGreen;

                EndShif.Enabled = true;
                EndShif.BackColor = Color.LightCoral;
                EndShif.ForeColor = Color.DarkRed;

                Orders.Enabled = true;
                Shifts.Enabled = true;
                Statistics.Enabled = true;
            }
            else
            {
                Startingshifts.Enabled = true;
                Startingshifts.Text = "Начать смену";
                Startingshifts.BackColor = Color.FromArgb(251, 228, 255);
                Startingshifts.ForeColor = Color.FromArgb(4, 0, 66);

                EndShif.Enabled = false;
                EndShif.BackColor = SystemColors.Control;
                EndShif.ForeColor = SystemColors.ControlText;

                Orders.Enabled = false;
                Shifts.Enabled = true;
                Statistics.Enabled = true;
            }
        }

        // ========== НАЧАЛО СМЕНЫ ==========
        private void Startingshifts_Click(object sender, EventArgs e)
        {
            if (IsShiftActive())
            {
                MessageBox.Show("У вас уже есть активная смена! Завершите её, чтобы начать новую.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Начать смену?", "Начало смены",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string insertQuery = @"
                        INSERT INTO work_schedule 
                            (driver_id, shift_status_id, start_datetime, end_datetime)
                        VALUES 
                            (@driver_id, 1, @start_datetime, NULL)
                        RETURNING work_schedule;";

                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", currentDriverId);
                            cmd.Parameters.AddWithValue("@start_datetime", DateTime.Now);

                            currentWorkScheduleId = (int)cmd.ExecuteScalar();
                            shiftStartTime = DateTime.Now;
                        }
                    }

                    // Сбрасываем статистику при начале новой смены
                    ResetShiftStatistics();

                    UpdateUI();
                    MessageBox.Show($"Смена начата в {shiftStartTime:HH:mm}",
                        "Смена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при начале смены: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentWorkScheduleId = null;
                }
            }
        }

        // ========== ЗАВЕРШЕНИЕ СМЕНЫ ==========
        private void EndShif_Click(object sender, EventArgs e)
        {
            if (!IsShiftActive() || currentWorkScheduleId == null)
            {
                MessageBox.Show("Нет активной смены!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Завершить смену?", "Завершение смены",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string updateQuery = @"
                        UPDATE work_schedule 
                        SET end_datetime = @end_datetime,
                            shift_status_id = 2
                        WHERE work_schedule = @id;";

                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@end_datetime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@id", currentWorkScheduleId.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    TimeSpan duration = DateTime.Now - shiftStartTime;
                    currentWorkScheduleId = null;
                    UpdateUI();

                    // Показываем итоги смены с накопленной статистикой
                    MessageBox.Show($"Смена завершена!\n\n" +
                                    $"Время работы: {duration:hh\\:mm}\n" +
                                    $"Выполнено заказов: {shiftTotalOrders}\n" +
                                    $"Заработано: {shiftTotalEarnings:N0} ₽\n" +
                                    $"Средний чек: {(shiftTotalOrders > 0 ? (shiftTotalEarnings / shiftTotalOrders).ToString("N0") : "0")} ₽",
                        "Итоги смены", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Сбрасываем статистику
                    ResetShiftStatistics();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при завершении смены: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ---------- Кнопки ----------
        private void button4_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            back = true;
            loginForm.Show();
            this.Close();
        }

        private void Orders_Click(object sender, EventArgs e)
        {
            if (!IsShiftActive())
            {
                MessageBox.Show("Сначала начните смену!", "Доступ запрещён",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentDriverId == 0)
            {
                MessageBox.Show("Не удалось определить ID водителя", "Ошибка");
                return;
            }

            DriverOrdersForm ordersForm = new DriverOrdersForm(this.currentDriverId, this.connectionString, this.AccountId);
            ordersForm.Closed += (s, args) => ordersForm.OnClosed();
            ordersForm.Show();
            this.Hide();
        }

        private void Statistics_Click(object sender, EventArgs e)
        {
            if (currentDriverId == 0)
            {
                MessageBox.Show("Не удалось определить ID водителя", "Ошибка");
                return;
            }

            DriverStatisticsRatingForm statsForm = new DriverStatisticsRatingForm(this.currentDriverId, this.connectionString, this.AccountId);
            statsForm.Closed += (s, args) => statsForm.OnClosed();
            statsForm.Show();
            this.Hide();
        }

        private void Shifts_Click(object sender, EventArgs e)
        {
            if (currentDriverId == 0)
            {
                MessageBox.Show("Не удалось определить ID водителя", "Ошибка");
                return;
            }

            DriverShiftsForm shiftsForm = new DriverShiftsForm(this.currentDriverId, this.connectionString, this.AccountId);
            shiftsForm.Closed += (s, args) => shiftsForm.OnClosed();
            shiftsForm.Show();
            this.Close();
        }

        // Пустые обработчики для дизайнера
        private void button1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}